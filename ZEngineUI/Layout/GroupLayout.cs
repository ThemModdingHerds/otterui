using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class GroupLayout(ControlLayout controlLayout,uint? mColor): ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "GPLT";
    [JsonPropertyName("mColor")]
    public uint? Color {get;set;} = mColor;
    public new const int BYTES = ControlLayout.BYTES + 4;
    public GroupLayout(): this(new(),null)
    {

    }
    public override uint GetDataSize()
    {
        return (uint)(Color != null ? BYTES : ControlLayout.BYTES);
    }
}
public static class GroupLayoutExt
{
    public static GroupLayout ReadZEngineUIGroupLayout(this Reader reader)
    {
        ControlLayout controlLayout = reader.ReadZEngineUIControlLayout();
        uint? mColor = controlLayout.DataSize < GroupLayout.BYTES ? null : reader.ReadUInt();
        return new(controlLayout,mColor);
    }
    public static void Write(this Writer writer,GroupLayout groupLayout)
    {
        ControlLayoutExt.Write(writer,groupLayout);
        if(groupLayout.Color != null) 
            writer.Write(groupLayout.Color.Value);
    }
}