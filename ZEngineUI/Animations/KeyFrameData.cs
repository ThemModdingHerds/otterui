using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Animations;
public class KeyFrameData
(uint mFrame,int mEaseType,int mEaseAmount,
float mLeftAbs,float mLeftRel,
float mRightAbs,float mRightRel,
float mTopAbs,float mTopRel,
float mBottomAbs,float mBottomRel,ControlLayout mControl) : Header(FOURCC,BYTES)
{
    public const string FOURCC = "KFRM";
    [JsonPropertyName("mFrame")]
    public uint Frame {get;set;} = mFrame;
    [JsonPropertyName("mEaseType")]
    public int EaseType {get;set;} = mEaseType;
    [JsonPropertyName("mEaseAmount")]
    public int EaseAmount {get;set;} = mEaseAmount;
    [JsonPropertyName("mLeftAbs")]
    public float LeftAbs {get;set;} = mLeftAbs;
    [JsonPropertyName("mLeftRel")]
    public float LeftRel {get;set;} = mLeftRel;
    [JsonPropertyName("mRightAbs")]
    public float RightAbs {get;set;} = mRightAbs;
    [JsonPropertyName("mRightRel")]
    public float RightRel {get;set;} = mRightRel;
    [JsonPropertyName("mTopAbs")]
    public float TopAbs {get;set;} = mTopAbs;
    [JsonPropertyName("mTopRel")]
    public float TopRel {get;set;} = mTopRel;
    [JsonPropertyName("mBottomAbs")]
    public float BottomAbs {get;set;} = mBottomAbs;
    [JsonPropertyName("mBottomRel")]
    public float BottomRel {get;set;} = mBottomRel;
    [JsonPropertyName("mControl")]
    public ControlLayout Control {get;set;} = mControl;
    public new const int BYTES = Header.BYTES + 44;
    public KeyFrameData(): this(0,0,0,0,0,0,0,0,0,0,0,new())
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + Control?.GetDataSize() ?? 0;
    }
    public override string ToString()
    {
        return $"{Frame}:{Control}";
    }
}
public static class KeyFrameDataExt
{
    public static KeyFrameData ReadZEngineUIKeyFrameData(this Reader reader)
    {
        Header data = HeaderExt.ReadZEngineUIHeader(reader);

        uint mFrame = reader.ReadUInt();

        int mEaseType = reader.ReadInt();
        int mEaseAmount = reader.ReadInt();

        float mLeftAbs = reader.ReadFloat();
        float mLeftRel = reader.ReadFloat();

        float mRightAbs = reader.ReadFloat();
        float mRightRel = reader.ReadFloat();

        float mTopAbs = reader.ReadFloat();
        float mTopRel = reader.ReadFloat();

        float mBottomAbs = reader.ReadFloat();
        float mBottomRel = reader.ReadFloat();
        ControlLayout mControl = ControlLayout.Read(reader);
        return new(mFrame,mEaseType,mEaseAmount,mLeftAbs,mLeftRel,mRightAbs,mRightRel,mTopAbs,mTopRel,mBottomAbs,mBottomRel,mControl);
    }
    public static void Write(this Writer writer,KeyFrameData keyFrameData)
    {
        HeaderExt.Write(writer,keyFrameData);

        writer.Write(keyFrameData.Frame);

        writer.Write(keyFrameData.EaseType);
        writer.Write(keyFrameData.EaseAmount);

        writer.Write(keyFrameData.LeftAbs);
        writer.Write(keyFrameData.LeftRel);

        writer.Write(keyFrameData.RightAbs);
        writer.Write(keyFrameData.RightRel);

        writer.Write(keyFrameData.TopAbs);
        writer.Write(keyFrameData.TopRel);

        writer.Write(keyFrameData.BottomAbs);
        writer.Write(keyFrameData.BottomRel);

        ControlLayout.Write(writer,keyFrameData.Control);
    }
}