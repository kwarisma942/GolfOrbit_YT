using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Homebrew;
using Pinpin;

public class FeedbackManager : MonoBehaviour
{
	[SerializeField] private ScreenShake m_cameraShake;
	[SerializeField] private RectTransform m_mainPanel;

	[Foldout("ShotFeedback", true)]
	[SerializeField] private ParticleSystem m_shotBallParticles;
	[SerializeField] private ParticleSystem.Burst m_shotBallBurst;
	[SerializeField] private int m_shotBallBurstCount;

	[Foldout("Perfect Shot UI Feedback", true)]
	[SerializeField] private ParticleSystem m_perfectShotUIParticles;
	[SerializeField] private ParticleSystem m_perfectShotUIParticlesOld;

	[Foldout("Perfect Shot Feedback", true)]
	[SerializeField] private ParticleSystem m_preparePerfectShotParticlesClub;
	[SerializeField] private ParticleSystem m_preparePerfectShotParticlesClub2;
	[SerializeField] private ParticleSystem m_preparePerfectShotParticlesBall;
	[SerializeField] private ParticleSystem m_preparePerfectShotParticlesBall2;
	[SerializeField] private ParticleSystem m_perfectShotParticlesBall;
	[SerializeField] private ParticleSystem m_perfectShotParticlesBall2;

	[Foldout("Gauge Feedbacks", true)]
	[SerializeField] private RectTransform m_gaugeShotTextContainer;
	[SerializeField] private RectTransform m_sunBurstContainer;
	[SerializeField] private Vector3 m_sunBurstBaseScale;
	[SerializeField] private Text m_gaugeShotText;
	[SerializeField] private RectTransform m_gaugeArrowFeedbackContainer;
	[SerializeField] private Image m_gaugeArrowFeedbackImage;
	private Color m_gaugeArrowFeedbackImageBaseColor;

	[Foldout("Ball Trails", true)]
	[SerializeField] private TrailRenderer m_ballTrail;
	[SerializeField] private float m_ballTrailsEmissionMultiplier;
	private Gradient m_ballTrailBaseGradient;

	[Foldout("Ball Landing Particles", true)]
	[SerializeField] private ParticleSystem m_landGreenParticles;
	private ParticleSystem.MainModule m_landGreenParticlesMain;
	private ParticleSystem.Burst m_landGreenParticlesBurst;
	[SerializeField] private ParticleSystem m_landSandParticles;
	private ParticleSystem.MainModule m_landSandParticlesMain;
	private ParticleSystem.Burst m_landSandParticlesBurst;
	[SerializeField] private ParticleSystem m_landWaterParticles;
	private ParticleSystem.MainModule m_landWaterParticlesMain;
	private ParticleSystem.Burst m_landWaterParticlesBurst;
	[SerializeField] private ParticleSystem m_landFairwayParticles;
	private ParticleSystem.MainModule m_landFairwayParticlesMain;
	private ParticleSystem.Burst m_landFairwayParticlesBurst;
	[SerializeField] private ParticleSystem m_hitGroundParticles;
	[SerializeField] private ParticleSystem m_hitGroundParticlesLava;
	[SerializeField] private ParticleSystem m_hitGroundParticlesOrbit;

	[Foldout("GoldenBall", true)]
	[SerializeField] private ParticleSystem m_onGoldenPopParticles;
	[SerializeField] private ParticleSystem m_goldenBallParticles;
	[SerializeField] private Gradient m_goldenBallTrailColor;

	[Foldout("Shop", true)]
	[SerializeField] RectTransform m_clubHouseContainer;
	private float m_clubHouseContainerScreenXDelta;
	private float m_clubHouseContainerBaseXPosition;

	[SerializeField] public ParticleSystem m_collectCoinsParticles;
	[SerializeField] public ParticleSystem.EmissionModule m_collectCoinsEmission;
	[SerializeField] public ParticleSystem m_collectCoinsExplosionParticles;
	public ParticleSystem.Burst m_collectCoinsExplosionParticlesBurst;

	[SerializeField] public ParticleSystem m_characterUnlockParticles;

