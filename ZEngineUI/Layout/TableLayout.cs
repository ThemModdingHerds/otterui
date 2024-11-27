using ThemModdingHerds.ZEngineUI.Base;

namespace ThemModdingHerds.ZEngineUI.Layout;
public class TableLayout(ControlLayout controlLayout) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "TBLT";
    public TableLayout(): this(new())
    {

    }
}