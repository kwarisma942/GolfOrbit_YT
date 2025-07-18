using Pinpin;
using Pinpin.UI;
using UnityEngine;
using UnityEngine.UI;
using IAPProduct = Pinpin.GameAssets.IAPAsset;

[RequireComponent(typeof(PushButton))]
public class ShopCard : MonoBehaviour
{
	[SerializeField] private PushButton m_buyButton;
	[SerializeField] private IAPProduct.Id m_productID;
	[SerializeField] private Text m_productNameText;
	[SerializeField] private Text m_hardCurrencyAmountText;
	[SerializeField] private Text m_priceText;
	[SerializeField] private Text m_NewpriceText;
	[SerializeField] private GameObject NewPrice;
	[SerializeField] private PushButton m_fakeButton;
	[SerializeField] private Sprite m_normalSprite;
	[SerializeField] private Sprite m_mostPopularSprite;
	[SerializeField] private GameObject m_featuredContainer;
	[SerializeField] private Text m_featuredText;

	public static System.Action<IAPProduct.Id> onBuyProduct;

	private void Awake ()
	{
		if (ExampleRemoteConfigABtests._instance.UseNewIAP)
        {
			m_productID = (IAPProduct.Id)((int)m_productID + 1);
			if (NewPrice != null)
			{
				m_priceText.gameObject.SetActive(false);
				NewPrice.SetActive(true);
			}
		}

		IAPProduct product = ApplicationManager.assets.inAppProducts[(int)m_productID];

		m_buyButton.onClick += OnBuyButtonPressed;

		if (!ReferenceEquals(m_productNameText, null))
		{
			string name = PurchasingManager.getProductName(product.productId);
			if (name == null)
			{
				name = product.name;
			}
			//name = name.Replace(" (Golf Orbit)", "");
			//m_productNameText.text = name;
		}

		if (!ReferenceEquals(m_priceText, null))
		{
			string price = PurchasingManager.getProductPriceString(product.productId);
			if (price == null)
			{
				price = product.fakeUSPrice;
			}
			m_priceText.text = price ;
			m_NewpriceText.text = price ;
		}
		if (!ReferenceEquals(m_hardCurrencyAmountText, null))
		{
			m_hardCurrencyAmountText.text = product.hardCurrencyAmount.ToString();
		}

		if (product.isMostpopular)
		{
			if (!ReferenceEquals(m_fakeButton, null))
			{
				m_fakeButton.image.sprite = m_mostPopularSprite;
				m_fakeButton.image.color = Color.white;
				m_fakeButton.m_isShining = true;
			}
			if (!ReferenceEquals(m_featuredContainer, null))
			{
				m_featuredContainer.SetActive(true);
			}
			if (!ReferenceEquals(m_featuredText, null))
			{
				m_featuredText.text = I2.Loc.ScriptLocalization.most_popular;
			}
		}
		else if (product.isBestOffer)
		{
			if (!ReferenceEquals(m_fakeButton, null))
			{
				m_fakeButton.image.sprite = m_normalSprite;
				m_fakeButton.m_isShining = false;
			}
			if (!ReferenceEquals(m_featuredContainer, null))
			{
				m_featuredContainer.SetActive(true);
			}
			if(!ReferenceEquals(m_featuredText, null))
			{
				m_featuredText.text = I2.Loc.ScriptLocalization.best_offer;
			}
		}
		else
		{
			if(!ReferenceEquals(m_fakeButton, null))
			{
				m_fakeButton.image.sprite = m_normalSprite;
				m_fakeButton.m_isShining = false;
			}
			if(!ReferenceEquals(m_featuredContainer, null))
			{
				m_featuredContainer.SetActive(false);
			}
		}
	}

	private void OnDestroy ()
	{
		m_buyButton.onClick -= OnBuyButtonPressed;
	}

	public void OnBuyButtonPressed()
	{
		if (onBuyProduct != null)
			onBuyProduct.Invoke(m_productID);
	}
}
