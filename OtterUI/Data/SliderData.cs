using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Data;
public class SliderData(uint mDataSize,ControlData controlData,uint mThumbWidth,uint mThumbHeight,int mStartTextureID,int mMiddleTextureID,int mEndTextureID,int mThumbTextureID,int mMin,int mMax,int mStep,int mValue,uint mColor) : ControlData(FOURCC,mDataSize,controlData)
{
    public const string FOURCC = "GSLD";
    [JsonPropertyName("mThumbWidth")]
    public uint ThumbWidth {get;set;} = mThumbWidth;
    [JsonPropertyName("mThumbHeight")]
    public uint ThumbHeight {get;set;} = mThumbHeight;
    [JsonPropertyName("mStartTextureID")]
    public int StartTextureID {get;set;} = mStartTextureID;
    [JsonPropertyName("mMiddleTextureID")]
    public int MiddleTextureID {get;set;} = mMiddleTextureID;
    [JsonPropertyName("mEndTextureID")]
    public int EndTextureID {get;set;} = mEndTextureID;
    [JsonPropertyName("mThumbTextureID")]
    public int ThumbTextureID {get;set;} = mThumbTextureID;
    [JsonPropertyName("mMin")]
    public int Min {get;set;} = mMin;
    [JsonPropertyName("mMax")]
    public int Max {get;set;} = mMax;
    [JsonPropertyName("mStep")]
    public int Step {get;set;} = mStep;
    [JsonPropertyName("mValue")]
    public int Value {get;set;} = mValue;
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlData.BYTES + 44;
    public SliderData(): this(0,new(),0,0,0,0,0,0,0,0,0,0,0)
    {

    }
}
public static class SliderDataExt
{
    public static SliderData ReadOtterUISliderData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        uint mThumbWidth = reader.ReadUInt();
        uint mThumbHeight = reader.ReadUInt();
        int mStartTextureID = reader.ReadInt();
        int mMiddleTextureID = reader.ReadInt();
        int mEndTextureID = reader.ReadInt();
        int mThumbTextureID = reader.ReadInt();
        int mMin = reader.ReadInt();
        int mMax = reader.ReadInt();
        int mStep = reader.ReadInt();
        int mValue = reader.ReadInt();
        uint mColor = reader.ReadUInt();
        return new(controlData.DataSize,controlData,mThumbWidth,mThumbHeight,mStartTextureID,mMiddleTextureID,mEndTextureID,mThumbTextureID,mMin,mMax,mStep,mValue,mColor);
    }
    public static void Write(this Writer writer,SliderData sliderData)
    {
        ControlDataExt.Write(writer,sliderData);
        writer.Write(sliderData.ThumbWidth);
        writer.Write(sliderData.ThumbHeight);
        writer.Write(sliderData.StartTextureID);
        writer.Write(sliderData.MiddleTextureID);
        writer.Write(sliderData.EndTextureID);
        writer.Write(sliderData.ThumbTextureID);
        writer.Write(sliderData.Min);
        writer.Write(sliderData.Max);
        writer.Write(sliderData.Step);
        writer.Write(sliderData.Value);
        writer.Write(sliderData.Color);
    }
}