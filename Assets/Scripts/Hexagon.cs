using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;
using DG.Tweening;

/// <summary>
/// Sahnede olan hexagonları içeren class
/// </summary>
public class Hexagon : MonoBehaviour
{
	public Color color;

	public void SetColor(Color newColor) { GetComponent<SpriteRenderer>().SetColor(newColor); color = newColor; }


	/// <summary>
	/// İlk oluşturma için hareket kodu
	/// </summary>
	/// <param name="hexpos"></param>
	public void MoveHexagonInitial(Vector3 hexpos)
	{
		transform.DOMove(hexpos, Constants.HEX_DURATION).SetEase(Ease.OutBack);
		// OutCubic, InOutSine, InOutBack InOutSine,OutSine, OutQuint ,OutQuart ,OutBounce OutBack* , Linear ,InSine,InQuint* InQuart*
	}

	/// <summary>
	/// Daha sonraki hücre hareketi için sadece Ease hareketi değişiyor
	/// </summary>
	/// <param name="hexpos"></param>
	public void MoveHexagonCreate(Vector3 hexpos)
	{
		transform.DOMove(hexpos, Constants.HEX_DURATION).SetEase(Ease.OutCubic);
		// OutCubic, InOutSine, InOutBack InOutSine,OutSine, OutQuint ,OutQuart ,OutBounce OutBack* , Linear ,InSine,InQuint* InQuart*
	}

	/// <summary>
	/// Obje yok olduktan sonra bir particle oynatır
	/// </summary>
	private void OnDestroy()
	{
		ParticleManager.instance.PlayParticle(color, transform.position);
	}

}
