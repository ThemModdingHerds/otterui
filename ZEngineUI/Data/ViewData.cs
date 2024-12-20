using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Animations;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class ViewData(ControlData controlData,uint mAnimationListOffset,AnimationListData mAnimationListData,uint mTexturesOffset,IEnumerable<uint> mTextureIDs,uint mSoundsOffset,IEnumerable<uint> mSoundIDs,uint mControlsOffset,IEnumerable<ControlData> mControls) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GGVW";
    [JsonPropertyName("mTexturesOffset")]
    public uint TexturesOffset {get;set;} = mTexturesOffset;
    [JsonPropertyName("mSoundsOffset")]
    public uint SoundsOffset {get;set;} = mSoundsOffset;
    [JsonPropertyName("mControlsOffset")]
    public uint ControlsOffset {get;set;} = mControlsOffset;
    [JsonPropertyName("mAnimationListOffset")]
    public uint AnimationListOffset {get;set;} = mAnimationListOffset;
    [JsonPropertyName("mAnimationListData")]
    public AnimationListData AnimationListData {get;set;} = mAnimationListData;
    [JsonPropertyName("mTextureIDs")]
    public List<uint> TextureIDs {get;set;} = [..mTextureIDs];
    [JsonPropertyName("mSoundIDs")]
    public List<uint> SoundIDs {get;set;} = [..mSoundIDs];
    [JsonPropertyName("mControls")]
    public List<ControlData> Controls {get;set;} = [..mControls];
    public new const int BYTES = ControlData.BYTES + 28;
    public ViewData(ControlData controlData,uint mAnimationListOffset,AnimationListData mAnimationListData,uint mTexturesOffset,uint mSoundsOffset,uint mControlsOffset): this(controlData,mAnimationListOffset,mAnimationListData,mTexturesOffset,[],mSoundsOffset,[],mControlsOffset,[])
    {

    }
    public ViewData(): this(new(),0,new(),0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + AnimationListData.GetDataSize() + GetControlsSize() + GetTextureIDsSize() + GetSoundIDsSize();
    }
    private uint GetTextureIDsSize()
    {
        return (uint)TextureIDs.Count * 4;
    }
    private uint GetSoundIDsSize()
    {
        return (uint)SoundIDs.Count * 4;
    }
    private uint GetControlsSize()
    {
        return (from control in Controls select control.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
    public void RecalculateOffsets()
    {
        TexturesOffset = 0;
        SoundsOffset = TexturesOffset + GetTextureIDsSize();
        ControlsOffset = SoundsOffset + GetSoundIDsSize();
        AnimationListOffset = ControlsOffset + GetControlsSize();
    }
}
public static class ViewDataExt
{
    public static ViewData ReadZEngineUIViewData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        uint mNumTextures = reader.ReadUInt();
        uint mNumSounds = reader.ReadUInt();
        uint mNumControls = reader.ReadUInt();
        uint mTexturesOffset = reader.ReadUInt();
        uint mSoundsOffset = reader.ReadUInt();
        uint mControlsOffset = reader.ReadUInt();
        uint mAnimationListOffset = reader.ReadUInt();
        List<uint> mTextureIDs = [];
        List<uint> mSoundIDs = [];
        List<ControlData> mControls = [];

        long _offset = reader.Offset;
        if(mNumTextures != 0)
        {
            reader.Offset = _offset + mTexturesOffset;
            for(uint i = 0;i < mNumTextures;i++)
                mTextureIDs.Add(reader.ReadUInt());
        }
        if(mNumSounds != 0)
        {
            reader.Offset = _offset + mSoundsOffset;
            for(uint i = 0;i < mNumSounds;i++)
                mSoundIDs.Add(reader.ReadUInt());
        }
        if(mNumControls != 0)
        {
            reader.Offset = _offset + mControlsOffset;
            for(uint i = 0;i < mNumControls;i++)
                mControls.Add(ControlData.Read(reader));
        }
        reader.Offset = _offset + mAnimationListOffset;
        AnimationListData animationListData = reader.ReadZEngineUIAnimationListData();
        return new(controlData,mAnimationListOffset,animationListData,mTexturesOffset,mTextureIDs,mSoundsOffset,mSoundIDs,mControlsOffset,mControls);
    }
    public static void Write(this Writer writer,ViewData viewData)
    {
        viewData.RecalculateOffsets();
        ControlDataExt.Write(writer,viewData);
        writer.Write((uint)viewData.TextureIDs.Count);
        writer.Write((uint)viewData.SoundIDs.Count);
        writer.Write((uint)viewData.Controls.Count);
        writer.Write(viewData.TexturesOffset);
        writer.Write(viewData.SoundsOffset);
        writer.Write(viewData.ControlsOffset);
        writer.Write(viewData.AnimationListOffset);
        long _offset = writer.Offset;
        
        if(viewData.TextureIDs.Count != 0)
        {
            writer.Offset = _offset + viewData.TexturesOffset;
            foreach(uint id in viewData.TextureIDs)
                writer.Write(id);
        }
        if(viewData.SoundIDs.Count != 0)
        {
            writer.Offset = _offset + viewData.SoundsOffset;
            foreach(uint id in viewData.SoundIDs)
                writer.Write(id);
        }
        if(viewData.Controls.Count != 0)
        {
            writer.Offset = _offset + viewData.ControlsOffset;
            foreach(ControlData controlData in viewData.Controls)
                ControlData.Write(writer,controlData);
        }
        
        writer.Offset = _offset + viewData.AnimationListOffset;
        writer.Write(viewData.AnimationListData);
    }
}