namespace ThemModdingHerds.OtterUI.Layout;
using ThemModdingHerds.OtterUI.Base;
public class ButtonLayout(uint mDataSize,ControlLayout controlLayout) : ControlLayout(FOURCC,mDataSize,controlLayout)
{
    public new const string FOURCC = "BTLT";
    public ButtonLayout(): this(0,new())
    {
        
    }
}