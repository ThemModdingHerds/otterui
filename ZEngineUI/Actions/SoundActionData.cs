using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Actions;
public class SoundActionData(uint mDataSize,uint mSoundID,float mVolume) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "SNDA";
    [JsonPropertyName("mSoundID")]
    public uint SoundID {get;set;} = mSoundID;
    [JsonPropertyName("mVolume")]
    public float Volume {get;set;} = mVolume;
    public new const int BYTES = Header.BYTES + 8;
    public SoundActionData(Header data,uint mSoundID,float mVolume): this(data.DataSize,mSoundID,mVolume)
    {

    }
    public SoundActionData(): this(BYTES,0,0)
    {

    }
}
public static class SoundActionDataExt
{
    public static SoundActionData ReadZEngineUISoundActionData(this Reader reader)
    {
        Header data = HeaderExt.ReadZEngineUIHeader(reader);
        uint mSoundID = reader.ReadUInt();
        float mVolume = reader.ReadFloat();
        return new(data,mSoundID,mVolume);
    }
    public static void Write(this Writer writer,SoundActionData soundActionData)
    {
        HeaderExt.Write(writer,soundActionData);
        writer.Write(soundActionData.SoundID);
        writer.Write(soundActionData.Volume);
    }
}