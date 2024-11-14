using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class ControlData(string mFourCC,uint mDataSize,uint mID,string mName,Vector2 mPosition,Vector2 mCenter,Vector2 mSize,float mRotation,uint mAnchorFlags,int mMaskID) : Data(mFourCC,mDataSize)
{
    [JsonPropertyName("mID")]
    public uint ID {get;set;} = mID;
    public const int NAME_LENGTH = 64;
    [JsonPropertyName("mName")]
    public string Name {get;set;} = mName[..NAME_LENGTH];
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
    public ControlData(Data data,uint mID,string mName,Vector2 mPosition,Vector2 mCenter,Vector2 mSize,float mRotation,uint mAnchorFlags,int mMaskID): this(data.FourCC,data.DataSize,mID,mName,mPosition,mCenter,mSize,mRotation,mAnchorFlags,mMaskID)
    {

    }
    public static ControlData Read(Reader reader)
    {

    }
    public static void Write(Writer writer,ControlData controlData)
    {
        if(controlData is SpriteData spriteData)
            writer.Write(spriteData);
    }
}
public static class ControlDataExt
{
    public static ControlData ReadOtterUIControlData(this Reader reader)
    {
        Data data = DataExt.ReadOtterUIData(reader);
        uint mID = reader.ReadUInt();
        string mName = reader.ReadASCII(ControlData.NAME_LENGTH);
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
        DataExt.Write(writer,controlData);
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