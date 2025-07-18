using DG.Tweening;
using Pinpin.Helpers;
using Pinpin.Scene.MainScene.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.UI
{
	[RequireComponent(typeof(PushButton))]
	public class UIShopButton : MonoBehaviour
	{

		public static Action forceUpdate;
		public GameObject RvButon;
		public enum CurrencyType
		{
			Coins, Diamonds,
		}

		public enum UpgradeType
		{
			Strength, Speed, Bounce, GoldenBall,
		}

		
		[SerializeField] private UpgradeType m_upgradeType;
		[SerializeField] private CurrencyType m_currencyType;
		[SerializeField] private PushButton m_button;
		[SerializeField] private RectTransform m_shopButtonContainer;
		[SerializeField] private Text m_costText;
		[SerializeField] private string m_beforeProgressionText;
		[SerializeField] private string m_afterProgressionText;
		[SerializeField] private Text m_progression;
		[SerializeField] private bool m_enableNotifications;
		[SerializeField] private Image m_notificationImage;
		[SerializeField] private Text m_notificationText;
		[SerializeField] private Image m_notificationShineImage;
		[SerializeField] private ParticleSystem m_levelUpFX;

		private Sequence m_notificationShineSequence;
        private static int s_rvUpgradeCounter = 0;

        public System.Action onPurchase;

		[SerializeField] private ulong m_cost;
		public ulong cost
		{
			get { return m_cost; }
			set
			{
				m_costText.text = MathHelper.ConvertToEgineeringNotation(value);//.ToString("n0", ApplicationManager.currentCulture);
				m_cost = value;
			}
		}

		public int level
		{
			get;
			private set;
		}

		private void Start ()
		{
			SetState();
			m_button.onClick += OnClick;
			forceUpdate += UpdateValues;

			if (ApplicationManager.IsFirstTimeLaunch())
			{
				DOVirtual.DelayedCall(1.5f,CheckForFirstTimeRVButton);
			}
		}

		private void OnDestroy ()
		{
			m_button.onClick -= OnClick;
			forceUpdate -= UpdateValues;
		}

		private void OnClick ()
		{
			bool success = false;
			if (m_upgradeType == UpgradeType.GoldenBall)
			{
				success = OnBonusPurchase();
			}
			else
			{
				success = OnUpgradePurchase();
			}

			if (success && onPurchase != null)
			{
				onPurchase.Invoke();
			}
		}

		private void OnEnable ()
		{
			UpdateValues();
		}

		private void UpdateValues()
		{
			if(m_upgradeType != UpgradeType.GoldenBall)
			{
				level = ApplicationManager.datas.GetUpgradeLevel(m_upgradeType);
				cost = ApplicationManager.config.game.GetUpgradePrice(level);
				m_progression.text = GetProgressionText();
			}
			else
			{
				cost = m_cost;
			}

			SetState();
		}

		private void OnDisable ()
		{
			m_shopButtonContainer.localScale = Vector3.one;
		}

		private bool OnUpgradePurchase ()
		{
			if (cost <= ApplicationManager.datas.coins)
			{
				++level;
				ApplicationManager.datas.SetUpgradeLevel(m_upgradeType, level);

				ApplicationManager.datas.coins -= cost;
				cost = ApplicationManager.config.game.GetUpgradePrice(level);

				m_progression.text = GetProgressionText();
				PlayLevelUpFX();
				return true;
			}
			return false;
		}

		public void RvUpgrade()
		{
			//FGMediation.ShowRewarded((status) =>
			//{
			//    Time.timeScale = 1f;
			//    if (status)
			//    {
			//        level += 2;
			//        ApplicationManager.datas.SetUpgradeLevel(m_upgradeType, level);
			//        cost = ApplicationManager.config.game.GetUpgradePrice(level);
			//        m_progression.text = GetProgressionText();
			//        PlayLevelUpFX();
			//    }

			//});
			EGameEvent.Invoke(EEvents.RewardRVUpgrade, new EData(this));

        }

		public void OnRewardedRVUpgrade(bool succeed)
		{
			if (succeed)
			{
                level += 2;
				ApplicationManager.datas.SetUpgradeLevel(m_upgradeType, level);
				cost = ApplicationManager.config.game.GetUpgradePrice(level);
				m_progression.text = GetProgressionText();
				PlayLevelUpFX();

				CounterCheck();
            }
        }

		void CounterCheck() 
		{
            s_rvUpgradeCounter++;
            Debug.Log("UIShopButton COunter " + s_rvUpgradeCounter);
            if (s_rvUpgradeCounter >= 3)
            {
                MainPanel mainPanel = FindObjectOfType<MainPanel>();
                mainPanel.DisableRvButtons();
            }
        }
		private bool OnBonusPurchase ()
		{

			if (cost <= (ulong)ApplicationManager.datas.diamonds )
			{
				ApplicationManager.datas.diamonds -= (int)cost;
				SetState();
				return true;
			}
			return false;
		}
        public static void ResetRVCount()
        {
            s_rvUpgradeCounter = 0;
        }
        public void SetState ()
		{
			if (GetCurrencyAmount() >= cost)
			{
				EnableButton();
			}
			else
			{
				DisableButton();
			}
		}

		public void PlayLevelUpFX ()
		{
			if (ApplicationManager.config.game.useNewFXs)
			{
				m_levelUpFX.Play();
			}
		}

		private void DisableButton ()
		{
			m_button.isInteractable = false;
			m_button.Fade(0.5f, 0f);
			if (RvButon)
			{
				if (ExampleRemoteConfigABtests._instance.ShowUpgrade &&!GPlayPassManager.IsActive)
					RvButon.SetActive(true);
				else
					RvButon.SetActive(false);

			}
			if (m_enableNotifications)
			{
				StopNotificationAnimation();
			}

			//ResetRVCount();
		}

		private void EnableButton ()
		{
			m_button.isInteractable = true;
			m_button.Fade(1f, 0f);
			if (RvButon)
            {
				RvButon.SetActive(false);

			}
			if (m_enableNotifications)
			{
				m_notificationImage.gameObject.SetActive(true);

				if(m_notificationShineSequence == null)
				{
					m_notificationShineSequence = DOTween.Sequence();
					m_notificationShineSequence.SetDelay(0.5f);
					m_notificationShineSequence.AppendInterval(0.8f);
					m_notificationShineSequence.Append(m_notificationShineImage.DOFade(0.8f, 0.4f).SetEase(Ease.InQuad));
					m_notificationShineSequence.Append(m_notificationShineImage.DOFade(0f, 0.4f).SetEase(Ease.OutQuad));
					m_notificationShineSequence.AppendInterval(0.8f);
					m_notificationShineSequence.SetLoops(-1);
				}
				else
				{
					m_notificationShineSequence.Restart();
				}

				SetNotificationValue();
			}
		}

		private void SetNotificationValue ()
		{
			int index = 0;
			ulong totalCost = ApplicationManager.config.game.GetUpgradePrice(level);

			while (totalCost <= GetCurrencyAmount())
			{
				index++;
				totalCost += ApplicationManager.config.game.GetUpgradePrice(level + index);
			}

			m_notificationText.text = index.ToString();
		}

		private void StopNotificationAnimation ()
		{
			m_notificationImage.gameObject.SetActive(false);
			m_notificationShineSequence.Complete();
			m_notificationShineSequence.Kill();
			m_notificationShineImage.color = new Color(1f, 1f, 1f, 0f);
		}


		private ulong GetCurrencyAmount ()
		{
			switch (m_currencyType)
			{
				case CurrencyType.Coins:
					return ApplicationManager.datas.coins;
				case CurrencyType.Diamonds:
					return (ulong)ApplicationManager.datas.diamonds;
				default:
					return ApplicationManager.datas.coins;
			}
		}

		private string GetProgressionText ()
		{
			int progression = 5 * level;
			switch (m_upgradeType)
			{
				case UpgradeType.Strength:
					return string.Format(I2.Loc.ScriptLocalization.strength_upgrade_tooltip, progression);
				case UpgradeType.Speed:
					return string.Format(I2.Loc.ScriptLocalization.speed_upgrade_tooltip, progression);
				case UpgradeType.Bounce:
					return string.Format(I2.Loc.ScriptLocalization.bounce_upgrade_tooltip, progression);
				default:
					return string.Format(I2.Loc.ScriptLocalization.bounce_upgrade_tooltip, progression);
			}
		}

		private void CheckForFirstTimeRVButton() 
		{
			RvButon.SetActive(false);
		}
	}

}

