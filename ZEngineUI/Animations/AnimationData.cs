using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Animations;
public class AnimationData(uint mDataSize,string mName,uint mNumFrames,int mRepeatStart,int mRepeatEnd,uint mMainChannelFramesOffset,IEnumerable<AnimationChannelData> mAnimationChannels,IEnumerable<MainChannelFrameData> mMainChannelFrames) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "GGAN";
    public const int NAME_SIZE = 64;
    [JsonPropertyName("mName")]
    public string Name {get;set;} = mName.MaybeSubstring(NAME_SIZE);
    [JsonPropertyName("mNumFrames")]
    public uint Frames {get;set;} = mNumFrames;
    [JsonPropertyName("mRepeatStart")]
    public int RepeatStart {get;set;} = mRepeatStart;
    [JsonPropertyName("mRepeatEnd")]
    public int RepeatEnd {get;set;} = mRepeatEnd;
    [JsonPropertyName("mMainChannelFramesOffset")]
    public uint MainChannelFramesOffset {get;set;} = mMainChannelFramesOffset;
    [JsonPropertyName("mAnimationChannels")]
    public List<AnimationChannelData> AnimationChannels {get;set;} = [..mAnimationChannels];
    [JsonPropertyName("mMainChannelFrames")]
    public List<MainChannelFrameData> MainChannelFrames {get;set;} = [..mMainChannelFrames];
    public new const int BYTES = Header.BYTES + NAME_SIZE + 24;
    public AnimationData(uint mDataSize,string mName,uint mNumFrames,int mRepeatStart,int mRepeatEnd,uint mMainChannelFramesOffset): this(mDataSize,mName,mNumFrames,mRepeatStart,mRepeatEnd,mMainChannelFramesOffset,[],[])
    {

    }
    public AnimationData(): this(BYTES,string.Empty,0,0,0,0)
    {

    }
    public override string ToString()
    {
        return Name;
    }
    public override uint GetDataSize()
    {
        return BYTES + GetAnimationChannelSize() + GetMainChannelFrameSize();
    }
    private uint GetAnimationChannelSize()
    {
        return (from channel in AnimationChannels select channel.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
    private uint GetMainChannelFrameSize()
    {
        return (from frame in MainChannelFrames select frame.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
    public void RecalculateOffsets()
    {
        MainChannelFramesOffset = GetAnimationChannelSize();
    }
}
public static class AnimationDataExt
{
    public static AnimationData ReadZEngineUIAnimationData(this Reader reader)
    {
        Header data = reader.ReadZEngineUIHeader();
        string mName = reader.ReadASCII(AnimationData.NAME_SIZE).RemoveInvalids();
        uint mNumFrames = reader.ReadUInt();
        int mRepeatStart = reader.ReadInt();
        int mRepeatEnd = reader.ReadInt();
        uint mNumAnimationChannels = reader.ReadUInt();
        uint mNumMainChannelFrames = reader.ReadUInt();
        uint mMainChannelFramesOffset = reader.ReadUInt();
        long _offset = reader.Offset;

        List<AnimationChannelData> mAnimationChannels = [];
        List<MainChannelFrameData> mMainChannelFrames = [];
        if(mNumAnimationChannels != 0)
        {
            for(uint i = 0;i < mNumAnimationChannels;i++)
                mAnimationChannels.Add(reader.ReadZEngineUIAnimationChannelData());
        }
        if(mNumMainChannelFrames != 0)
        {
            reader.Offset = _offset + mMainChannelFramesOffset;
            for(uint i = 0;i < mNumMainChannelFrames;i++)
                mMainChannelFrames.Add(reader.ReadZEngineUIMainChannelFrameData());
        }
        return new(data.DataSize,mName,mNumFrames,mRepeatStart,mRepeatEnd,mMainChannelFramesOffset,mAnimationChannels,mMainChannelFrames);
    }
    public static void Write(this Writer writer,AnimationData animationData)
    {
        animationData.RecalculateOffsets();
        HeaderExt.Write(writer,animationData);
        writer.Write(animationData.Name.ToFixedStringBytes(AnimationData.NAME_SIZE));
        writer.Write(animationData.Frames);
        writer.Write(animationData.RepeatStart);
        writer.Write(animationData.RepeatEnd);
        writer.Write((uint)animationData.AnimationChannels.Count);
        writer.Write((uint)animationData.MainChannelFrames.Count);
        writer.Write(animationData.MainChannelFramesOffset);
        long _offset = writer.Offset;
        foreach(AnimationChannelData animationChannelData in animationData.AnimationChannels)
            writer.Write(animationChannelData);
        if(animationData.MainChannelFrames.Count == 0) return;
        
        writer.Offset = _offset + animationData.MainChannelFramesOffset;
        foreach(MainChannelFrameData mainChannelFrameData in animationData.MainChannelFrames)
            writer.Write(mainChannelFrameData);
    }
}