using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Layout;
public class SpriteLayout(uint mDataSize,ControlLayout controlLayout,uint mColor,float mSkew) : ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "SPLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    public new const int BYTES = ControlLayout.BYTES + 8;
    public SpriteLayout(): this(0,new(),0,0)
    {

    }
}
public static class SpriteLayoutExt
{
    public static SpriteLayout ReadOtterUISpriteLayout(this Reader reader)
    {
        ControlLayout controlLayout = ControlLayoutExt.ReadOtterUIControlLayout(reader);
        uint mColor = reader.ReadUInt();
        float mSkew = reader.ReadFloat();
        return new(controlLayout.DataSize,controlLayout,mColor,mSkew);
    }
    public static void Write(this Writer writer,SpriteLayout spriteLayout)
    {
        ControlLayoutExt.Write(writer,spriteLayout);
        writer.Write(spriteLayout.Color);
        writer.Write(spriteLayout.Skew);
    }
}