using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Layout;
public class SliderLayout(uint mDataSize,ControlLayout controlLayout,uint mColor) : ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "SLLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public SliderLayout(): this(0,new(),0)
    {

    }
}
public static class SliderLayoutExt
{
    public static SliderLayout ReadOtterUISliderLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadOtterUIControlLayout();
        uint mColor = reader.ReadUInt();
        return new(controlLayout.DataSize,controlLayout,mColor);
    }
    public static void Write(this Writer writer,SliderLayout sliderLayout)
    {
        ControlLayoutExt.Write(writer,sliderLayout);
        writer.Write(sliderLayout.Color);
    }
}