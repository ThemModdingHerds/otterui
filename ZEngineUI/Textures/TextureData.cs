using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Textures;
public class TextureData(uint mTextureID,string mTextureName,int mRefCount,TextureRect mTextureRect)
{
    [JsonPropertyName("mTextureID")]
    public uint TextureID {get;set;} = mTextureID;
    public const int TEXTURE_NAME_SIZE = 256;
    [JsonPropertyName("mTextureName")]
    public string TextureName {get;set;} = mTextureName.MaybeSubstring(TEXTURE_NAME_SIZE);
    [JsonPropertyName("mRefCount")]
    public int RefCount {get;set;} = mRefCount;
    [JsonPropertyName("mTextureRect")]
    public TextureRect TextureRect {get;set;} = mTextureRect;
    [JsonIgnore]
    public bool IsTextureAtlas {get => TextureName.StartsWith("Textures/TA_");}
    public const int BYTES = 8 + TEXTURE_NAME_SIZE + TextureRect.BYTES;
    public TextureData(): this(0xFFFFFFFF,string.Empty,0,new())
    {

    }
    public override string ToString()
    {
        return TextureName;
    }
}
public static class TextureDataExt
{
    public static TextureData ReadZEngineUITextureData(this Reader reader)
    {
        uint mTextureID = reader.ReadUInt();
        string mTextureName = reader.ReadASCII(TextureData.TEXTURE_NAME_SIZE).RemoveInvalids();
        int mRefCount = reader.ReadInt();
        TextureRect mTextureRect = reader.ReadZEngineUITextureRect();
        return new(mTextureID,mTextureName,mRefCount,mTextureRect);
    }
    public static void Write(this Writer writer,TextureData textureData)
    {
        writer.Write(textureData.TextureID);
        writer.Write(textureData.TextureName.ToFixedStringBytes(TextureData.TEXTURE_NAME_SIZE));
        writer.Write(textureData.RefCount);
        writer.Write(textureData.TextureRect);
    }
}