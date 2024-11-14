using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;

public class Scene(uint mDataSize,uint mID,uint mFontsOffset,IEnumerable<FontData> fonts,uint mTexturesOffset,IEnumerable<TextureData> textures,uint mSoundsOffset,IEnumerable<SoundData> sounds,uint mViewsOffset,IEnumerable<ViewData> views,uint mMessageOffset,IEnumerable<MessageData> messages) : Data(FOURCC,mDataSize)
{
    public const string FOURCC = "CSGG";
    public const float VERSION = 0.95f;
    [JsonPropertyName("version")]
    public float Version {get;set;}
    [JsonPropertyName("mID")]
    public uint ID {get;set;} = mID;
    public uint FontsOffset {get;set;} = mFontsOffset;
    [JsonPropertyName("mBuffer")]
    public List<FontData> Fonts {get;set;} = [..fonts];
    public uint TexturesOffset {get;set;} = mTexturesOffset;
    [JsonPropertyName("mBuffer")]
    public List<TextureData> Textures {get;set;} = [..textures];
    public uint SoundsOffset {get;set;} = mSoundsOffset;
    [JsonPropertyName("mBuffer")]
    public List<SoundData> Sounds {get;set;} = [..sounds];
    public uint ViewsOffset {get;set;} = mViewsOffset;
    [JsonPropertyName("mBuffer")]
    public List<ViewData> Views {get;set;} = [..views];
    public uint MessagesOffset {get;set;} = mMessageOffset;
    [JsonPropertyName("mBuffer")]
    public List<MessageData> Messages {get;set;} = [..messages];
    public Scene(uint mDataSize,uint mID,uint mFontsOffset,uint mTexturesOffset,uint mSoundsOffset,uint mViewsOffset,uint mMessageOffset): this(mDataSize,mID,mFontsOffset,[],mTexturesOffset,[],mSoundsOffset,[],mViewsOffset,[],mMessageOffset,[])
    {

    }
    public Scene(Data data,uint mID,uint mFontsOffset,uint mTexturesOffset,uint mSoundsOffset,uint mViewsOffset,uint mMessageOffset): this(data.DataSize,mID,mFontsOffset,[],mTexturesOffset,[],mSoundsOffset,[],mViewsOffset,[],mMessageOffset,[])
    {
        
    }
}
public static class SceneExt
{
    public static Scene ReadOtterUIScene(this Reader reader)
    {

    }
    public static void Write(this Writer writer,Scene scene)
    {
        DataExt.Write(writer,scene);
    }
}