using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;

public class OutlineController : Singleton<OutlineController>
{
	private GameObject[] outlines;

	private void Start()
	{
		outlines = new GameObject[3];

		for (int i = 0; i < transform.childCount; i++)
		{
			outlines[i] = transform.GetChild(i).gameObject;
		}
	}

	/// <summary>
	/// Seçili olan objelerin altına outline açar
	/// </summary>
	/// <param name="Hexs"></param>
	public void SetOutLines(GameObject[] Hexs)
	{
		//transform.SetParent(jointTransform);
		outlines.EnableObjs(true);

		for (int i = 0; i < Hexs.Length; i++)
		{
			outlines[i].transform.position = Hexs[i].transform.position;
			outlines[i].transform.SetParent(Hexs[i].transform);
		}
	}

	/// <summary>
	/// Outline'ları kaldırır
	/// </summary>
	public void ResetOutlines()
	{
		outlines.EnableObjs(false);

		for (int i = 0; i < outlines.Length; i++)
		{
			outlines[i].transform.SetParent(transform);
		}

		if (GameManager.instance.activeHexJoint != null)
			GameManager.instance.activeHexJoint.GetComponent<SpriteRenderer>().enabled = false;
	}
}
