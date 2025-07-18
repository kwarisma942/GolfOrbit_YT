using DG.Tweening;
using Pinpin.Helpers;
using Pinpin.Scene.MainScene.UI;
using Pinpin.UI;
using MobileJoyPad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
//using CrazyGames;

namespace Pinpin
{
	public class GameManager : MonoBehaviour
	{
		public int ShootCount;
		public float ShootTime;
		public enum HoleScores
		{
			None, Birdie, Eagle, Albatros
		}

		public enum GameState
		{
			None, Fairway, Green, InHole
		}
		[SerializeField] private FeedbackManager feedback;
		[SerializeField] private Ball m_ball;
		[SerializeField] private Transform m_ballPivot;
		[SerializeField] private Golfer m_golfer;
		[SerializeField] private Transform m_golferPlanetPivot;
		[SerializeField] private Transform m_golferBallPivot;
		[SerializeField] private Transform m_golferParent;
		[SerializeField] private PlanetManager m_planet;
		[SerializeField] private float m_shotPower = 75.0f;
		[SerializeField] private float m_perfectShotBoost = 1.2f;
		[SerializeField] private float m_greenShotPower;
		[SerializeField] private CameraManager m_camera;
		[SerializeField] private PhysicsMaterial2D m_bounceMaterial;
		[SerializeField] private PhysicsMaterial2D m_greenMaterial;
		[SerializeField] private ShotArea m_shotArea;
		[SerializeField] private MainPanel m_mainPanel;
		[SerializeField] private Scene.MainScene.MainSceneManager m_mainSceneManager;
		[SerializeField] private HighScoreFX m_highScoreFX;

		private Vector2 m_greenShotAxis;
		private const float ShotAngleDivider = 0.9f;
		private bool m_isPerfectShot;

		[Header("Prediction")]
		[SerializeField]
		private int m_framesPerDot;
		[SerializeField] private SpriteRenderer[] m_predictionDots;
		[SerializeField] private Transform m_predictionWorldPivot;
		[SerializeField] private Transform m_predictionLocalPivot;

		public Action<int> onDistanceChange;
		public Action onGameOver;
		public Action onFirstGreenLanding;
		public Action onOtherGreenLanding;
		public Action onStartGame;
		public Action<HoleScores> onHoleLanding;
		public Action<WorldElement.WorldElementType> onWorldLanding;
		public Action onOrbit;
		public Action onOrbitShot;

		private float m_planetPerimeter = 3000f;
		private bool m_playing = false;
		public int lastEarnedCoins { get; private set; }
		public int lastEarnedDiamonds { get; private set; }
		private GameState m_gameState = GameState.None;
		private HoleScores m_holeScore = HoleScores.None;
		private Vector3 m_golferBasePosition;
		private Vector3 m_greenLandingPosition;
		private bool m_isPlayingOrbitalShot;
		private bool m_canOrbitShot
		{
			get
			{
				return (m_forceOrbitShot) || (m_isPerfectShot && !m_mainSceneManager.IsLastPlanet() && m_mainSceneManager.IsLastPlanetGoal() && !m_mainSceneManager.IsNextWordUnlocked());
			}
		}
		private Sequence orbitSequenceLoop;

		[SerializeField] private PushButton m_unlockAllButton;
		[SerializeField] private PushButton m_startAlbatrosButton;
		[SerializeField] private PushButton m_startOrbitShotButton;
		private bool m_forceOrbitShot;
		private DaytimeMaterial m_dayTimeController;
		[SerializeField] private AudioSource m_musicSource;
		[SerializeField] private AudioSource m_driveSource;
		[SerializeField] private AudioSource m_perfectDriveSource;
		[SerializeField] private AudioSource m_puttSource;
		[SerializeField] private AudioSource m_wooshSource;
		[SerializeField] private AudioSource m_orbitShotSource;
		[SerializeField] private AudioSource m_orbitShotFlameSource;

		//public static bool isGamePanel = false;

		private bool m_firstGoldenBall;
		public GameState gameState { get => m_gameState; }

