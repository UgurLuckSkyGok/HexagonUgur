using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    public static class IntExtensions
    {
        private const string COMMAS_FORMAT = "{0:n0}";
        private const string STR_K = "K";

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static string ToNumberWithCommas(this int value)
        {
            return string.Format(COMMAS_FORMAT, value);
        }

        public static string ToNumberWithK(this int value)
        {
            if(value < 1000)
            {
                return value.ToString();
            }

            //return string.Concat(
            //    Mathf.FloorToInt(value / 1000).ToString(),
            //    STR_K);
            return string.Concat(
                (value / 1000f).ToString(), STR_K);
        }
    }
}