using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Animations;
public class AnimationListData(uint mDataSize,IEnumerable<AnimationData> mAnimations) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "GGAL";
    [JsonPropertyName("mAnimations")]
    public List<AnimationData> Animations {get;set;} = [..mAnimations];
    public new const int BYTES = Header.BYTES + 4;
    public AnimationListData(uint mDataSize): this(mDataSize,[])
    {

    }
    public AnimationListData(): this(0)
    {

    }
}
public static class AnimationListDataExt
{
    public static AnimationListData ReadOtterUIAnimationListData(this Reader reader)
    {
        Header data = reader.ReadOtterUIHeader();
        uint mNumAnimations = reader.ReadUInt();
        List<AnimationData> mAnimations = [];
        for(uint i = 0;i < mNumAnimations;i++)
            mAnimations.Add(reader.ReadOtterUIAnimationData());
        return new(data.DataSize,mAnimations);
    }
    public static void Write(this Writer writer,AnimationListData animationListData)
    {
        HeaderExt.Write(writer,animationListData);
        writer.Write((uint)animationListData.Animations.Count);
        foreach(AnimationData animationData in animationListData.Animations)
            writer.Write(animationData);
    }
}