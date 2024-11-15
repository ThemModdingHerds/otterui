using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Actions;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Base;
public static class ActionData
{
    public static Header Read(Reader reader)
    {
        Header data = reader.PeekOtterUIHeader();
        return data.FourCC switch
        {
            MessageActionData.FOURCC => reader.ReadOtterUIMessageActionData(),
            SoundActionData.FOURCC => reader.ReadOtterUISoundActionData(),
            _ => throw new Exception($"reading unknown ActionData '{data.FourCC}'")
        };
    }
    public static void Write(Writer writer,Header data)
    {
        if(data is MessageActionData messageActionData)
        {
            writer.Write(messageActionData);
            return;
        }
        if(data is SoundActionData soundActionData)
        {
            writer.Write(soundActionData);
            return;
        }
        throw new Exception($"writing unknown ActionData '{data.FourCC}'");
    }
}