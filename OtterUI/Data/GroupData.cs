using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Data;
public class GroupData(uint mDataSize,ControlData controlData,IEnumerable<ControlData> controls) : ControlData(FOURCC,mDataSize,controlData)
{
    public const string FOURCC = "GGRP";
    [JsonPropertyName("mControls")]
    public List<ControlData> Controls {get;set;} = [..controls];
    public new const int BYTES = ControlData.BYTES + 4;
    public GroupData(uint mDataSize,ControlData controlData): this(mDataSize,controlData,[])
    {

    }
    public GroupData(): this(0,new())
    {

    }
}
public static class GroupDataExt
{
    public static GroupData ReadOtterUIGroupData(this Reader reader)
    {
        ControlData controlData = reader.ReadOtterUIControlData();
        uint mNumControls = reader.ReadUInt();
        List<ControlData> controls = [];
        for(uint i = 0;i < mNumControls;i++)
            controls.Add(ControlData.Read(reader));
        return new(controlData.DataSize,controlData,controls);
    }
    public static void Write(this Writer writer,GroupData groupData)
    {
        ControlDataExt.Write(writer,groupData);
        writer.Write((uint)groupData.Controls.Count);
        foreach(ControlData controlData in groupData.Controls)
            ControlData.Write(writer,controlData);
    }
}