        private void Start ()
		{
            //StartCoroutine( Initialize());
			ShootCount = PlayerPrefs.GetInt("ShootCountFunnel", 0);
			ShootTime = PlayerPrefs.GetFloat("ShootTimeFunnel", 0);


            Golfer.onShoot += Shoot;
			Golfer.onStartWoosh += Woosh;
			if (m_planet == null)
				UpdateWorld();

			m_planetPerimeter = m_planet.endRadius * 2f * Mathf.PI;
			m_ball.SetGravityCenter(m_planet.transform.position);
			m_ball.onStop += StopGame;
			m_ball.onGreen += GreenLanding;
			m_ball.onStopInHole += StopInHole;
			m_ball.onLand += OnWorldlLanding;
			m_ball.Setup(m_planet.endRadius);

			UpdateGolfer();
			UpdateBall();

			StartCoroutine(ActivateInputAfterDelay(0.5f));
			m_isPerfectShot = false;
			m_golferBasePosition = m_golferParent.transform.localPosition;
			m_startAlbatrosButton.onClick += ForcePerfectGame;
			m_startOrbitShotButton.onClick += ForceOrbitShot;
			m_unlockAllButton.onClick += UnlockAllCharacter;

			m_predictionDots = m_predictionLocalPivot.GetComponentsInChildren<SpriteRenderer>();


			if (ApplicationManager.config.game.useNewFXs)
			{
				int bestScore = ApplicationManager.datas.GetWorldBestScore(ApplicationManager.datas.selectedWorldId);
				m_bestScoreAngle = bestScore / m_planetPerimeter * 360f;

				m_highScoreFX.transform.eulerAngles = new Vector3(0, 0, -m_bestScoreAngle);
				m_highScoreFX.gameObject.SetActive(m_bestScoreAngle < 300 && m_bestScoreAngle > 1);
			}
           // EGameEvent.Invoke(EEvents.OnLevelStarted, null);
        }
   //     private IEnumerator Initialize()
   //     {
			//bool SDKInit = false;
   ////         CrazySDK.Init(() =>
   ////         {
   ////             Debug.Log("CrazyGamesManager: Initialize: CrazySDK");
			////	SDKInit = true;
   ////         });

			////yield return new WaitUntil(()=>SDKInit);

   ////         if (CrazySDK.IsInitialized)
   ////         {
   ////             CrazySDK.Game.GameplayStart();
   ////             Debug.Log("Here is the Start Event ");
   ////         }
   ////         else
   ////         {
   ////             Debug.LogError(" CrazySDK not Initialized  ");
   ////         }
   //     }
        private void UnlockAllCharacter ()
		{
			for (int i = 0; i < ApplicationManager.assets.golfers.Length; i++)
			{
				ApplicationManager.datas.UnlockCharacter(i);
			}
		}

		private void OnDestroy ()
		{
			m_ball.onStop -= StopGame;
			m_ball.onStopInHole -= StopInHole;

			Golfer.onShoot -= Shoot;
			Golfer.onStartWoosh -= Woosh;

			m_golfer.onPreparePerfectClub -= PreparePerfectShotClub;
			m_golfer.onPreparePerfectBall -= PreparePerfectShotBall;
			m_startAlbatrosButton.onClick -= ForcePerfectGame;
			m_startOrbitShotButton.onClick -= ForceOrbitShot;
			m_unlockAllButton.onClick -= UnlockAllCharacter;
		}

		bool fireTrailActive = false;
		private void Update ()
		{
			if (m_playing && m_gameState == GameState.Fairway)
			{
				UpdateDistance();
				if (m_isPerfectShot)
				{
					if (m_ball.rigidbody.velocity.sqrMagnitude < ApplicationManager.config.game.ballSqrSpeedToDestroyObstacles)
					{
						if (fireTrailActive)
						{
							feedback.StopPerfectShotFire();
							fireTrailActive = false;
						}
					}
					else
					{
						if (!fireTrailActive)
						{
							feedback.StartPerfectShotFire();
							fireTrailActive = true;
						}
					}
				}
			}
			Quaternion rotation = Quaternion.Euler(0f, 0f, m_lastAngle);
			m_ball.SetTrailRotation(rotation);
			if (!m_playing && (m_forcePerfect || m_forceOrbitShot))
			{
				float angle = m_mainPanel.GetShotAngle();
				if (angle > 28.0f && angle < 32.0f)
					PrepareShot();
				m_camera.SetTargetAngle(-m_totalRotation);
			}
			ControlTimeScale();
		}

		private void LateUpdate ()
		{
			if (!ReferenceEquals(m_planet, null))
			{
				m_planet.UpdateParalax(m_camera.currentAngle);
			}
		}

		float m_lastAngle = 0f;
		float m_totalRotation = 0f;

		private void FixedUpdate ()
		{
			if (m_gameState == GameState.Fairway)
			{
				Vector2 up = (m_ball.rigidbody.position - (Vector2)m_planet.transform.position);
				float newAngle = Vector2.SignedAngle(Vector2.up, up);
				float frameRotation = Mathf.DeltaAngle(newAngle, m_lastAngle);


				m_lastAngle = newAngle;
				m_totalRotation += frameRotation;
				m_ball.rigidbody.velocity = GeometryHelper.Rotate(m_ball.rigidbody.velocity, -frameRotation);
				Physics.gravity = -up.normalized * 9.81f;


				if (!m_isPlayingOrbitalShot)
				{
					feedback.SetBallTrailsValues(m_ball.rigidbody.velocity.magnitude);
					m_camera.SetTargetAngle(-m_totalRotation);
				}

				if (ApplicationManager.config.game.useNewFXs)
				{
					if (!m_highScoreFX.gameObject.activeSelf && m_totalRotation > m_bestScoreAngle - 300 && m_bestScoreAngle > 1)
					{
						m_highScoreFX.gameObject.SetActive(true);
					}

					if (m_totalRotation - frameRotation < m_bestScoreAngle && m_totalRotation >= m_bestScoreAngle)
					{
						m_highScoreFX.PlayHit();
						m_ball.PlaySlowMotion();
					}
					else if (m_totalRotation - frameRotation >= m_bestScoreAngle && m_totalRotation < m_bestScoreAngle)
						m_highScoreFX.ResetFX();
				}
			}
		}

