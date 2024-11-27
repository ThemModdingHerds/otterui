using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class SpriteLayout(ControlLayout controlLayout,uint mColor,float mSkew) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "SPLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    public new const int BYTES = ControlLayout.BYTES + 8;
    public SpriteLayout(): this(new(),0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class SpriteLayoutExt
{
    public static SpriteLayout ReadZEngineUISpriteLayout(this Reader reader)
    {
        ControlLayout controlLayout = ControlLayoutExt.ReadZEngineUIControlLayout(reader);
        uint mColor = reader.ReadUInt();
        float mSkew = reader.ReadFloat();
        return new(controlLayout,mColor,mSkew);
    }
    public static void Write(this Writer writer,SpriteLayout spriteLayout)
    {
        ControlLayoutExt.Write(writer,spriteLayout);
        writer.Write(spriteLayout.Color);
        writer.Write(spriteLayout.Skew);
    }
}