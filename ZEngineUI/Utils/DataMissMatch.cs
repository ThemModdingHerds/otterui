namespace ThemModdingHerds.ZEngineUI.Utils;
public class DataMissMatchException(object? expected,object? got) : Exception($"expected '{expected}', got '{got}' instead")
{

}