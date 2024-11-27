using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class ToggleData(ControlData controlData,int mOnTextureID,int mOffTextureID,uint mColor) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GTGL";
    [JsonPropertyName("mOnTextureID")]
    public int OnTextureID {get;set;} = mOnTextureID;
    [JsonPropertyName("mOffTextureID")]
    public int OffTextureID {get;set;} = mOffTextureID;
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlData.BYTES + 12;
    public ToggleData(): this(new(),0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class ToggleDataExt
{
    public static ToggleData ReadZEngineUIToggleData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        int mOnTextureID = reader.ReadInt();
        int mOffTextureID = reader.ReadInt();
        uint mColor = reader.ReadUInt();
        return new(controlData,mOnTextureID,mOffTextureID,mColor);
    }
    public static void Write(this Writer writer,ToggleData toggleData)
    {
        ControlDataExt.Write(writer,toggleData);
        writer.Write(toggleData.OnTextureID);
        writer.Write(toggleData.OffTextureID);
        writer.Write(toggleData.Color);
    }
}