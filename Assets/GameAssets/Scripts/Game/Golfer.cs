using DG.Tweening;
using System;
using UnityEngine;

public class Golfer : MonoBehaviour
{
	[SerializeField] Animator m_animator;
	static public Action onShoot;
	static public Action onStartWoosh;
	public Action onPreparePerfectClub;
	public Action onPreparePerfectBall;
	public GameObject club;

	public void Shoot ()
	{
		if (onShoot != null)
			onShoot.Invoke();
	}

	public void Woosh ()
	{
		if (onStartWoosh != null)
			onStartWoosh.Invoke();
	}

	public void PlayShootAnimation ()
	{
		m_animator.SetTrigger("Shoot");
	}

	public void PlayPerfectShootAnimation ()
	{
		m_animator.SetTrigger("PerfectShoot");
	}

	public void PreparePefectShotClub ()
	{
		if(onPreparePerfectClub != null)
		{
			onPreparePerfectClub.Invoke();
		}
	}

	public void PreparePefectShotBall ()
	{
		if (onPreparePerfectBall != null)
		{
			onPreparePerfectBall.Invoke();
		}
	}

	public float GetPerfectShotSpeed ()
	{
		return m_animator.GetFloat("PerfectShotSpeed");
	}

	public void SetPerfectShotSpeed(float speed )
	{
		m_animator.SetFloat("PerfectShotSpeed", speed);
	}

	internal void PlayPuttAnimation ()
	{
		m_animator.SetTrigger("PuttShoot");
	}

	public void FadeIn ()
	{
		SpriteRenderer[] golferSprites = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < golferSprites.Length; i++)
		{
			golferSprites[i].color = new Color(1f, 1f, 1f, 0f);
			golferSprites[i].DOFade(1.0f, 1.0f);
		}
	}
}
