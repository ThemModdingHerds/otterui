using System.Numerics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Data;
using ThemModdingHerds.OtterUI.Utils;

namespace ThemModdingHerds.OtterUI.Base;
public class ControlData(string mFourCC,uint mDataSize,uint mID,string mName,Vector2 mPosition,Vector2 mCenter,Vector2 mSize,float mRotation,uint mAnchorFlags,int mMaskID) : Header(mFourCC,mDataSize)
{
    [JsonPropertyName("mID")]
    public uint ID {get;set;} = mID;
    public const int NAME_LENGTH = 64;
    [JsonPropertyName("mName")]
    public string Name {get;set;} = mName.MaybeSubstring(NAME_LENGTH);
    [JsonPropertyName("mPosition")]
    public Vector2 Position {get;set;} = mPosition;
    [JsonPropertyName("mCenter")]
    public Vector2 Center {get;set;} = mCenter;
    [JsonPropertyName("mSize")]
    public Vector2 Size {get;set;} = mSize;
    [JsonPropertyName("mRotation")]
    public float Rotation {get;set;} = mRotation;
    [JsonPropertyName("mAnchorFlags")]
    public uint AnchorFlags {get;set;} = mAnchorFlags;
    [JsonPropertyName("mMaskID")]
    public int MaskID {get;set;} = mMaskID;
    public new const int BYTES = Header.BYTES + 104;
    public ControlData(Header data,uint mID,string mName,Vector2 mPosition,Vector2 mCenter,Vector2 mSize,float mRotation,uint mAnchorFlags,int mMaskID): this(data.FourCC,data.DataSize,mID,mName,mPosition,mCenter,mSize,mRotation,mAnchorFlags,mMaskID)
    {

    }
    public ControlData(string mFourCC,uint mDataSize,ControlData controlData): this(mFourCC,mDataSize,controlData.ID,controlData.Name,controlData.Position,controlData.Center,controlData.Size,controlData.Rotation,controlData.AnchorFlags,controlData.MaskID)
    {

    }
    public ControlData(): this(EMPTY_FOURCC,0,new())
    {

    }
    public static ControlData Read(Reader reader)
    {
        Header data = reader.PeekOtterUIHeader();
        return data.FourCC switch
        {
            SpriteData.FOURCC => reader.ReadOtterUISpriteData(),
            LabelData.FOURCC => reader.ReadOtterUILabelData(),
            ButtonData.FOURCC => reader.ReadOtterUIButtonData(),
            GroupData.FOURCC => reader.ReadOtterUIGroupData(),
            TableData.FOURCC => reader.ReadOtterUITableData(),
            ToggleData.FOURCC => reader.ReadOtterUIToggleData(),
            SliderData.FOURCC => reader.ReadOtterUISliderData(),
            MaskData.FOURCC => reader.ReadOtterUIMaskData(),
            ViewData.FOURCC => reader.ReadOtterUIViewData(),
            _ => throw new Exception($"reading unknown ControlData '{data.FourCC}'")
        };
    }
    public static void Write(Writer writer,ControlData controlData)
    {
        if(controlData is SpriteData spriteData)
        {
            SpriteDataExt.Write(writer,spriteData);
            return;
        }
        if(controlData is LabelData labelData)
        {
            LabelDataExt.Write(writer,labelData);
            return;
        }
        if(controlData is ButtonData buttonData)
        {
            ButtonDataExt.Write(writer,buttonData);
            return;
        }
        if(controlData is GroupData groupData)
        {
            GroupDataExt.Write(writer,groupData);
            return;
        }
        if(controlData is ViewData viewData)
        {
            ViewDataExt.Write(writer,viewData);
            return;
        }
        if(controlData is MaskData maskData)
        {
            MaskDataExt.Write(writer,maskData);
            return;
        }
        if(controlData is SliderData sliderData)
        {
            SliderDataExt.Write(writer,sliderData);
            return;
        }
        if(controlData is ToggleData toggleData)
        {
            ToggleDataExt.Write(writer,toggleData);
            return;
        }
        throw new Exception($"writing unknown ControlData '{controlData.FourCC}'");
    }
}
public static class ControlDataExt
{
    public static ControlData ReadOtterUIControlData(this Reader reader)
    {
        Header data = HeaderExt.ReadOtterUIHeader(reader);
        uint mID = reader.ReadUInt();
        string mName = reader.ReadASCII(ControlData.NAME_LENGTH).RemoveInvalids();
        Vector2 mPosition = reader.ReadVector2();
        Vector2 mCenter = reader.ReadVector2();
        Vector2 mSize = reader.ReadVector2();
        float mRotation = reader.ReadFloat();
        uint mAnchorFlags = reader.ReadUInt();
        int mMaskID = reader.ReadInt();
        return new(data,mID,mName,mPosition,mCenter,mSize,mRotation,mAnchorFlags,mMaskID);
    }
    public static void Write(this Writer writer,ControlData controlData)
    {
        HeaderExt.Write(writer,controlData);
        writer.Write(controlData.ID);
        writer.WriteASCII(controlData.Name[..ControlData.NAME_LENGTH]);
        writer.WriteVector2(controlData.Position);
        writer.WriteVector2(controlData.Center);
        writer.WriteVector2(controlData.Size);
        writer.Write(controlData.Rotation);
        writer.Write(controlData.AnchorFlags);
        writer.Write(controlData.MaskID);
    }
}