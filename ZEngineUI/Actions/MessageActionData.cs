using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Actions;
public class MessageActionData(uint mDataSize,uint mMessageID) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "MSGA";
    [JsonPropertyName("mMessageID")]
    public uint MessageID {get;set;} = mMessageID;
    public new const int BYTES = Header.BYTES + 4;
    public MessageActionData(Header data,uint mMessageID): this(data.DataSize,mMessageID)
    {

    }
    public MessageActionData(): this(BYTES,0)
    {

    }
}
public static class MessageActionDataExt
{
    public static MessageActionData ReadZEngineUIMessageActionData(this Reader reader)
    {
        Header data = HeaderExt.ReadZEngineUIHeader(reader);
        uint mMessageID = reader.ReadUInt();
        return new(data,mMessageID);
    }
    public static void Write(this Writer writer,MessageActionData messageActionData)
    {
        HeaderExt.Write(writer,messageActionData);
        writer.Write(messageActionData.MessageID);
    }
}