using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombHexagon : Hexagon
{
	[SerializeField] private Text bomText;
	private int bombCount;
	
	private void Start()
	{
		bombCount = Random.Range(8, 12);
		bomText.text = bombCount.ToString();
	}

	/// <summary>
	/// Hareket Sonunda Bomba patlama sayısı 1 azaltılacak
	/// </summary>
	public void DecrementBombCount()
	{
		bombCount--;
		bomText.text = bombCount.ToString();
		if (bombCount == 0)
		{
			GameManager.instance.GameOver();
		}

	}
}
