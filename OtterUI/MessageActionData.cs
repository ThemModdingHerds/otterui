using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class MessageActionData(uint mDataSize,uint mMessageID) : Data(FOURCC,mDataSize)
{
    public const string FOURCC = "MSGA";
    [JsonPropertyName("mMessageID")]
    public uint MessageID {get;set;} = mMessageID;
    public MessageActionData(Data data,uint mMessageID): this(data.DataSize,mMessageID)
    {

    }
}
public static class MessageActionDataExt
{
    public static MessageActionData ReadOtterUIMessageActionData(this Reader reader)
    {
        Data data = DataExt.ReadOtterUIData(reader);
        uint mMessageID = reader.ReadUInt();
        return new(data,mMessageID);
    }
    public static void Write(this Writer writer,MessageActionData messageActionData)
    {
        DataExt.Write(writer,messageActionData);
        writer.Write(messageActionData.MessageID);
    }
}