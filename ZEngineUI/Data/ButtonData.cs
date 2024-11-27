using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Data;
public class ButtonData(ControlData controlData,uint mDefaultTextureID,uint mDownTextureID,uint mFontID,uint mDefaultColor,uint mDownColor,float mScaleX,float mScaleY,uint mHAlign,uint mVAlign,byte[] mText,IEnumerable<Header> mActions) : ControlData(FOURCC,controlData)
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
    [JsonPropertyName("mScaleX")]
    public float ScaleX {get;set;} = mScaleX;
    [JsonPropertyName("mScaleY")]
    public float ScaleY {get;set;} = mScaleY;
    [JsonPropertyName("mHAlign")]
    public uint HorizontalAlign {get;set;} = mHAlign;
    [JsonPropertyName("mVAlign")]
    public uint VerticalAlign {get;set;} = mVAlign;
    [JsonPropertyName("mActions")]
    public List<Header> Actions {get;set;} = [..mActions];
    [JsonPropertyName("mBuffer")]
    public byte[] Buffer {get;set;} = mText;
    [JsonIgnore]
    [XmlIgnore]
    public string Text {get => Encoding.ASCII.GetString(Buffer);set => Buffer = value.ToStringBytes();}
    public new const int BYTES = ControlData.BYTES + 44;
    public ButtonData(): this(new(),0,0,0,0,0,0,0,0,0,[],[])
    {

    }
    public override string ToString()
    {
        return $"{Name}: '{Text}'";
    }
    public override uint GetDataSize()
    {
        return BYTES + (uint)Buffer.Length + (from action in Actions select action.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
}
public static class ButtonDataExt
{
    public static ButtonData ReadZEngineUIButtonData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        uint mDefaultTextureID = reader.ReadUInt();
        uint mDownTextureID = reader.ReadUInt();
        uint mFontID = reader.ReadUInt();
        uint mDefaultColor = reader.ReadUInt();
        uint mDownColor = reader.ReadUInt();
        float mScaleX = reader.ReadFloat();
        float mScaleY = reader.ReadFloat();
        uint mHAlign = reader.ReadUInt();
        uint mVAlign = reader.ReadUInt();
        uint mNumOnClickActions = reader.ReadUInt();
        uint mTextBufferSize = reader.ReadUInt();
        byte[] mBuffer = mTextBufferSize == 0 ? [] : reader.ReadBytes((int)mTextBufferSize);
        List<Header> mActions = [];
        for(uint i = 0;i < mNumOnClickActions;i++)
            mActions.Add(ActionData.Read(reader));
        return new(controlData,mDefaultTextureID,mDownTextureID,mFontID,mDefaultColor,mDownColor,mScaleX,mScaleY,mHAlign,mVAlign,mBuffer,mActions);
    }
    public static void Write(this Writer writer,ButtonData buttonData)
    {
        ControlDataExt.Write(writer,buttonData);
        writer.Write(buttonData.DefaultTextureID);
        writer.Write(buttonData.DownTextureID);
        writer.Write(buttonData.FontID);
        writer.Write(buttonData.DefaultColor);
        writer.Write(buttonData.DownColor);
        writer.Write(buttonData.ScaleX);
        writer.Write(buttonData.ScaleY);
        writer.Write(buttonData.HorizontalAlign);
        writer.Write(buttonData.VerticalAlign);
        writer.Write((uint)buttonData.Actions.Count);
        writer.Write((uint)buttonData.Buffer.Length);
        if(buttonData.Buffer.Length != 0)
            writer.Write(buttonData.Buffer);
        foreach(Header data in buttonData.Actions)
            ActionData.Write(writer,data);
    }
}