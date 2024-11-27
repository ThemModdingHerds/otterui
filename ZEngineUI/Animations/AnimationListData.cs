using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Animations;
public class AnimationListData(uint mDataSize,IEnumerable<AnimationData> mAnimations) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "GGAL";
    [JsonPropertyName("mAnimations")]
    public List<AnimationData> Animations {get;set;} = [..mAnimations];
    public new const int BYTES = Header.BYTES + 4;
    public AnimationListData(uint mDataSize): this(mDataSize,[])
    {

    }
    public AnimationListData(): this(BYTES)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + (from animation in Animations select animation.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
}
public static class AnimationListDataExt
{
    public static AnimationListData ReadZEngineUIAnimationListData(this Reader reader)
    {
        Header data = reader.ReadZEngineUIHeader();
        uint mNumAnimations = reader.ReadUInt();
        List<AnimationData> mAnimations = [];
        for(uint i = 0;i < mNumAnimations;i++)
            mAnimations.Add(reader.ReadZEngineUIAnimationData());
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