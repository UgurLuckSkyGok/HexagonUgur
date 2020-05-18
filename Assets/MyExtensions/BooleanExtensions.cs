using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyExtension
{
	public static class BooleanExtensions
	{
		public static bool AllSame<T>(List<T> values)
		{
			return values.Distinct().Count() == 1;
		}

		public static bool AllDifferent<T>(List<T> values)
		{
			return values.Distinct().Count() == values.Count();
		}

		public static bool IsMouseMoved()
		{
			return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
		}
	}
}