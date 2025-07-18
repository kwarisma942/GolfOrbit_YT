using Pinpin;
using Pinpin.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IAPProduct = Pinpin.GameAssets.IAPAsset;

[RequireComponent(typeof(PushButton))]
public class SubscriptionCard : MonoBehaviour
{
	[SerializeField] private PushButton m_buyButton;
	[SerializeField] private IAPProduct.Id m_productID;
	[SerializeField] private Text m_priceText;
	[SerializeField] private Text m_NewpriceText;
	[SerializeField] private Text m_LastpriceText;
	[SerializeField] private GameObject NewPrice;

	public static Action<IAPProduct.Id> onBuyProduct;

	private void Awake ()
	{
		if (ExampleRemoteConfigABtests._instance.UseNewIAP)
        {
			m_productID = (IAPProduct.Id)((int)m_productID + 1);
			IAPProduct.Id lastM_productID = (IAPProduct.Id)((int)m_productID);
            if (NewPrice != null)
			{
				m_priceText.gameObject.SetActive(false);
				NewPrice.SetActive(true);
				IAPProduct product2 = ApplicationManager.assets.inAppProducts[(int)m_productID];
				if (!ReferenceEquals(m_NewpriceText, null))
				{
					string price = PurchasingManager.getProductPriceString(product2.productId);
					if (price == null)
					{
						price = product2.fakeUSPrice;
					}
					m_NewpriceText.text = price;
				}
			}

		}

		IAPProduct product = ApplicationManager.assets.inAppProducts[(int)m_productID];

		m_buyButton.onClick += OnBuyButtonPressed;
		
		if (!ReferenceEquals(m_priceText, null))
		{
			string price = PurchasingManager.getProductPriceString(product.productId);
			if (price == null)
			{
				price = product.fakeUSPrice;
			}
			m_priceText.text = price;
			if(m_LastpriceText)
				m_LastpriceText.text = price;
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
