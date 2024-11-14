using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class KeyFrameData(string mFourCC,uint mDataSize,uint mFrame,int mEaseType,int mEaseAmount,Vector2 mLeft,Vector2 mRight,Vector2 mTop,Vector2 mBottom) : Data(mFourCC,mDataSize)
{
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
    public KeyFrameData(Data data,uint mFrame,int mEaseType,int mEaseAmount,Vector2 mLeft,Vector2 mRight,Vector2 mTop,Vector2 mBottom): this(data.FourCC,data.DataSize,mFrame,mEaseType,mEaseAmount,mLeft,mRight,mTop,mBottom)
    {

    }
}
public static class KeyFrameDataExt
{
    public static KeyFrameData ReadOtterUIKeyFrameData(this Reader reader)
    {
        Data data = DataExt.ReadOtterUIData(reader);
        uint mFrame = reader.ReadUInt();
        int mEaseType = reader.ReadInt();
        int mEaseAmount = reader.ReadInt();
        Vector2 mLeft = reader.ReadVector2();
        Vector2 mRight = reader.ReadVector2();
        Vector2 mTop = reader.ReadVector2();
        Vector2 mBottom = reader.ReadVector2();
        return new(data,mFrame,mEaseType,mEaseAmount,mLeft,mRight,mTop,mBottom);
    }
    public static void Write(this Writer writer,KeyFrameData keyFrameData)
    {
        DataExt.Write(writer,keyFrameData);
        writer.Write(keyFrameData.Frame);
        writer.Write(keyFrameData.EaseType);
        writer.Write(keyFrameData.EaseAmount);
        writer.WriteVector2(keyFrameData.Left);
        writer.WriteVector2(keyFrameData.Right);
        writer.WriteVector2(keyFrameData.Top);
        writer.WriteVector2(keyFrameData.Bottom);
    }
}