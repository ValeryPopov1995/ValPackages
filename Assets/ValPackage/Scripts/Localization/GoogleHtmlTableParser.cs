using System.Collections.Generic;
using System.Threading.Tasks;
using ValPackage.Common.Extensions;

namespace ValPackage.Common.Localization
{
    public static class GoogleHtmlTableParser
    {
        /// <summary>
        /// Parse html google-table
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns>Lines of cells from table</returns>
        public static async Task<string[][]> ParseTable(string htmlText)
        {
            List<string[]> cells = new();
            var lines = htmlText.Split("<th id=\""); // may be "<th id="

            for (int i = 1; i < lines.Length; i++) // lines[from 0] is useless html data
            {
                var texts = SpltiLine(lines[i]);
                cells.Add(texts);

                if (i % 10 == 0) await Task.Yield();
            }

            return cells.ToArray();
        }

        public static string[] SpltiLine(string htmlLine)
        {
            htmlLine = TrimDiv(htmlLine);
            // <td class="s0" dir="ltr">text</td>
            var lines = htmlLine.Split("<td");
            for (int i = 1; i < lines.Length; i++) // lines[0] is html line data
            {
                // class="s0" dir="ltr">text</td>
                lines[i] = lines[i]
                    .Substring(lines[i].IndexOf(">") + 1) // text</td>
                    .Replace("</td>", "") // text
                    .ReplaceSpecialChars(); // text
            }

            return lines;
        }

        public static string TrimDiv(string text) => TrimHtmlData(text, "<div").Replace("</div>", "");

        private static string TrimHtmlData(string text, string startFrom, string endWith = ">")
        {
            int divIndex = text.IndexOf(startFrom);
            while (divIndex > 0)
            {
                int endIndex = text.IndexOf(endWith, divIndex);
                string beforeDiv = text.Substring(0, divIndex);
                text = beforeDiv + text.Substring(endIndex + endWith.Length, text.Length - endIndex - endWith.Length);
                divIndex = text.IndexOf(startFrom);
            }

            return text;
        }
    }
}