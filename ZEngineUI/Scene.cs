using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Data;
using ThemModdingHerds.ZEngineUI.Fonts;
using ThemModdingHerds.ZEngineUI.Sounds;
using ThemModdingHerds.ZEngineUI.Textures;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI;

public class Scene(uint mDataSize,uint mID,uint mFontsOffset,IEnumerable</*FontData*/byte[]> mFonts,uint mTexturesOffset,IEnumerable<TextureData> mTextures,uint mSoundsOffset,IEnumerable<SoundData> mSounds,uint mViewsOffset,IEnumerable<ViewData> mViews,uint mMessageOffset,IEnumerable<MessageData> mMessages) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "GGSC";
    public const float VERSION = 0.95f;
    [JsonPropertyName("version")]
    [XmlElement("Version")]
    public float Version {get;set;} = VERSION;
    [JsonPropertyName("mID")]
    [XmlElement("ID")]
    public uint ID {get;set;} = mID;
    [JsonPropertyName("mFontsOffset")]
    [XmlIgnore]
    public uint FontsOffset {get;set;} = mFontsOffset;
    [JsonPropertyName("mFonts")]
    //public List<FontData> Fonts {get;set;} = [..mFonts];
    public List<byte[]> Fonts {get;set;} = [..mFonts];
    [JsonPropertyName("mTexturesOffset")]
    public uint TexturesOffset {get;set;} = mTexturesOffset;
    [JsonPropertyName("mTextures")]
    public List<TextureData> Textures {get;set;} = [..mTextures];
    [JsonPropertyName("mSoundsOffset")]
    public uint SoundsOffset {get;set;} = mSoundsOffset;
    [JsonPropertyName("mSounds")]
    public List<SoundData> Sounds {get;set;} = [..mSounds];
    [JsonPropertyName("mViewsOffset")]
    public uint ViewsOffset {get;set;} = mViewsOffset;
    [JsonPropertyName("mViews")]
    public List<ViewData> Views {get;set;} = [..mViews];
    [JsonPropertyName("mMessagesOffset")]
    public uint MessagesOffset {get;set;} = mMessageOffset;
    [JsonPropertyName("mMessages")]
    public List<MessageData> Messages {get;set;} = [..mMessages];
    public new const int BYTES = Header.BYTES + 48;
    public const uint END_MARKER = 0x12344321U;
    public Scene(uint mDataSize,uint mID,uint mFontsOffset,uint mTexturesOffset,uint mSoundsOffset,uint mViewsOffset,uint mMessageOffset): this(mDataSize,mID,mFontsOffset,[],mTexturesOffset,[],mSoundsOffset,[],mViewsOffset,[],mMessageOffset,[])
    {

    }
    public Scene(Header data,uint mID,uint mFontsOffset,uint mTexturesOffset,uint mSoundsOffset,uint mViewsOffset,uint mMessageOffset): this(data.DataSize,mID,mFontsOffset,[],mTexturesOffset,[],mSoundsOffset,[],mViewsOffset,[],mMessageOffset,[])
    {
        
    }
    public Scene(): this(0,0,0,0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + GetFontsSize() + GetTexturesSize() + GetSoundsSize() + GetViewsSize() + GetMessagesSize() + 4;
    }
    public override string ToString()
    {
        return $"Scene: {ID}";
    }
    private uint GetFontsSize()
    {
        //return (from font in Fonts select font.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
        return (from font in Fonts select (uint)font.Length).Aggregate((uint)0,(acc,x) => acc + x);
    }
    private uint GetTexturesSize()
    {
        return (uint)Textures.Count * TextureData.BYTES;
    }
    private uint GetSoundsSize()
    {
        return (uint)Sounds.Count * SoundData.BYTES;
    }
    private uint GetViewsSize()
    {
        return (from view in Views select view.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
    private uint GetMessagesSize()
    {
        return (uint)Messages.Count * MessageData.BYTES;
    }
    public void RecalculateOffsets()
    {
        FontsOffset = 0;
        TexturesOffset = FontsOffset + GetFontsSize();
        SoundsOffset = TexturesOffset + GetTexturesSize();
        ViewsOffset = SoundsOffset + GetSoundsSize();
        MessagesOffset = ViewsOffset + GetViewsSize();
    }
    public void Save(string path,bool overwrite = true)
    {
        if(File.Exists(path))
        {
            if(!overwrite) throw new Exception($"{path} already exists");
            File.Delete(path);
        }
        Writer writer = new(path);
        writer.Write(this);
        writer.Close();
    }
    public static Scene OpenRead(string path)
    {
        if(!File.Exists(path)) throw new FileNotFoundException($"couldn't find {path}",path);
        Reader reader = new(path);
        Scene scene = reader.ReadZEngineUIScene();
        reader.Close();
        return scene;
    }
    public static Scene OpenWrite(string path)
    {
        if(File.Exists(path)) throw new Exception($"{path} already exists");
        Scene scene = new();
        Writer writer = new(path);
        writer.Write(scene);
        writer.Close();
        return scene;
    }
    public static Scene Open(string path)
    {
        if(File.Exists(path))
            return OpenRead(path);
        return OpenWrite(path);
    }
}
public static class SceneExt
{
    public static Scene ReadZEngineUIScene(this Reader reader)
    {
        Header data = reader.ReadZEngineUIHeader();
        float mVersion = reader.ReadFloat();
        if(mVersion != Scene.VERSION)
            throw new DataMissMatchException(Scene.VERSION,mVersion);
        uint mID = reader.ReadUInt();

        uint mNumFonts = reader.ReadUInt();
        uint mNumTextures = reader.ReadUInt();
        uint mNumSounds = reader.ReadUInt();
        uint mNumViews = reader.ReadUInt();
        uint mNumMessages = reader.ReadUInt();

        uint mFontsOffset = reader.ReadUInt();
        uint mTexturesOffset = reader.ReadUInt();
        uint mSoundsOffset = reader.ReadUInt();
        uint mViewsOffset = reader.ReadUInt();
        uint mMessageOffset = reader.ReadUInt();

        long _offset = reader.Offset;
        
        //List<FontData> mFonts = [];
        List<byte[]> mFonts = [];
        List<TextureData> mTextures = [];
        List<SoundData> mSounds = [];
        List<ViewData> mViews = [];
        List<MessageData> mMessages = [];

        if(mNumFonts != 0)
        {
            reader.Offset = _offset + mFontsOffset;
            for(uint i = 0;i < mNumFonts;i++)
                //mFonts.Add(reader.ReadZEngineUIFontData());
            {
                Header header = reader.PeekZEngineUIHeader();
                List<byte> bytes = [];
                for(uint b = 0;b < header.DataSize;b++)
                    bytes.Add(reader.ReadByte());
                mFonts.Add([..bytes]);
            }
        }
        if(mNumTextures != 0)
        {
            reader.Offset = _offset + mTexturesOffset;
            for(uint i = 0;i < mNumTextures;i++)
                mTextures.Add(reader.ReadZEngineUITextureData());
        }
        if(mNumSounds != 0)
        {
            reader.Offset = _offset + mSoundsOffset;
            for(uint i = 0;i < mNumSounds;i++)
                mSounds.Add(reader.ReadZEngineUISoundData());
        }
        if(mNumViews != 0)
        {
            reader.Offset = _offset + mViewsOffset;
            for(uint i = 0;i < mNumViews;i++)
                mViews.Add(reader.ReadZEngineUIViewData());
        }
        if(mNumMessages != 0)
        {
            reader.Offset = _offset + mMessageOffset;
            for(uint i = 0;i < mNumMessages;i++)
            mMessages.Add(reader.ReadZEngineUIMessageData());
        }
        return new(data.DataSize,mID,mFontsOffset,mFonts,mTexturesOffset,mTextures,mSoundsOffset,mSounds,mViewsOffset,mViews,mMessageOffset,mMessages);
    }
    public static void Write(this Writer writer,Scene scene)
    {
        scene.RecalculateOffsets();
        HeaderExt.Write(writer,scene);
        writer.Write(scene.Version);
        writer.Write(scene.ID);

        writer.Write((uint)scene.Fonts.Count);
        writer.Write((uint)scene.Textures.Count);
        writer.Write((uint)scene.Sounds.Count);
        writer.Write((uint)scene.Views.Count);
        writer.Write((uint)scene.Messages.Count);

        writer.Write(scene.FontsOffset);
        writer.Write(scene.TexturesOffset);
        writer.Write(scene.SoundsOffset);
        writer.Write(scene.ViewsOffset);
        writer.Write(scene.MessagesOffset);

        long _offset = writer.Offset;
        writer.Offset = _offset + scene.FontsOffset;
        //foreach(FontData fontData in scene.Fonts)
        //    writer.Write(fontData);
        foreach(byte[] bytes in scene.Fonts)
            writer.Write(bytes);

        writer.Offset = _offset + scene.TexturesOffset;
        foreach(TextureData textureData in scene.Textures)
            writer.Write(textureData);

        writer.Offset = _offset + scene.SoundsOffset;
        foreach(SoundData soundData in scene.Sounds)
            writer.Write(soundData);

        writer.Offset = _offset + scene.ViewsOffset;
        foreach(ViewData viewData in scene.Views)
            writer.Write(viewData);

        writer.Offset = _offset + scene.MessagesOffset;
        foreach(MessageData messageData in scene.Messages)
            writer.Write(messageData);
            
        writer.Write(Scene.END_MARKER);
    }
}