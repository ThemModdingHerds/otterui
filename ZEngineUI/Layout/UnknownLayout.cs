using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class UnknownLayout(ControlLayout controlLayout,IEnumerable<byte> mUnknown) : ControlLayout(controlLayout)
{
    [JsonPropertyName("mUnknown")]
    public byte[] Unknown {get;set;} = mUnknown.ToArray();
    public override uint GetDataSize()
    {
        return BYTES + (uint)Unknown.Length;
    }
    public UnknownLayout(): this(new(),[])
    {

    }
}
public static class UnknownLayoutExt
{
    public static UnknownLayout ReadZEngineUIUnknownLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadZEngineUIControlLayout();
        Console.Error.WriteLine($"unknown ControlLayout '{controlLayout.FourCC}' Z{controlLayout.DataSize-ControlLayout.BYTES}@0x{reader.Offset-controlLayout.DataSize:X04}");
        long size = controlLayout.DataSize - ControlLayout.BYTES;
        List<byte> bytes = [];
        for(long i = 0;i < size;i++)
            bytes.Add(reader.ReadByte());
        return new(controlLayout,bytes);
    }
    public static void Write(this Writer writer,UnknownLayout unknownLayout)
    {
        Console.Error.WriteLine($"unknown ControlLayout '{unknownLayout.FourCC}' Z{unknownLayout.DataSize-ControlLayout.BYTES}@0x{writer.Offset:X04}");
        ControlLayoutExt.Write(writer,unknownLayout);
        writer.Write(unknownLayout.Unknown);
    }
}