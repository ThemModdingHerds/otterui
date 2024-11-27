using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Actions;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Base;
public static class ActionData
{
    private static Header ReadUnknown(Reader reader)
    {
        Header unknown = reader.ReadZEngineUIHeader();
        Console.Error.WriteLine($"unknown Header '{unknown.FourCC}' Z{unknown.DataSize-Header.BYTES}@0x{reader.Offset-unknown.DataSize:X04}");
        long size = unknown.DataSize - Header.BYTES;
        for(long i = 0;i < size;i++)
            reader.ReadByte();
        return unknown;
    }
    private static void WriteUnknown(Writer writer,Header unknown)
    {
        Console.Error.WriteLine($"unknown Header '{unknown.FourCC}' Z{unknown.DataSize-Header.BYTES}@0x{writer.Offset-unknown.DataSize:X04}");
        writer.Write(unknown);
        long size = unknown.DataSize - Header.BYTES;
        for(long i = 0;i < size;i++)
            writer.Write((byte)0);
    }
    public static Header Read(Reader reader)
    {
        Header data = reader.PeekZEngineUIHeader();
        return data.FourCC switch
        {
            MessageActionData.FOURCC => reader.ReadZEngineUIMessageActionData(),
            SoundActionData.FOURCC => reader.ReadZEngineUISoundActionData(),
            _ => ReadUnknown(reader)
        };
    }
    public static void Write(Writer writer,Header data)
    {
        if(data.FourCC == MessageActionData.FOURCC)
        {
            MessageActionDataExt.Write(writer,(MessageActionData)data);
            return;
        }
        if(data.FourCC == SoundActionData.FOURCC)
        {
            SoundActionDataExt.Write(writer,(SoundActionData)data);
            return;
        }
        WriteUnknown(writer,data);
    }
}