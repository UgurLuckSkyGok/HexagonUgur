using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    public static class RichTextExtensions 
    {

        public static string RichColored(this string message, Color color)
        {
            return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGBA(color), message);
        }
    }
}