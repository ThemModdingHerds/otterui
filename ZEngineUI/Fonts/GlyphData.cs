using System.Text;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Fonts;
public class GlyphData(uint mCharCode,uint mIsImageGlyph,uint mX,uint mY,uint mWidth,uint mHeight,uint mTop,uint mAdvance,uint mLeftBearing,uint mAtlasIndex)
{
    [JsonPropertyName("mCharCode")]
    public uint CharCode {get;set;} = mCharCode;
    [JsonIgnore]
    public string Char2 {
        get
        {
            byte[] bytes = BitConverter.GetBytes(CharCode);
            char first = BitConverter.ToChar([bytes[0],bytes[1]]);
            char last = BitConverter.ToChar([bytes[2],bytes[3]]);
            string result = new([first,last]);
            return result.RemoveInvalids();
        }
        set
        {
            char first = value[0];
            char last = value[1];
            byte[] bytes = [..BitConverter.GetBytes(first),..BitConverter.GetBytes(last)];
            CharCode = BitConverter.ToUInt32(bytes);
        }
    }
    [JsonPropertyName("mIsImageGlyph")]
    public uint IsImageGlyph {get;set;} = mIsImageGlyph;
    [JsonPropertyName("mX")]
    public uint X {get;set;} = mX;
    [JsonPropertyName("mY")]
    public uint Y {get;set;} = mY;
    [JsonPropertyName("mWidth")]
    public uint Width {get;set;} = mWidth;
    [JsonPropertyName("mHeight")]
    public uint Height {get;set;} = mHeight;
    [JsonPropertyName("mTop")]
    public uint Top {get;set;} = mTop;
    [JsonPropertyName("mAdvance")]
    public uint Advance {get;set;} = mAdvance;
    [JsonPropertyName("mLeftBearing")]
    public uint LeftBearing {get;set;} = mLeftBearing;
    [JsonPropertyName("mAtlasIndex")]
    public uint AtlasIndex {get;set;} = mAtlasIndex;
    public const int BYTES = 40;
    public GlyphData(): this(0,0,0,0,0,0,0,0,0,0)
    {

    }
}
public static class GlyphDataExt
{
    public static GlyphData ReadZEngineUIGlyphData(this Reader reader)
    {
        uint mCharCode = reader.ReadUInt();
        uint mIsImageGlyph = reader.ReadUInt();
        uint mX = reader.ReadUInt();
        uint mY = reader.ReadUInt();
        uint mWidth = reader.ReadUInt();
        uint mHeight = reader.ReadUInt();
        uint mTop = reader.ReadUInt();
        uint mAdvance = reader.ReadUInt();
        uint mLeftBearing = reader.ReadUInt();
        uint mAtlasIndex = reader.ReadUInt();
        return new(mCharCode,mIsImageGlyph,mX,mY,mWidth,mHeight,mTop,mAdvance,mLeftBearing,mAtlasIndex);
    }
    public static void Write(this Writer writer,GlyphData glyphData)
    {
        writer.Write(glyphData.CharCode);
        writer.Write(glyphData.IsImageGlyph);
        writer.Write(glyphData.X);
        writer.Write(glyphData.Y);
        writer.Write(glyphData.Width);
        writer.Write(glyphData.Height);
        writer.Write(glyphData.Top);
        writer.Write(glyphData.Advance);
        writer.Write(glyphData.LeftBearing);
        writer.Write(glyphData.AtlasIndex);
    }
}