using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    public static class GameObjectExtensions 
    {

        public static void EnableObjs(this GameObject[] array, bool doEnable)
        {
            int len = array.Length;
            for (int i = 0; i < len; i++)
                array[i].SetActive(doEnable);
        }

		public static int childSize(this GameObject GO)
		{
			return GO.transform.childCount;
		}
	}
}