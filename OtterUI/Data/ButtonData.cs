using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;
using ThemModdingHerds.OtterUI.Utils;

namespace ThemModdingHerds.OtterUI.Data;
public class ButtonData(uint mDataSize,ControlData controlData,uint mDefaultTextureID,uint mDownTextureID,uint mFontID,uint mDefaultColor,uint mDownColor,Vector2 mScale,uint mHAlign,uint mVAlign,string mText,IEnumerable<Header> mActions) : ControlData(FOURCC,mDataSize,controlData)
{
    public const string FOURCC = "GBTT";
    [JsonPropertyName("mDefaultTextureID")]
    public uint DefaultTextureID {get;set;} = mDefaultTextureID;
    [JsonPropertyName("mDownTextureID")]
    public uint DownTextureID {get;set;} = mDownTextureID;
    [JsonPropertyName("mFontID")]
    public uint FontID {get;set;} = mFontID;
    [JsonPropertyName("mDefaultColor")]
    public uint DefaultColor {get;set;} = mDefaultColor;
    [JsonPropertyName("mDownColor")]
    public uint DownColor {get;set;} = mDownColor;
    [JsonPropertyName("mScale")]
    public Vector2 Scale {get;set;} = mScale;
    [JsonPropertyName("mHAlign")]
    public uint HorizontalAlign {get;set;} = mHAlign;
    [JsonPropertyName("mVAlign")]
    public uint VerticalAlign {get;set;} = mVAlign;
    [JsonPropertyName("mActions")]
    public List<Header> Actions {get;set;} = [..mActions];
    [JsonPropertyName("mText")]
    public string Text {get;set;} = mText;
    public new const int BYTES = ControlData.BYTES + 44;
    public ButtonData(uint mDataSize,ControlData controlData,uint mDefaultTextureID,uint mDownTextureID,uint mFontID,uint mDefaultColor,uint mDownColor,Vector2 mScale,uint mHAlign,uint mVAlign,string mText): this(mDataSize,controlData,mDefaultTextureID,mDownTextureID,mFontID,mDefaultColor,mDownColor,mScale,mHAlign,mVAlign,mText,[])
    {

    }
    public ButtonData(): this(0,new(),0,0,0,0,0,Vector2.Zero,0,0,string.Empty)
    {

    }
}
public static class ButtonDataExt
{
    public static ButtonData ReadOtterUIButtonData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        uint mDefaultTextureID = reader.ReadUInt();
        uint mDownTextureID = reader.ReadUInt();
        uint mFontID = reader.ReadUInt();
        uint mDefaultColor = reader.ReadUInt();
        uint mDownColor = reader.ReadUInt();
        Vector2 mScale = reader.ReadVector2();
        uint mHAlign = reader.ReadUInt();
        uint mVAlign = reader.ReadUInt();
        uint mNumOnClickActions = reader.ReadUInt();
        uint mTextBufferSize = reader.ReadUInt();
        string mText = mTextBufferSize == 0 ? string.Empty : reader.ReadASCII(mTextBufferSize).RemoveInvalids();
        List<Header> mActions = [];
        for(uint i = 0;i < mNumOnClickActions;i++)
            mActions.Add(ActionData.Read(reader));
        return new(controlData.DataSize,controlData,mDefaultTextureID,mDownTextureID,mFontID,mDefaultColor,mDownColor,mScale,mHAlign,mVAlign,mText,mActions);
    }
    public static void Write(this Writer writer,ButtonData buttonData)
    {
        ControlDataExt.Write(writer,buttonData);
        writer.Write(buttonData.DefaultTextureID);
        writer.Write(buttonData.DownTextureID);
        writer.Write(buttonData.FontID);
        writer.Write(buttonData.DefaultColor);
        writer.Write(buttonData.DownColor);
        writer.WriteVector2(buttonData.Scale);
        writer.Write(buttonData.HorizontalAlign);
        writer.Write(buttonData.VerticalAlign);
        writer.Write((uint)buttonData.Actions.Count);
        writer.Write((uint)buttonData.Text.Length);
        if(buttonData.Text.Length != 0)
            writer.WriteASCII(buttonData.Text);
        if(buttonData.Actions.Count != 0)
            foreach(Header data in buttonData.Actions)
                ActionData.Write(writer,data);
    }
}