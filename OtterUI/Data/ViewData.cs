using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Animations;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Data;
public class ViewData(uint mDataSize,ControlData controlData,uint mAnimationListOffset,AnimationListData mAnimationListData,uint mTexturesOffset,IEnumerable<uint> mTextureIDs,uint mSoundsOffset,IEnumerable<uint> mSoundIDs,uint mControlsOffset,IEnumerable<ControlData> mControls) : ControlData(FOURCC,mDataSize,controlData)
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
    public ViewData(uint mDataSize,ControlData controlData,uint mAnimationListOffset,AnimationListData mAnimationListData,uint mTexturesOffset,uint mSoundsOffset,uint mControlsOffset): this(mDataSize,controlData,mAnimationListOffset,mAnimationListData,mTexturesOffset,[],mSoundsOffset,[],mControlsOffset,[])
    {

    }
    public ViewData(): this(0,new(),0,new(),0,0,0)
    {

    }
}
public static class ViewDataExt
{
    public static ViewData ReadOtterUIViewData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        uint mNumTextures = reader.ReadUInt();
        uint mNumSounds = reader.ReadUInt();
        uint mNumControls = reader.ReadUInt();
        uint mTexturesOffset = reader.ReadUInt();
        uint mSoundsOffset = reader.ReadUInt();
        uint mControlsOffset = reader.ReadUInt();
        uint mAnimationListOffset = reader.ReadUInt();
        long _offset = reader.Offset;
        reader.Offset = _offset + mAnimationListOffset;
        AnimationListData animationListData = reader.ReadOtterUIAnimationListData();
        reader.Offset = _offset + mTexturesOffset;
        List<uint> mTextureIDs = [];
        for(uint i = 0;i < mNumTextures;i++)
            mTextureIDs.Add(reader.ReadUInt());
        reader.Offset = _offset + mSoundsOffset;
        List<uint> mSoundIDs = [];
        for(uint i = 0;i < mNumSounds;i++)
            mSoundIDs.Add(reader.ReadUInt());
        reader.Offset = _offset + mControlsOffset;
        List<ControlData> mControls = [];
        for(uint i = 0;i < mNumControls;i++)
            mControls.Add(ControlData.Read(reader));
        return new(controlData.DataSize,controlData,mAnimationListOffset,animationListData,mTexturesOffset,mTextureIDs,mSoundsOffset,mSoundIDs,mControlsOffset,mControls);
    }
    public static void Write(this Writer writer,ViewData viewData)
    {
        ControlDataExt.Write(writer,viewData);
        writer.Write((uint)viewData.TextureIDs.Count);
        writer.Write((uint)viewData.SoundIDs.Count);
        writer.Write((uint)viewData.Controls.Count);
        writer.Write(viewData.TexturesOffset);
        writer.Write(viewData.SoundsOffset);
        writer.Write(viewData.ControlsOffset);
        writer.Write(viewData.AnimationListOffset);
        long _offset = writer.Offset;
        writer.Offset = _offset + viewData.AnimationListOffset;
        writer.Write(viewData.AnimationListData);
        writer.Offset = _offset + viewData.TexturesOffset;
        foreach(uint id in viewData.TextureIDs)
            writer.Write(id);
        writer.Offset = _offset + viewData.SoundsOffset;
        foreach(uint id in viewData.SoundIDs)
            writer.Write(id);
        writer.Offset = _offset + viewData.ControlsOffset;
        foreach(ControlData controlData in viewData.Controls)
            ControlData.Write(writer,controlData);
    }
}