using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class MaskData(ControlData controlData,int mTextureID,float mSkew,uint mFlipType) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GMSK";
    [JsonPropertyName("mTextureID")]
    public int TextureID {get;set;} = mTextureID;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    [JsonPropertyName("mFliptype")]
    public uint FlipType {get;set;} = mFlipType;
    public new const int BYTES = ControlData.BYTES + 12;
    public MaskData(): this(new(),0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}
public static class MaskDataExt
{
    public static MaskData ReadZEngineUIMaskData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        int mTextureID = reader.ReadInt();
        float mSkew = reader.ReadFloat();
        uint mFlipType = reader.ReadUInt();
        return new(controlData,mTextureID,mSkew,mFlipType);
    }
    public static void Write(this Writer writer,MaskData maskData)
    {
        ControlDataExt.Write(writer,maskData);
        writer.Write(maskData.TextureID);
        writer.Write(maskData.Skew);
        writer.Write(maskData.FlipType);
    }
}