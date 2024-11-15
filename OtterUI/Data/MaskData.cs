using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Data;
public class MaskData(uint mDataSize,ControlData controlData,int mTextureID,float mSkew,uint mFlipType) : ControlData(FOURCC,mDataSize,controlData)
{
    public const string FOURCC = "GMSK";
    [JsonPropertyName("mTextureID")]
    public int TextureID {get;set;} = mTextureID;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    [JsonPropertyName("mFliptype")]
    public uint FlipType {get;set;} = mFlipType;
    public new const int BYTES = ControlData.BYTES + 12;
    public MaskData(): this(0,new(),0,0,0)
    {

    }
}
public static class MaskDataExt
{
    public static MaskData ReadOtterUIMaskData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        int mTextureID = reader.ReadInt();
        float mSkew = reader.ReadFloat();
        uint mFlipType = reader.ReadUInt();
        return new(controlData.DataSize,controlData,mTextureID,mSkew,mFlipType);
    }
    public static void Write(this Writer writer,MaskData maskData)
    {
        ControlDataExt.Write(writer,maskData);
        writer.Write(maskData.TextureID);
        writer.Write(maskData.Skew);
        writer.Write(maskData.FlipType);
    }
}