using System.Numerics;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Data;
using ThemModdingHerds.ZEngineUI.Layout;

namespace ThemModdingHerds.ZEngineUI.Base;
[XmlInclude(typeof(ARLTLayout))]
[XmlInclude(typeof(ButtonLayout))]
[XmlInclude(typeof(GroupLayout))]
[XmlInclude(typeof(LabelLayout))]
[XmlInclude(typeof(MaskLayout))]
[XmlInclude(typeof(SliderLayout))]
[XmlInclude(typeof(SPPLLayout))]
[XmlInclude(typeof(SpriteLayout))]
[XmlInclude(typeof(TableLayout))]
[XmlInclude(typeof(ToggleLayout))]
[XmlInclude(typeof(UnknownLayout))]
public class ControlLayout(string mFourCC,uint mDataSize,float mCenterX,float mCenterY,float mPositionX,float mPositionY,float mSizeX,float mSizeY,float mRotation) : Header(mFourCC,mDataSize)
{
    public const string FOURCC = "CLLT";
    [JsonPropertyName("mCenterX")]
    public float CenterX {get;set;} = mCenterX;
    [JsonPropertyName("mCenterY")]
    public float CenterY {get;set;} = mCenterY;
    [JsonPropertyName("mPositionX")]
    public float PositionX {get;set;} = mPositionX;
    [JsonPropertyName("mPositionY")]
    public float PositionY {get;set;} = mPositionY;
    [JsonPropertyName("mSizeX")]
    public float SizeX {get;set;} = mSizeX;
    [JsonPropertyName("mSizeY")]
    public float SizeY {get;set;} = mSizeY;
    [JsonPropertyName("mRotation")]
    public float Rotation {get;set;} = mRotation;
    public new const int BYTES = Header.BYTES + 28;
    public ControlLayout(Header data,float mCenterX,float mCenterY,float mX,float mY,float mWidth,float mHeight,float mRotation): this(data.FourCC,data.DataSize,mCenterX,mCenterY,mX,mY,mWidth,mHeight,mRotation)
    {

    }
    public ControlLayout(float mCenterX,float mCenterY,float mX,float mY,float mWidth,float mHeight,float mRotation): this(FOURCC,BYTES,mCenterX,mCenterY,mX,mY,mWidth,mHeight,mRotation)
    {

    }
    public ControlLayout(ControlLayout controlLayout): this(controlLayout.FourCC,controlLayout.DataSize,controlLayout.CenterX,controlLayout.CenterY,controlLayout.PositionX,controlLayout.PositionY,controlLayout.SizeX,controlLayout.SizeY,controlLayout.Rotation)
    {

    }
    public ControlLayout(string mFourCC,ControlLayout controlLayout): this(mFourCC,controlLayout.DataSize,controlLayout.CenterX,controlLayout.CenterY,controlLayout.PositionX,controlLayout.PositionY,controlLayout.SizeX,controlLayout.SizeY,controlLayout.Rotation)
    {

    }
    public ControlLayout(): this(new Header(BYTES),0,0,0,0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
    public static ControlLayout Read(Reader reader)
    {
        Header data = reader.PeekZEngineUIHeader();
        if(data.FourCC == MaskLayout.FOURCC || data.FourCC == SpriteLayout.FOURCC)
        {
            // why is this the case? stupid ZEngineUI...
            // but SpriteLayout is bigger than MaskLayout (because of the mColor field)
            return data.DataSize switch
            {
                MaskLayout.BYTES => reader.ReadZEngineUIMaskLayout(),
                SpriteLayout.BYTES => reader.ReadZEngineUISpriteLayout(),
                _ => reader.ReadZEngineUIUnknownLayout(),
            };
        }
        return data.FourCC switch
        {
            LabelLayout.FOURCC => reader.ReadZEngineUILabelLayout(),
            ButtonLayout.FOURCC => reader.ReadZEngineUIControlLayout(),
            GroupLayout.FOURCC => reader.ReadZEngineUIGroupLayout(),
            TableLayout.FOURCC => reader.ReadZEngineUIControlLayout(),
            ToggleLayout.FOURCC => reader.ReadZEngineUIToggleLayout(),
            SliderLayout.FOURCC => reader.ReadZEngineUISliderLayout(),
            ARLTLayout.FOURCC => reader.ReadZEngineUIARLTLayout(),
            SPPLLayout.FOURCC => reader.ReadZEngineUISPPLLayout(),
            FOURCC => reader.ReadZEngineUIControlLayout(),
            _ => reader.ReadZEngineUIUnknownLayout()
        };
    }
    public static void Write(Writer writer,ControlLayout controlLayout)
    {
        if(controlLayout.FourCC == MaskLayout.FOURCC)
        {
            MaskLayoutExt.Write(writer,(MaskLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == SpriteLayout.FOURCC)
        {
            SpriteLayoutExt.Write(writer,(SpriteLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == LabelLayout.FOURCC)
        {
            LabelLayoutExt.Write(writer,(LabelLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == ButtonLayout.FOURCC)
        {
            writer.Write(controlLayout);
            return;
        }
        if(controlLayout.FourCC == GroupLayout.FOURCC)
        {
            GroupLayoutExt.Write(writer,(GroupLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == TableLayout.FOURCC)
        {
            writer.Write(controlLayout);
            return;
        }
        if(controlLayout.FourCC == ToggleLayout.FOURCC)
        {
            ToggleLayoutExt.Write(writer,(ToggleLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == SliderLayout.FOURCC)
        {
            SliderLayoutExt.Write(writer,(SliderLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == ARLTLayout.FOURCC)
        {
            ARLTLayoutExt.Write(writer,(ARLTLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == SPPLLayout.FOURCC)
        {
            SPPLLayoutExt.Write(writer,(SPPLLayout)controlLayout);
            return;
        }
        if(controlLayout.FourCC == FOURCC)
        {
            writer.Write(controlLayout);
            return;
        }
        UnknownLayoutExt.Write(writer,(UnknownLayout)controlLayout);
    }
}
public static class ControlLayoutExt
{
    public static ControlLayout ReadZEngineUIControlLayout(this Reader reader)
    {
        Header data = HeaderExt.ReadZEngineUIHeader(reader);

        float mCenterX = reader.ReadFloat();
        float mCenterY = reader.ReadFloat();

        float mX = reader.ReadFloat();
        float mY = reader.ReadFloat();

        float mWidth = reader.ReadFloat();
        float mHeight = reader.ReadFloat();

        float mRotation = reader.ReadFloat();
        return new(data,mCenterX,mCenterY,mX,mY,mWidth,mHeight,mRotation);
    }
    public static void Write(this Writer writer,ControlLayout controlLayout)
    {
        HeaderExt.Write(writer,controlLayout);

        writer.Write(controlLayout.CenterX);
        writer.Write(controlLayout.CenterY);

        writer.Write(controlLayout.PositionX);
        writer.Write(controlLayout.PositionY);

        writer.Write(controlLayout.SizeX);
        writer.Write(controlLayout.SizeY);

        writer.Write(controlLayout.Rotation);
    }
}