using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Animations;
public class KeyFrameData(uint mDataSize,uint mFrame,int mEaseType,int mEaseAmount,Vector2 mLeft,Vector2 mRight,Vector2 mTop,Vector2 mBottom) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "KFRM";
    [JsonPropertyName("mFrame")]
    public uint Frame {get;set;} = mFrame;
    [JsonPropertyName("mEaseType")]
    public int EaseType {get;set;} = mEaseType;
    [JsonPropertyName("mEaseAmount")]
    public int EaseAmount {get;set;} = mEaseAmount;
    [JsonPropertyName("mLeft")]
    public Vector2 Left {get;set;} = mLeft;
    [JsonPropertyName("mRight")]
    public Vector2 Right {get;set;} = mRight;
    [JsonPropertyName("mTop")]
    public Vector2 Top {get;set;} = mTop;
    [JsonPropertyName("mBottom")]
    public Vector2 Bottom {get;set;} = mBottom;
    public new const int BYTES = Header.BYTES + 44;
    public KeyFrameData(): this(0,0,0,0,Vector2.Zero,Vector2.Zero,Vector2.Zero,Vector2.Zero)
    {

    }
}
public static class KeyFrameDataExt
{
    public static KeyFrameData ReadOtterUIKeyFrameData(this Reader reader)
    {
        Header data = HeaderExt.ReadOtterUIHeader(reader);

        uint mFrame = reader.ReadUInt();

        int mEaseType = reader.ReadInt();
        int mEaseAmount = reader.ReadInt();

        Vector2 mLeft = reader.ReadVector2();

        Vector2 mRight = reader.ReadVector2();

        Vector2 mTop = reader.ReadVector2();

        Vector2 mBottom = reader.ReadVector2();
        return new(data.DataSize,mFrame,mEaseType,mEaseAmount,mLeft,mRight,mTop,mBottom);
    }
    public static void Write(this Writer writer,KeyFrameData keyFrameData)
    {
        HeaderExt.Write(writer,keyFrameData);

        writer.Write(keyFrameData.Frame);

        writer.Write(keyFrameData.EaseType);
        writer.Write(keyFrameData.EaseAmount);

        writer.WriteVector2(keyFrameData.Left);

        writer.WriteVector2(keyFrameData.Right);

        writer.WriteVector2(keyFrameData.Top);

        writer.WriteVector2(keyFrameData.Bottom);
    }
}