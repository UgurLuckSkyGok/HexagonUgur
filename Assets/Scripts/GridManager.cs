using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
	public bool bombHexagon = false;
	[Space(20)]

	[Header("Transform Parents")]
	public Transform hexParent;
	public Transform hexJointParent;
	public Transform hexColumnsParent;
	public Transform firstHex;
	[Space(10)]

	[Header("Grid Info")]
	[Range(8, 12)]  public int columnCount;
	[Range(8, 11)]  public int rowCount;
	[Range(5, 15)]  [SerializeField] private int colorCount;
	[SerializeField] Color[] hexColors;

	[Space(10)]

	[Header("Grid Prefabs")]
	[SerializeField] GameObject hexPrefab;
	[SerializeField] GameObject hexJointPrefab;
	[SerializeField] GameObject hexColumnPrefab;
	[SerializeField] GameObject hexBombPrefab;



	



	void Awake()
	{
		firstHex.position -= new Vector3(Constants.HEX_DISTANCE_HORIZONTAL * ((columnCount - 1) / 2f), 0);

		StartCoroutine(CreateHexsJoint()); // HexJointleri Oluşturur
		StartCoroutine(CreateHexs());   // Hexagonları oluşturur

	}

	IEnumerator CreateHexsJoint()
	{
		Vector3 firstJointPos = firstHex.position + new Vector3(Constants.HEX_DISTANCE_HORIZONTAL / 3, Constants.HEX_DISTANCE_VERTICAL / 2);
		Vector3 secondeJointPos = firstHex.position + new Vector3(Constants.HEX_DISTANCE_HORIZONTAL / 3 * 2, 0);

		for (int i = 0; i < columnCount - 1; i++)
		{
			for (int j = 0; j < rowCount - 1; j++)
			{
				Vector3 jointPos = firstJointPos + new Vector3(Constants.HEX_DISTANCE_HORIZONTAL * i, Constants.HEX_DISTANCE_VERTICAL * j);
				GameObject newHex = Instantiate(hexJointPrefab, jointPos, Quaternion.identity, hexJointParent);
				newHex.name = "FHJ";

				jointPos = secondeJointPos + new Vector3(Constants.HEX_DISTANCE_HORIZONTAL * i, Constants.HEX_DISTANCE_VERTICAL * j);
				newHex = Instantiate(hexJointPrefab, jointPos, Quaternion.identity, hexJointParent);

				newHex.name = "SHJ";
				yield return new WaitForSeconds(Constants.CREATE_DELAY);
			}

			float temp = (i % 2 == 0) ? -Constants.HEX_DISTANCE_VERTICAL / 2 : +Constants.HEX_DISTANCE_VERTICAL / 2;

			firstJointPos += new Vector3(0, temp, 0);
			secondeJointPos += new Vector3(0, -temp, 0);

		}
		yield return null;
		GameManager.instance.FillJointList();
	}
	IEnumerator CreateHexs()
	{
		Color lastColor = Color.white;

		for (int i = 0; i < columnCount; i++)
		{
			Vector3 columnPos = firstHex.position + new Vector3(Constants.HEX_DISTANCE_HORIZONTAL * i, 0);
			GameObject column = Instantiate(hexColumnPrefab, columnPos, Quaternion.identity, hexColumnsParent);

			column.name = i + "x";

			for (int j = 0; j < rowCount; j++)
			{
				Vector3 hexpos = firstHex.position + new Vector3(Constants.HEX_DISTANCE_HORIZONTAL * i, Constants.HEX_DISTANCE_VERTICAL * j);
				Color hexColor;

				GameObject newHex = Instantiate(hexPrefab, hexpos + new Vector3(0, Constants.HEX_DROP_DISTANCE, 0), Quaternion.identity, hexParent);
				newHex.GetComponent<Hexagon>().MoveHexagonInitial(hexpos);

				do
				{
					hexColor = hexColors[Random.Range(0, colorCount)];
				} while (hexColor == lastColor);
				lastColor = hexColor;
				newHex.GetComponent<Hexagon>().SetColor(hexColor);

				yield return new WaitForSeconds(Constants.CREATE_DELAY);
			}

			float temp = (i % 2 == 0) ? -Constants.HEX_DISTANCE_VERTICAL / 2 : +Constants.HEX_DISTANCE_VERTICAL / 2;
			firstHex.position += new Vector3(0, temp, 0);
		}
		GameManager.instance.FillColumnList();
		yield return new WaitForSeconds(Constants.HEX_DURATION);
		InputManager.instance.canSelect = true;
	}

	public GameObject CreateNewHex(Vector3 hexpos)
	{
		if (bombHexagon)
		{
			GameObject newHex = Instantiate(hexBombPrefab, hexpos, Quaternion.identity, hexParent);
			Color hexColor = hexColors[Random.Range(0, colorCount)];
			newHex.GetComponent<Hexagon>().SetColor(hexColor);
			bombHexagon = false;
			return newHex;
		}
		else
		{
			GameObject newHex = Instantiate(hexPrefab, hexpos, Quaternion.identity, hexParent);
			Color hexColor = hexColors[Random.Range(0, colorCount)];
			newHex.GetComponent<Hexagon>().SetColor(hexColor);
			return newHex;
		}
	
	}
}
