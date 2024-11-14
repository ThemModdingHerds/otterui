using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.OtterUI;
public class SpriteData(ControlData controlData,int mTextureID,uint mColor,float mSkew,uint mFlipType,IEnumerable<ControlData> mControls) : ControlData(controlData.FourCC,controlData.DataSize,controlData.ID,controlData.Name,controlData.Position,controlData.Center,controlData.Size,controlData.Rotation,controlData.AnchorFlags,controlData.MaskID)
{
    [JsonPropertyName("mTextureID")]
    public int TextureID {get;set;} = mTextureID;
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    [JsonPropertyName("mFlipType")]
    public uint FlipType {get;set;} = mFlipType;
    [JsonPropertyName("mControls")]
    public List<ControlData> Controls {get;set;} = [..mControls];
    public SpriteData(ControlData controlData,int mTextureID,uint mColor,float mSkew,uint mFlipType): this(controlData,mTextureID,mColor,mSkew,mFlipType,[])
    {

    }
}
public static class SpriteDataExt
{
    public static SpriteData ReadOtterUISpriteData(this Reader reader)
    {
        ControlData controlData = ControlDataExt.ReadOtterUIControlData(reader);
        int mTextureID = reader.ReadInt();
        uint mColor = reader.ReadUInt();
        float mSkew = reader.ReadFloat();
        uint mFlipType = reader.ReadUInt();
        uint mNumControls = reader.ReadUInt();
        List<ControlData> controls = [];
        for(uint i = 0;i < mNumControls;i++)
            controls.Add(ControlData.Read(reader));
        return new(controlData,mTextureID,mColor,mSkew,mFlipType,controls);
    }
    public static void Write(this Writer writer,SpriteData spriteData)
    {
        ControlDataExt.Write(writer,spriteData);
        writer.Write(spriteData.TextureID);
        writer.Write(spriteData.Color);
        writer.Write(spriteData.Skew);
        writer.Write(spriteData.FlipType);
        writer.Write((uint)spriteData.Controls.Count);
        foreach(ControlData controlData in spriteData.Controls)
            ControlData.Write(writer,controlData);
    }
}