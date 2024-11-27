using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class SliderLayout(ControlLayout controlLayout,uint mColor) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "SLLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public SliderLayout(): this(new(),0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class SliderLayoutExt
{
    public static SliderLayout ReadZEngineUISliderLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadZEngineUIControlLayout();
        uint mColor = reader.ReadUInt();
        return new(controlLayout,mColor);
    }
    public static void Write(this Writer writer,SliderLayout sliderLayout)
    {
        ControlLayoutExt.Write(writer,sliderLayout);
        writer.Write(sliderLayout.Color);
    }
}