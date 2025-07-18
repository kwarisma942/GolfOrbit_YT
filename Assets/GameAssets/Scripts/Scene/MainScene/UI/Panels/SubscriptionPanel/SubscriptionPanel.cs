using System.Collections;
using UnityEngine;
using Pinpin.UI;
using IAPProduct = Pinpin.GameAssets.IAPAsset;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public class SubscriptionPanel : AUIPanel
	{
		[SerializeField] private PushButton m_restorePurchaseButton;
		[SerializeField] private PushButton m_restorePurchaseButton2;
		private IAPProduct pendingIAPProduct;
		[SerializeField] private TextResizer[] m_textToResize;

		[Header("Design 1")]
		[SerializeField] private GameObject m_panel1;

		[Header("Design 2")]
		[SerializeField] private Text m_title;
		[SerializeField] private GameObject m_panel2;
		[SerializeField] private GameObject m_subPanel1;
		[SerializeField] private GameObject m_subPanel2;

		[SerializeField] private GameObject m_type1;
		[SerializeField] private GameObject m_type2;
		[SerializeField] private GameObject m_type3;
		[SerializeField] private Text m_startText;

		[Header("Common")]
		[SerializeField] private GameObject m_allContent;

		[SerializeField] private GameObject m_startAnimation;
		[SerializeField] private GameObject m_bigTitle;
		[SerializeField] private bool m_isNewDesing;


		private bool m_isWelcomePopup;
		string m_creativeName = "sub";

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected virtual void Start ()
		{
#if UNITY_ANDROID
			m_restorePurchaseButton.gameObject.SetActive(false);
			m_restorePurchaseButton2.gameObject.SetActive(false);
#else
			m_restorePurchaseButton.onClick += OnRestorePurchaseClick;
			m_restorePurchaseButton2.onClick += OnRestorePurchaseClick;
#endif
			SubscriptionCard.onBuyProduct += OnBuyIAP;

			StartCoroutine(TextInit());
		}

		protected override void OnDisable ()
		{
			if(m_isWelcomePopup && ApplicationManager.config.game.enableStartAnimation && m_startAnimation != null)
			{
				Instantiate(m_startAnimation, Camera.main.transform);
			}
		}

		protected void OnEnable()
        {
			if (GPlayPassManager.IsActive)
				gameObject.SetActive(false);
		}

		protected override void OnDestroy ()
		{
#if !UNITY_ANDROID
			m_restorePurchaseButton.onClick -= OnRestorePurchaseClick;
			m_restorePurchaseButton2.onClick -= OnRestorePurchaseClick;
#endif
			SubscriptionCard.onBuyProduct -= OnBuyIAP;
			base.OnDestroy();
		}

		private IEnumerator TextInit ()
		{
			yield return new WaitForEndOfFrame();

			for (int i = 0; i < m_textToResize.Length; i++)
			{
				m_textToResize[i].Resize();
			}
		}

		public void Setup ( int mode, int subMode, int type, bool isEditor = false )
		{
			m_isWelcomePopup = false;
			if (mode == -1) //clear
			{
				m_panel1.SetActive(false);
				m_panel2.SetActive(false);
			}
			else if (mode == 0) // big popup
			{
				m_panel1.SetActive(true);
				if(m_allContent != null)
				{
					m_allContent.SetActive(true);
				}
				m_panel2.SetActive(false);
				m_creativeName = "sub_complete";
			}
			else if (mode == 1) // small popup
			{
				m_panel1.SetActive(false);
				m_panel2.SetActive(true);

				if (subMode == 0) // all content
				{
					if (m_isNewDesing)
						m_title.gameObject.SetActive(false);
					if (m_bigTitle != null)
						m_bigTitle.gameObject.SetActive(true);
					if (m_subPanel1 != null)
						m_subPanel1.SetActive(true);
					m_subPanel2.SetActive(false);
					if (m_allContent != null)
						m_allContent.SetActive(true);

					if (type == 0)
					{
						m_title.text = I2.Loc.ScriptLocalization.welcome_title;
						m_isWelcomePopup = true;
						m_creativeName = "sub_boot";
					}
					else if (type == 1)
					{
						m_title.text = I2.Loc.ScriptLocalization.vip_advantages_title;
						m_creativeName = "sub_weekly_push";
					}
				}
				else if (subMode == 1)  // specific content
				{
					if (m_isNewDesing)
						m_title.gameObject.SetActive(true);
					if (m_bigTitle != null)
						m_bigTitle.gameObject.SetActive(false);
					if (m_subPanel1 != null)
						m_subPanel1.SetActive(false);

					m_subPanel2.SetActive(true);
					if (m_allContent != null)
						m_allContent.SetActive(false);

					m_type1.SetActive(false);
					m_type2.SetActive(false);
					m_type3.SetActive(false);

					switch (type)
					{
						case 0:
							m_type1.SetActive(true);
							m_title.text = I2.Loc.ScriptLocalization.unlimited_golden_ball_title;
							m_creativeName = "sub_golden_ball";
							break;
						case 1:
							m_type2.SetActive(true);
							m_title.text = I2.Loc.ScriptLocalization.vip_character_title;
							m_creativeName = "sub_char";
							break;
						case 2:
							m_type3.SetActive(true);
							m_title.text = I2.Loc.ScriptLocalization.remove_ads_title;
							m_creativeName = "sub_inter";
							break;
					}
				}
			}
            //if (!isEditor)
                //AnalyticsManager.NewDesignEvent("sub_display", "creative name", m_creativeName);
		}

		private void OnRestorePurchaseClick ()
		{
			this.UIManager.OpenProcessingPopup(I2.Loc.ScriptLocalization.restoring_purchases_loading);
			PurchasingManager.RestorePurchases(OnPurchaseRestored);
		}

		private void OnBuyIAP ( IAPProduct.Id IAPProductId )
		{

			this.pendingIAPProduct = ApplicationManager.assets.inAppProducts[(int)IAPProductId];

#if DEBUG
			Debug.Log("SubscriptionPanel - Initiate buy of IAP product [" + this.pendingIAPProduct.productId + "]");
#endif
			/*if (m_isWelcomePopup)
				AnalyticsManager.NewDesignEvent("sub_click_startbutton", "creative name", m_creativeName);
			AnalyticsManager.NewDesignEvent("sub_click_startbuttonglobal", "creative name", m_creativeName);*/

			//string name = this.pendingIAPProduct.name;
			//string price = PurchasingManager.getProductPriceString(this.pendingIAPProduct.productId);
			this.BuyIAP();

			/*PurchaseConfirmationPopup popup = this.UIManager.OpenPopup<PurchaseConfirmationPopup>();

			popup.Configure(name, price, PurchaseConfirmationPopup.Currency.Fiat);
			popup.onConfirm += this.BuyIAP;*/
		}

		private void BuyIAP ()
		{
#if DEBUG
			Debug.Log("SubscriptionPanel - Buying IAP product [" + this.pendingIAPProduct.productId + "]");
#endif
			this.UIManager.OpenProcessingPopup(I2.Loc.ScriptLocalization.shop_loading);
			PurchasingManager.PurchaseProduct(this.pendingIAPProduct.productId, this.OnIAPPurchaseComplete);

			//AnalyticsManager.NewDesignEvent("Shop:IAP:StartPurchase:" + this.pendingIAPProduct.productId);
		}

		private void OnIAPPurchaseComplete ( PurchasingManagement.PurchaseEvent IAPEvent )
		{
			this.UIManager.CloseProcessingPopup();
			/*PurchaseCompletePopup popup = this.UIManager.OpenPopup<PurchaseCompletePopup>();
			popup.onClosed += this.OnPurchaseCompletePopupClosed;*/

			if (IAPEvent.type == PurchasingManagement.PurchaseEvent.Status.Fail)
			{
#if DEBUG
				Debug.Log("SubscriptionPanel - Product [" + this.pendingIAPProduct.productId + "] purchase failed.");
#endif
				//popup.status = PurchaseCompletePopup.Status.Fail;
				/*AnalyticsManager.NewDesignEvent("Shop:IAP:PurchaseFailed:" + this.pendingIAPProduct.productId);
				AnalyticsManager.NewDesignEvent("sub_CancelPurchase", "creative name", m_creativeName);*/
			}
			else
			{
#if DEBUG
				Debug.Log("SubscriptionPanel - Product [" + this.pendingIAPProduct.productId + "] purchase success.");
#endif
				//popup.status = PurchaseCompletePopup.Status.Success;
				//AnalyticsManager.NewDesignEvent("Shop:IAP:PurchaseSuccess:" + this.pendingIAPProduct.productId);

				if (!ApplicationManager.datas.firstTimeVIPPurchaseDone)
				{
					//AnalyticsManager.NewDesignEvent("sub_StartPurchase", "creative name", m_creativeName);
					ApplicationManager.datas.firstTimeVIPPurchaseDone = true;
				}
				else
				{
					//AnalyticsManager.NewDesignEvent("sub_ReactivatePurchase", "creative name", m_creativeName);
				}
				UIManager.sceneManager.UpdateBall();
				UIManager.Back();
			}
		}

		private void OnPurchaseCompletePopupClosed ()
		{
			/*UISoftCurrencyUpdater.TriggerUpdate();
			UIHardCurrencyUpdater.TriggerUpdate();
			m_noAdsItem.gameObject.SetActive(!GameManager.datas.noAds);
			GameManager.datas.SaveDatas();*/
		}


		private void OnPurchaseRestored(bool complete)
		{
            this.UIManager.CloseProcessingPopup();
            InformationPopup informationPopup = UIManager.OpenPopup<InformationPopup>();
			informationPopup.message = I2.Loc.ScriptLocalization.purchase_restored;
			informationPopup.onClosed += OnRestorePurchasePopupClosed;
		}

		private void OnRestorePurchasePopupClosed()
		{
			if (ApplicationManager.datas.vip)
			{
				UIManager.sceneManager.UpdateBall();
				UIManager.Back();
			}
		}

		protected override void OnBackButtonPressed ()
		{
			//AnalyticsManager.NewDesignEvent("sub_click_cross", "creative name", m_creativeName);
			base.OnBackButtonPressed();
		}
	}
}
