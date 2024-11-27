using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class SpriteData(ControlData controlData,int mTextureID,uint mColor,float mSkew,uint mFlipType,IEnumerable<ControlData> mControls) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GSPR";
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
    public new const int BYTES = ControlData.BYTES + 20;
    public SpriteData(ControlData controlData,int mTextureID,uint mColor,float mSkew,uint mFlipType): this(controlData,mTextureID,mColor,mSkew,mFlipType,[])
    {

    }
    public SpriteData(): this(new(),0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + (from controls in Controls select controls.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
}
public static class SpriteDataExt
{
    public static SpriteData ReadZEngineUISpriteData(this Reader reader)
    {
        ControlData controlData = ControlDataExt.ReadZEngineUIControlData(reader);
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