		#region Fairway

		private float m_shotAngle = 0f;

		public void PrepareShot ()
		{
			if (!m_playing)
			{
				//Update world bounciness
				float bounciness = ApplicationManager.config.game.GetBounciness(ApplicationManager.datas.bounceLevel * m_bounceMultiplier);
				m_greenMaterial.bounciness = bounciness / 2f;
				m_bounceMaterial.bounciness = bounciness;
				StartCoroutine(m_planet.EnableColliders(m_bounceMaterial, m_greenMaterial));

				m_playing = true;
				m_mainPanel.StopArrow();
				float shotAngle = m_mainPanel.GetShotAngle();
				m_shotAngle = shotAngle;
				if (shotAngle > Pinpin.ApplicationManager.config.game.minPerfectAngle && shotAngle < Pinpin.ApplicationManager.config.game.maxPerfectAngle)
				{
					m_isPerfectShot = true;
					if (m_canOrbitShot)
					{
						m_golfer.onPreparePerfectClub += PrepareOrbitShotClub;
					}
					else
					{
						m_golfer.onPreparePerfectClub += PreparePerfectShotClub;
						m_golfer.onPreparePerfectBall += PreparePerfectShotBall;
					}
					m_golfer.PlayPerfectShootAnimation();
					feedback.OnPerfectShotUIFeedback();
					UISounds.PlayJaugePerfectSound();
				}
				else
				{
					m_golfer.PlayShootAnimation();
					UISounds.PlayJaugeNonPerfectSound();
				}

				feedback.OnShotGaugeStop(m_shotAngle);
				m_shotArea.onPress.RemoveListener(PrepareShot);

			}
		}

		public void PreparePerfectShotClub ()
		{
			feedback.OnPreparePerfectShotClub();
			m_camera.SetPerfectZoomDelta(-0.8f, 1f);
			m_camera.SetPerfectYDelta(-0.55f, 1f);
		}

		public void PrepareOrbitShotClub ()
		{
			feedback.PlayOrbitShotClubEffects();
			m_orbitShotFlameSource.Play();
			m_orbitShotSource.PlayDelayed(2.2f);
		}

		public void PreparePerfectShotBall ()
		{
			feedback.OnPreparePerfectShotBall();
			m_camera.SetPerfectZoomDelta(0.0f, 0.2f);
			m_camera.SetPerfectYDelta(10.0f, 0.5f);
		}

		private void Shoot ()
		{
			ShootCount++;
			ShootTime += Time.time;
			PlayerPrefs.SetInt("ShootCountFunnel", ShootCount);
			PlayerPrefs.SetFloat("ShootTimeFunnel", ShootTime);
			//FirebaseAnalytics.LogEvent("Level", "num", ShootCount);
			//switch (ShootCount)
   //         {
			//	case 10: FirebaseAnalytics.LogEvent("Shoot", "10", ShootTime);break;
			//	case 20: FirebaseAnalytics.LogEvent("Shoot", "20", ShootTime);break;
			//	case 30: FirebaseAnalytics.LogEvent("Shoot", "30", ShootTime);break;
			//	case 40: FirebaseAnalytics.LogEvent("Shoot", "40", ShootTime);break;
			//	case 50: FirebaseAnalytics.LogEvent("Shoot", "50", ShootTime);break;
			//	case 60: FirebaseAnalytics.LogEvent("Shoot", "60", ShootTime);break;
			//	case 70: FirebaseAnalytics.LogEvent("Shoot", "70", ShootTime);break;
			//	case 80: FirebaseAnalytics.LogEvent("Shoot", "80", ShootTime);break;
			//	case 90: FirebaseAnalytics.LogEvent("Shoot", "90", ShootTime);break;
			//	case 100: FirebaseAnalytics.LogEvent("Shoot", "100", ShootTime);break;
			//	case 110: FirebaseAnalytics.LogEvent("Shoot", "110", ShootTime);break;
			//	case 120: FirebaseAnalytics.LogEvent("Shoot", "120", ShootTime);break;
			//	case 130: FirebaseAnalytics.LogEvent("Shoot", "130", ShootTime);break;
			//	case 140: FirebaseAnalytics.LogEvent("Shoot", "140", ShootTime);break;
			//	case 150: FirebaseAnalytics.LogEvent("Shoot", "150", ShootTime);break;
			//}
	
			if (m_gameState == GameState.None)
			{
				if (m_forcePerfect)
				{
					StartPerfectGame();
				}
				else if (m_canOrbitShot)
				{
					m_isPlayingOrbitalShot = true;
					m_perfectDriveSource.Play();
					PlayOrbitAnimation();
					m_golfer.onPreparePerfectClub -= PrepareOrbitShotClub;

				}
				else
				{
					StartGame(m_shotAngle);
				}

				ApplicationManager.datas.lifetimeShots++;
			}
			else if (m_gameState == GameState.Green)
			{
				GreenShot();
			}
			m_camera.SetPerfectYDelta(0.0f, 1.0f);
		}

