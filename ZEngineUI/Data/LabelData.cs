using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Data;
public class LabelData(ControlData controlData,uint mFontID,uint mColor,float mScaleX,float mScaleY,float mSkew,int mDropShadow,uint mHAlign,uint mVAlign,float mLeading,int mTracking,uint mTextFit,byte[] mText) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GLBL";
    [JsonPropertyName("mFontID")]
    public uint FontID {get;set;} = mFontID;
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mScaleX")]
    public float ScaleX {get;set;} = mScaleX;
    [JsonPropertyName("mScaleY")]
    public float ScaleY {get;set;} = mScaleY;
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
    [JsonPropertyName("mBuffer")]
    public byte[] Buffer {get;set;} = mText;
    [JsonIgnore]
    [XmlIgnore]
    public string Text {get => Encoding.ASCII.GetString(Buffer);set => Buffer = value.ToStringBytes();}
    public new const int BYTES = ControlData.BYTES + 48;
    public LabelData(): this(new(),0,0,0,0,0,0,0,0,0,0,0,[])
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + (uint)Buffer.Length;
    }
    public override string ToString()
    {
        return $"{Name}: '{Text}'";
    }
}
public static class LabelDataExt
{
    public static LabelData ReadZEngineUILabelData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        uint mFontID = reader.ReadUInt();
        uint mColor = reader.ReadUInt();
        float mScaleX = reader.ReadFloat();
        float mScaleY = reader.ReadFloat();
        float mSkew = reader.ReadFloat();
        int mDropShadow = reader.ReadInt();
        uint mHAlign = reader.ReadUInt();
        uint mVAlign = reader.ReadUInt();
        float mLeading = reader.ReadFloat();
        int mTracking = reader.ReadInt();
        uint mTextFit = reader.ReadUInt();
        uint mTextBufferSize = reader.ReadUInt();
        byte[] mBuffer = mTextBufferSize == 0 ? [] : reader.ReadBytes((int)mTextBufferSize);
        return new(controlData,mFontID,mColor,mScaleX,mScaleY,mSkew,mDropShadow,mHAlign,mVAlign,mLeading,mTracking,mTextFit,mBuffer);
    }
    public static void Write(this Writer writer,LabelData labelData)
    {
        ControlDataExt.Write(writer,labelData);
        writer.Write(labelData.FontID);
        writer.Write(labelData.Color);
        writer.Write(labelData.ScaleX);
        writer.Write(labelData.ScaleY);
        writer.Write(labelData.Skew);
        writer.Write(labelData.DropShadow);
        writer.Write(labelData.HorizontalAlign);
        writer.Write(labelData.VerticalAlign);
        writer.Write(labelData.Leading);
        writer.Write(labelData.Tracking);
        writer.Write(labelData.TextFit);
        writer.Write((uint)labelData.Buffer.Length);
        if(labelData.Buffer.Length != 0)
            writer.Write(labelData.Buffer);
    }
}