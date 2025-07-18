using DG.Tweening;
using Pinpin.Helpers;
using Pinpin.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using IAPProduct = Pinpin.GameAssets.IAPAsset;
using ShopProduct = Pinpin.GameAssets.ShopItemAsset;

namespace Pinpin.Scene.MainScene.UI
{

	public sealed class ShopPanel : AUIPanel
	{
		[SerializeField] private CanvasGroup m_canvasGroup;
		[SerializeField] private Text m_diamondText;
		[SerializeField] private Text m_coinText;
		[SerializeField] private ParticleSystem m_coinParticles;
		[SerializeField] private ParticleSystem m_coinExplosionParticles;
		[SerializeField] private ParticleSystem m_gemParticles;
		[SerializeField] private ParticleSystem m_explosionParticles;

		[FormerlySerializedAs("m_productTexts")]
		[SerializeField] private TextResizer[] m_textToResize;

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void Awake ()
		{
			m_canvasGroup.alpha = 0f;
			base.Awake();
		}

		private void Start ()
		{
			ShopCard.onBuyProduct += OnBuyIAP;
			ShopCardHC.onBuyProduct += OnBuyHCProduct;
			StartCoroutine(TextInit());
		}

		private void OnEnable ()
		{
			UpdateDiamonds();
			UpdateCoins();
			m_canvasGroup.DOFade(1f, 0.2f);
            if (GPlayPassManager.IsActive)
            {
				gameObject.SetActive(false);
            }
		}

		protected override void OnDestroy ()
		{
			ShopCard.onBuyProduct -= OnBuyIAP;

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

		protected override void OnBackButtonPressed ()
		{
			UIManager.sceneManager.UpdateCurrencies();
			m_canvasGroup.DOFade(0f, 0.2f).onComplete += base.OnBackButtonPressed;
		}

		public override bool OnBackAction ()
		{
			return base.OnBackAction();
		}

		#region IAP

		private IAPProduct pendingIAPProduct { get; set; }

		private void OnBuyIAP ( IAPProduct.Id IAPProductId )
		{

			this.pendingIAPProduct = ApplicationManager.assets.inAppProducts[(int)IAPProductId];

#if DEBUG
			Debug.Log("ShopPanel - Initiate buy of IAP product [" + this.pendingIAPProduct.productId + "]");
#endif

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
			Debug.Log("ShopPanel - Buying IAP product [" + this.pendingIAPProduct.productId + "]");
#endif
			this.UIManager.OpenProcessingPopup(I2.Loc.ScriptLocalization.shop_loading);
			PurchasingManager.PurchaseProduct(this.pendingIAPProduct.productId, this.OnIAPPurchaseComplete);
		}

		private void OnIAPPurchaseComplete ( PurchasingManagement.PurchaseEvent IAPEvent )
		{
			this.UIManager.CloseProcessingPopup();
			/*PurchaseCompletePopup popup = this.UIManager.OpenPopup<PurchaseCompletePopup>();
			popup.onClosed += this.OnPurchaseCompletePopupClosed;*/

			if (IAPEvent.type == PurchasingManagement.PurchaseEvent.Status.Fail)
			{
#if DEBUG
				Debug.Log("ShopPanel - Product [" + this.pendingIAPProduct.productId + "] purchase failed.");
#endif
				//popup.status = PurchaseCompletePopup.Status.Fail;
			}
			else
			{
#if DEBUG
				Debug.Log("ShopPanel - Product [" + this.pendingIAPProduct.productId + "] purchase success.");
#endif
				//popup.status = PurchaseCompletePopup.Status.Success;
				UpdateDiamonds();
				m_gemParticles.Play();
				m_explosionParticles.Play();
				UIManager.sceneManager.UpdateCurrencies();
				UISounds.PlayCoinGainSound();
			}
		}

		private void OnPurchaseCompletePopupClosed ()
		{
			/*UISoftCurrencyUpdater.TriggerUpdate();
			UIHardCurrencyUpdater.TriggerUpdate();
			m_noAdsItem.gameObject.SetActive(!GameManager.datas.noAds);
			GameManager.datas.SaveDatas();*/
		}

		private void OnRestorePurchaseButtonPressed ()
		{
			this.UIManager.OpenProcessingPopup(I2.Loc.ScriptLocalization.restoring_purchases_loading);
			PurchasingManager.RestorePurchases(this.OnRestorePurchaseDone);
		}

		private void OnRestorePurchaseDone ( bool result )
		{
			this.UIManager.CloseProcessingPopup();
			//this.UIManager.OpenPopup<PurchaseRestoredPopup>();
			ApplicationManager.datas.SaveDatas();
		}

		#endregion

		#region HC Products
		private void OnBuyHCProduct( ShopProduct.Id id)
		{
			switch (id)
			{
				case ShopProduct.Id.ChestOfGold:
					ShopProduct product = ApplicationManager.assets.shopItems[(int)id];
					if(product.hardCurrencyPrice <= ApplicationManager.datas.diamonds)
					{
						ApplicationManager.datas.diamonds -= product.hardCurrencyPrice;
						ApplicationManager.datas.coins += (ulong)product.softCurrencyAmount;
						ApplicationManager.datas.SaveDatas();
						UpdateDiamonds();
						UpdateCoins();
						UIManager.sceneManager.UpdateCurrencies();
						m_coinParticles.Play();
						m_coinExplosionParticles.Play();
						UISounds.PlayCoinGainSound();
					}
					break;
				default:
					break;
			}
		}
		#endregion

		public void UpdateDiamonds ()
		{
			m_diamondText.text = MathHelper.ConvertToEgineeringNotation((ulong)ApplicationManager.datas.diamonds);//ApplicationManager.datas.diamonds.ToString("n0", ApplicationManager.currentCulture);
		}

		public void UpdateCoins ()
		{
			m_coinText.text = MathHelper.ConvertToEgineeringNotation(ApplicationManager.datas.coins);//ApplicationManager.datas.coins.ToString("n0", ApplicationManager.currentCulture);
		}
		public void UpdateCurrencies()
		{
			UIManager.sceneManager.UpdateCurrencies();
			UpdateCoins();
			UpdateDiamonds();
		}

		
	}
}