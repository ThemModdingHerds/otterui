using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;
using ThemModdingHerds.OtterUI.Data;
using ThemModdingHerds.OtterUI.Fonts;
using ThemModdingHerds.OtterUI.Sounds;
using ThemModdingHerds.OtterUI.Textures;

namespace ThemModdingHerds.OtterUI;

public class Scene(uint mDataSize,uint mID,uint mFontsOffset,IEnumerable<FontData> mFonts,uint mTexturesOffset,IEnumerable<TextureData> mTextures,uint mSoundsOffset,IEnumerable<SoundData> mSounds,uint mViewsOffset,IEnumerable<ViewData> mViews,uint mMessageOffset,IEnumerable<MessageData> mMessages) : Header(FOURCC,mDataSize)
{
    public const string FOURCC = "CSGG";
    public const float VERSION = 0.95f;
    [JsonPropertyName("version")]
    public float Version {get;set;}
    [JsonPropertyName("mID")]
    public uint ID {get;set;} = mID;
    public uint FontsOffset {get;set;} = mFontsOffset;
    [JsonPropertyName("mFonts")]
    public List<FontData> Fonts {get;set;} = [..mFonts];
    public uint TexturesOffset {get;set;} = mTexturesOffset;
    [JsonPropertyName("mTextures")]
    public List<TextureData> Textures {get;set;} = [..mTextures];
    public uint SoundsOffset {get;set;} = mSoundsOffset;
    [JsonPropertyName("mSounds")]
    public List<SoundData> Sounds {get;set;} = [..mSounds];
    public uint ViewsOffset {get;set;} = mViewsOffset;
    [JsonPropertyName("mViews")]
    public List<ViewData> Views {get;set;} = [..mViews];
    public uint MessagesOffset {get;set;} = mMessageOffset;
    [JsonPropertyName("mMessages")]
    public List<MessageData> Messages {get;set;} = [..mMessages];
    public new const int BYTES = Header.BYTES + 48;
    public Scene(uint mDataSize,uint mID,uint mFontsOffset,uint mTexturesOffset,uint mSoundsOffset,uint mViewsOffset,uint mMessageOffset): this(mDataSize,mID,mFontsOffset,[],mTexturesOffset,[],mSoundsOffset,[],mViewsOffset,[],mMessageOffset,[])
    {

    }
    public Scene(Header data,uint mID,uint mFontsOffset,uint mTexturesOffset,uint mSoundsOffset,uint mViewsOffset,uint mMessageOffset): this(data.DataSize,mID,mFontsOffset,[],mTexturesOffset,[],mSoundsOffset,[],mViewsOffset,[],mMessageOffset,[])
    {
        
    }
    public Scene(): this(0,0,0,0,0,0,0)
    {

    }
}
public static class SceneExt
{
    public static Scene ReadOtterUIScene(this Reader reader)
    {
        Header data = reader.ReadOtterUIHeader();
        float mVersion = reader.ReadFloat();
        uint mID = reader.ReadUInt();

        uint mNumFonts = reader.ReadUInt();
        uint mNumtextures = reader.ReadUInt();
        uint mNumSounds = reader.ReadUInt();
        uint mNumViews = reader.ReadUInt();
        uint mNumMessages = reader.ReadUInt();

        uint mFontsOffset = reader.ReadUInt();
        uint mTexturesOffset = reader.ReadUInt();
        uint mSoundsOffset = reader.ReadUInt();
        uint mViewsOffset = reader.ReadUInt();
        uint mMessageOffset = reader.ReadUInt();

        long _offset = reader.Offset;
        reader.Offset = _offset + mFontsOffset;
        List<FontData> mFonts = [];
        for(uint i = 0;i < mNumFonts;i++)
            mFonts.Add(reader.ReadOtterUIFontData());
        reader.Offset = _offset + mTexturesOffset;
        List<TextureData> mTextures = [];
        for(uint i = 0;i < mNumtextures;i++)
            mTextures.Add(reader.ReadOtterUITextureData());
        reader.Offset = _offset + mSoundsOffset;
        List<SoundData> mSounds = [];
        for(uint i = 0;i < mNumSounds;i++)
            mSounds.Add(reader.ReadOtterUISoundData());
        reader.Offset = _offset + mViewsOffset;
        List<ViewData> mViews = [];
        for(uint i = 0;i < mNumViews;i++)
            mViews.Add(reader.ReadOtterUIViewData());
        reader.Offset = _offset + mMessageOffset;
        List<MessageData> mMessages = [];
        for(uint i = 0;i < mNumMessages;i++)
            mMessages.Add(reader.ReadOtterUIMessageData());
        return new(data.DataSize,mID,mFontsOffset,mFonts,mTexturesOffset,mTextures,mSoundsOffset,mSounds,mViewsOffset,mViews,mMessageOffset,mMessages);
    }
    public static void Write(this Writer writer,Scene scene)
    {
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
        foreach(FontData fontData in scene.Fonts)
            writer.Write(fontData);
        writer.Offset = _offset + scene.TexturesOffset;
        foreach(TextureData textureData in scene.Textures)
            writer.Write(textureData);
        writer.Offset = _offset + scene.SoundsOffset;
        foreach(SoundData soundData in scene.Sounds)
            writer.Write(soundData);
        writer.Offset = _offset + scene.MessagesOffset;
        foreach(MessageData messageData in scene.Messages)
            writer.Write(messageData);
    }
}