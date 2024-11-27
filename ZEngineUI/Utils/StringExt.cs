namespace ThemModdingHerds.ZEngineUI.Utils;
public static class StringExt
{
    public static string Reverse(string str)
    {
        char[] chars = str.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
    public static string CreateEmptyString(int length)
    {
        string result = string.Empty;
        for(int i = 0;i < length;i++)
            result += '\0';
        return result;
    }
    public static byte[] ToStringBytes(this string str)
    {
        byte[] bytes = new byte[str.Length];
        char[] chars = str.ToCharArray();
        for(int i = 0;i < str.Length;i++)
            bytes[i] = (byte)chars[i];
        return bytes;
    }
    public static byte[] ToFixedStringBytes(this string str,int max)
    {
        byte[] bytes = new byte[max];
        char[] chars = str.ToCharArray();
        int length = str.Length;
        if(length > max)
            length = max;
        int i;
        for(i = 0;i < length;i++)
            bytes[i] = (byte)chars[i];
        for(;i < bytes.Length;i++)
            bytes[i] = 0;
        return bytes;
    }
    public static string MaybeSubstring(this string str,int max)
    {
        string result = string.Empty;
        int length = str.Length;
        if(length > max)
            length = max;
        for(int i = 0;i < length;i++)
            result += str[i];
        return result;
    }
    public static string RemoveInvalids(this string str)
    {
        string result = string.Empty;
        foreach(char letter in str)
        {
            if(letter == '\0')
                break;
            result += letter;
        }
        return result;
    }
}