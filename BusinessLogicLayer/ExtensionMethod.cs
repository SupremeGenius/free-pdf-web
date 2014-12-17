using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer
{
    public static class ExtensionMethod
    {
        public static String ToTitleCase(this String ToBeConvert)
        {
            List<StringBuilder> arrWords = new List<StringBuilder>();
            String Formated = "";

            foreach (String word in ToBeConvert.Split(' '))
            {
                arrWords.Add(new StringBuilder(word));
            }

            for (int i = 0; i < arrWords.Count; i++)
            {
                StringBuilder tmpWord = arrWords[i];
                Char firstLetter = Char.Parse(tmpWord[0].ToString().ToUpper());
                tmpWord[0] = firstLetter;

                Formated += arrWords[i];
                if (i < arrWords.Count - 1)
                    Formated += " ";
            }

            return Formated;
        }

        public static bool Like(this string value, string term)
        {
            Regex regex = new Regex(string.Format("^{0}$", term.Replace("*", ".*")), RegexOptions.IgnoreCase);
            return regex.IsMatch(value ?? string.Empty);
        }        
    }
}
