using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Layout;
public class ToggleLayout(uint mDataSize,ControlLayout controlLayout,uint mColor) : ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "TGLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public ToggleLayout(): this(0,new(),0)
    {

    }
}
public static class ToggleLayoutExt
{
    public static ToggleLayout ReadOtterUIToggleLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadOtterUIControlLayout();
        uint mColor = reader.ReadUInt();
        return new(controlLayout.DataSize,controlLayout,mColor);
    }
    public static void Write(this Writer writer,ToggleLayout toggleLayout)
    {
        ControlLayoutExt.Write(writer,toggleLayout);
        writer.Write(toggleLayout.Color);
    }
}