	private Action onPerfectShot;
	public Action onEndMenuAnimationEnd;
	private float ballSpeed;

	[Foldout("Orbit Shot", true)]
	[SerializeField] private Golfer m_golfer;
	[SerializeField] private ParticleSystem m_orbitClubExplosionParticlesPrefab;
	[SerializeField] private ParticleSystem m_orbitClubParticlesPrefab;
	[SerializeField] private ParticleSystem m_groundSmashParticles;
	[SerializeField] private SpriteRenderer m_planetImage;
	[SerializeField] private Image m_whiteFlashImage;

	private ParticleSystem m_orbitClubExplosionParticles;
	private ParticleSystem m_orbitClubParticles;
	

	[Foldout("Others", true)]
	[SerializeField] ParticleSystem m_birdSmashParticles;

	private void Awake ()
	{
		m_shotBallBurst = m_shotBallParticles.emission.GetBurst(0);


		m_gaugeShotTextContainer.localScale = Vector3.zero;
		//m_perfectShotUIParticles.transform.position = m_gaugeShotTextContainer.position;
		//m_perfectShotUIParticles.transform.position += Vector3.forward * 1.0f;
		m_sunBurstBaseScale = m_sunBurstContainer.transform.localScale;

		m_gaugeArrowFeedbackImageBaseColor = m_gaugeArrowFeedbackImage.color;

		m_clubHouseContainerBaseXPosition = m_clubHouseContainer.localPosition.x;

		m_ballTrailBaseGradient = m_ballTrail.colorGradient;
	}

	public void ResetFeedbacks ()
	{
		m_gaugeShotTextContainer.localScale = Vector3.zero;
		m_gaugeArrowFeedbackImage.color = m_gaugeArrowFeedbackImageBaseColor;
		m_gaugeArrowFeedbackContainer.transform.localScale = Vector3.one;
		m_sunBurstContainer.DOKill();
		m_sunBurstContainer.transform.localScale = m_sunBurstBaseScale;
		m_sunBurstContainer.gameObject.SetActive(false);
		//m_clubHouseContainer.DOLocalMoveX(m_clubHouseContainerBaseXPosition, 1.0f).SetEase(Ease.OutBack);

		StopPerfectShotFire();
		StopGoldenBallParticles();

		onPerfectShot -= OnPerfectShot;
	}

	// Use this for initialization
	void Start ()
	{
		m_landGreenParticlesMain = m_landGreenParticles.main;
		m_landGreenParticlesBurst = m_landGreenParticles.emission.GetBurst(0);
		m_landSandParticlesMain = m_landSandParticles.main;
		m_landSandParticlesBurst = m_landSandParticles.emission.GetBurst(0);
		m_landWaterParticlesMain = m_landWaterParticles.main;
		m_landWaterParticlesBurst = m_landWaterParticles.emission.GetBurst(0);
		m_landFairwayParticlesMain = m_landFairwayParticles.main;
		m_landFairwayParticlesBurst = m_landFairwayParticles.emission.GetBurst(0);

		m_clubHouseContainerScreenXDelta = m_mainPanel.rect.width;

		//m_collectCoinsExplosionParticlesBurst = m_collectCoinsExplosionParticles.emission.GetBurst(0);

		m_collectCoinsEmission = m_collectCoinsParticles.emission;

		StartCoroutine(BallTrailSmoothing());
	}

	#region Shot Effects
	public void OnShotParticles ( float power )
	{
		m_shotBallBurst.count = power * m_shotBallBurstCount;
		m_shotBallParticles.emission.SetBurst(0, m_shotBallBurst);
		m_shotBallParticles.Play();

		if (onPerfectShot != null)
		{
			onPerfectShot.Invoke();
		}
	}

	public void OnPerfectShotUIFeedback ()
	{
		if (ApplicationManager.config.game.useNewFXs)
			m_perfectShotUIParticles.Play();
		else
			m_perfectShotUIParticlesOld.Play();
	}

