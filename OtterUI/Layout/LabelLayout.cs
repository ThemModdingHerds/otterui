using System.Numerics;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Layout;
public class LabelLayout(uint mDataSize,ControlLayout controlLayout,uint mColor,Vector2 mScale,float mSkew,int mDropShadow) : ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "LBLT";
    [JsonPropertyName("mColor")]
    public uint Color {get;set;} = mColor;
    [JsonPropertyName("mScale")]
    public Vector2 Scale {get;set;} = mScale;
    [JsonPropertyName("mSkew")]
    public float Skew {get;set;} = mSkew;
    [JsonPropertyName("mDropShadow")]
    public int DropShadow {get;set;} = mDropShadow;
    public new const int BYTES = ControlLayout.BYTES + 20;
    public LabelLayout(): this(0,new(),0,Vector2.Zero,0,0)
    {

    }
}

public static class LabelLayoutExt
{
    public static LabelLayout ReadOtterUILabelLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadOtterUIControlLayout();
        uint mColor = reader.ReadUInt();
        Vector2 mScale = reader.ReadVector2();
        float mSkew = reader.ReadFloat();
        int mDropShadow = reader.ReadInt();
        return new(controlLayout.DataSize,controlLayout,mColor,mScale,mSkew,mDropShadow);
    }
    public static void Write(this Writer writer,LabelLayout labelLayout)
    {
        ControlLayoutExt.Write(writer,labelLayout);
        writer.Write(labelLayout.Color);
        writer.WriteVector2(labelLayout.Scale);
        writer.Write(labelLayout.Skew);
        writer.Write(labelLayout.DropShadow);
    }
}