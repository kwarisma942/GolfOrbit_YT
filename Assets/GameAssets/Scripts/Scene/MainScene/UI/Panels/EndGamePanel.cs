using DG.Tweening;
using Pinpin.UI;
using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using I2LocManager = I2.Loc.LocalizationManager;

namespace Pinpin.Scene.MainScene.UI
{

    public class EndGamePanel : AUIPanel
    {
        [SerializeField] private FeedbackManager feedback;
        [SerializeField] private RectTransform m_topSunBurst;
        [SerializeField] private ParticleSystem m_topParticles;
        [SerializeField] private PushButton m_collectCoinsButton;
        [SerializeField] private PushButton m_collectCoinsRewardButton;
        [SerializeField] private UICurrencyUpdater m_coinGainText;
        [SerializeField] private Text m_endGameTooltipText;
        [SerializeField] private Text m_holeMultiplierText;
        [SerializeField] private Text m_multiplierNumberText;
        private int m_holeScoreMultiplier;
        [SerializeField] private Image m_goldenBallMultiplierImage;
        [SerializeField] private ParticleSystem m_goldenBallParticles;
        [SerializeField] private ParticleSystem m_characterUnlockParticles;
        [SerializeField] private RectTransform m_panelContainer;
        [SerializeField] private Image m_birdImage;
        [SerializeField] private Sprite[] m_birdImages;
        [SerializeField] private TextResizer[] m_textToResize;

        [Header("Jackpot")]
        [SerializeField] private Image m_jackpotFillBar;
        [SerializeField] private RectTransform m_jackpotFillBarParticlesContainer;
        [SerializeField] private GameObject m_jackpotFillBarParticles;
        [SerializeField] private PushButton m_jackpotButton;
        [SerializeField] private ParticleSystem m_jackpotButtonParticles;

        private bool m_isJackpotReady;
        private bool m_hasCollectCoins;
        private float m_endPanelContainerBaseYPosition;
        private float m_endPanelContainerScreenYDelta;
        private bool isWaiting = false;
        private new MainSceneUIManager UIManager
        {
            get { return (base.UIManager as MainSceneUIManager); }
        }

        public static Action<int> onCharacterUnlocked;

        private ulong m_coinGain;

        protected override void Awake()
        {
            m_jackpotButton.gameObject.SetActive(false);

            UIManager.GetPopup<CharacterUnlockPopup>().onPopupClosed += OnOtherPopupClosed;
            UIManager.GetPopup<NewWorldPopup>().onPopupClosed += OnOtherPopupClosed;
            m_collectCoinsButton.onClick += OnCollectCoins;
            m_collectCoinsRewardButton.onClick += OnCollectRewardedClick;
            m_jackpotButton.onClick += OnJackpotClick;


            if (ApplicationManager.datas.jackpotProgression == ApplicationManager.config.game.gamesForJackpot)
            {
                m_isJackpotReady = true;
            }

            float fillAmount = (float)ApplicationManager.datas.jackpotProgression / ApplicationManager.config.game.gamesForJackpot;
            m_jackpotFillBar.fillAmount = fillAmount;
            m_jackpotFillBarParticlesContainer.localScale = new Vector3(fillAmount, 1f, 1f);

            feedback = FindObjectOfType<FeedbackManager>();

            base.Awake();
        }

        protected override void OnDestroy()
        {
            UIManager.GetPopup<CharacterUnlockPopup>().onPopupClosed -= OnOtherPopupClosed;
            UIManager.GetPopup<NewWorldPopup>().onPopupClosed -= OnOtherPopupClosed;
            m_collectCoinsButton.onClick += OnCollectCoins;
            m_collectCoinsRewardButton.onClick += OnCollectRewardedClick;
            m_jackpotButton.onClick += OnJackpotClick;

            base.OnDestroy();
        }

