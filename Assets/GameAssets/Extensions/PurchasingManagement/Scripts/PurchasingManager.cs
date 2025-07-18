#if UNITY_ANDROID || UNITY_IOS
	#define MOBILE
#endif

namespace PurchasingManagement
{

	public static class PurchasingManager
	{

		public delegate void OnInitializationDoneDelegate ( bool isInitialized );
		public delegate void OnProductBoughtDelegate ( string productId );
		public delegate void OnPurchaseEventDelegate ( PurchaseEvent IAPEvent );

		public static bool havePurchasingAgent
		{
			get
			{
				#if MOBILE && UNITY_PURCHASING
					return (true);
				#else
					return (false);
				#endif
			}
		}

		public static bool isInitialized
		{
			get
			{
				#if MOBILE && UNITY_PURCHASING
					return (UnityPurchasingAgent.isInitialized);
				#else
					return (false);
				#endif
			}
		}

		public static string getProductName ( string productID )
		{
			#if MOBILE && UNITY_PURCHASING
				return (UnityPurchasingAgent.getProductName(productID));
			#else
				return ("none");
			#endif
		}

		public static string getProductCurrencyCode(string productID)
		{
#if MOBILE && UNITY_PURCHASING
			return (UnityPurchasingAgent.getProductCurrencyCode(productID));
#else
				return ("none");
#endif
		}

		public static string getProductPriceString ( string productID )
		{
			#if MOBILE && UNITY_PURCHASING
				return (UnityPurchasingAgent.getProductPriceString(productID));
			#else
				return ("none");
			#endif
		}

		public static string getProductTransactionID(string productID)
		{
#if MOBILE && UNITY_PURCHASING
			return (UnityPurchasingAgent.getProductTransactionID(productID));
#else
				return ("none");
#endif
		}

		public static decimal getProductPrice ( string productID )
		{
#if MOBILE && UNITY_PURCHASING
				return (UnityPurchasingAgent.getProductPrice(productID));
			#else
				return (0);
			#endif
		}

		public static bool isSubscriptionActive ( string productID )
		{
			#if MOBILE && UNITY_PURCHASING
				return (UnityPurchasingAgent.isSubscriptionActive(productID));
			#else
				return (false);
			#endif
		}

		public static void Initialize ( OnInitializationDoneDelegate onInitDone, OnProductBoughtDelegate onProductBought )
		{
			#if MOBILE && UNITY_PURCHASING
				UnityPurchasingAgent.Initialize(onInitDone, onProductBought);
			#endif
		}

		public static void PurchaseProduct ( string productID, OnPurchaseEventDelegate onPurchase )
		{
			#if MOBILE && UNITY_PURCHASING
				UnityPurchasingAgent.PurchaseProduct(productID, onPurchase);
			#endif
		}

		public static void RestorePurchases ( System.Action<bool> onComplete )
		{
			#if MOBILE && UNITY_PURCHASING
				UnityPurchasingAgent.RestorePurchases( onComplete );
			#endif
		}

	}

}