using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyExtension
{
	public static class ListExtensions
	{
		public static List<GameObject> SortByDistance(this List<GameObject> objects, Vector3 mesureFrom)
		{
			return objects.OrderBy(x => Vector3.Distance(x.transform.position, mesureFrom)).ToList();
		}

		public static List<GameObject> RemoveNull(this List<GameObject> objects)
		{
			return objects.Where(x => x != null).ToList();
		}
	}
}