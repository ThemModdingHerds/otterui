using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI;
public class MessageData(uint mID,string mText)
{
    [JsonPropertyName("mID")]
    public uint ID {get;set;} = mID;
    public const int TEXT_SIZE = 64;
    [JsonPropertyName("mText")]
    public string Text {get;set;} = mText.MaybeSubstring(TEXT_SIZE);
    public const int BYTES = 4 + TEXT_SIZE;
    public MessageData(): this(0,string.Empty)
    {

    }
    public override string ToString()
    {
        return Text;
    }
}
public static class MessageDataExt
{
    public static MessageData ReadZEngineUIMessageData(this Reader reader)
    {
        uint mId = reader.ReadUInt();
        string mText = reader.ReadASCII(MessageData.TEXT_SIZE).RemoveInvalids();
        return new(mId,mText);
    }
    public static void Write(this Writer writer,MessageData messageData)
    {
        writer.Write(messageData.ID);
        writer.Write(messageData.Text.ToFixedStringBytes(MessageData.TEXT_SIZE));
    }
}