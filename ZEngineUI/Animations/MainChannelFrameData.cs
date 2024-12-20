using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Animations;
public class MainChannelFrameData(uint mDataSize,string mName,uint mFrame,IEnumerable<Header> mActions) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "MCFR";
    public const int NAME_SIZE = 64;
    [JsonPropertyName("mName")]
    public string Name {get;set;} = mName.MaybeSubstring(NAME_SIZE);
    [JsonPropertyName("mFrame")]
    public uint Frame {get;set;} = mFrame;
    [JsonPropertyName("mActions")]
    public List<Header> Actions {get;set;} = [..mActions];
    public new const int BYTES = Header.BYTES + NAME_SIZE + 8;
    public MainChannelFrameData(uint mDataSize,string mName,uint mFrame): this(mDataSize,mName,mFrame,[])
    {

    }
    public MainChannelFrameData(): this(BYTES,string.Empty,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + (from action in Actions select action.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
    public override string ToString()
    {
        return Name;
    }
}
public static class MainChannelFrameDataExt
{
    public static MainChannelFrameData ReadZEngineUIMainChannelFrameData(this Reader reader)
    {
        Header data = reader.ReadZEngineUIHeader();
        string mName = reader.ReadASCII(MainChannelFrameData.NAME_SIZE).RemoveInvalids();
        uint mFrame = reader.ReadUInt();
        uint mNumActions = reader.ReadUInt();
        List<Header> mActions = [];
        for(uint i = 0;i < mNumActions;i++)
            mActions.Add(ActionData.Read(reader));
        return new(data.DataSize,mName,mFrame,mActions);
    }
    public static void Write(this Writer writer,MainChannelFrameData mainChannelFrameData)
    {
        HeaderExt.Write(writer,mainChannelFrameData);
        writer.Write(mainChannelFrameData.Name.ToFixedStringBytes(MainChannelFrameData.NAME_SIZE));
        writer.Write(mainChannelFrameData.Frame);
        writer.Write((uint)mainChannelFrameData.Actions.Count);
        foreach(Header action in mainChannelFrameData.Actions)
            ActionData.Write(writer,action);
    }
}