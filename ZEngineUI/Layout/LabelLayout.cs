using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class LabelLayout(ControlLayout controlLayout,uint mColor,float mScaleX,float mScaleY,float mSkew,int mDropShadow) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "LBLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mScaleX")]
    public float ScaleX {get;set;} = mScaleX;
    [JsonPropertyName("mScaleY")]
    public float ScaleY {get;set;} = mScaleY;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    [JsonPropertyName("mDropShadow")]
    public int DropShadow {get;set;} = mDropShadow;
    public new const int BYTES = ControlLayout.BYTES + 20;
    public LabelLayout(): this(new(),0,0,0,0,0)
    {

    }
    public override uint GetDataSize()
    {
        return BYTES;
    }
}

public static class LabelLayoutExt
{
    public static LabelLayout ReadZEngineUILabelLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadZEngineUIControlLayout();
        uint mColor = reader.ReadUInt();
        float mScaleX = reader.ReadFloat();
        float mScaleY = reader.ReadFloat();
        float mSkew = reader.ReadFloat();
        int mDropShadow = reader.ReadInt();
        return new(controlLayout,mColor,mScaleX,mScaleY,mSkew,mDropShadow);
    }
    public static void Write(this Writer writer,LabelLayout labelLayout)
    {
        ControlLayoutExt.Write(writer,labelLayout);
        writer.Write(labelLayout.Color);
        writer.Write(labelLayout.ScaleX);
        writer.Write(labelLayout.ScaleY);
        writer.Write(labelLayout.Skew);
        writer.Write(labelLayout.DropShadow);
    }
}