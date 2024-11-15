using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Data;
public class ToggleData(uint mDataSize,ControlData controlData,int mOnTextureID,int mOffTextureID,uint mColor) : ControlData(FOURCC,mDataSize,controlData)
{
    public const string FOURCC = "GTGL";
    [JsonPropertyName("mOnTextureID")]
    public int OnTextureID {get;set;} = mOnTextureID;
    [JsonPropertyName("mOffTextureID")]
    public int OffTextureID {get;set;} = mOffTextureID;
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlData.BYTES + 12;
    public ToggleData(): this(0,new(),0,0,0)
    {

    }
}
public static class ToggleDataExt
{
    public static ToggleData ReadOtterUIToggleData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        int mOnTextureID = reader.ReadInt();
        int mOffTextureID = reader.ReadInt();
        uint mColor = reader.ReadUInt();
        return new(controlData.DataSize,controlData,mOnTextureID,mOffTextureID,mColor);
    }
    public static void Write(this Writer writer,ToggleData toggleData)
    {
        ControlDataExt.Write(writer,toggleData);
        writer.Write(toggleData.OnTextureID);
        writer.Write(toggleData.OffTextureID);
        writer.Write(toggleData.Color);
    }
}