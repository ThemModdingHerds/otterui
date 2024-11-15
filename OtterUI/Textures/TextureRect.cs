using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI.Textures;
public class TextureRect(uint mTextureID,float left,float top,float right,float bottom)
{
    [JsonPropertyName("mTextureID")]
    public uint TextureID {get;set;} = mTextureID;
    public const int UV_SIZE = 4;
    [JsonPropertyName("uv")]
    public float[] UV {get;set;} = [left,top,right,bottom];
    public const int BYTES = 20;
    public TextureRect(): this(0xFFFFFFFF,0,0,0,0)
    {

    }
}
public static class TextureRectExt
{
    public static TextureRect ReadOtterUITextureRect(this Reader reader)
    {
        uint mTextureID = reader.ReadUInt();
        float left = reader.ReadFloat();
        float top = reader.ReadFloat();
        float right = reader.ReadFloat();
        float bottom = reader.ReadFloat();
        return new(mTextureID,left,top,right,bottom);
    }
    public static void Write(this Writer writer,TextureRect textureRect)
    {
        writer.Write(textureRect.TextureID);
        writer.Write(textureRect.UV,(writer,value) => writer.Write(value));
    }
}