using UnityEngine;
using IAP = PurchasingManagement.PurchasingManager;
using PurchaseEvent = PurchasingManagement.PurchaseEvent;

namespace Pinpin
{

	public static class PurchasingManager
	{

		private static IAP.OnProductBoughtDelegate onProductBoughtCallback { get; set; }
		private static IAP.OnPurchaseEventDelegate onPurchaseCallBack { get; set; }

		public static bool havePurchasingAgent { get { return (IAP.havePurchasingAgent); } }
		public static bool isInitialized { get { return (IAP.isInitialized); } }

		public static string getProductName ( string productID ) { return (IAP.getProductName(productID)); }
		public static string getProductPriceString ( string productID ) { return (IAP.getProductPriceString(productID)); }
		public static string getProductTransactionID( string productID ) { return (IAP.getProductTransactionID(productID)); }
		public static string getProductCurrencyCode( string productID ) { return (IAP.getProductCurrencyCode(productID)); }
		public static decimal getProductPrice ( string productID ) { return (IAP.getProductPrice(productID)); }
		public static bool isSubscriptionActive ( string productID ) { return (IAP.isSubscriptionActive(productID)); }

		public static void Initialize ( IAP.OnInitializationDoneDelegate onInitDone, IAP.OnProductBoughtDelegate onProductBought )
		{
			onProductBoughtCallback = onProductBought;
			IAP.Initialize(onInitDone, PurchasingManager.OnProductBought);
		}

		public static void PurchaseProduct ( string productID, IAP.OnPurchaseEventDelegate onPurchase )
		{
			onPurchaseCallBack = onPurchase;
			IAP.PurchaseProduct(productID, PurchasingManager.OnPurchaseEvent);
		}

		public static void RestorePurchases ( System.Action<bool> onComplete )
		{
			IAP.RestorePurchases(onComplete);
		}

		private static void OnProductBought ( string productID )
		{
			// DO Analytics
			onProductBoughtCallback.Invoke(productID);
		}

		private static void OnPurchaseEvent ( PurchaseEvent IAPEvent )
		{
			switch (IAPEvent.type)
			{
				case PurchaseEvent.Status.Fail:
					//AnalyticsManager.NewDesignEvent("IAP:PurchaseFailed:" + IAPEvent.productId);
					break;
				case PurchaseEvent.Status.Success:
					//AnalyticsManager.NewDesignEvent("IAP:PurchaseDone:" + IAPEvent.productId);

					break;
			}
			onPurchaseCallBack.Invoke(IAPEvent);
		}

	}

}