		private void Woosh ()
		{
			m_wooshSource.Play();
		}

		public void ForcePerfectGame ()
		{
			m_forcePerfect = true;
		}

		public void ForceOrbitShot ()
		{
			m_forceOrbitShot = true;
		}

		float m_timeAtGameStart = 0;
		public void StartPerfectGame ()
		{
			m_forcePerfect = false;
			feedback.OnShotParticles(1f);

			float shotAngle = 30f * Mathf.Deg2Rad;
			shotAngle *= ShotAngleDivider;
			Vector2 shotVelocity = new Vector2((Mathf.Cos(shotAngle) * m_shotPower), Mathf.Sin(shotAngle) * m_shotPower);
			shotVelocity *= m_perfectShotBoost;

			m_ball.rigidbody.velocity = shotVelocity;
			m_ball.Play(true);

			m_golfer.onPreparePerfectClub -= PreparePerfectShotClub;
			m_golfer.onPreparePerfectBall -= PreparePerfectShotBall;

			CommonStart();
		}

		private void StartGame (float shotAngle)
		{
			float shotForce = 0.0f;
			if (shotAngle < 30.0f)
			{
				shotForce = (shotAngle / 30.0f) * 0.5f + 0.5f;
			}
			else
			{
				shotForce = ((60.0f - shotAngle) / 30.0f) * 0.5f + 0.5f;
			}
			feedback.OnShotParticles(shotForce);

			shotAngle *= Mathf.Deg2Rad;
			float shotPower = ApplicationManager.config.game.GetPower(ApplicationManager.datas.strengthLevel) * m_powerMultiplier + ApplicationManager.config.game.GetBonusPower(ApplicationManager.datas.speedLevel) * m_speedMultiplier;

			shotAngle *= ShotAngleDivider;
			Vector2 shotVelocity = new Vector2((Mathf.Cos(shotAngle) * shotPower), Mathf.Sin(shotAngle) * shotPower) * shotForce;
			shotVelocity *= UnityEngine.Random.Range(0.95f, 1.05f);
			if (m_isPerfectShot)
			{
				m_perfectDriveSource.Play();
				if (ApplicationManager.datas.isVibrationActive)
					MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
				shotVelocity *= m_perfectShotBoost;
				m_golfer.onPreparePerfectClub -= PreparePerfectShotClub;
				m_golfer.onPreparePerfectBall -= PreparePerfectShotBall;
			}
			else
			{
				m_driveSource.Play();
				if (ApplicationManager.datas.isVibrationActive)
					MMVibrationManager.Haptic(HapticTypes.MediumImpact);
			}

			m_ball.rigidbody.velocity = shotVelocity;
			m_ball.Play(m_isPerfectShot);
			CommonStart();
		}

		private void CommonStart ()
		{
			m_gameState = GameState.Fairway;

			m_camera.StartGame();

			if (onStartGame != null)
			{
				onStartGame.Invoke();
			}
			//FunGames.Sdk.Analytics.FunGamesAnalytics.OnLevelStart(0);
			m_timeAtGameStart = Time.time;


			if (!ApplicationManager.datas.alreadyUsedAGoldenBall && ApplicationManager.datas.selectedBallId == 1)
			{
				ApplicationManager.datas.alreadyUsedAGoldenBall = true;
				m_firstGoldenBall = true;
			}

		}

		#endregion

