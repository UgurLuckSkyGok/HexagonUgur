using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;

/// <summary>
/// 3 hücre arasındaki bağlantı noktasında bulunan obje içerisindeki class
/// </summary>
public class HexJoint : MonoBehaviour
{
	[SerializeField] private List<Color> neighborHexsColor = new List<Color>();
	[SerializeField] private List<GameObject> neighbors = new List<GameObject>();
	private OutlineController outlineController;
	private GridManager gridManager;

	private void Start()
	{
		gridManager = GridManager.instance;
		outlineController = OutlineController.instance;
	}

	/// <summary>
	/// Bağlantı noktasının bağlı olduğu hücreleri getirir
	/// </summary>
	private void GetNeighbors()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
		colliders = colliders.FilterCollider(Constants.HEX_TAG);

		neighborHexsColor.Clear();
		neighbors.Clear();

		colliders.ForEachWithIndex((item, index) =>
		{
			neighborHexsColor.Add(item.GetComponent<Hexagon>().color);
			neighbors.Add(item.gameObject);
		});
	}

	/// <summary>
	/// Bağlantı noktasının durumunu kontrol eder. Eğer tüm renkler eşit ise patlama listesine gönderilir
	/// </summary>
	public void CheckHexagons()
	{
		GetNeighbors();

		if (BooleanExtensions.AllSame(neighborHexsColor) && neighborHexsColor.Count == 3)
		{
			
			//GetComponent<SpriteRenderer>().SetColor(Color.red);
			//GetComponent<SpriteRenderer>().enabled = false;

			for (int i = 0; i < neighbors.Count; i++)
			{
				GameManager.instance.AddExplosionList(neighbors[i]);
			}
		}
		else if(BooleanExtensions.AllDifferent(neighborHexsColor) && neighborHexsColor.Count == 3)
		{
			GameManager.instance.IncrementImposibleJoint();
		}
	}

	
	/// <summary>
	/// Bağlantı noktası seçilince etrafındaki hücreleri seçili hale getirir.
	/// </summary>
	public void SelectHexagons()
	{
		GameManager.instance.SetHexJoint(gameObject);
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
		GameObject[] Hexs = new GameObject[3];
		colliders = colliders.FilterCollider(Constants.HEX_TAG);

		neighborHexsColor.Clear();

		colliders.ForEachWithIndex((item, index) =>
		{
			neighborHexsColor.Add(item.GetComponent<Hexagon>().color);
			Hexs[index] = item.gameObject;
			item.gameObject.transform.SetParent(transform);
			item.GetComponent<SpriteRenderer>().sortingOrder = Constants.SELECTED_HEX_ORDER;
		});



		outlineController.SetOutLines(Hexs);
	}

	/// <summary>
	/// Seçili olan bağlantı noktasını sıfırlar
	/// </summary>
	public void ResetJoint()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		int childCount = gameObject.childSize();
		for (int i = 0; i < childCount; i++)
		{
			Transform Hexagon = transform.GetChild(0);
			Hexagon.SetParent(gridManager.hexParent);
			Hexagon.GetComponent<SpriteRenderer>().sortingOrder = Constants.DEFAULT_ORDER;
		}
	}

}
