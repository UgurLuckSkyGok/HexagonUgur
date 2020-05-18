using MyExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
	[SerializeField] private List<GameObject> hexs = new List<GameObject>();

	/// <summary>
	/// Patlama işleminden sonra satırdaki hücrelerin pozisyonları yeniden hesaplanıyor
	/// </summary>
	public void CalculatePositions()
	{
		hexs.Clear();

		Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.01f, 20), 0);
		colliders = colliders.FilterCollider(Constants.HEX_TAG);

		foreach (var item in colliders)
		{
			hexs.Add(item.gameObject);
		}

		int rowCount = GridManager.instance.rowCount;

		if (rowCount == hexs.Count) // No change row
			return;

		hexs = hexs.SortByDistance(transform.position);

		int newHexCount = rowCount - hexs.Count;
		
		for (int i = 0; i < newHexCount; i++)
		{
			GameObject newHex = GridManager.instance.CreateNewHex(transform.position + new Vector3(0, (Constants.HEX_DROP_DISTANCE + i)));
			hexs.Add(newHex);
		}

		Vector3 hexpos;

		for (int i = 0; i < hexs.Count; i++)
		{
			hexpos = transform.position + new Vector3(0, Constants.HEX_DISTANCE_VERTICAL * i);
			hexs[i].GetComponent<Hexagon>().MoveHexagonCreate(hexpos);
		}
	}
}