        protected void Start()
        {
            m_endPanelContainerBaseYPosition = m_panelContainer.localPosition.y;
            m_endPanelContainerScreenYDelta = -m_panelContainer.rect.height;

            m_panelContainer.localPosition += Vector3.up * m_endPanelContainerScreenYDelta;

            StartCoroutine(TextInit());
        }

        private void OnEnable()
        {
            m_hasCollectCoins = false;

            m_panelContainer.DOLocalMoveY(m_endPanelContainerBaseYPosition, 0.5f).SetEase(Ease.OutBack).onComplete += PanelAnimationEnd;

            m_jackpotButtonParticles.Stop();

            m_topSunBurst.DORotate(Vector3.forward * 6000.0f, 300.0f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
            m_topParticles.Play();

            m_holeMultiplierText.gameObject.SetActive(false);
            m_multiplierNumberText.gameObject.SetActive(false);

            m_collectCoinsButton.isInteractable = true;
            //m_collectCoinsRewardButton.isInteractable = true;

           // m_collectCoinsRewardButton.gameObject.SetActive(true);

            UpdateJackpotProgression();

            UISounds.PlayEndGameSound();
            if (GPlayPassManager.IsActive)
            {
               // m_collectCoinsRewardButton.gameObject.SetActive(false);
                m_jackpotButton.gameObject.SetActive(false);
                m_collectCoinsButton.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isWaiting)
            {
                StartCoroutine(DelayedCollect());
            }
        }

        private IEnumerator DelayedCollect()
        {
            isWaiting = true;
            yield return new WaitForSeconds(0.1f);
            OnCollectCoins();
            isWaiting = false;
        }

        protected override void OnDisable()
        {
            m_topSunBurst.DOKill();
            m_topSunBurst.rotation = Quaternion.identity;
            m_topParticles.Stop();

            base.OnDisable();
        }

        private IEnumerator TextInit()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < m_textToResize.Length; i++)
            {
                m_textToResize[i].Resize();
            }
        }

        private void PanelAnimationEnd()
        {
            if (UIManager.sceneManager.characterUnlocked)
            {
                m_characterUnlockParticles.Play();
            }
            if (UIManager.sceneManager.newWorldUnlocked)
            {
                m_characterUnlockParticles.Play();
            }
        }

        public float Close()
        {
            float closeTime = 0.5f;
            m_panelContainer.DOLocalMoveY(m_endPanelContainerScreenYDelta, closeTime).SetEase(Ease.InBack);
            return closeTime;
        }

        bool m_characterUnlocked = false;
        private bool m_rewardedVideoRewarded;

