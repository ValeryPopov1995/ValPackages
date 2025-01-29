namespace ValPackage.Common.Extensions
{
    public static class StringExtension
    {
        public static string ReplaceSpecialChars(this string text)
        {
            return text
                //.Substring(0, text.IndexOf("</td>"))
                .Replace("<br>", "\n")
                .Replace("\\n", "\n")
                .Replace("&#39;", "\'")
                .Replace("&quot;", "\"")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">");
        }
    }
}