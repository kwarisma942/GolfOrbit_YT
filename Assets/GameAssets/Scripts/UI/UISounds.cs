using Pinpin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{

	private static UISounds singleton;
	[SerializeField] private AudioSource m_clicSound;
	[SerializeField] private AudioSource m_coinGainSound;
	[SerializeField] private AudioSource m_endGameSound;
	[SerializeField] private AudioSource m_jaugePerfectSound;
	[SerializeField] private AudioSource m_jaugeNonPerfectSound;
	[SerializeField] private AudioSource m_congratulationSound;
	[SerializeField] private AudioSource m_birdSound;

	[SerializeField] private AudioClip m_birdyClip;
	[SerializeField] private AudioClip m_eagleClip;
	[SerializeField] private AudioClip m_albatrosClip;

	private void Start ()
	{
		if (singleton == null)
			singleton = this;
		else
			Destroy(this.gameObject);
	}

	public static void PlayClickSound ()
	{
		if (singleton != null)
			singleton.m_clicSound.Play();
	}

	public static void PlayCoinGainSound ()
	{
		if (singleton != null)
			singleton.m_coinGainSound.Play();
	}

	public static void PlayEndGameSound ()
	{
		if (singleton != null)
			singleton.m_endGameSound.Play();
	}

	public static void PlayJaugePerfectSound ()
	{
		if (singleton != null)
			singleton.m_jaugePerfectSound.Play();
	}

	public static void PlayJaugeNonPerfectSound ()
	{
		if (singleton != null)
			singleton.m_jaugeNonPerfectSound.Play();
	}

	public static void PlayCongratulationSound ()
	{
		if (singleton != null)
			singleton.m_congratulationSound.Play();
	}

	public static void PlayBirdSound ( GameManager.HoleScores birdType)
	{
		if (singleton != null)
		{
			switch (birdType)
			{
				case GameManager.HoleScores.Birdie:
					singleton.m_birdSound.clip = singleton.m_birdyClip;
					break;
				case GameManager.HoleScores.Eagle:
					singleton.m_birdSound.clip = singleton.m_eagleClip;
					break;
				case GameManager.HoleScores.Albatros:
					singleton.m_birdSound.clip = singleton.m_albatrosClip;
					break;
			}
			singleton.m_birdSound.Play();
		}
	}



}
