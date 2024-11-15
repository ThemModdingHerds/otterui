using ThemModdingHerds.OtterUI.Base;

namespace ThemModdingHerds.OtterUI.Layout;
public class TableLayout(uint mDataSize,ControlLayout controlLayout) : ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "TBLT";
    public TableLayout(): this(0,new())
    {

    }
}