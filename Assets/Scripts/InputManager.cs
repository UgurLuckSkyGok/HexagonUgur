using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;

public class InputManager : Singleton<InputManager>
{
	private Vector2 touchStartPosition;
	private GameManager GM;
	private bool canTurn = false;

	public bool canSelect = false;// { private get; set; }
	public bool isTurning = false;//{ private get; set; }

	private void Start()
	{
		GM = GameManager.instance;
	}

	private void Update()
	{

#if UNITY_EDITOR
		#region EditorInput
		if (Input.GetKey(KeyCode.KeypadEnter))
		{
			Debug.Break();
		}

		if (Input.GetKey(KeyCode.Keypad0))
		{
			Time.timeScale = 0.1f;
		}
		if (Input.GetKey(KeyCode.Keypad1))
		{
			Time.timeScale = 1f;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GM.FillColumn();
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			GM.MapControl();
		}


		if (Input.GetMouseButtonUp(0) && touchStartPosition == (Vector2)Input.mousePosition && canSelect && !isTurning)
		{
			CheckSelection();
		}

		if (Input.GetMouseButtonDown(0))
		{
			canTurn = true;
			touchStartPosition = Input.mousePosition;
		}

		if (Input.GetMouseButton(0) && !isTurning && canTurn)
		{
			CheckRotation();
		}
		#endregion EditorInput
#else
		#region MobileInput
		if (Input.touchCount >= 1)
		{
			if (Input.touches[0].phase == TouchPhase.Ended && touchStartPosition == (Vector2)Input.mousePosition && canSelect && !isTurning)
			{
				CheckSelection();
			}

			if (Input.touches[0].phase == TouchPhase.Began)
			{
				canTurn = true;
				touchStartPosition = Input.mousePosition;
			}

			if (Input.touches[0].phase == TouchPhase.Moved && !isTurning && canTurn)
			{
				CheckRotation();
			}
		}
		#endregion MobileInput
#endif
	}

	/// <summary>
	/// Seçilip seçilememe durumu
	/// </summary>
	private void CheckSelection()
	{
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(worldPoint.x, worldPoint.y);
		Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, 0.3f);

		Collider2D closestCollider = colliders.GetClosestCollider(mousePos, Constants.HEXJOINT_TAG);
		if (closestCollider != null)
		{
			//closestCollider.GetComponent<SpriteRenderer>().enabled = true;
			//closestCollider.GetComponent<SpriteRenderer>().SetColor(Color.white);
			closestCollider.GetComponent<HexJoint>().SelectHexagons();
		}

		//selectedHexagon = GridManagerObject.GetSelectedHexagon();

	}

	/// <summary>
	/// Dönmeyi kontrol eder
	/// </summary>
	private void CheckRotation()
	{
		bool selection = (GM.activeHexJoint != null) ? true : false;

		if (BooleanExtensions.IsMouseMoved())
		{
			Vector2 touchCurrentPosition = Input.mousePosition;
			float distanceX = touchCurrentPosition.x - touchStartPosition.x;
			float distanceY = touchCurrentPosition.y - touchStartPosition.y;

			if ((Mathf.Abs(distanceX) > Constants.HEX_ROTATE_SLIDE_DISTANCE || Mathf.Abs(distanceY) > Constants.HEX_ROTATE_SLIDE_DISTANCE) && selection)
			{

				Vector3 screenPosition = Camera.main.WorldToScreenPoint(GM.activeHexJoint.transform.position);

				bool isX = Mathf.Abs(distanceX) > Mathf.Abs(distanceY);
				bool isRightUp = isX ? distanceX > 0 : distanceY > 0; // ClockWise
				bool touchThanHex = isX ? touchCurrentPosition.y > screenPosition.y : touchCurrentPosition.x > screenPosition.x;
				bool clockWise = isX ? isRightUp == touchThanHex : isRightUp != touchThanHex;

				GM.TurnJoint(clockWise);
				isTurning = true;
				canTurn = false;
			}
		}
	}
}
