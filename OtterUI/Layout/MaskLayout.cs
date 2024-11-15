using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Layout;
public class MaskLayout(uint mDataSize,ControlLayout controlLayout,float mSkew) : ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "SPLT";
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public MaskLayout(): this(0,new(),0)
    {

    }
}
public static class MaskLayoutExt
{
    public static MaskLayout ReadOtterUIMaskLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadOtterUIControlLayout();
        float mSkew = reader.ReadFloat();
        return new(controlLayout.DataSize,controlLayout,mSkew);
    }
    public static void Write(this Writer writer,MaskLayout maskLayout)
    {
        ControlLayoutExt.Write(writer,maskLayout);
        writer.Write(maskLayout.Skew);
    }
}