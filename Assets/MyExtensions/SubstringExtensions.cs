using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    public static class SubstringExtensions
    {
        public static string After(this string val, string a)
        {
            int posA = val.LastIndexOf(a);
            if (posA == -1)
                return string.Empty;

            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= val.Length)
                return string.Empty;

            return val.Substring(adjustedPosA);
        }

        public static string GetNextWord(this string str, string word)
        {
            string afterVal = str.Substring(str.IndexOf(word));

            string[] words = str.Split(' ');
            int len = words.Length;
            int countAfterNext = 0;
            for (int i = 0; i < len; i++)
            {
                if (countAfterNext == 0 && words[i] == word)
                    countAfterNext++;

                else if (countAfterNext > 0)
                    return words[i];              
                    
            }

            return string.Empty;
        }
    }
}