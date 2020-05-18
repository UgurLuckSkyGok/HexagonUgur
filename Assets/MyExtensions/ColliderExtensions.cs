using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
	public static class ColliderExtensions
	{
		public static Collider2D GetClosestCollider(this Collider2D[] colliders, Vector3 originPosition)
		{
			float bestDistance = float.MaxValue;

			Collider2D closestCollider = null;

			foreach (Collider2D collider in colliders)
			{
				float distance = Vector3.Distance(originPosition, collider.transform.position);

				if (distance < bestDistance)
				{
					bestDistance = distance;
					closestCollider = collider;
				}
			}

			return closestCollider;
		}

		public static Collider2D GetClosestCollider(this Collider2D[] colliders, Vector3 originPosition, string filterTag)
		{
			float bestDistance = float.MaxValue;
			Collider2D closestCollider = null;

			foreach (Collider2D collider in colliders)
			{
				if (!collider.CompareTag(filterTag))
					continue;

				float distance = Vector3.Distance(originPosition, collider.transform.position);

				if (distance < bestDistance)
				{
					bestDistance = distance;
					closestCollider = collider;
				}
			}

			return closestCollider;
		}

		public static Collider2D[] FilterCollider(this Collider2D[] colliders, string filterTag)
		{
			List<Collider2D> filterColliders = new List<Collider2D>();

			foreach (Collider2D collider in colliders)
			{
				if (collider.CompareTag(filterTag))
					filterColliders.Add(collider);
			}

			return filterColliders.ToArray();
		}
	}
}