using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Base;
public class Header(string mFourCC,uint mDataSize)
{
    public const int FOURCC_LENGTH = 4;
    public const string EMPTY_FOURCC = "\0\0\0\0";
    [JsonPropertyName("mFourCC")]
    public string FourCC {get;} = mFourCC[..FOURCC_LENGTH];
    [JsonPropertyName("mDataSize")]
    public uint DataSize {get;set;} = mDataSize;
    public const int BYTES = FOURCC_LENGTH + 4;
    public Header(uint mDataSize): this(EMPTY_FOURCC,mDataSize)
    {

    }
    public Header(): this(EMPTY_FOURCC,BYTES)
    {

    }
    public virtual uint GetDataSize()
    {
        return BYTES;
    }
    public void RecalculateDataSize()
    {
        DataSize = GetDataSize();
    }
    public override string ToString()
    {
        return $"{FourCC}";
    }
}
public static class HeaderExt
{
    public static Header ReadZEngineUIHeader(this Reader reader)
    {
        string mFourCC = StringExt.Reverse(reader.ReadASCII(Header.FOURCC_LENGTH));
        uint mDataSize = reader.ReadUInt();
        return new(mFourCC,mDataSize);
    }
    public static Header PeekZEngineUIHeader(this Reader reader)
    {
        Header data = ReadZEngineUIHeader(reader);
        reader.Offset -= Header.BYTES;
        return data;
    }
    public static void Write(this Writer writer,Header header)
    {
        writer.WriteASCII(StringExt.Reverse(header.FourCC.MaybeSubstring(Header.FOURCC_LENGTH)));
        header.RecalculateDataSize();
        writer.Write(header.DataSize);
    }
}