		#region Green
		private void GreenLanding (GreenMesh green)
		{
			if (m_gameState == GameState.Fairway)
			{
				if (ApplicationManager.config.game.useNewFXs)
					m_highScoreFX.gameObject.SetActive(false);
				m_gameState = GameState.Green;
				m_greenLandingPosition = m_ball.transform.position;

				m_predictionWorldPivot.gameObject.SetActive(true);
				m_predictionWorldPivot.position = m_ball.transform.position;
				m_predictionWorldPivot.rotation = Quaternion.Euler(0f, 0f, m_camera.targetAngle);

				m_shotArea.onRelease.AddListener(PlayPuttAnimation);
				m_shotArea.onValueChangeScreen.AddListener(PrepareGreenShotTouch);

				//Hide dots
				PrepareGreenShot(Vector2.zero, false);

				if (!ReferenceEquals(green, null))
				{
					float angle = 0f;
					float width = 0f;
					Vector2 position = green.GetLimits(out angle, out width);
					angle -= (int)(m_totalRotation / 360) * 360f;
					m_camera.SetGreenView(position, angle, width);
				}

				m_golferPlanetPivot.localEulerAngles = new Vector3(0f, 0f, m_lastAngle);
				m_golferBallPivot.localPosition = new Vector3(0f, Vector2.Distance(m_ball.rigidbody.position, m_planet.transform.position), 0f);
				float holeAngle = green.GetHoleAngle(m_planet.transform.position);
				if (m_lastAngle < holeAngle)
				{
					m_golferBallPivot.localScale = new Vector3(-1f, 1f, 1f);
				}
				m_golferParent.localPosition = m_golferBasePosition + new Vector3(0.93f, 0.06f);
				m_golfer.FadeIn();

				if (onFirstGreenLanding != null)
				{
					onFirstGreenLanding.Invoke();
				}
			}
			else if (m_gameState == GameState.Green)
			{
				if (onOtherGreenLanding != null)
				{
					onOtherGreenLanding.Invoke();
				}
			}
		}

		private void PrepareGreenShotTouch (Vector2 axis)
		{
			PrepareGreenShot(InputManager.AxisScreen);
		}

		public void PrepareGreenShot (Vector2 axis, bool isThumbDown = true)
		{
			if (ApplicationManager.config.game.puttType == 0)
			{
				float maxMagnitude = 0.3f;
				float force = axis.magnitude;
				if (force > maxMagnitude)
				{
					axis = Vector2.ClampMagnitude(axis, maxMagnitude);
				}

				Vector2 position = Vector2.zero;
				Vector2 dotVelocity = m_greenShotAxis = axis * (m_greenShotPower * m_camera.targetOrthographicSize / 5.0f);
				int dotIndex = 0;
				Color predictionDotColor = Color.white;
				predictionDotColor.a = 0f;

				//Shot prediction
				for (int i = 0; i < m_framesPerDot * m_predictionDots.Length; ++i)
				{
					dotVelocity += Physics2D.gravity * Time.fixedDeltaTime;
					position += dotVelocity * Time.fixedDeltaTime;

					if (i % m_framesPerDot == Mathf.Min(m_framesPerDot - 1, 2))
					{
						if (isThumbDown)
						{
							predictionDotColor.a = (m_predictionDots.Length - dotIndex) / (float)m_predictionDots.Length;
						}

						m_predictionDots[dotIndex].color = predictionDotColor;
						m_predictionDots[dotIndex].transform.localPosition = position;
						dotIndex++;
					}
				}
			}
			else
			{
				float maxMagnitude = 0.3f;
				float force = axis.magnitude;
				if (force > maxMagnitude)
				{
					axis = Vector2.ClampMagnitude(axis, maxMagnitude);
				}

				m_greenShotAxis = axis * m_greenShotPower;

				m_predictionLocalPivot.localRotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, axis));
				int dotIndex = (int)((force / maxMagnitude) * m_predictionDots.Length);
				Color predictionDotColor = Color.white;

