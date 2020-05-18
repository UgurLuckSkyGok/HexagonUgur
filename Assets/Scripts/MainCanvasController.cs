using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasController : Singleton<MainCanvasController>
{
	[SerializeField] private Text scoreText;
	[SerializeField] private Text movesText;
	[SerializeField] private GameObject GameOverPanel;

	/// <summary>
	/// Skoru günceller
	/// </summary>
	/// <param name="score"></param>
	public void UpdateScore(int score)
	{
		scoreText.text = "Score: " + score;
	}

	/// <summary>
	/// Hareket Sayısını günceller
	/// </summary>
	/// <param name="moves"></param>
	public void UpdateMoves(int moves)
	{
		movesText.text = "Moves: " + moves;
	}

	/// <summary>
	/// Oyun bitince panel açar
	/// </summary>
	public void GameOver()
	{
		GameOverPanel.SetActive(true);
	}

	/// <summary>
	/// Oyunu yeniden başlatır.
	/// </summary>
	public void Restart()
	{
		SceneManager.LoadScene(0);
	}
}
