using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Utils;

namespace ThemModdingHerds.OtterUI.Base;
public class Header(string mFourCC,uint mDataSize)
{
    public const int FOURCC_LENGTH = 4;
    public const string EMPTY_FOURCC = "\0\0\0\0";
    [JsonPropertyName("mFourCC")]
    public string FourCC {get;} = mFourCC[..FOURCC_LENGTH];
    [JsonPropertyName("mDataSize")]
    public uint DataSize {get;set;} = mDataSize;
    public const int BYTES = FOURCC_LENGTH + 4;
    public Header(): this(EMPTY_FOURCC,0)
    {

    }
}
public static class HeaderExt
{
    public static Header ReadOtterUIHeader(this Reader reader)
    {
        string mFourCC = StringExt.Reverse(reader.ReadASCII(Header.FOURCC_LENGTH));
        uint mDataSize = reader.ReadUInt();
        return new(mFourCC,mDataSize);
    }
    public static Header PeekOtterUIHeader(this Reader reader)
    {
        Header data = ReadOtterUIHeader(reader);
        reader.Offset -= Header.BYTES;
        return data;
    }
    public static void Write(this Writer writer,Header header)
    {
        writer.WriteASCII(StringExt.Reverse(header.FourCC[..Header.FOURCC_LENGTH]));
        writer.Write(header.DataSize);
    }
}