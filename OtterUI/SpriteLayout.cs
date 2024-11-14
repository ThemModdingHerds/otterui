using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class SpriteLayout(ControlLayout controlLayout,uint mColor,float mSkew) : ControlLayout(controlLayout.FourCC,controlLayout.DataSize,controlLayout.Center,controlLayout.Position,controlLayout.Size,controlLayout.Rotation)
{
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
}
public static class SpriteLayoutExt
{
    public static SpriteLayout ReadOtterUISpriteLayout(this Reader reader)
    {
        ControlLayout controlLayout = ControlLayoutExt.ReadOtterUIControlLayout(reader);
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