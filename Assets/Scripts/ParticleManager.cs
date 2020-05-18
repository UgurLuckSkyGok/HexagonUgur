using MyExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
	private List<ParticleSystem> deActiveParticle = new List<ParticleSystem>();

	private void Awake()
	{
		ParticleSystem particle;

		for (int i = 0; i < gameObject.childSize(); i++)
		{
			particle = transform.GetChild(i).GetComponent<ParticleSystem>();
			deActiveParticle.Add(particle);
		}
	}

	/// <summary>
	/// Particle Çalıştırır
	/// </summary>
	/// <param name="color"></param>
	/// <param name="pos"></param>
	public void PlayParticle(Color color, Vector3 pos)
	{
		StartCoroutine(PlayParticleIE(color, pos));
	}

	IEnumerator PlayParticleIE(Color color, Vector3 pos)
	{
		if (deActiveParticle.Count == 0)
			yield break;

		ParticleSystem particle;
		particle = deActiveParticle[0];
		particle.gameObject.transform.position = pos;
		deActiveParticle.RemoveAt(0);
		var main = particle.main;
		main.startColor = color;
		particle.Play();
		yield return new WaitForSeconds(1);
		deActiveParticle.Add(particle);
	}

}
