using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Sounds;
public class SoundData(uint mSoundID,string mSoundName,int mRefCount)
{
    [JsonPropertyName("mSoundID")]
    public uint SoundID {get;set;} = mSoundID;
    public const int SOUND_NAME_SIZE = 256;
    [JsonPropertyName("mSoundName")]
    public string SoundName {get;set;} = mSoundName.MaybeSubstring(SOUND_NAME_SIZE);
    [JsonPropertyName("mRefCount")]
    public int RefCount {get;set;} = mRefCount;
    public const int BYTES = SOUND_NAME_SIZE + 8;
    public SoundData(): this(0,string.Empty,0)
    {

    }
    public override string ToString()
    {
        return SoundName;
    }
}
public static class SoundDataExt
{
    public static SoundData ReadZEngineUISoundData(this Reader reader)
    {
        uint mSoundID = reader.ReadUInt();
        string mSoundName = reader.ReadASCII(SoundData.SOUND_NAME_SIZE).RemoveInvalids();
        int mRefCount = reader.ReadInt();
        return new(mSoundID,mSoundName,mRefCount);
    }
    public static void Write(this Writer writer,SoundData soundData)
    {
        writer.Write(soundData.SoundID);
        writer.Write(soundData.SoundName.ToFixedStringBytes(SoundData.SOUND_NAME_SIZE));
        writer.Write(soundData.RefCount);
    }
}