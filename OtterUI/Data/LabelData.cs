using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;
using ThemModdingHerds.OtterUI.Utils;

namespace ThemModdingHerds.OtterUI.Data;
public class LabelData(uint mDataSize,ControlData controlData,uint mFontID,uint mColor,Vector2 mScale,float mSkew,int mDropShadow,uint mHAlign,uint mVAlign,float mLeading,int mTracking,uint mTextFit,string mText) : ControlData(FOURCC,mDataSize,controlData)
{
    public const string FOURCC = "GLBL";
    [JsonPropertyName("mFontID")]
    public uint FontID {get;set;} = mFontID;
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mScale")]
    public Vector2 Scale {get;set;} = mScale;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    [JsonPropertyName("mDropShadow")]
    public int DropShadow {get;set;} = mDropShadow;
    [JsonPropertyName("mHAlign")]
    public uint HorizontalAlign {get;set;} = mHAlign;
    [JsonPropertyName("mVAlign")]
    public uint VerticalAlign {get;set;} = mVAlign;
    [JsonPropertyName("mLeading")]
    public float Leading {get;set;} = mLeading;
    [JsonPropertyName("mTracking")]
    public int Tracking {get;set;} = mTracking;
    [JsonPropertyName("mTextFit")]
    public uint TextFit {get;set;} = mTextFit;
    [JsonPropertyName("mText")]
    public string Text {get;set;} = mText;
    public new const int BYTES = ControlData.BYTES + 48;
    public LabelData(): this(0,new(),0,0,Vector2.Zero,0,0,0,0,0,0,0,string.Empty)
    {

    }
}
public static class LabelDataExt
{
    public static LabelData ReadOtterUILabelData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        uint mFontID = reader.ReadUInt();
        uint mColor = reader.ReadUInt();
        Vector2 mScale = reader.ReadVector2();
        float mSkew = reader.ReadFloat();
        int mDropShadow = reader.ReadInt();
        uint mHAlign = reader.ReadUInt();
        uint mVAlign = reader.ReadUInt();
        float mLeading = reader.ReadFloat();
        int mTracking = reader.ReadInt();
        uint mTextFit = reader.ReadUInt();
        uint mTextBufferSize = reader.ReadUInt();
        string mBuffer = mTextBufferSize == 0 ? string.Empty : reader.ReadASCII(mTextBufferSize).RemoveInvalids();
        return new(controlData.DataSize,controlData,mFontID,mColor,mScale,mSkew,mDropShadow,mHAlign,mVAlign,mLeading,mTracking,mTextFit,mBuffer);
    }
    public static void Write(this Writer writer,LabelData labelData)
    {
        ControlDataExt.Write(writer,labelData);
        writer.Write(labelData.FontID);
        writer.Write(labelData.Color);
        writer.WriteVector2(labelData.Scale);
        writer.Write(labelData.Skew);
        writer.Write(labelData.DropShadow);
        writer.Write(labelData.HorizontalAlign);
        writer.Write(labelData.VerticalAlign);
        writer.Write(labelData.Leading);
        writer.Write(labelData.Tracking);
        writer.Write(labelData.TextFit);
        writer.Write((uint)labelData.Text.Length);
        if(labelData.Text.Length != 0)
            writer.WriteASCII(labelData.Text);
    }
}