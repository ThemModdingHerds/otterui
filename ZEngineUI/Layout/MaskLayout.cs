using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class MaskLayout(ControlLayout controlLayout,float mSkew) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "SPLT";
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public MaskLayout(): this(new(),0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class MaskLayoutExt
{
    public static MaskLayout ReadZEngineUIMaskLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadZEngineUIControlLayout();
        float mSkew = reader.ReadFloat();
        return new(controlLayout,mSkew);
    }
    public static void Write(this Writer writer,MaskLayout maskLayout)
    {
        ControlLayoutExt.Write(writer,maskLayout);
        writer.Write(maskLayout.Skew);
    }
}