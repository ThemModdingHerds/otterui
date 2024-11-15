using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Layout;
public class GroupLayout(uint mDataSize,ControlLayout controlLayout,uint mColor): ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "GPLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public GroupLayout(): this(0,new(),0)
    {

    }
}
public static class GroupLayoutExt
{
    public static GroupLayout ReadOtterUIGroupLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadOtterUIControlLayout();
        uint mColor = reader.ReadUInt();
        return new(controlLayout.DataSize,controlLayout,mColor);
    }
    public static void Write(this Writer writer,GroupLayout groupLayout)
    {
        ControlLayoutExt.Write(writer,groupLayout);
        writer.Write(groupLayout.Color);
    }
}