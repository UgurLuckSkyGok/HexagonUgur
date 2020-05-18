using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MyExtension;

public class GameManager : Singleton<GameManager>
{
	public GameObject activeHexJoint; //Seçili olan HexJoint

	private int score;
	private int bomCount = 0;
	private int moves = 0;

	private GridManager gridManager;
	private List<HexJoint> jointList = new List<HexJoint>();
	private List<Column> columnList = new List<Column>();
	private List<GameObject> explosionList = new List<GameObject>();

	private int imposibleJointCount = 0;

	private Coroutine TurningIE;

	private void Awake()
	{
		Time.timeScale = 1;
	}

	private void Start()
	{
		gridManager = GridManager.instance;
	}

	/// <summary>
	/// Yeni Seçilen HexJoint
	/// </summary>
	/// <param name="hexJoint"></param>
	public void SetHexJoint(GameObject hexJoint)
	{
		if (activeHexJoint != null)
		{
			activeHexJoint.GetComponent<HexJoint>().ResetJoint();
		}

		activeHexJoint = hexJoint;
		activeHexJoint.GetComponent<SpriteRenderer>().enabled = true;
	}

	/// <summary>
	/// HexJoint oluşumu bittikten sonra listeye çekiliyor
	/// </summary>
	public void FillJointList()
	{
		GameObject jointParent = gridManager.hexJointParent.gameObject;
		for (int i = 0; i < jointParent.childSize(); i++)
		{
			jointList.Add(jointParent.transform.GetChild(i).GetComponent<HexJoint>());
		}
	}

	/// <summary>
	/// Column Objeleri oluştuktan sonra listeye çekiliyor
	/// </summary>
	public void FillColumnList()
	{
		GameObject columnParent = gridManager.hexColumnsParent.gameObject;

		for (int i = 0; i < columnParent.childSize(); i++)
		{
			columnList.Add(columnParent.transform.GetChild(i).GetComponent<Column>());
		}
	}

	/// <summary>
	/// Joint Objesinin dönme fonksiyonu
	/// </summary>
	/// <param name="isClockWise"></param>
	public void TurnJoint(bool isClockWise)
	{
		if (TurningIE == null)
		{
			TurningIE = StartCoroutine(StartTurn(isClockWise));
		}

	}

	/// <summary>
	/// Dönme Başlangıcı
	/// </summary>
	/// <param name="isClockWise"></param>
	/// <returns></returns>
	IEnumerator StartTurn(bool isClockWise)
	{
		float stepDegree = isClockWise ? -120 : 120;

		activeHexJoint.transform.DOLocalRotate(new Vector3(0, 0, stepDegree * 1), 0.15f).SetEase(Ease.Linear);

		yield return new WaitForSeconds(0.16f);
		MapControl();
		yield return new WaitForSeconds(0.02f);

		activeHexJoint.transform.DOLocalRotate(new Vector3(0, 0, stepDegree * 2), 0.15f).SetEase(Ease.Linear);
		yield return new WaitForSeconds(0.16f);
		MapControl();
		yield return new WaitForSeconds(0.02f);

		activeHexJoint.transform.DOLocalRotate(new Vector3(0, 0, stepDegree * 3), 0.15f).SetEase(Ease.Linear);
		yield return new WaitForSeconds(0.16f);
		MapControl();
		yield return new WaitForSeconds(0.02f);

		InputManager.instance.isTurning = false;
		yield return null;

		TurningIE = null;
	}

	/// <summary>
	/// Dönme Durdurma
	/// </summary>
	public void StopTurning()
	{
		if (TurningIE != null)
		{
			StartCoroutine(WaitForUpdate());
			StopCoroutine(TurningIE);
		}
			
		
		TurningIE = null;
		InputManager.instance.isTurning = false;
	}

	/// <summary>
	/// Hamle sonunu bekler ve gerekli işlemleri yapar
	/// </summary>
	/// <returns></returns>
	private IEnumerator WaitForUpdate()
	{
		while (!InputManager.instance.canSelect) // Wait Until Moves End
		{

			yield return new WaitForFixedUpdate();
		}

		moves++;
		MainCanvasController.instance.UpdateMoves(moves);
		var bombHexagon = FindObjectsOfType<BombHexagon>();

		foreach (var item in bombHexagon)
		{
			item.DecrementBombCount();
		}

		yield return null;
	}

	/// <summary>
	/// Haritayı tarar ve patlaması gereken objeleri tespit eder
	/// </summary>
	public void MapControl()
	{
		imposibleJointCount = 0;
		//var hexagons = FindObjectsOfType<Hexagon>();
		InputManager.instance.canSelect = false;

		//foreach (var item in hexagons)
		//{
		//	item.FindPositions();
		//}
		foreach (var item in jointList)
		{
			item.CheckHexagons();
		}

		ExplodeHexagons();
	}

	/// <summary>
	/// Patlanacak hücrelerin eklendiği liste
	/// </summary>
	/// <param name="hexagon"></param>
	public void AddExplosionList(GameObject hexagon)
	{
		if (!explosionList.Contains(hexagon.gameObject))
			explosionList.Add(hexagon);
	}

	/// <summary>
	/// Eşleşme olmaması garanti hexJointleri sayar eğer hiç bir hücrede eğleşme olmazsa Oyun Sonu gelmiş olur
	/// </summary>
	public void IncrementImposibleJoint()
	{
		imposibleJointCount++;
		if (imposibleJointCount == jointList.Count) // Daha fazla eşleşme olma şansı olmuyor
			GameManager.instance.GameOver();
		
	}

	/// <summary>
	/// Hücreleri Patlatır
	/// </summary>
	private void ExplodeHexagons()
	{
		if (explosionList.Count > 0)
		{
			OutlineController.instance.ResetOutlines();
			StopTurning();
		}
		else
		{
			InputManager.instance.canSelect = true;
			return;
		}
			
		if (activeHexJoint != null)
			activeHexJoint.GetComponent<HexJoint>().ResetJoint();
		activeHexJoint = null;

		foreach (var item in explosionList)
		{
			score += 5;
			Destroy(item.gameObject);
		}

		if ((score / 1000) > bomCount )
		{
			bomCount++;
			gridManager.bombHexagon = true;
		}

		MainCanvasController.instance.UpdateScore(score);
		explosionList.Clear();
		FillColumn();
		Invoke("MapControl", 1.1f);
	}

	/// <summary>
	/// Hücreleri Doldurur
	/// </summary>
	public void FillColumn()
	{
		foreach (var item in columnList)
		{
			item.CalculatePositions();
		}
	}

	/// <summary>
	/// Game Over
	/// </summary>
	public void GameOver()
	{
		Time.timeScale = 0;
		MainCanvasController.instance.GameOver();
	}

}
