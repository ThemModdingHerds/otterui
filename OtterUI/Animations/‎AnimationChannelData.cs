using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Animations;
public class AnimationChannelData(uint mDataSize,int mControlID,IEnumerable<KeyFrameData> mKeyFrames) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "GGAC";
    [JsonPropertyName("mControlID")]
    public int ControlID {get;set;} = mControlID;
    [JsonPropertyName("mKeyFrames")]
    public List<KeyFrameData> KeyFrames {get;set;} = [..mKeyFrames];
    public new const int BYTES = Header.BYTES + 8;
    public AnimationChannelData(uint mDataSize,int mControlID): this(mDataSize,mControlID,[])
    {

    }
    public AnimationChannelData(): this(0,0)
    {

    }
}
public static class AnimationChannelDataExt
{
    public static AnimationChannelData ReadOtterUIAnimationChannelData(this Reader reader)
    {
        Header data = reader.ReadOtterUIHeader();
        int mControlID = reader.ReadInt();
        uint mNumKeyFrames = reader.ReadUInt();
        List<KeyFrameData> mKeyFrames = [];
        for(uint i = 0;i < mNumKeyFrames;i++)
            mKeyFrames.Add(reader.ReadOtterUIKeyFrameData());
        return new(data.DataSize,mControlID,mKeyFrames);
    }
    public static void Write(this Writer writer,AnimationChannelData animationChannelData)
    {
        HeaderExt.Write(writer,animationChannelData);
        writer.Write(animationChannelData.ControlID);
        writer.Write((uint)animationChannelData.KeyFrames.Count);
        foreach(KeyFrameData keyFrame in animationChannelData.KeyFrames)
            writer.Write(keyFrame);
    }
}