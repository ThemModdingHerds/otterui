using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class ARLTLayout(ControlLayout controlLayout,uint mUnknown1,uint mUnknown2,uint mUnknown3,uint mUnknown4,uint mUnknown5) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "ARLT";
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
    public new const int BYTES = ControlLayout.BYTES + 20;
    public ARLTLayout(): this(new(),0,0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class ARLTLayoutExt
{
    public static ARLTLayout ReadZEngineUIARLTLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadZEngineUIControlLayout();
        uint mUnknown1 = reader.ReadUInt();
        uint mUnknown2 = reader.ReadUInt();
        uint mUnknown3 = reader.ReadUInt();
        uint mUnknown4 = reader.ReadUInt();
        uint mUnknown5 = reader.ReadUInt();
        return new(controlLayout,mUnknown1,mUnknown2,mUnknown3,mUnknown4,mUnknown5);
    }
    public static void Write(this Writer writer,ARLTLayout arlt)
    {
        ControlLayoutExt.Write(writer,arlt);
        writer.Write(arlt.Unknown1);
        writer.Write(arlt.Unknown2);
        writer.Write(arlt.Unknown3);
        writer.Write(arlt.Unknown4);
        writer.Write(arlt.Unknown5);
    }
}