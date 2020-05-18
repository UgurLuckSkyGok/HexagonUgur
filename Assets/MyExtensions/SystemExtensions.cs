using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
	public static class SystemExtensions 
	{
		public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
		{
			int index = 0;
			foreach (T item in enumerable)
				handler(item, index++);
		}
	}
}