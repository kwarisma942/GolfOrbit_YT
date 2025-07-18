using DG.Tweening;
using Pinpin;
using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CrazyGames;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	public enum State
	{
		NONE,
		ON_GREEN,
		ON_FAIR,
		ON_WATER,
	}

	[SerializeField] private TrailRenderer m_trailRenderer;
	[SerializeField] private SpriteRenderer m_renderer;
	private bool m_playing = false;
	private bool m_isInWater = false;
	private bool m_isOnGreen = false;
	private bool m_wasOnGreen = false;
	private bool m_isPerfect = false;
	private Vector2 m_gravityCenter;
	private Vector2 m_up;
	private float m_planetRadius;
	[SerializeField] private FeedbackManager feedback;
	public new Rigidbody2D rigidbody;
	public new Collider2D collider;

	public bool isPlayingOrbitShot;
	public bool useGravity { get; set; }
	public float height { get { return (rigidbody.position - m_gravityCenter).magnitude - m_planetRadius; } }
	[SerializeField] private float m_sizePerHeight;
	[SerializeField] private float m_minHeightForSizeUpdate;

	public bool isInWater { get { return m_isInWater; } }
	public bool isOnGreen { get { return m_isOnGreen; } }
	private float m_touchGreenTime;
	private GreenMesh m_green;
	private ContactPoint2D[] landContact = new ContactPoint2D[10];

	public Action onStop;
	public Action<GreenMesh> onGreen;
	public Action<float> onStopInHole;
	public Action<WorldElement.WorldElementType> onLand;
	private float m_baseAngularDrag = 0f;
	[SerializeField] private float m_stopOnGreenVelocity = 10f;

	private Vector3 m_baseScale;
	private Camera m_mainCamera;

	private Tween m_timeScaleTween;
	private Tween m_fixedTimeScaleTween;
	[SerializeField] private AudioSource m_hitGrassSound;
	[SerializeField] private AudioSource m_hitWaterSound;
	[SerializeField] private AudioSource m_hitBirdSound;
	[SerializeField] private AudioSource m_hitObstacleSound;
	[SerializeField] private AudioSource m_enterHoleSound;
	[SerializeField] private AudioSource m_explodeObstacleSound;

	private void Awake ()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		m_baseAngularDrag = rigidbody.angularDrag;
	}

	private void Start ()
	{
		m_mainCamera = Camera.main;
		ResetBall();
		m_baseScale = m_renderer.transform.localScale;
	}

	public void Setup ( float planetRadius )
	{
		m_planetRadius = planetRadius;
	}

	public void ResetBall ()
	{
		isPlayingOrbitShot = false;
		m_playing = false;
		m_wasOnGreen = false;
		m_isInWater = false;
		m_isOnGreen = false;
		m_isPerfect = false;
		useGravity = true;
		rigidbody.drag = 0f;
		rigidbody.gameObject.layer = 0;
		rigidbody.velocity = Vector2.zero;
		rigidbody.rotation = 0f;
		rigidbody.angularVelocity = 0f;
		rigidbody.angularDrag = m_baseAngularDrag;
	}

	public void Play ( bool isPerfect )
	{
		m_playing = true;
		m_isPerfect = isPerfect;
	}

	float m_stopedTimer = 0f;

	void FixedUpdate ()
	{
		m_up = (rigidbody.position - m_gravityCenter).normalized;

		if (useGravity)
		{
			if (!isInWater)
			{
				int contactCount = rigidbody.GetContacts(landContact);
				if (m_playing && contactCount > 0 && Vector2.Angle(landContact[0].normal, m_up) < 5f)
				{
					if (!m_isOnGreen)
					{
						rigidbody.angularDrag += 0.1f;
						rigidbody.drag += 0.1f;
					}
					else
					{
						rigidbody.angularDrag += 0.05f;
						rigidbody.drag += 0.05f;
					}
				}
				else
				{
					rigidbody.angularDrag = 1f;
					rigidbody.drag = 0f;
				}
			}

			if (m_playing && rigidbody.velocity.magnitude < 0.04f /*&& rigidbody.velocity.magnitude > 0f*/ && rigidbody.IsTouchingLayers())
			{
				m_stopedTimer += Time.fixedDeltaTime;
				if (m_stopedTimer > 0.4f)
				{
					rigidbody.velocity = Vector2.zero;
					rigidbody.angularVelocity = 0f;
					if (m_isOnGreen || m_wasOnGreen)
					{
						StopInGreen();
					}
					else
					{
						Stop();
					}
				}
				else
					rigidbody.AddForce(m_up * Physics2D.gravity.y * rigidbody.mass * Mathf.Max(1f, height / 10f));
			}
			else
			{
				if (m_stopedTimer > 0f)
					m_stopedTimer -= Time.fixedDeltaTime;
				rigidbody.AddForce(m_up * Physics2D.gravity.y * rigidbody.mass * Mathf.Max(1f, height / 10f));
			}
		}

		if (height > m_minHeightForSizeUpdate)
		{
			float scale = (height - m_minHeightForSizeUpdate) * m_sizePerHeight;
			m_renderer.transform.localScale = m_baseScale + Vector3.one * scale;
			m_trailRenderer.widthMultiplier = 0.15f + 0.15f * scale;
		}
		else
		{
			m_renderer.transform.localScale = m_baseScale;
			m_trailRenderer.widthMultiplier = 0.15f;
		}

	}

	public void SetGravityCenter ( Vector2 gravityCenter )
	{
		m_gravityCenter = gravityCenter;
	}

	public void SetTrailRotation ( Quaternion rotation )
	{
		m_trailRenderer.transform.position = new Vector3(rigidbody.position.x, rigidbody.position.y, -0.1f);
		m_trailRenderer.transform.rotation = rotation;
	}

	private void OnCollisionEnter2D ( Collision2D collision )
	{
		if (!m_playing)
			return;
		WorldElement worldElement = collision.collider.GetComponent<WorldElement>();
		if (worldElement != null)
		{

			float landingAngle = 0f;

			if (collision.GetContacts(landContact) > 0)
				landingAngle = Vector2.SignedAngle(Vector2.Reflect(this.rigidbody.velocity, landContact[0].normal), Vector2.up);

			if (this.rigidbody.velocity.sqrMagnitude > 13f)
			{
				if (ApplicationManager.datas.isVibrationActive)
					MMVibrationManager.Haptic(HapticTypes.LightImpact);
			}
			switch (worldElement.worldElementType)
			{
				case WorldElement.WorldElementType.Bunker:

					m_hitGrassSound.Play();
					if (!(m_isPerfect && collision.relativeVelocity.sqrMagnitude > ApplicationManager.config.game.ballSqrSpeedToDestroyObstacles))
					{
						m_isOnGreen = false;
						if (collision.GetContacts(landContact) > 0)
							rigidbody.position = landContact[0].point + landContact[0].normal * 0.1f;
						feedback.PlayLandSandParticles(landingAngle, rigidbody.position, rigidbody.velocity.magnitude);
						if (m_wasOnGreen)
						{
							StopInGreen();
						}
						else
						{
							Stop();
						}
					}
					break;

				case WorldElement.WorldElementType.Fair:
					m_isOnGreen = false;
					if (m_isInWater)
					{
						rigidbody.velocity = -collision.relativeVelocity;
					}
					else
					{
						if (collision.GetContacts(landContact) > 0)
						{
							if (ApplicationManager.config.game.useNewFXs)
							{
								feedback.PlayLandParticles(landContact[0].point, rigidbody.velocity.magnitude);
							}
							feedback.PlayLandFairwayParticles(landingAngle, landContact[0].point, rigidbody.velocity.magnitude);
						}

						if (collision.relativeVelocity.sqrMagnitude > 1f)
							m_hitGrassSound.Play();
					}
					break;

				case WorldElement.WorldElementType.Water:
					m_isOnGreen = false;
					rigidbody.velocity = -collision.relativeVelocity;
					if (collision.GetContacts(landContact) > 0)
						rigidbody.position = landContact[0].point + landContact[0].normal * 0.1f;
					feedback.PlayLandWaterParticles(rigidbody.position, rigidbody.velocity.magnitude);
					m_hitWaterSound.Play();
					if (m_wasOnGreen)
					{
						StopInGreen();
					}
					else
					{
						StopInWater();
					}
					break;

				case WorldElement.WorldElementType.Obstacle:
					m_isOnGreen = false;
					if (m_isPerfect && !worldElement.isDestroyed && collision.relativeVelocity.sqrMagnitude > ApplicationManager.config.game.ballSqrSpeedToDestroyObstacles)
					{
						rigidbody.velocity = -collision.relativeVelocity * 0.8f;
						if (collision.GetContacts(landContact) > 0)
							rigidbody.position = landContact[0].point + landContact[0].normal * 0.1f;
						worldElement.Deactivate(rigidbody.velocity);
						PlaySlowMotion();
						m_mainCamera.DOShakePosition(0.4f, 2).SetUpdate(true);
						m_explodeObstacleSound.Play();
					}
					else
					{
						if (collision.relativeVelocity.sqrMagnitude > 1f)
							m_hitObstacleSound.Play();
					}
					break;

				case WorldElement.WorldElementType.Green:
					if (collision.relativeVelocity.sqrMagnitude > 1f)
						m_hitGrassSound.Play();
					if (!m_isOnGreen)
					{
						if (rigidbody.velocity.sqrMagnitude < m_stopOnGreenVelocity * m_stopOnGreenVelocity)
						{
							rigidbody.velocity = rigidbody.velocity * 0.1f;
						}
						feedback.PlayLandGreenParticles(landingAngle, this.transform.position, rigidbody.velocity.magnitude);
						m_isOnGreen = true;
					}
					GreenMesh green = collision.gameObject.GetComponent<GreenMesh>();
					if (green != m_green)
					{
						m_green = green;
						m_touchGreenTime = Time.time;
					}
					break;
			}

			if (onLand != null)
			{
				onLand.Invoke(worldElement.worldElementType);
			}
		}
	}

	public void PlaySlowMotion ()
	{
		m_timeScaleTween.Kill();
		m_fixedTimeScaleTween.Kill();
		DOTween.To(GetTimeScale, SetTimeScale, 0.01f, 0.01f).SetEase(Ease.InSine).OnComplete(
			() => m_timeScaleTween = DOTween.To(GetTimeScale, SetTimeScale, 1f, 0.05f).SetEase(Ease.InSine));
		DOTween.To(GetFixedTimeScale, SetFixedTimeScale, 0.001f, 0.01f).SetEase(Ease.InSine).OnComplete(
			() => m_fixedTimeScaleTween = DOTween.To(GetFixedTimeScale, SetFixedTimeScale, 0.02f, 0.05f).SetEase(Ease.InSine));
	}

	private float GetTimeScale ()
	{
		return Time.timeScale;
	}

	private void SetTimeScale ( float value )
	{
		Time.timeScale = value;
	}

	private float GetFixedTimeScale ()
	{
		return Time.fixedDeltaTime;
	}

	private void SetFixedTimeScale ( float value )
	{
		Time.fixedDeltaTime = value;
	}

	private void OnTriggerEnter2D ( Collider2D collision )
	{
		WorldElement worldElement = collision.GetComponent<WorldElement>();
		if (worldElement != null)
		{
			switch (worldElement.worldElementType)
			{
				case WorldElement.WorldElementType.Hole:
					m_enterHoleSound.Play();
					if (worldElement.GetComponent<Hole>() != null)
						worldElement.GetComponent<Hole>().PlayAnimation();
					StopInHole();
					break;
				case WorldElement.WorldElementType.Bird:
					worldElement.Deactivate(rigidbody.velocity);
					feedback.PlayBirdSmashParticles();
					if(!isPlayingOrbitShot)
						m_hitBirdSound.Play();
					break;
			}


		}
	}

	private void Stop ()
	{
		rigidbody.velocity = Vector2.zero;
		rigidbody.angularVelocity = 0f;
		m_playing = false;

		if (onStop != null)
		{
			onStop.Invoke();
		}
	}

	public void StopInGreen ()
	{
		m_isOnGreen = true;
		useGravity = false;
		m_wasOnGreen = true;

		rigidbody.velocity = Vector2.zero;
		rigidbody.angularVelocity = 0f;

		if (onGreen != null)
		{
			onGreen.Invoke(m_green);
		}
	}

	private void StopInWater ()
	{
		m_isInWater = true;
		rigidbody.gameObject.layer = 8;
		rigidbody.angularVelocity = 0f;
		rigidbody.velocity = Vector2.zero;
		rigidbody.drag = 10f;
		m_playing = false;

		if (onStop != null)
		{
			onStop.Invoke();
		}
	}

	private void StopInHole()
	{
		rigidbody.velocity = Vector2.zero;
		rigidbody.angularVelocity = 0f;
		m_playing = false;

		if (onStopInHole != null)
		{
			onStopInHole.Invoke(Time.time - m_touchGreenTime);
		}
		DOVirtual.DelayedCall(3, ShowMidAd);
	}

	void ShowMidAd() 
	{
        //CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () => {/** ad started */}, (error) => {/** ad error */}, () => {/** ad finished, rewarded players here for CrazyAdType.Rewarded */});
    }

    public void ChangeColor ()
	{
		m_renderer.color = ApplicationManager.assets.balls[ApplicationManager.datas.selectedBallId].ballColor;
		if (ApplicationManager.datas.selectedBallId == 1)
		{
			feedback.OnSetGoldenBallFeedbacks();
		}
		else
		{
			feedback.StopGoldenBallParticles();
		}
	}
}
