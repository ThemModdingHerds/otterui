using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class Data(string mFourCC,uint mDataSize)
{
    public const int FOURCC_LENGTH = 4;
    [JsonPropertyName("mFourCC")]
    public string FourCC {get;set;} = mFourCC[..FOURCC_LENGTH];
    [JsonPropertyName("mDataSize")]
    public uint DataSize {get;set;} = mDataSize;
    public const int SIZE = FOURCC_LENGTH + 4;
}
public static class DataExt
{
    public static Data ReadOtterUIData(this Reader reader)
    {
        string mFourCC = reader.ReadASCII(Data.FOURCC_LENGTH);
        uint mDataSize = reader.ReadUInt();
        return new(mFourCC,mDataSize);
    }
    public static Data PeekOtterUIData(this Reader reader)
    {
        Data data = ReadOtterUIData(reader);
        reader.Offset -= Data.SIZE;
        return data;
    }
    public static void Write(this Writer writer,Data data)
    {
        writer.WriteASCII(data.FourCC[..Data.FOURCC_LENGTH]);
        writer.Write(data.DataSize);
    }
}