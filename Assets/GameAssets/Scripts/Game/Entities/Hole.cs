using UnityEngine;

public class Hole : MonoBehaviour
{
	[SerializeField] private Animator m_animator;
	[SerializeField] private ParticleSystem m_particles;

	public void PlayAnimation()
	{
		m_animator.SetTrigger("Goal");
		m_particles.Play();
	}
}
