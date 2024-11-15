using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Data;
public class TableData(uint mDataSize,ControlData controlData,uint mDefaultRowHeight,uint mRowSpacing) : ControlData(FOURCC,mDataSize,controlData)
{
    public const string FOURCC = "GTBL";
    [JsonPropertyName("mDefaultRowHeight")]
    public uint DefaultRowHeight {get;set;} = mDefaultRowHeight;
    [JsonPropertyName("mRowSpacing")]
    public uint RowSpacing {get;set;} = mRowSpacing;
    public new const int BYTES = ControlData.BYTES + 8;
    public TableData(): this(0,new(),0,0)
    {

    }
}
public static class TableDataExt
{
    public static TableData ReadOtterUITableData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        uint mDefaultRowHeight = reader.ReadUInt();
        uint mRowSpacing = reader.ReadUInt();
        return new(controlData.DataSize,controlData,mDefaultRowHeight,mRowSpacing);
    }
    public static void Write(this Writer writer,TableData tableData)
    {
        ControlDataExt.Write(writer,tableData);
        writer.Write(tableData.DefaultRowHeight);
        writer.Write(tableData.RowSpacing);
    }
}