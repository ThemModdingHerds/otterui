using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class ToggleLayout(ControlLayout controlLayout,uint mColor) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "TGLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public ToggleLayout(): this(new(),0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class ToggleLayoutExt
{
    public static ToggleLayout ReadZEngineUIToggleLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadZEngineUIControlLayout();
        uint mColor = reader.ReadUInt();
        return new(controlLayout,mColor);
    }
    public static void Write(this Writer writer,ToggleLayout toggleLayout)
    {
        ControlLayoutExt.Write(writer,toggleLayout);
        writer.Write(toggleLayout.Color);
    }
}