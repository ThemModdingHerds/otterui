using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Actions;
public class MessageActionData(uint mDataSize,uint mMessageID) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "MSGA";
    [JsonPropertyName("mMessageID")]
    public uint MessageID {get;set;} = mMessageID;
    public new const int BYTES = Header.BYTES + 4;
    public MessageActionData(Header data,uint mMessageID): this(data.DataSize,mMessageID)
    {

    }
    public MessageActionData(): this(0,0)
    {

    }
}
public static class MessageActionDataExt
{
    public static MessageActionData ReadOtterUIMessageActionData(this Reader reader)
    {
        Header data = HeaderExt.ReadOtterUIHeader(reader);
        uint mMessageID = reader.ReadUInt();
        return new(data,mMessageID);
    }
    public static void Write(this Writer writer,MessageActionData messageActionData)
    {
        HeaderExt.Write(writer,messageActionData);
        writer.Write(messageActionData.MessageID);
    }
}