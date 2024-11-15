using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Layout;

namespace ThemModdingHerds.OtterUI.Base;
public class ControlLayout(string mFourCC,uint mDataSize,Vector2 mCenter,Vector2 mPosition,Vector2 mSize,float mRotation) : Header(mFourCC,mDataSize)
{
    public const string FOURCC = "CLLT";
    [JsonPropertyName("mCenter")]
    public Vector2 Center {get;set;} = mCenter;
    [JsonPropertyName("mPosition")]
    public Vector2 Position {get;set;} = mPosition;
    [JsonPropertyName("mSize")]
    public Vector2 Size {get;set;} = mSize;
    [JsonPropertyName("mRotation")]
    public float Rotation {get;set;} = mRotation;
    public new const int BYTES = Header.BYTES + 28;
    public ControlLayout(uint mDataSize,Vector2 mCenter,Vector2 mPosition,Vector2 mSize,float mRotation): this(FOURCC,mDataSize,mCenter,mPosition,mSize,mRotation)
    {

    }
    public ControlLayout(string mFourCC,uint mDataSize,float mCenterX,float mCenterY,float mX,float mY,float mWidth,float mHeight,float mRotation): this(mFourCC,mDataSize,new(mCenterX,mCenterY),new(mX,mY),new(mWidth,mHeight),mRotation)
    {

    }
    public ControlLayout(Header data,float mCenterX,float mCenterY,float mX,float mY,float mWidth,float mHeight,float mRotation): this(data.FourCC,data.DataSize,new(mCenterX,mCenterY),new(mX,mY),new(mWidth,mHeight),mRotation)
    {

    }
    public ControlLayout(string mFourCC,uint mDataSize,ControlLayout controlLayout): this(mFourCC,mDataSize,controlLayout.Center,controlLayout.Position,controlLayout.Size,controlLayout.Rotation)
    {

    }
    public ControlLayout(): this(EMPTY_FOURCC,0,0,0,0,0,0,0,0)
    {

    }
    public static ControlLayout Read(Reader reader)
    {
        Header data = reader.PeekOtterUIHeader();
        if(data.FourCC == MaskLayout.FOURCC || data.FourCC == SpriteLayout.FOURCC)
        {
            // why is this the case? stupid OtterUI...
            // but SpriteLayout is bigger than MaskLayout (because of the mColor field)
            throw new Exception("cannot parse MaskLayout and SpriteLayout yet");
        }
        return data.FourCC switch
        {
            LabelLayout.FOURCC => reader.ReadOtterUILabelLayout(),
            ButtonLayout.FOURCC => reader.ReadOtterUIControlLayout(),
            GroupLayout.FOURCC => reader.ReadOtterUIGroupLayout(),
            TableLayout.FOURCC => reader.ReadOtterUIControlLayout(),
            ToggleLayout.FOURCC => reader.ReadOtterUIToggleLayout(),
            SliderLayout.FOURCC => reader.ReadOtterUISliderLayout(),
            MaskLayout.FOURCC => reader.ReadOtterUIMaskLayout(),
            _ => throw new Exception($"reading unknown ControlLayout '{data.FourCC}'"),
        };
    }
    public static void Write(Writer writer,ControlLayout controlLayout)
    {
        if(controlLayout is SpriteLayout spriteLayout)
        {
            SpriteLayoutExt.Write(writer,spriteLayout);
            return;
        }
        if(controlLayout is LabelLayout labelLayout)
        {
            LabelLayoutExt.Write(writer,labelLayout);
            return;
        }
        if(controlLayout is GroupLayout groupLayout)
        {
            GroupLayoutExt.Write(writer,groupLayout);
            return;
        }
        if(controlLayout is MaskLayout maskLayout)
        {
            MaskLayoutExt.Write(writer,maskLayout);
            return;
        }
        if(controlLayout is SliderLayout sliderLayout)
        {
            SliderLayoutExt.Write(writer,sliderLayout);
            return;
        }
        if(controlLayout is ToggleLayout toggleLayout)
        {
            ToggleLayoutExt.Write(writer,toggleLayout);
            return;
        }
        throw new Exception($"writing unknown ControlLayout '{controlLayout.FourCC}'");
    }
}
public static class ControlLayoutExt
{
    public static ControlLayout ReadOtterUIControlLayout(this Reader reader)
    {
        Header data = HeaderExt.ReadOtterUIHeader(reader);

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

        writer.WriteVector2(controlLayout.Center);

        writer.WriteVector2(controlLayout.Position);

        writer.WriteVector2(controlLayout.Size);

        writer.Write(controlLayout.Rotation);
    }
}