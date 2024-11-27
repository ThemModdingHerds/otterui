using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Fonts;
public class FontData(uint mDataSize,uint mID,string mName,uint mFontSize,uint mFontWidth,uint mFontHeight,uint mMaxTop,uint mNumTextures,IEnumerable<GlyphData> glyphs) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "GFNT";
    [JsonPropertyName("mID")]
    public uint ID {get;set;} = mID;
    public const int NAME_SIZE = 64;
    [JsonPropertyName("mName")]
    public string Name {get;set;} = mName.MaybeSubstring(NAME_SIZE);
    [JsonPropertyName("mFontSize")]
    public uint FontSize {get;set;} = mFontSize;
    [JsonPropertyName("mFontWidth")]
    public uint FontWidth {get;set;} = mFontWidth;
    [JsonPropertyName("mFontHeight")]
    public uint FontHeight {get;set;} = mFontHeight;
    [JsonPropertyName("mMaxTop")]
    public uint MaxTop {get;set;} = mMaxTop;
    [JsonPropertyName("mNumTextures")]
    public uint NumTextures {get;set;} = mNumTextures;
    [JsonPropertyName("mGlyphs")]
    public List<GlyphData> Glyphs {get;set;} = [..glyphs];
    public new const int BYTES = Header.BYTES + NAME_SIZE + 28;
    public FontData(uint mDataSize,uint mID,string mName,uint mFontSize,uint mFontWidth,uint mFontHeight,uint mMaxTop,uint mNumTextures): this(mDataSize,mID,mName,mFontSize,mFontWidth,mFontHeight,mMaxTop,mNumTextures,[])
    {

    }
    public FontData(): this(0,0,string.Empty,0,0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + ((uint)Glyphs.Count * GlyphData.BYTES);
    }
}
public static class FontDataExt
{
    public static FontData ReadZEngineUIFontData(this Reader reader)
    {
        Header data = reader.ReadZEngineUIHeader();

        uint mID = reader.ReadUInt();
        string mName = reader.ReadASCII(FontData.NAME_SIZE).RemoveInvalids();
        uint mFontSize = reader.ReadUInt();
        uint mFontWidth = reader.ReadUInt();
        uint mFontHeight = reader.ReadUInt();

        uint mMaxTop = reader.ReadUInt();
        uint mNumTextures = reader.ReadUInt();
        uint mNumGlyphs = reader.ReadUInt();

        List<GlyphData> glyphs = [];
        for(ulong i = 0;i < mNumGlyphs;i++)
            glyphs.Add(reader.ReadZEngineUIGlyphData());
        return new(data.DataSize,mID,mName,mFontSize,mFontWidth,mFontHeight,mMaxTop,mNumTextures,glyphs);
    }
    public static void Write(this Writer writer,FontData fontData)
    {
        HeaderExt.Write(writer,fontData);

        writer.Write(fontData.ID);
        writer.Write(fontData.Name.ToFixedStringBytes(FontData.NAME_SIZE));
        writer.Write(fontData.FontSize);
        writer.Write(fontData.FontWidth);
        writer.Write(fontData.FontHeight);

        writer.Write(fontData.MaxTop);
        writer.Write(fontData.NumTextures);
        writer.Write((uint)fontData.Glyphs.Count);

        foreach(GlyphData glyph in fontData.Glyphs)
            writer.Write(glyph);
    }
}