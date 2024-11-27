namespace ThemModdingHerds.ZEngineUI.Layout;
using ThemModdingHerds.ZEngineUI.Base;
public class ButtonLayout(ControlLayout controlLayout) : ControlLayout(FOURCC,controlLayout)
{
    public new const string FOURCC = "BTLT";
    public ButtonLayout(): this(new())
    {
        
    }
}