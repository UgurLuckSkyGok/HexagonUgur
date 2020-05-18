using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    public static class ColorExtensions
    {

        /// <summary>
        /// Get new color, based on new alpha channel
        /// </summary>
        /// <returns>The color alpha.</returns>
        /// <param name="col">Col.</param>
        /// <param name="alpha">Alpha.</param>
        public static Color GetColorAlpha(this Color col, float alpha)
        {
            return new Color(col.r, col.g, col.b, alpha);
        }

        public static void SetColor(this SpriteRenderer spriteRen, Color color)
        {
            spriteRen.color = color;
        }

        public static void SetColorAlpha(this SpriteRenderer spriteRen, float alpha)
        {
            spriteRen.color =
                new Color(spriteRen.color.r, spriteRen.color.g, spriteRen.color.b, alpha);
        }

        public static Color SetColorAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }

        public static void SetColorAlpha(this TextMesh txtMesh, float alpha)
        {
            Color col = txtMesh.color;
            txtMesh.color = new Color(col.r, col.g, col.b, alpha);
        }
    }
}