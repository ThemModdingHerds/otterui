using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class TableData(ControlData controlData,uint mDefaultRowHeight,uint mRowSpacing) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GTBL";
    [JsonPropertyName("mDefaultRowHeight")]
    public uint DefaultRowHeight {get;set;} = mDefaultRowHeight;
    [JsonPropertyName("mRowSpacing")]
    public uint RowSpacing {get;set;} = mRowSpacing;
    public new const int BYTES = ControlData.BYTES + 8;
    public TableData(): this(new(),0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class TableDataExt
{
    public static TableData ReadZEngineUITableData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        uint mDefaultRowHeight = reader.ReadUInt();
        uint mRowSpacing = reader.ReadUInt();
        return new(controlData,mDefaultRowHeight,mRowSpacing);
    }
    public static void Write(this Writer writer,TableData tableData)
    {
        ControlDataExt.Write(writer,tableData);
        writer.Write(tableData.DefaultRowHeight);
        writer.Write(tableData.RowSpacing);
    }
}