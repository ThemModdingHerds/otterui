using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class SPPLLayout(ControlLayout controlLayout,uint mColor,uint mUnknown1,uint mUnknown2,uint mColor2,uint mUnknown3,uint mUnknown4,uint mFooter) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "SPPL";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mUnknown1")]
    public uint Unknown1 {get;set;} = mUnknown1;
    [JsonPropertyName("mUnknown2")]
    public uint Unknown2 {get;set;} = mUnknown2;
    [JsonPropertyName("mColor2")]
    public uint Color2 {get;set;} = mColor2;
    [JsonPropertyName("mUnknown3")]
    public uint Unknown3 {get;set;} = mUnknown3;
    [JsonPropertyName("mUnknown4")]
    public uint Unknown4 {get;set;} = mUnknown4;
    [JsonPropertyName("mFooter")]
    public uint Footer {get;set;} = mFooter;
    public new const int BYTES = ControlLayout.BYTES + 28;
    public const int FOOTER = 12345678;
    public SPPLLayout(): this(new(),0,0,0,0,0,0,FOOTER)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class SPPLLayoutExt
{
    public static SPPLLayout ReadZEngineUISPPLLayout(this Reader reader)
    {
        ControlLayout controlData = reader.ReadZEngineUIControlLayout();
        uint mColor = reader.ReadUInt();
        uint mUnknown1 = reader.ReadUInt();
        uint mUnknown2 = reader.ReadUInt();
        uint mColor2 = reader.ReadUInt();
        uint mUnknown3 = reader.ReadUInt();
        uint mUnknown4 = reader.ReadUInt();
        uint mFooter = reader.ReadUInt();
        return new(controlData,mColor,mUnknown1,mUnknown2,mColor2,mUnknown3,mUnknown4,mFooter);
    }
    public static void Write(this Writer writer,SPPLLayout sppl)
    {
        ControlLayoutExt.Write(writer,sppl);
        writer.Write(sppl.Color);
        writer.Write(sppl.Unknown1);
        writer.Write(sppl.Unknown2);
        writer.Write(sppl.Color2);
        writer.Write(sppl.Unknown3);
        writer.Write(sppl.Unknown4);
        writer.Write(sppl.Footer);
    }
}