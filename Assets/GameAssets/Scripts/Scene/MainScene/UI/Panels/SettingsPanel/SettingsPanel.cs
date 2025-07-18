using UnityEngine;
using Pinpin.UI;
using UnityEngine.UI;
using YTGameSDK;

namespace Pinpin.Scene.MainScene.UI
{

	public class SettingsPanel : AUIPanel {

        [SerializeField] private YTGameWrapper gameWrapper;
        [SerializeField] private GameManager m_gameManager;
		[SerializeField] private UISettingsButton m_enableSoundButton;
		[SerializeField] private UISettingsButton m_enableVibrationButton;
		[SerializeField] private PushButton m_restorePurchaseButton;
		[SerializeField] private PushButton m_vipSubscriptionButton;
		[SerializeField] private PushButton m_termsOfUsesButton;
		[SerializeField] private PushButton m_subscriptionTermsButton;
		[SerializeField] private DayTimeToggle[] m_daytimeToggles;
		[SerializeField] private GameObject m_themePickerDeactivator;
		[SerializeField] private PushButton m_giveMoneyButton;
		[SerializeField] private PushButton m_languageButton;

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		private void Start ()
		{
			//m_enableSoundButton.GetComponent<PushButton>().onClick += OnEnableSoundClick;
			m_enableVibrationButton.GetComponent<PushButton>().onClick += OnEnableVibrationClick;

            //if (m_enableSoundButton.gameObject.activeSelf) m_enableSoundButton.SetState(ApplicationManager.datas.isSoundActive);
			if (m_enableVibrationButton.gameObject.activeSelf) m_enableVibrationButton.SetState(ApplicationManager.datas.isVibrationActive);
#if UNITY_ANDROID
			m_restorePurchaseButton.interactable = false;
#else
			m_restorePurchaseButton.onClick += OnRestorePurchaseClick;
			m_restorePurchaseButton.interactable = ApplicationManager.networkReachable;
#endif
			m_vipSubscriptionButton.onClick += OnVIPSubsciptionButtonPressed;

			m_termsOfUsesButton.onClick += OntermsOfUsesButtonPressed;
			m_subscriptionTermsButton.onClick += OnSubscriptionTermsButtonPressed;

			m_giveMoneyButton.onClick += OnGiveMoneyButtonPressed;
			m_languageButton.onClick += OnLanguageButtonPressed;

			if (gameWrapper == null)
				gameWrapper = GameObject.Find("YTGameWrapper").GetComponent<YTGameWrapper>();

        }

        private void OnGiveMoneyButtonPressed()
		{
#if CAPTURE
			UIManager.sceneManager.AddCoins(10000000);
			UIManager.sceneManager.UpdateCurrencies();
#endif
		}

		private void OnLanguageButtonPressed ()
		{
			UIManager.OpenPopup<LanguagePopup>();
		}

		private void OntermsOfUsesButtonPressed ()
		{
			Application.OpenURL("https://www.quiet.fun/legal-notices/");
		}

		private void OnSubscriptionTermsButtonPressed ()
		{
			Application.OpenURL("https://www.quiet.fun/legal-notices/");
		}

		private void OnEnable ()
		{
			m_vipSubscriptionButton.interactable = !ApplicationManager.datas.vip;
			if (ApplicationManager.datas.selectedWorldId == 0)
			{
				int dayTime = (int)ApplicationManager.datas.dayTime;
				for (int i = 0; i < m_daytimeToggles.Length; i++)
				{
					m_daytimeToggles[i].isOn = i == dayTime - 1;
				}
				m_themePickerDeactivator.SetActive(false);
			}
			else
			{
				for (int i = 0; i < m_daytimeToggles.Length; i++)
				{
					m_daytimeToggles[i].isOn = i == 0;
				}
				m_themePickerDeactivator.SetActive(true);
			}
			DayTimeToggle.onDayTimeSelect += SetDayTime;

			if (GPlayPassManager.IsActive)
				m_vipSubscriptionButton.gameObject.SetActive(false);
		}

		private void OnVIPSubsciptionButtonPressed ()
		{
			if(!ApplicationManager.datas.vip)
			{
				SubscriptionPanel panel;
				if (ApplicationManager.config.game.subPopupABTest >= 0)
					panel = UIManager.OpenPanel<NewSubscriptionPanel>();
				else
					panel = UIManager.OpenPanel<SubscriptionPanel>();
				panel.Setup(0, 0, 0);

			}
		}

		protected override void OnDisable ()
		{
			ApplicationManager.SaveGameDatas();
			DayTimeToggle.onDayTimeSelect -= SetDayTime;
			base.OnDisable();
		}

		protected override void OnDestroy ()
		{
			//m_enableSoundButton.GetComponent<PushButton>().onClick -= OnEnableSoundClick;
			m_enableVibrationButton.GetComponent<PushButton>().onClick -= OnEnableVibrationClick;
#if !UNITY_ANDROID
			m_restorePurchaseButton.onClick -= OnRestorePurchaseClick;
#endif
			m_vipSubscriptionButton.onClick -= OnVIPSubsciptionButtonPressed;
			m_termsOfUsesButton.onClick -= OntermsOfUsesButtonPressed;
			m_subscriptionTermsButton.onClick -= OnSubscriptionTermsButtonPressed;
			m_giveMoneyButton.onClick -= OnGiveMoneyButtonPressed;
			m_languageButton.onClick -= OnLanguageButtonPressed;
			base.OnDestroy();
		}

		private void OnEnableSoundClick ()
		{
			ApplicationManager.datas.isSoundActive = !ApplicationManager.datas.isSoundActive;
			//m_enableSoundButton.SetState(ApplicationManager.datas.isSoundActive);
			//gameWrapper.NotifyAudioStateChange(ApplicationManager.datas.isSoundActive);
		}

        private void OnEnableVibrationClick ()
		{
			ApplicationManager.datas.isVibrationActive = !ApplicationManager.datas.isVibrationActive;
			m_enableVibrationButton.SetState(ApplicationManager.datas.isVibrationActive);
		}

		private void OnRestorePurchaseClick ()
		{
			PurchasingManager.RestorePurchases(OnPurchaseRestored);
		}

		private void OnPurchaseRestored ( bool complete )
		{
			InformationPopup informationPopup = UIManager.OpenPopup<InformationPopup>();
			if (complete)
			{
				informationPopup.message = I2.Loc.ScriptLocalization.purchase_restored;
			}
			else
			{
				informationPopup.message = "Restoring purchases failed";
			}
			informationPopup.onClosed += OnRestorePurchasePopupClosed;
		}

		private void OnRestorePurchasePopupClosed ()
		{
			if (ApplicationManager.datas.vip)
			{
				UIManager.sceneManager.UpdateBall();
			}
		}

		private void SetDayTime ( DaytimeMaterial.DayTime dayTime )
		{
			for (int i = 0; i < m_daytimeToggles.Length; i++)
			{
				if (m_daytimeToggles[i].dayTime != dayTime)
					m_daytimeToggles[i].isOn = false;
			}
			m_gameManager.ChangeDayTime( dayTime );
		}

	}

}