				if (isThumbDown)
				{
					for (int i = 0; i < m_predictionDots.Length; i++)
					{
						if (i <= dotIndex)
						{
							predictionDotColor.a = 1.0f;
						}
						else
						{
							predictionDotColor.a = 0.4f;
						}
						m_predictionDots[i].color = predictionDotColor;
					}
				}
				else
				{
					predictionDotColor.a = 0f;
					for (int i = 0; i < m_predictionDots.Length; i++)
					{
						m_predictionDots[i].color = predictionDotColor;
					}
				}
			}

		}

		private void PlayPuttAnimation ()
		{
			if (m_gameState == GameState.Green)
			{
				m_shotArea.onRelease.RemoveListener(PlayPuttAnimation);
				m_shotArea.onValueChangeScreen.RemoveListener(PrepareGreenShotTouch);
				m_golfer.PlayPuttAnimation();
			}
		}

		public float GetGolferScaleSign ()
		{
			return Mathf.Sign(m_golferBallPivot.localScale.x);
		}

		public void GreenShot ()
		{
			if (m_gameState == GameState.Green)
			{
				m_predictionWorldPivot.gameObject.SetActive(false);
				m_ball.useGravity = true;

				m_ball.rigidbody.angularDrag = 1f;
				m_ball.rigidbody.drag = 0f;
				m_ball.rigidbody.velocity = GeometryHelper.Rotate(m_greenShotAxis, m_camera.targetAngle);

				if (ApplicationManager.datas.isVibrationActive)
					MMVibrationManager.Haptic(HapticTypes.MediumImpact);
				m_puttSource.Play();
			}
		}

		public void StopInHole (float time)
		{
			switch (m_gameState)
			{
				case GameState.Green:
					m_holeScore = HoleScores.Birdie;
					break;
				case GameState.Fairway:
					if (m_ball.isOnGreen && time > 0.2f)
					{
						m_holeScore = HoleScores.Eagle;
					}
					else
					{
						m_holeScore = HoleScores.Albatros;
					}
					break;
			}

			if (ApplicationManager.datas.isVibrationActive)
				MMVibrationManager.Haptic(HapticTypes.MediumImpact);

			if (onHoleLanding != null)
			{
				onHoleLanding.Invoke(m_holeScore);
			}
			StopGame();
		}

		private IEnumerator CheckBallInScreen ()
		{
			Rect pixelCameraRect = Camera.main.pixelRect;
			Rect worldCameraRect = Rect.zero;
			worldCameraRect.min = Camera.main.ScreenToWorldPoint(pixelCameraRect.min);
			worldCameraRect.max = Camera.main.ScreenToWorldPoint(pixelCameraRect.max);

			while (worldCameraRect.Contains(m_ball.transform.position))
			{
				yield return new WaitForSeconds(0.2f);
			}

			m_ball.StopInGreen();

			yield return false;
		}

		public void OneMoreChance ()
		{
			PrepareGreenShot(Vector2.zero, false);

			m_ball.transform.position = m_greenLandingPosition;
			m_predictionWorldPivot.gameObject.SetActive(true);

			StartCoroutine(WaitForInput());
		}

		private IEnumerator WaitForInput ()
		{
			yield return new WaitForSeconds(0.2f);

			m_shotArea.onRelease.AddListener(PlayPuttAnimation);
			m_shotArea.onValueChangeScreen.AddListener(PrepareGreenShotTouch);

			yield return false;
		}

		#endregion

		#region orbit

		private void PlayOrbitAnimation ()
		{
			if (onStartGame != null)
			{
				onStartGame.Invoke();
			}

			if (onOrbitShot != null)
			{
				onOrbitShot.Invoke();
			}

			m_ball.rigidbody.isKinematic = true;
			m_ball.isPlayingOrbitShot = true;
			float m_orbitDuration = 6.0f;
			Ease orbitEase = Ease.InSine;
			Ease launchEase = Ease.InSine;

			Camera mainCamera = Camera.main;
			m_camera.StopFollowingBall();
			feedback.PlayOrbitShot();

			Sequence launchSequence = DOTween.Sequence();
			Sequence orbitSequence = DOTween.Sequence();

			orbitSequence.Insert(0f, m_ballPivot.DORotate(Vector3.forward * -300.0f, m_orbitDuration, RotateMode.WorldAxisAdd).SetEase(orbitEase));
			orbitSequence.Insert(0f, m_camera.transform.parent.transform.DORotate(Vector3.forward * -300.0f, m_orbitDuration, RotateMode.WorldAxisAdd).SetEase(orbitEase));
			orbitSequence.onComplete += PlayInfiniteOrbit;
			m_musicSource.DOFade(0f, m_orbitDuration);

			launchSequence.Insert(0f, m_ball.transform.DOLocalMoveY(m_ball.transform.position.y + 800.0f, m_orbitDuration)).SetEase(launchEase);
			launchSequence.Insert(0f, mainCamera.DOOrthoSize(900f, m_orbitDuration * 1.5f)).SetEase(launchEase);
			feedback.PlayOrbitShotBallParticles();
			if (ApplicationManager.datas.selectedWorldId < ApplicationManager.assets.planets.Length - 1)
				StartCoroutine(WaitToShowPlanetSprite());
		}

		IEnumerator WaitToShowPlanetSprite ()
		{
			yield return new WaitForSeconds(2f);

			float delay = feedback.ShowPlanetSprite();

			yield return new WaitForSeconds(delay + 0.18f);
			m_planet.gameObject.SetActive(false);
		}

		private void PlayInfiniteOrbit ()
		{
			float timeToOpenPanel = 5.0f;
			m_ballPivot.DORotate(Vector3.forward * -360.0f, 3.6f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
			m_camera.transform.parent.transform.DORotate(Vector3.forward * -18.0f, timeToOpenPanel, RotateMode.WorldAxisAdd).SetEase(Ease.OutSine);

			m_camera.transform.DOLocalMoveY(0f, 1.5f).SetEase(Ease.InOutQuad);
			m_camera.transform.DOLocalMoveX(0f, 1.5f).SetEase(Ease.InOutQuad);
			if (ApplicationManager.datas.selectedWorldId < ApplicationManager.assets.planets.Length - 1)
				StartCoroutine(WaitToOpenOrbitPanel(timeToOpenPanel));
		}

		IEnumerator WaitToOpenOrbitPanel (float time)
		{
			yield return new WaitForSeconds(time);

			if (onOrbit != null)
			{
				onOrbit.Invoke();
			}
		}

		public void StopOrbit ()
		{
			feedback.StopOrbitShot();
			m_ballPivot.transform.rotation = Quaternion.identity;
			m_ballPivot.DOKill();
			m_camera.transform.parent.transform.DOKill();
			m_camera.StartFollowingBall();
			m_ball.rigidbody.isKinematic = false;
			m_isPlayingOrbitalShot = false;
			m_forceOrbitShot = false;
			m_playing = false;
		}

		#endregion

		public void StopGame ()
		{
			m_playing = false;
			float time = 0.7f;

			switch (m_gameState)
			{
				case GameState.Fairway:
					if (m_holeScore == HoleScores.Albatros)
					{
						time = 3.0f;
						AddCurrencies(ApplicationManager.config.game.holescoreMultipliers[2]/*, 0.5f*/);
					}
					else if (m_holeScore == HoleScores.Eagle)
					{
						AddCurrencies(ApplicationManager.config.game.holescoreMultipliers[1]/*, 0.2f*/);
						time = 2.0f;
					}
					else
					{
						UpdateDistance();
						AddCurrencies(1/*, 0f*/);
					}
					break;
				case GameState.Green:
					if (m_holeScore == HoleScores.None)
					{
						//FunGames.Sdk.Analytics.FunGamesAnalytics.NewDesignEvent("putt", "state", "putt_miss");
						time = 0f;
						AddCurrencies(1/*, 0f*/);
						//GameManager.isGamePanel = true;
                    }
					else if (m_holeScore == HoleScores.Birdie)
					{
						//AnalyticsManager.NewDesignEvent("putt", "state", "putt_success");
						time = 2.0f;
						AddCurrencies(ApplicationManager.config.game.holescoreMultipliers[0]/*, 0.1f*/);
					}
					break;
			}

			if (GetScore() > ApplicationManager.datas.GetWorldBestScore(ApplicationManager.datas.selectedWorldId))
			{
				ApplicationManager.datas.SetWorldBestScore(ApplicationManager.datas.selectedWorldId, GetScore());
			}
			/*switch ((AShopCard.Type)ApplicationManager.datas.selectedCharacterType)
			{
				case AShopCard.Type.Normal:
					AnalyticsManager.NewDesignEvent("character", "favorite", ApplicationManager.assets.golfers[ApplicationManager.datas.selectedCharacterId].name);
					break;
				case AShopCard.Type.Premium:
					AnalyticsManager.NewDesignEvent("character", "favorite", ApplicationManager.assets.premiumGolfers[ApplicationManager.datas.selectedCharacterId].name);
					break;
				case AShopCard.Type.Instagram:
					AnalyticsManager.NewDesignEvent("character", "favorite", ApplicationManager.assets.instagramGolfer.name);
					break;
				default:
					break;
			}*/

			//AnalyticsManager.NewDesignEvent("end_distance", GetScore(), true);

			StartCoroutine(WaitForStop(time));
		}

		public IEnumerator WaitForStop (float time)
		{
			yield return new WaitForSeconds(time);

			if (onGameOver != null)
			{
				onGameOver.Invoke();
				m_camera.ResetGame();

			}
		}

		public bool ResetGame ()
		{

			float gameTime = Time.time - m_timeAtGameStart;
			//AnalyticsManager.NewDesignEvent("game_end2");
			//AnalyticsManager.NewDesignEvent("swing_achieved");


			m_totalRotation = 0f;
			m_lastAngle = 0f;
			feedback.ResetFeedbacks();
			m_ball.ResetBall();
			m_ball.rigidbody.position = new Vector2(0f, -3.523596f);
			Physics.gravity = Vector3.down * 9.81f;
			UpdateBall();

			m_gameState = GameState.None;
			m_holeScore = HoleScores.None;
			m_isPerfectShot = false;
			m_golferParent.localPosition = m_golferBasePosition;

			lastEarnedCoins = 0;
			lastEarnedDiamonds = 0;
			m_golferPlanetPivot.localEulerAngles = new Vector3(0f, 0f, 0f);
			m_golferBallPivot.localPosition = new Vector3(0f, Vector2.Distance(m_ball.rigidbody.position, (Vector2)m_planet.transform.position), 0f);
			m_golferBallPivot.localScale = Vector3.one;

			m_camera.SetStartView();
			if (!m_planet.gameObject.activeSelf)
			{
				m_planet.gameObject.SetActive(true);
			}

			StartCoroutine(ActivateInputAfterDelay(0.5f));
			WorldElement.ReactivateALL();

			if (ApplicationManager.config.game.useNewFXs)
			{
				int bestScore = ApplicationManager.datas.GetWorldBestScore(ApplicationManager.datas.selectedWorldId);
				m_bestScoreAngle = bestScore / m_planetPerimeter * 360f;

				m_highScoreFX.transform.eulerAngles = new Vector3(0, 0, -m_bestScoreAngle);
				m_highScoreFX.gameObject.SetActive(m_bestScoreAngle < 300 && m_bestScoreAngle > 1);
				m_highScoreFX.ResetFX();
			}

			if (m_firstGoldenBall)
			{
				m_firstGoldenBall = false;
				return true;
			}
			return false;
		}

		private void AddCurrencies (int coinMultiplier/*, float diamondPercentage*/ )
		{
			lastEarnedCoins = Mathf.Max((int)(GetScore() * coinMultiplier * m_earningMultiplier * ApplicationManager.config.game.baseCoinMultiplier * ApplicationManager.config.game.moneyMultiplier * ApplicationManager.datas.GetWorldMultiplier(ApplicationManager.datas.selectedWorldId)), 0);
			//lastEarnedDiamonds = Mathf.Max(Mathf.FloorToInt(GetScore() * diamondPercentage), 0);
		}

		private IEnumerator ActivateInputAfterDelay (float time)
		{
			yield return new WaitForSeconds(time);
			m_shotArea.onPress.AddListener(PrepareShot);
		}
		private int m_distance = 0;
		private bool m_forcePerfect = false;

		private void UpdateDistance ()
		{
			m_distance = (int)(m_totalRotation / 360f * m_planetPerimeter);
			if (onDistanceChange != null)
				onDistanceChange.Invoke(m_distance);
		}

		public int GetScore ()
		{
			return m_distance;
		}

		public void OnWorldlLanding (WorldElement.WorldElementType elementType)
		{
			if (onWorldLanding != null)
			{
				onWorldLanding.Invoke(elementType);
			}
		}

		public void UpdateGolfer ()
		{
			if (m_golfer != null)
			{
				Destroy(m_golfer.gameObject);
			}
			switch ((AShopCard.Type)ApplicationManager.datas.selectedCharacterType)
			{
				case AShopCard.Type.Normal:
					m_golfer = Instantiate(ApplicationManager.assets.golfers[ApplicationManager.datas.selectedCharacterId].prefab, m_golferParent);
					break;
				case AShopCard.Type.Premium:
					m_golfer = Instantiate(ApplicationManager.assets.premiumGolfers[ApplicationManager.datas.selectedCharacterId].prefab, m_golferParent);
					break;
				case AShopCard.Type.Instagram:
					m_golfer = Instantiate(ApplicationManager.assets.instagramGolfer.prefab, m_golferParent);
					break;
				default:
					break;
			}

			feedback.UpdateGolfer(m_golfer);
		}



		private float m_speedMultiplier = 1f;
		private float m_powerMultiplier = 1f;
		private float m_earningMultiplier = 1f;
		private int m_bounceMultiplier = 1;
		private float m_bestScoreAngle;

		public void UpdateBall ()
		{
			m_speedMultiplier = 1f;
			m_powerMultiplier = 1f;
			m_earningMultiplier = 1f;
			m_bounceMultiplier = 1;
			switch (ApplicationManager.datas.selectedBallId)
			{
				case 0:
					break;
				case 1:
					m_earningMultiplier = 3f;
					break;
				case 2:
					m_powerMultiplier = 2f;
					break;
				case 3:
					m_earningMultiplier = 3f;
					m_powerMultiplier = 3f;
					break;
				case 4:
					m_speedMultiplier = 2f;
					break;
				case 5:
					m_bounceMultiplier = 2;
					break;
			}
			m_ball.ChangeColor();
		}

		public void UpdateWorld ()
		{
			//WorldElement.ReactivateALL();
			if (m_planet != null)
			{
				Destroy(m_planet.gameObject);
			}
			GameAssets.WorldData worldDatas = ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId];
			m_planet = Instantiate(worldDatas.prefabs[ApplicationManager.config.game.planetDifficulty], transform, true);
			m_planet.PlaceDecorations();
			if (m_dayTimeController != null)
			{
				Destroy(m_dayTimeController.gameObject);
			}
			m_dayTimeController = Instantiate(worldDatas.dayTimeController);
			if (ApplicationManager.datas.selectedWorldId == 0)
			{
				m_dayTimeController.ChangeDaytime(ApplicationManager.datas.dayTime, m_planet);
			}
			m_musicSource.clip = worldDatas.backgroundMusic;
			m_musicSource.Play();
			m_musicSource.DOFade(0.2f, 1f);
			feedback.UpdateWorld(worldDatas);
		}

		public void ChangeDayTime (DaytimeMaterial.DayTime dayTime)
		{
			m_dayTimeController.ChangeDaytime(dayTime, m_planet);
		}

		private void ControlTimeScale ()
		{
#if UNITY_EDITOR
			float timescale;

			timescale = Time.timeScale;

			if (Input.GetKeyDown(KeyCode.UpArrow))
				timescale = timescale < 1 ? 1 : 3.0f;
			else if (Input.GetKeyDown(KeyCode.DownArrow))
				timescale = timescale > 1 ? 1 : 0.3f;

			if (Input.GetKey(KeyCode.RightArrow))
				timescale += Time.unscaledDeltaTime;
			else if (Input.GetKey(KeyCode.LeftArrow))
				timescale -= Time.unscaledDeltaTime;

			Time.timeScale = timescale;
#endif
		}
	}
}