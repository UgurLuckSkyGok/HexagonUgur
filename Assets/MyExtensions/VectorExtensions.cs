using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
	public static class VectorExtensions 
	{

		public static Vector3 ReturnMultByCoeffXY(this Vector3 vec, Vector2 coeffXY)
		{
			return new Vector3(vec.x * coeffXY.x, vec.y * coeffXY.y, vec.z);
		}

		public static Vector3 ReturnMultByCoeffXY(this Vector3 vec, float coeffX, float coeffY)
		{
			return new Vector3(vec.x * coeffX, vec.y * coeffY, vec.z);
		}

		public static Vector3 ReturnMultByCoeff2D(this Vector3 vec, float coeff)
		{
			return new Vector3(vec.x * coeff, vec.y * coeff, vec.z);
		}

		public static Vector3 SetX(this Vector3 vec, float xValue)
		{
			vec.x = xValue;
			return vec;
		}
		public static Vector3 SetY(this Vector3 vec, float yValue)
		{
			vec.y = yValue;
			return vec;
		}
		public static Vector3 SetZ(this Vector3 vec, float zValue)
		{
			vec.z = zValue;
			return vec;
		}

		/// <summary>
		/// Return new vector3 with altered Z
		/// </summary>
		public static Vector3 ReturnZ(this Vector3 vec, float zValue)
		{
			return new Vector3(vec.x , vec.y, zValue);
		}

		/// <summary>
		/// Return new vector2 with altered X
		/// </summary>
		public static Vector3 ReturnX(this Vector2 vec, float xValue)
		{
			return new Vector2(xValue , vec.y);
		}

		public static Vector3 ChangeZ(this Vector3 vec, float zValue)
		{
			vec.z += zValue;
			return vec;
		}

		/// <summary>
		/// Alters the z array values to defined value
		/// </summary>
		public static Vector3[] AlterZ(this Vector3[] vecs, float zValue)
		{
			int len = vecs.Length;
			for (int i = 0; i < len; i++)
			{
				vecs[i].z = zValue;
			}
			return vecs;
		}

		public static Vector3 AlterXY(this Vector3 vec, float xValue,  float yValue)
		{
			vec.x = xValue;
			vec.y = yValue;
			return vec;
		}

		public static Vector3 AlterXY(this Vector3 vec, Vector2 vec2)
		{
			vec = new Vector3(vec2.x, vec2.y, vec.z);
			return vec;
		}
	}
}