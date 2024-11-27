using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class GSPPData(ControlData controlData,uint mTextureID,uint[] mUnknown,uint mFooter) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GSPP";
    [JsonPropertyName("mTextureId")]
    public uint TextureID {get;set;} = mTextureID;
    [JsonPropertyName("mUnknown")]
    public uint[] Unknown {get;set;} = mUnknown;
    [JsonPropertyName("mFooter")]
    public uint Footer {get;set;} = mFooter;
    public new const int BYTES = ControlData.BYTES + 44;
    public const uint FOOTER = 12345678;
    public GSPPData(): this(new(),0,new uint[9],FOOTER)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class GSPPDataExt
{
    public static GSPPData ReadZEngineUIGSPPData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        uint mTextureID = reader.ReadUInt();
        uint[] mUnknown = reader.ReadArray((r) => r.ReadUInt(),9);
        uint mFooter = reader.ReadUInt();
        return new(controlData,mTextureID,mUnknown,mFooter);
    }
    public static void Write(this Writer writer,GSPPData gspp)
    {
        ControlDataExt.Write(writer,gspp);
        writer.Write(gspp.TextureID);
        foreach(uint unknown in gspp.Unknown)
            writer.Write(unknown);
        writer.Write(gspp.Footer);
    }
}