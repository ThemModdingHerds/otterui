using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class UnknownData(ControlData controlData,IEnumerable<byte> mUnknown) : ControlData(controlData)
{
    [JsonPropertyName("mUnknown")]
    public byte[] Unknown {get;set;} = mUnknown.ToArray();
    public override uint GetDataSize()
    {
        return BYTES + (uint)Unknown.Length;
    }
    public UnknownData(): this(new(),[])
    {

    }
}
public static class UnknownDataExt
{
    public static UnknownData ReadZEngineUIUnknownData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        Console.Error.WriteLine($"unknown ControlData '{controlData.FourCC}' Z{controlData.DataSize-ControlData.BYTES}@0x{reader.Offset-controlData.DataSize:X04}");
        long size = controlData.DataSize - ControlData.BYTES;
        List<byte> bytes = [];
        for(long i = 0;i < size;i++)
            bytes.Add(reader.ReadByte());
        return new(controlData,bytes);
    }
    public static void Write(this Writer writer,UnknownData unknownData)
    {
        Console.Error.WriteLine($"unknown ControlData '{unknownData.FourCC}' Z{unknownData.DataSize-ControlData.BYTES}@0x{writer.Offset-unknownData.DataSize:X04}");
        ControlDataExt.Write(writer,unknownData);
        writer.Write(unknownData.Unknown);
    }
}