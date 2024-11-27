using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class ARCCData(ControlData controlData,uint mUnknown1,uint mUnknown2,uint mUnknown3,uint mUnknown4,uint mUnknown5,uint mUnknown6,uint mFlag) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "ARCC";
    [JsonPropertyName("mUnknown1")]
    public uint Unknown1 {get;set;} = mUnknown1;
    [JsonPropertyName("mUnknown2")]
    public uint Unknown2 {get;set;} = mUnknown2;
    [JsonPropertyName("mUnknown3")]
    public uint Unknown3 {get;set;} = mUnknown3;
    [JsonPropertyName("mUnknown4")]
    public uint Unknown4 {get;set;} = mUnknown4;
    [JsonPropertyName("mUnknown5")]
    public uint Unknown5 {get;set;} = mUnknown5;
    [JsonPropertyName("mUnknown6")]
    public uint Unknown6 {get;set;} = mUnknown6;
    [JsonPropertyName("mUnknown7")]
    public uint Flag {get;set;} = mFlag;
    public new const int BYTES = ControlData.BYTES + 28;
    public ARCCData(): this(new(),0,0,0,0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class ARCCDataExt
{
    public static ARCCData ReadZEngineUIARCCData(this Reader reader)
    {
        ControlData ControlData = reader.ReadZEngineUIControlData();
        uint mUnknown1 = reader.ReadUInt();
        uint mUnknown2 = reader.ReadUInt();
        uint mUnknown3 = reader.ReadUInt();
        uint mUnknown4 = reader.ReadUInt();
        uint mUnknown5 = reader.ReadUInt();
        uint mUnknown6 = reader.ReadUInt();
        uint mFlag = reader.ReadUInt();
        return new(ControlData,mUnknown1,mUnknown2,mUnknown3,mUnknown4,mUnknown5,mUnknown6,mFlag);
    }
    public static void Write(this Writer writer,ARCCData aarcc)
    {
        ControlDataExt.Write(writer,aarcc);
        writer.Write(aarcc.Unknown1);
        writer.Write(aarcc.Unknown2);
        writer.Write(aarcc.Unknown3);
        writer.Write(aarcc.Unknown4);
        writer.Write(aarcc.Unknown5);
        writer.Write(aarcc.Unknown6);
        writer.Write(aarcc.Flag);
    }
}