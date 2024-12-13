namespace ValeryPopov.Common.Extantions
{
    public static class StringExtantion
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