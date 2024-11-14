using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class SoundActionData(uint mDataSize,uint mSoundID,float mVolume) : Data(FOURCC,mDataSize)
{
    public const string FOURCC = "SNDA";
    [JsonPropertyName("mSoundID")]
    public uint SoundID {get;set;} = mSoundID;
    [JsonPropertyName("mVolume")]
    public float Volume {get;set;} = mVolume;
    public SoundActionData(Data data,uint mSoundID,float mVolume): this(data.DataSize,mSoundID,mVolume)
    {

    }
}
public static class SoundActionDataExt
{
    public static SoundActionData ReadOtterUISoundActionData(this Reader reader)
    {
        Data data = DataExt.ReadOtterUIData(reader);
        uint mSoundID = reader.ReadUInt();
        float mVolume = reader.ReadFloat();
        return new(data,mSoundID,mVolume);
    }
    public static void Write(this Writer writer,SoundActionData soundActionData)
    {
        DataExt.Write(writer,soundActionData);
        writer.Write(soundActionData.SoundID);
        writer.Write(soundActionData.Volume);
    }
}