using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class ControlLayout(string mFourCC,uint mDataSize,Vector2 mCenter,Vector2 mPosition,Vector2 mSize,float mRotation) : Data(mFourCC,mDataSize)
{
    [JsonPropertyName("mCenter")]
    public Vector2 Center {get;set;} = mCenter;
    [JsonPropertyName("mPosition")]
    public Vector2 Position {get;set;} = mPosition;
    [JsonPropertyName("mSize")]
    public Vector2 Size {get;set;} = mSize;
    [JsonPropertyName("mRotation")]
    public float Rotation {get;set;} = mRotation;
    public ControlLayout(string mFourCC,uint mDataSize,float mCenterX,float mCenterY,float mX,float mY,float mWidth,float mHeight,float mRotation): this(mFourCC,mDataSize,new(mCenterX,mCenterY),new(mX,mY),new(mWidth,mHeight),mRotation)
    {

    }
    public ControlLayout(Data data,float mCenterX,float mCenterY,float mX,float mY,float mWidth,float mHeight,float mRotation): this(data.FourCC,data.DataSize,new(mCenterX,mCenterY),new(mX,mY),new(mWidth,mHeight),mRotation)
    {

    }
    public static ControlLayout Read(Reader reader)
    {
        
    }
    public static void Write(Writer writer,ControlLayout controlLayout)
    {
        if(controlLayout is SpriteLayout spriteLayout)
            writer.Write(spriteLayout);
        if(controlLayout is LabelLayout labelLayout)
            writer.Write(labelLayout);
    }
}
public static class ControlLayoutExt
{
    public static ControlLayout ReadOtterUIControlLayout(this Reader reader)
    {
        Data data = DataExt.ReadOtterUIData(reader);
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
        DataExt.Write(writer,controlLayout);
        writer.WriteVector2(controlLayout.Center);
        writer.WriteVector2(controlLayout.Position);
        writer.WriteVector2(controlLayout.Size);
        writer.Write(controlLayout.Rotation);
    }
}