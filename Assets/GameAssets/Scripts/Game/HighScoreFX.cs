using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinpin
{

	public class HighScoreFX : MonoBehaviour
	{

		[SerializeField] private ParticleSystem m_hitParticles;
		[SerializeField] private ParticleSystem m_idleParticles;

		public void ResetFX ()
		{
			m_idleParticles.gameObject.SetActive(true);
		}
		public void PlayHit ()
		{
			m_idleParticles.gameObject.SetActive(false);
			m_hitParticles.Play();
		}

	}

}