        public void SetScore(int score, int earnedCoins, int earnedDiamonds, GameManager.HoleScores holeScore, WorldElement.WorldElementType elementType)
        {
            m_holeScoreMultiplier = 1;

            if (holeScore != GameManager.HoleScores.None)
            {
                if (m_birdImage != null)
                    m_birdImage.enabled = true;

                switch (holeScore)
                {
                    case GameManager.HoleScores.Birdie:
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.scored_birdie;
                        if (m_birdImage != null)
                            m_birdImage.sprite = m_birdImages[0];
                        m_holeScoreMultiplier = ApplicationManager.config.game.holescoreMultipliers[0];
                        break;
                    case GameManager.HoleScores.Eagle:
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.scored_eagle;
                        if (m_birdImage != null)
                            m_birdImage.sprite = m_birdImages[1];
                        m_holeScoreMultiplier = ApplicationManager.config.game.holescoreMultipliers[1];
                        break;
                    case GameManager.HoleScores.Albatros:
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.scored_albatros;
                        if (m_birdImage != null)
                            m_birdImage.sprite = m_birdImages[2];
                        m_holeScoreMultiplier = ApplicationManager.config.game.holescoreMultipliers[2];
                        break;
                    default:
                        break;
                }
                AnimateHoleMutliplierText();

                if (score >= ApplicationManager.config.game.characterUnlockDistance[Mathf.Min(ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId), ApplicationManager.config.game.characterUnlockDistance.Length - 1)]
                    && !(ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) > ApplicationManager.config.game.characterUnlockDistance.Length - 1))
                {
                    if (ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) < UIManager.sceneManager.GetMaxCharacterUnlock())
                    {
                        UIManager.sceneManager.lastUnlockedCharacter = ApplicationManager.datas.UnlockRandomCharacter();
                        if (onCharacterUnlocked != null)
                            onCharacterUnlocked.Invoke(UIManager.sceneManager.lastUnlockedCharacter);

                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.new_character;
                        UIManager.sceneManager.characterUnlocked = true;
                    }
                    else if (!UIManager.sceneManager.IsLastPlanet())
                    {
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.new_planet;
                        UIManager.sceneManager.newWorldUnlocked = true;
                    }
                    ApplicationManager.datas.IncrementWorldCurrentGoal(ApplicationManager.datas.selectedWorldId);
                }
            }
            else
            {
                if (m_birdImage != null)
                    m_birdImage.enabled = false;
                switch (elementType)
                {
                    case WorldElement.WorldElementType.Fair:
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.on_fairway;
                        break;
                    case WorldElement.WorldElementType.Bunker:
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.on_bunker;
                        break;
                    case WorldElement.WorldElementType.Water:
                        m_endGameTooltipText.text = I2LocManager.GetTermTranslation(ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId].waterName);
                        break;
                    case WorldElement.WorldElementType.Obstacle:
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.on_obstacle;
                        break;
                    case WorldElement.WorldElementType.Green:
                        m_endGameTooltipText.text = I2.Loc.ScriptLocalization.on_green;
                        break;
                }
            }


            if (ApplicationManager.datas.selectedBallId == 1 || ApplicationManager.datas.selectedBallId == 4)
            {
                //earnedCoins *= 3;
                m_goldenBallMultiplierImage.gameObject.SetActive(true);
                m_goldenBallParticles.Play();
            }
            else
            {
                m_goldenBallMultiplierImage.gameObject.SetActive(false);
                m_goldenBallParticles.Stop();
            }

            m_coinGain = (ulong)earnedCoins;
            m_coinGainText.UpdateValue(m_coinGain / (ulong)m_holeScoreMultiplier, m_coinGain / (ulong)m_holeScoreMultiplier);
            m_coinGainText.TriggerUpdate(0f);
        }

        private void AnimateHoleMutliplierText()
        {
            m_holeMultiplierText.text = "x" + m_holeScoreMultiplier.ToString();
            m_holeMultiplierText.gameObject.SetActive(true);
            m_holeMultiplierText.color = new Color(0f, 0f, 0f, 0f);
            m_holeMultiplierText.DOFade(1.0f, 0.2f).SetEase(Ease.InSine).SetDelay(0.4f);
            m_holeMultiplierText.rectTransform.localScale = Vector3.one * 20.0f;
            m_holeMultiplierText.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InSine).SetDelay(0.5f).OnComplete(HoleCoinMultiplier);
        }

        private void HoleCoinMultiplier()
        {
            if (ApplicationManager.datas.isVibrationActive)
            {
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
            }

            m_coinGainText.UpdateValue(m_coinGain / (ulong)m_holeScoreMultiplier, m_coinGain);
            m_coinGainText.TriggerUpdate(1f);
        }

        private void OnCollectCoins()
        {
            if (!m_hasCollectCoins)
            {
                m_hasCollectCoins = true;
                DisableCollectButtons();
                m_jackpotFillBar.DOComplete(true);
                m_jackpotFillBarParticlesContainer.DOComplete(true);
                UISounds.PlayCoinGainSound();

                UIManager.sceneManager.AddCoins(m_coinGain);
                feedback.OnCollectCoinFeedback(m_coinGain);

                if (ApplicationManager.datas.isVibrationActive)
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);

                StartCoroutine(OpenOtherPopup());
            }
        }

        private IEnumerator OpenOtherPopup()
        {
            if (UIManager.sceneManager.characterUnlocked)
            {
                m_topParticles.Stop();
                yield return new WaitForSeconds(0.5f);
                UIManager.OpenPopup<CharacterUnlockPopup>().AnimateSlide(UIManager.sceneManager.lastUnlockedCharacter);
            }
            else if (UIManager.sceneManager.newWorldUnlocked)
            {
                m_topParticles.Stop();
                yield return new WaitForSeconds(0.5f);
                UIManager.OpenPopup<NewWorldPopup>();
            }
            else
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(WaitForReset(1f));
            }
        }

        private void OnOtherPopupClosed(AUIPopup popup)
        {
            UIManager.sceneManager.ResetGame();
        }

        private IEnumerator WaitForReset(float time)
        {
            yield return new WaitForSeconds(time);

            UIManager.sceneManager.ResetGame();
        }

        private void DisableCollectButtons()
        {
            m_collectCoinsButton.isInteractable = false;
            //m_collectCoinsRewardButton.isInteractable = false;
            m_jackpotButton.isInteractable = false;
        }


        #region Collect x3

        private void OnCollectRewardedClick()
        {
            if (ApplicationManager.datas.vip)
            {
                DisableCollectButtons();
                AnimateRewardMutliplierText("x3", 3);
                return;
            }
            //else if (UIManager.sceneManager.ShowRewardedVideo(OnCollectVideoEnd))
            //{
            //	DisableCollectButtons();
            //	m_rewardedVideoRewarded = false;
            //}
            EGameEvent.Invoke(EEvents.RewardCollectRewardMultiply, new EData(this));
        }

        public void OnCollectRewardMultiply(bool succeed)
        {
            if (succeed)
            {
                DisableCollectButtons();
                m_rewardedVideoRewarded = true;
                UIManager.sceneManager.rewardedShown = true;
                AnimateRewardMutliplierText("x3", 3);
                this.UIManager.CloseProcessingPopup();
            }
            else
            {
                m_rewardedVideoRewarded = false;
                m_collectCoinsButton.isInteractable = true;
               // m_collectCoinsRewardButton.isInteractable = true;
                this.UIManager.CloseProcessingPopup();
            }
        }

        private IEnumerator WaitToCollectCoins(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            OnCollectCoins();

            yield return false;
        }

        private void OnCollectVideoEnd( /*AdsManagement.RewardedVideoEvent adEvent*/ )
        {
            //switch (adEvent)
            //{
            //	case AdsManagement.RewardedVideoEvent.Closed:
            //		if (!m_rewardedVideoRewarded)
            //		{
            //			m_collectCoinsButton.isInteractable = true;
            //			m_collectCoinsRewardButton.isInteractable = true;
            //			this.UIManager.CloseProcessingPopup();
            //		}
            //		break;

            //	case AdsManagement.RewardedVideoEvent.Failed:
            //		m_collectCoinsButton.isInteractable = true;
            //		m_collectCoinsRewardButton.isInteractable = true;
            //		this.UIManager.CloseProcessingPopup();
            //		break;

            //	case AdsManagement.RewardedVideoEvent.Rewarded:

            //		m_rewardedVideoRewarded = true;
            //		UIManager.sceneManager.rewardedShown = true;
            //		AnimateRewardMutliplierText("x3", 3);
            //		this.UIManager.CloseProcessingPopup();
            //		break;

            //	default:
            //		break;
            //}
        }

        private void AnimateRewardMutliplierText(string text, int multiplier)
        {
            m_multiplierNumberText.text = text;
            m_multiplierNumberText.gameObject.SetActive(true);
            m_multiplierNumberText.rectTransform.localScale = Vector3.one * 20.0f;
            m_multiplierNumberText.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InSine).OnComplete(() => CollectCoinMultiplier(multiplier));
        }

        private void CollectCoinMultiplier(int multiplier)
        {
            if (ApplicationManager.datas.isVibrationActive)
            {
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
            }

            m_coinGainText.UpdateValue(m_coinGain, m_coinGain * (ulong)multiplier);
            m_coinGainText.TriggerUpdate(1f);
            StartCoroutine(WaitToCollectCoins(1f));
            m_coinGain *= (ulong)multiplier;
        }

        #endregion


        #region Jackpot

        private void UpdateJackpotProgression()
        {
            int gamesForJackpot = ApplicationManager.config.game.gamesForJackpot;

            if (m_isJackpotReady)
            {
               // m_collectCoinsRewardButton.gameObject.SetActive(false);
                UnlockJackpotButton();
            }

            if (ApplicationManager.datas.jackpotProgression < gamesForJackpot)
            {
                ApplicationManager.datas.jackpotProgression++;
                if (ApplicationManager.datas.jackpotProgression == gamesForJackpot)
                {
                    m_isJackpotReady = true;
                   // m_collectCoinsRewardButton.gameObject.SetActive(false);
                    m_collectCoinsButton.gameObject.SetActive(false);
                }

                StartCoroutine(PlayJackpotFillBarParticles());
                float fillAmount = (float)ApplicationManager.datas.jackpotProgression / gamesForJackpot;
                m_jackpotFillBarParticlesContainer.DOScaleX(fillAmount, 1.0f).SetEase(Ease.InOutSine).SetDelay(0.2f);
                m_jackpotFillBar.DOFillAmount(fillAmount, 1.0f).SetEase(Ease.InOutSine).SetDelay(0.2f).OnComplete(UnlockJackpotButton);
            }
        }

        private void UnlockJackpotButton()
        {
            if (m_isJackpotReady)
            {
                m_jackpotButton.gameObject.SetActive(true);
                m_jackpotButton.transform.localScale = Vector3.zero;
                m_jackpotButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InQuart);

                m_collectCoinsButton.gameObject.SetActive(true);
                m_collectCoinsButton.transform.localScale = Vector3.zero;
                m_collectCoinsButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InQuart).OnComplete(() => {
                    m_jackpotButton.isInteractable = true;
                    m_jackpotButtonParticles.Play();
                });
            }
        }

        private void OnJackpotClick()
        {
            m_jackpotButton.isInteractable = false;
            m_jackpotButtonParticles.Stop();
            m_topParticles.Stop();

            UIManager.sceneManager.OpenJackpotPanel(m_coinGain);
        }

        public void OnCloseJackpotPopup(int jackpotMultiplier, bool hasPlayedJackpot)
        {
            if (!hasPlayedJackpot)
            {
                m_jackpotButton.isInteractable = true;
                m_jackpotButtonParticles.Play();
            }
            else
            {
                m_jackpotButton.transform.localScale = Vector3.zero;
                m_jackpotButton.gameObject.SetActive(false);
                AnimateRewardMutliplierText(string.Format("x{0} !", jackpotMultiplier), jackpotMultiplier);
                m_jackpotFillBar.fillAmount = 0f;
                m_jackpotFillBarParticlesContainer.localScale = new Vector3(0f, 1f, 1f);
                m_jackpotButton.gameObject.SetActive(false);
               // m_collectCoinsRewardButton.gameObject.SetActive(true);
                DisableCollectButtons();
                m_isJackpotReady = false;
            }
            m_topParticles.Play();
        }

        private IEnumerator PlayJackpotFillBarParticles()
        {
            yield return new WaitForEndOfFrame();

            GameObject parts = Instantiate(m_jackpotFillBarParticles, m_jackpotFillBarParticlesContainer);
            parts.transform.localPosition = Vector3.right * m_jackpotFillBarParticlesContainer.rect.width;
        }

        #endregion

        public override bool OnBackAction()
        {
            if (!m_hasCollectCoins)
            {
                OnCollectCoins();
            }

            return false;
        }
    }
}
