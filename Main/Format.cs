using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Main
{
    public class Format
    {
        public static string FormatName(string input)
        {
            string cleaned = Regex.Replace(input, @"[^a-zA-Z\s\-]", "");

            cleaned = cleaned.ToLower();

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string titleCased = textInfo.ToTitleCase(cleaned);

            string[] words = titleCased.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains('-'))
                {
                    string[] hyphenParts = words[i].Split('-');
                    for (int j = 0; j < hyphenParts.Length; j++)
                    {
                        if (hyphenParts[j].Length > 0)
                        {
                            hyphenParts[j] = char.ToUpper(hyphenParts[j][0]) + hyphenParts[j].Substring(1);
                        }
                    }
                    words[i] = string.Join("-", hyphenParts);
                }
            }
            return string.Join(" ", words);
        }
    }
}
