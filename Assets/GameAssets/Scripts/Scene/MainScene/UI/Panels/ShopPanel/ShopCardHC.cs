using Pinpin;
using Pinpin.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using ShopProduct = Pinpin.GameAssets.ShopItemAsset;

public class ShopCardHC : MonoBehaviour
{
	[SerializeField] private PushButton m_buyButton;
	[SerializeField] private ShopProduct.Id m_productID;
	[SerializeField] private Text m_productNameText;
	[SerializeField] private Text m_priceText;
	[SerializeField] private Text m_softCurrencyAmountText;
	[SerializeField] private PushButton m_fakeButton;

	public static Action<ShopProduct.Id> onBuyProduct;

	private void Awake ()
	{
		ShopProduct product = ApplicationManager.assets.shopItems[(int)m_productID];
		m_buyButton.onClick += OnBuyButtonPressed;

		if (!ReferenceEquals(m_productNameText, null))
		{
			m_productNameText.text = LocalizationManager.GetTranslation(product.name);
		}
		if (!ReferenceEquals(m_priceText, null))
		{
			m_priceText.text = product.hardCurrencyPrice.ToString("n0", ApplicationManager.currentCulture);
		}
		if (!ReferenceEquals(m_softCurrencyAmountText, null))
		{
			m_softCurrencyAmountText.text = product.softCurrencyAmount.ToString("n0", ApplicationManager.currentCulture);
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

	private void UpdateStatus()
	{

	}

}
