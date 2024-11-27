using System.Numerics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Data;
using ThemModdingHerds.ZEngineUI.Utils;

namespace ThemModdingHerds.ZEngineUI.Base;
[XmlInclude(typeof(ARCCData))]
[XmlInclude(typeof(ButtonData))]
[XmlInclude(typeof(GroupData))]
[XmlInclude(typeof(GSPPData))]
[XmlInclude(typeof(LabelData))]
[XmlInclude(typeof(MaskData))]
[XmlInclude(typeof(SliderData))]
[XmlInclude(typeof(SpriteData))]
[XmlInclude(typeof(TableData))]
[XmlInclude(typeof(ToggleData))]
[XmlInclude(typeof(UnknownData))]
[XmlInclude(typeof(ViewData))]
public class ControlData(string mFourCC,uint mDataSize,uint mID,string mName,float mPositionX,float mPositionY,float mCenterX,float mCenterY,float mSizeX,float mSizeY,float mRotation,uint mAnchorFlags,int mMaskID) : Header(mFourCC,mDataSize)
{
    [JsonPropertyName("mID")]
    public uint ID {get;set;} = mID;
    public const int NAME_LENGTH = 64;
    [JsonPropertyName("mName")]
    public string Name {get;set;} = mName.MaybeSubstring(NAME_LENGTH);
    [JsonPropertyName("mPositionX")]
    public float PositionX {get;set;} = mPositionX;
    [JsonPropertyName("mPositionY")]
    public float PositionY {get;set;} = mPositionY;
    [JsonPropertyName("mCenterX")]
    public float CenterX {get;set;} = mCenterX;
    [JsonPropertyName("mCenterY")]
    public float CenterY {get;set;} = mCenterY;
    [JsonPropertyName("mSizeX")]
    public float SizeX {get;set;} = mSizeX;
    [JsonPropertyName("mSizeY")]
    public float SizeY {get;set;} = mSizeY;
    [JsonPropertyName("mRotation")]
    public float Rotation {get;set;} = mRotation;
    [JsonPropertyName("mAnchorFlags")]
    public uint AnchorFlags {get;set;} = mAnchorFlags;
    [JsonPropertyName("mMaskID")]
    public int MaskID {get;set;} = mMaskID;
    public new const int BYTES = Header.BYTES + 104;
    public ControlData(Header data,uint mID,string mName,float mPositionX,float mPositionY,float mCenterX,float mCenterY,float mSizeX,float mSizeY,float mRotation,uint mAnchorFlags,int mMaskID): this(data.FourCC,data.DataSize,mID,mName,mPositionX,mPositionY,mCenterX,mCenterY,mSizeX,mSizeY,mRotation,mAnchorFlags,mMaskID)
    {

    }
    public ControlData(ControlData controlData): this(controlData.FourCC,controlData.DataSize,controlData.ID,controlData.Name,controlData.PositionX,controlData.PositionY,controlData.CenterX,controlData.CenterY,controlData.SizeX,controlData.SizeY,controlData.Rotation,controlData.AnchorFlags,controlData.MaskID)
    {

    }
    public ControlData(string mFourCC,ControlData controlData): this(mFourCC,controlData.DataSize,controlData.ID,controlData.Name,controlData.PositionX,controlData.PositionY,controlData.CenterX,controlData.CenterY,controlData.SizeX,controlData.SizeY,controlData.Rotation,controlData.AnchorFlags,controlData.MaskID)
    {

    }
    public ControlData(): this(new Header(BYTES),0,string.Empty,0,0,0,0,0,0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
    public override string ToString()
    {
        return Name;
    }
    public static ControlData Read(Reader reader)
    {
        Header data = reader.PeekZEngineUIHeader();
        return data.FourCC switch
        {
            SpriteData.FOURCC => reader.ReadZEngineUISpriteData(),
            LabelData.FOURCC => reader.ReadZEngineUILabelData(),
            ButtonData.FOURCC => reader.ReadZEngineUIButtonData(),
            GroupData.FOURCC => reader.ReadZEngineUIGroupData(),
            TableData.FOURCC => reader.ReadZEngineUITableData(),
            ToggleData.FOURCC => reader.ReadZEngineUIToggleData(),
            SliderData.FOURCC => reader.ReadZEngineUISliderData(),
            MaskData.FOURCC => reader.ReadZEngineUIMaskData(),
            ViewData.FOURCC => reader.ReadZEngineUIViewData(),
            ARCCData.FOURCC => reader.ReadZEngineUIARCCData(),
            GSPPData.FOURCC => reader.ReadZEngineUIGSPPData(),
            _ => reader.ReadZEngineUIUnknownData()
        };
    }
    public static void Write(Writer writer,ControlData controlData)
    {
        if(controlData.FourCC == MaskData.FOURCC)
        {
            MaskDataExt.Write(writer,(MaskData)controlData);
            return;
        }
        if(controlData.FourCC == SpriteData.FOURCC)
        {
            SpriteDataExt.Write(writer,(SpriteData)controlData);
            return;
        }
        if(controlData.FourCC == LabelData.FOURCC)
        {
            LabelDataExt.Write(writer,(LabelData)controlData);
            return;
        }
        if(controlData.FourCC == ButtonData.FOURCC)
        {
            ButtonDataExt.Write(writer,(ButtonData)controlData);
            return;
        }
        if(controlData.FourCC == GroupData.FOURCC)
        {
            GroupDataExt.Write(writer,(GroupData)controlData);
            return;
        }
        if(controlData.FourCC == TableData.FOURCC)
        {
            TableDataExt.Write(writer,(TableData)controlData);
            return;
        }
        if(controlData.FourCC == ToggleData.FOURCC)
        {
            ToggleDataExt.Write(writer,(ToggleData)controlData);
            return;
        }
        if(controlData.FourCC == SliderData.FOURCC)
        {
            SliderDataExt.Write(writer,(SliderData)controlData);
            return;
        }
        if(controlData.FourCC == ARCCData.FOURCC)
        {
            ARCCDataExt.Write(writer,(ARCCData)controlData);
            return;
        }
        if(controlData.FourCC == GSPPData.FOURCC)
        {
            GSPPDataExt.Write(writer,(GSPPData)controlData);
            return;
        }
        UnknownDataExt.Write(writer,(UnknownData)controlData);
    }
}
public static class ControlDataExt
{
    public static ControlData ReadZEngineUIControlData(this Reader reader)
    {
        Header data = HeaderExt.ReadZEngineUIHeader(reader);
        uint mID = reader.ReadUInt();
        string mName = reader.ReadASCII(ControlData.NAME_LENGTH).RemoveInvalids();
        float mPositionX = reader.ReadFloat();
        float mPositionY = reader.ReadFloat();
        float mCenterX = reader.ReadFloat();
        float mCenterY = reader.ReadFloat();
        float mSizeX = reader.ReadFloat();
        float mSizeY = reader.ReadFloat();
        float mRotation = reader.ReadFloat();
        uint mAnchorFlags = reader.ReadUInt();
        int mMaskID = reader.ReadInt();
        return new(data,mID,mName,mPositionX,mPositionY,mCenterX,mCenterY,mSizeX,mSizeY,mRotation,mAnchorFlags,mMaskID);
    }
    public static void Write(this Writer writer,ControlData controlData)
    {
        HeaderExt.Write(writer,controlData);
        writer.Write(controlData.ID);
        writer.Write(controlData.Name.ToFixedStringBytes(ControlData.NAME_LENGTH));
        writer.Write(controlData.PositionX);
        writer.Write(controlData.PositionY);
        writer.Write(controlData.CenterX);
        writer.Write(controlData.CenterY);
        writer.Write(controlData.SizeX);
        writer.Write(controlData.SizeY);
        writer.Write(controlData.Rotation);
        writer.Write(controlData.AnchorFlags);
        writer.Write(controlData.MaskID);
    }
}