	public void OnPreparePerfectShotClub ()
	{
		m_preparePerfectShotParticlesClub.Play();
		m_preparePerfectShotParticlesClub2.Play();
		onPerfectShot += OnPerfectShot;
	}

	public void OnPreparePerfectShotBall ()
	{
		//m_preparePerfectShotParticlesBall.Play();
		//m_preparePerfectShotParticlesBall2.Play();
	}

	private void OnPerfectShot ()
	{
		//m_perfectShotParticlesBall.Play();
		this.StartPerfectShotFire();
	}

	public void OnShotGaugeStop ( float shotAngle )
	{
		float arrowScaleValue = 1.2f;
		if (shotAngle > Pinpin.ApplicationManager.config.game.minPerfectAngle && shotAngle < Pinpin.ApplicationManager.config.game.maxPerfectAngle)
		{
			m_sunBurstContainer.gameObject.SetActive(true);
			m_gaugeShotTextContainer.DOScale(Vector3.one * 1.5f, 0.8f).SetEase(Ease.OutElastic);
			m_sunBurstContainer.DORotate(Vector3.forward * 180.0f, 5.0f);
			m_sunBurstContainer.DOPunchScale(Vector3.one, 1.0f, 0, 0.5f).SetLoops(-1);
			m_gaugeShotText.text = I2.Loc.ScriptLocalization.perfect;
			arrowScaleValue = 2.1f;
		}
		else if (shotAngle > 14.0f && shotAngle < 46.0f)
		{
			m_gaugeShotTextContainer.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBack);
			m_gaugeShotText.text = I2.Loc.ScriptLocalization.great;
			arrowScaleValue = 1.7f;
		}
		else
		{
			m_gaugeShotTextContainer.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutCirc);
			m_gaugeShotText.text = I2.Loc.ScriptLocalization.cool;
		}

		Sequence gaugeArrow = DOTween.Sequence();
		gaugeArrow.Insert(0.0f, m_gaugeArrowFeedbackContainer.DOScale(arrowScaleValue, 2.0f)).SetEase(Ease.OutExpo);
		gaugeArrow.Insert(1.5f, m_gaugeArrowFeedbackImage.DOFade(0.0f, 0.5f)).SetEase(Ease.OutExpo);


		//m_clubHouseContainer.DOLocalMoveX(m_clubHouseContainerScreenXDelta, 1.0f).SetEase(Ease.InBack);
	}

	#endregion


	#region Game Effects

	public void SetBallTrailsValues ( float speed )
	{
		ballSpeed = speed;
	}

	IEnumerator BallTrailSmoothing ()
	{
		while (true)
		{
			m_ballTrail.time = Mathf.Lerp(m_ballTrail.time, ballSpeed * m_ballTrailsEmissionMultiplier, 0.3f);

			yield return false;
		}
	}

	public void PlayLandGreenParticles ( float angle, Vector3 landPosition, float speed )
	{
		if (m_landGreenParticles.isStopped && speed > 1f)
		{
			landPosition.z = m_landGreenParticles.transform.position.z;
			m_landGreenParticles.transform.position = landPosition;
			speed = Mathf.Clamp(speed, 3.0f, 10.0f);
			m_landGreenParticlesBurst.count = speed * 3.0f;
			m_landGreenParticles.emission.SetBurst(0, m_landGreenParticlesBurst);
			m_landGreenParticlesMain.startSpeedMultiplier = speed * 0.3f;
			m_landGreenParticles.Play();
		}
	}

	public void PlayLandSandParticles ( float angle, Vector3 landPosition, float speed )
	{
		if (m_landSandParticles.isStopped && speed > 1f)
		{
			landPosition.z = m_landSandParticles.transform.position.z;
			m_landSandParticles.transform.position = landPosition;
			speed = Mathf.Clamp(speed, 3.0f, 10.0f);
			m_landSandParticlesBurst.count = speed * 3.0f;
			m_landSandParticles.emission.SetBurst(0, m_landSandParticlesBurst);
			m_landSandParticlesMain.startSpeedMultiplier = speed * 0.3f;
			m_landSandParticles.Play();
		}
	}

	public void PlayLandFairwayParticles ( float angle, Vector3 landPosition, float speed )
	{
		if (m_landFairwayParticles.isStopped && speed > 1f)
		{
			landPosition.z = m_landFairwayParticles.transform.position.z;
			m_landFairwayParticles.transform.position = landPosition;
			speed = Mathf.Clamp(speed, 3.0f, 10.0f);
			m_landFairwayParticlesBurst.count = speed * 3.0f;
			m_landFairwayParticles.emission.SetBurst(0, m_landFairwayParticlesBurst);
			m_landFairwayParticlesMain.startSpeedMultiplier = speed * 0.3f;
			m_landFairwayParticles.Play();
		}
	}

	public void PlayLandWaterParticles ( Vector3 landPosition, float speed )
	{
		if (m_landWaterParticles.isStopped)
		{
			landPosition.z = m_landWaterParticles.transform.position.z;
			m_landWaterParticles.transform.position = landPosition;
			m_landWaterParticlesBurst.count = speed * 3.0f;
			m_landWaterParticles.emission.SetBurst(0, m_landWaterParticlesBurst);
			m_landWaterParticlesMain.startSpeedMultiplier = speed * 0.3f;
			m_landWaterParticles.Play();
		}
	}

	public void PlayLandParticles (Vector3 landPosition, float speed)
	{
		/*if (m_hitGroundParticles.isStopped)
		{*/
		ParticleSystem hitGround = m_hitGroundParticles;
		if (ApplicationManager.datas.selectedWorldId == 1)
			hitGround = m_hitGroundParticlesLava;
		if (ApplicationManager.datas.selectedWorldId == 2)
			hitGround = m_hitGroundParticlesOrbit;


		landPosition.z = hitGround.transform.position.z;
		speed = Mathf.Clamp(speed, 1.0f, 15.0f);
		hitGround.transform.localScale = Vector3.one * speed / 10f;
		hitGround.transform.position = landPosition;
		hitGround.Play();
		//}
	}

	public void StartPerfectShotFire ()
	{
		m_perfectShotParticlesBall2.Play();
	}

	public void StopPerfectShotFire ()
	{
		m_perfectShotParticlesBall2.Stop();
	}

	#endregion


	#region End Game Effects

	public void OnCollectCoinFeedback ( float coinGain )
	{
		coinGain = Mathf.Pow(coinGain, 0.75f);
		m_collectCoinsExplosionParticlesBurst.count = coinGain;
		//m_collectCoinsExplosionParticles.emission.SetBurst(0, m_collectCoinsExplosionParticlesBurst);
//		m_collectCoinsExplosionParticles.Play();
		m_collectCoinsEmission.rateOverTime = coinGain;
		m_collectCoinsParticles.Play();
	}

	#endregion


	#region Orbit Shot

	public float ShowPlanetSprite ()
	{
		float fadeDuration = 1f;
		float whiteFlashDelay = 0.5f;
		m_planetImage.sprite = Pinpin.ApplicationManager.assets.planets[Pinpin.ApplicationManager.datas.selectedWorldId].orbitImage;
		m_planetImage.DOFade(1.0f, fadeDuration).SetEase(Ease.InSine);
		m_whiteFlashImage.gameObject.SetActive(true);
		m_whiteFlashImage.DOFade(1.0f, 0.2f).SetDelay(fadeDuration + whiteFlashDelay).OnComplete(() =>
		{
			m_whiteFlashImage.DOFade(0f, 0.3f).SetDelay(0.1f);
		});

		return fadeDuration + whiteFlashDelay;
	}

	public void PlayOrbitShotClubEffects ()
	{
		m_orbitClubParticles.Play();
		m_preparePerfectShotParticlesClub.Play();
		Camera.main.DOShakePosition(0.7f, Vector2.one * 1.5f).SetEase(Ease.OutCirc).SetDelay(m_orbitClubParticles.main.startDelay.constant + 0.01f).SetUpdate(true);
		m_orbitClubExplosionParticles.Play();
		//DOTween.To(GetTimeScale, SetTimeScale, 0.1f, 0.3f).SetEase(Ease.InSine).SetDelay(0.5f).OnComplete(
		//	() => DOTween.To(GetTimeScale, SetTimeScale, 1f, 0.3f).SetEase(Ease.InSine).SetDelay(0.1f));
		DOTween.To(m_golfer.GetPerfectShotSpeed, m_golfer.SetPerfectShotSpeed, 0.1f, 0.5f).SetEase(Ease.InExpo).SetDelay(0.5f).OnComplete(
			() => { DOTween.To(m_golfer.GetPerfectShotSpeed, m_golfer.SetPerfectShotSpeed, 1.0f, 0.1f).SetDelay(0.6f); });
	}

	private float GetTimeScale ()
	{
		return Time.timeScale;
	}

	private void SetTimeScale ( float value )
	{
		Time.timeScale = value;
	}

	public void PlayOrbitShotBallParticles ()
	{
		Camera.main.DOShakePosition(5.0f, Vector2.one * 1, 15, 90, false).SetEase(Ease.Linear);
		Camera.main.DOShakeRotation(0.5f, Vector3.forward * 0.4f, 15, 90, false).SetEase(Ease.Linear).SetLoops(10, LoopType.Incremental);
		m_groundSmashParticles.Play();
	}

	public void PlayOrbitShot ()
	{
		m_perfectShotParticlesBall2.Play();
		ballSpeed = 60000f;

	}

	public void StopOrbitShot ()
	{
		m_perfectShotParticlesBall2.Stop();
		ballSpeed = 0f;
		m_planetImage.DOFade(0f, 0f);
	}

	#endregion
	
	public void PlayBirdSmashParticles ()
	{
		m_birdSmashParticles.Play();
	}


	public void UpdateGolfer ( Golfer newGolfer )
	{
		m_golfer = newGolfer;

		m_orbitClubParticles = Instantiate(m_orbitClubParticlesPrefab, m_golfer.club.transform);
		m_orbitClubParticles.transform.localPosition = new Vector3(-0.13f, -2.02f, 0f);
		m_orbitClubExplosionParticles = Instantiate(m_orbitClubExplosionParticlesPrefab, m_golfer.club.transform);
		m_orbitClubExplosionParticles.transform.localPosition = new Vector3(-0.13f, -2.02f, 0f);
	}

	public void UpdateWorld ( Pinpin.GameAssets.WorldData worldData )
	{
		ParticleSystem.MinMaxGradient startColor = m_landGreenParticlesMain.startColor;
		startColor.gradient = worldData.landParticlesColor[0];
		m_landGreenParticlesMain.startColor = startColor;

		startColor = m_landSandParticlesMain.startColor;
		startColor.gradient = worldData.landParticlesColor[1];
		m_landSandParticlesMain.startColor = startColor;

		startColor = m_landWaterParticlesMain.startColor;
		startColor.gradient = worldData.landParticlesColor[2];
		m_landWaterParticlesMain.startColor = startColor;

		startColor = m_landFairwayParticlesMain.startColor;
		startColor.gradient = worldData.landParticlesColor[3];
		m_landFairwayParticlesMain.startColor = startColor;

		ParticleSystem.MainModule particlesBirdMain = m_birdSmashParticles.main;
		startColor = particlesBirdMain.startColor;
		startColor = worldData.birdsColor;
		particlesBirdMain.startColor = startColor;
	}

	public void OnSetGoldenBallFeedbacks ()
	{
		m_onGoldenPopParticles.Play();
		m_goldenBallParticles.Play();
		m_ballTrail.colorGradient = m_goldenBallTrailColor;
	}

	public void StopGoldenBallParticles ()
	{
		m_goldenBallParticles.Stop();
		m_ballTrail.colorGradient = m_ballTrailBaseGradient;
	}

	private IEnumerator FreezeFrames ( int numberOfFrames )
	{
		float deltaTime = Time.deltaTime;

		Time.timeScale = 0.0f;

		yield return new WaitForSecondsRealtime(deltaTime * numberOfFrames);

		Time.timeScale = 1.0f;

		yield return false;
	}

}
