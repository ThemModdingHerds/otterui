using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Data;
public class GroupData(ControlData controlData,IEnumerable<ControlData> controls) : ControlData(FOURCC,controlData)
{
    public const string FOURCC = "GGRP";
    [JsonPropertyName("mControls")]
    public List<ControlData> Controls {get;set;} = [..controls];
    public new const int BYTES = ControlData.BYTES + 4;
    public GroupData(ControlData controlData): this(controlData,[])
    {

    }
    public GroupData(): this(new())
    {

    }
    public override uint GetDataSize()
    {
        return BYTES + (from control in Controls select control.GetDataSize()).Aggregate((uint)0,(acc,x) => acc + x);
    }
}
public static class GroupDataExt
{
    public static GroupData ReadZEngineUIGroupData(this Reader reader)
    {
        ControlData controlData = reader.ReadZEngineUIControlData();
        uint mNumControls = reader.ReadUInt();
        List<ControlData> controls = [];
        for(uint i = 0;i < mNumControls;i++)
            controls.Add(ControlData.Read(reader));
        return new(controlData,controls);
    }
    public static void Write(this Writer writer,GroupData groupData)
    {
        ControlDataExt.Write(writer,groupData);
        writer.Write((uint)groupData.Controls.Count);
        foreach(ControlData controlData in groupData.Controls)
            ControlData.Write(writer,controlData);
    }
}