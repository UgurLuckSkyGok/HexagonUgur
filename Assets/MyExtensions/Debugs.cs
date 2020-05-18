using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
	public enum Colors
	{
		aqua,
		black,
		blue,
		brown,
		cyan,
		darkblue,
		fuchsia,
		green,
		grey,
		lightblue,
		lime,
		magenta,
		maroon,
		navy,
		olive,
		purple,
		red,
		silver,
		teal,
		white,
		yellow
	}

	public enum Style
	{
		Bold,
		Italics
	}

	public static class Debugs
	{

		public static string StrColored(this string message, Colors color)
		{
			return string.Format("<color={0}>{1}</color>", color.ToString(), message);
		}

		public static string StrStyled(this string message,Style style)
		{
			switch (style)
			{
				case Style.Bold:
					return string.Format("<b>{0}</b>", message);
				case Style.Italics:
					return string.Format("<i>{0}</i>", message);
			}
			return "Style Error";
		}

		//public static string StrSized(this string message, int size)
		//{
		//	return string.Format("<size={0}>{1}</size>", size, message);
		//}



		public static void Log(string message)
		{
			Debug.Log(message);
		}

		public static void Log(float message)
		{
			Debug.Log(message.ToString());
		}

		public static void Log(double message)
		{
			Debug.Log(message.ToString());
		}

		public static void Log(string message,Style style)
		{
			message = StrStyled(message, style);
			Debug.Log(message);
		}

		public static void Log(string message, Colors color)
		{

			message = StrColored(message, color);
			Debug.Log(message);

		}

		public static void Log(string message, Colors color,Style style)
		{

			message = StrColored(message, color);
			message = StrStyled(message, style);
			Debug.Log(message);

		}
	}
}

