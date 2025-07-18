#if ((UNITY_IOS || UNITY_ANDROID) || ( UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID))) && UNITY_PURCHASING

using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using System;
using System.Collections.Generic;
using Pinpin;

namespace PurchasingManagement
{

	public class UnityPurchasingAgent: IStoreListener
	{

#pragma warning disable 0649

		[Serializable]
		private class AndroidPayLoad
		{
			public string json;
			public string signature;
		}
	
		[Serializable]
		private class StoreReceipe
		{
			public string Store;
			public string TransactionID;
			public string Payload;
		}

#pragma warning restore 0649

		private static UnityPurchasingSettings	settings { get; set; }
		private static IStoreController			storeController { get; set; }
		private static IExtensionProvider		storeExtensionProvider { get; set; }
		private static ProductCatalog			productCatalog { get; set; }
		private static IStoreListener			listenerInstance { get; set; }

		private static PurchasingManager.OnInitializationDoneDelegate onInitializedCallback { get; set; }
		private static PurchasingManager.OnProductBoughtDelegate onProductBoughtCallback { get; set; }
		private static PurchasingManager.OnPurchaseEventDelegate onPurchaseCallBack { get; set; }

		public static bool isInitialized
		{
			get { return (storeController != null && storeExtensionProvider != null); }
		}

		public static string getProductName ( string productID )
		{
			if (isInitialized)
			{
				Product product = storeController.products.WithID(productID);
				string nameString = product != null ? product.metadata.localizedTitle : "none";
				PlayerPrefs.SetString(productID + "_name", nameString);
				return nameString;
			}
			else if (PlayerPrefs.HasKey(productID + "_name"))
			{
				return PlayerPrefs.GetString(productID + "_name");
			}
			return null;
		}

		public static string getProductCurrencyCode(string productID)
		{
			Product product = storeController.products.WithID(productID);
			return product.metadata.isoCurrencyCode;
		}

		public static string getProductPriceString ( string productID )
		{
			if (isInitialized)
			{
				Product product = storeController.products.WithID(productID);
				string priceString = product != null ? product.metadata.localizedPriceString : "none";
				PlayerPrefs.SetString(productID + "_price", priceString);
				return priceString;
			}
			else if (PlayerPrefs.HasKey(productID + "_price"))
			{
				return PlayerPrefs.GetString(productID + "_price");
			}
			return null;
		}

		public static string getProductTransactionID(string productID)
		{
			if (isInitialized)
			{
				Product product = storeController.products.WithID(productID);
				string transactionID = product != null ? product.transactionID : "none";
				return transactionID;
			}
			return "none";
		}

		public static decimal getProductPrice ( string productID )
		{
			Product product = storeController.products.WithID(productID);
			return (product != null ? product.metadata.localizedPrice : 0);
		}

		public bool IsSubActive ( AppleInAppPurchaseReceipt e )
		{
			if (e.subscriptionExpirationDate > DateTime.Now.ToUniversalTime())
			{
				return true; //HAS_ACTIVE_SUBSCRIPTION
			}
			else
			{
				return false;
			}
		}

		public static bool isSubscriptionActive ( string productID )
		{
			Product product = storeController.products.WithID(productID);
			if (product != null && product.hasReceipt)
			{
#if UNITY_IOS
				SubscriptionManager subscriptionManager = new SubscriptionManager(product, null);
				SubscriptionInfo infos = subscriptionManager.getSubscriptionInfo();
				return (infos.isSubscribed() == Result.True);
#elif UNITY_ANDROID
				return true;
#endif
			}
			return false;
		}
        
        public static void Initialize ( PurchasingManager.OnInitializationDoneDelegate onInitialized, PurchasingManager.OnProductBoughtDelegate onProductBought )
        {
        	if (isInitialized)
				return ;


			settings = Resources.Load("UnityPurchasingSettings") as UnityPurchasingSettings;			
			onInitializedCallback = onInitialized;
			onProductBoughtCallback = onProductBought;

			listenerInstance = new UnityPurchasingAgent();

			productCatalog = ProductCatalog.LoadDefaultCatalog();
			StandardPurchasingModule module = StandardPurchasingModule.Instance();
			module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;

			ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
			foreach (ProductCatalogItem product in productCatalog.allProducts)
			{

				if (product.allStoreIDs.Count > 0)
				{
					var ids = new IDs();
					foreach (var storeID in product.allStoreIDs)
						ids.Add(storeID.id, storeID.store);
					builder.AddProduct(product.id, product.type, ids);
				}
				else
					builder.AddProduct(product.id, product.type);
			}
			UnityPurchasing.Initialize(listenerInstance, builder);

		}
        
        public void OnInitialized ( IStoreController controller, IExtensionProvider extensions )
		{
#if DEBUG
			if (settings.debug)
				Debug.Log("UnityPurchasingAgent - OnInitialized()");
#endif
			
			storeController = controller;
			bool hasSubscription = false;
			foreach (var product in controller.products.all)
			{
				Debug.Log(product.definition.id);

				//check other products..
				if (product.definition.id == "google_play_pass")
                {
                    if (product.hasReceipt)
                    {
						GPlayPassManager.IsActive = true;
						Debug.Log("google_play_pass has Receipt ");
                    }
                    else
                    {
						Debug.Log("google_play_pass hasn't Receipt ");
					}
				}

				if (product.definition.id.Contains("subscription") )
				{
					Debug.Log("product "+product.definition.id);
					if (product.hasReceipt)
					{
						Debug.Log(product.definition.id+ " has Receipt "+product.hasReceipt);
						hasSubscription = true;
						PlayerPrefs.SetInt("NoAds", 1);
						if (product.definition.id.Contains("monthly"))
                        {
							PlayerPrefs.SetString("UnlockVIP", "monthly_sub");
						}

						if (product.definition.id.Contains("weekly"))
                        {
							PlayerPrefs.SetString("UnlockVIP", "weekly_sub");
						}

						if (product.definition.id.Contains("yearly"))
                        {
							PlayerPrefs.SetString("UnlockVIP", "yearly_sub");
						}
					}
                    else
                    {
						if (!hasSubscription)
                        {
							Debug.Log(product.definition.id + "No  Receipt " + product.hasReceipt);
							PlayerPrefs.SetInt("NoAds", 0);
							PlayerPrefs.SetString("UnlockVIP", "null");
						}
					}
				}


                if (product.definition.id == "noads")
                {
                    if (product.hasReceipt)
                    {
						PlayerPrefs.SetInt("NoAds", 1);
                    }
                    else
                    {
						PlayerPrefs.SetInt("NoAds", 0);
					}
                }

			}
			storeExtensionProvider = extensions;
			onInitializedCallback.Invoke(true);
			ApplicationManager.isPurchaseInitializEnd = true;
		}

		public void OnInitializeFailed ( InitializationFailureReason error )
		{
			Debug.LogError("UnityPurchasingAgent - OnInitializeFailed() reason:" + error);
			onInitializedCallback.Invoke(false);
			ApplicationManager.isPurchaseInitializEnd = true;
		}

		public static void PurchaseProduct ( string productID, PurchasingManager.OnPurchaseEventDelegate onPurchase )
		{
			onPurchaseCallBack = onPurchase;
			
			if (!isInitialized)
			{
				Debug.LogError("UnityPurchasingAgent - BuyProductID FAIL. Not initialized.");
				onPurchaseCallBack.Invoke(new PurchaseEvent(PurchaseEvent.Status.Fail, productID));
				return ;
			}	

			Product product = storeController.products.WithID(productID);
			

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
#if DEBUG
					if (settings.debug)
						Debug.Log("UnityPurchasingAgent - Purchasing product asychronously: " + product.definition.id);
#endif
				storeController.InitiatePurchase(product);
			}
			else
			{
				Debug.LogError("UnityPurchasingAgent - BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				onPurchaseCallBack.Invoke(new PurchaseEvent(PurchaseEvent.Status.Fail, productID));
			}
		}

		public static void RestorePurchases ( Action<bool> onComplete )
		{
			if (!isInitialized)
			{
				Debug.LogError("UnityPurchasingAgent - BuyProductID FAIL. Not initialized.");
				return ;
			}	

			// If we are running on an Apple device ... 
			if (Application.platform == RuntimePlatform.IPhonePlayer ||
				Application.platform == RuntimePlatform.OSXPlayer)
			{
#if DEBUG
					if (settings.debug)
						Debug.Log("UnityPurchasingAgent - RestorePurchases started ...");
#endif
		
				var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();
				apple.RestoreTransactions((result) =>
				{
#if DEBUG
						if (settings.debug)
							Debug.Log("UnityPurchasingAgent - RestorePurchases complete: " + result);
#endif
					onComplete(result);
				});
			}
			else
			{
				Debug.LogWarning("UnityPurchasingAgent - RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
				onComplete(false);
			}
		}
		
		public PurchaseProcessingResult ProcessPurchase ( PurchaseEventArgs args )
		{
			// Presume valid for platforms with no R.V.
			bool validPurchase = true;
		
			// RECEIPT VALIDATOR
			// Unity IAP's validation logic is only included on these platforms.
#if !UNITY_EDITOR
				// Prepare the validator with the secrets we prepared in the Editor
				// obfuscation window.
				var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
					AppleTangle.Data(), Application.identifier);
			
				try
				{
					// On Google Play, result has a single product ID.
					// On Apple stores, receipts contain multiple products.
					var result = validator.Validate(args.purchasedProduct.receipt);
					// For informational purposes, we list the receipt(s)
					Debug.Log("IAPManager - Receipt is valid.");
				}
				catch (IAPSecurityException)
				{
					Debug.LogError("UnityPurchasingAgent - Invalid receipt, not unlocking content");
					validPurchase = false;
				}
#endif // !RECEIPT VALIDATOR
			
			Product product = args.purchasedProduct;

			if (validPurchase)
			{
#if DEBUG
					if (settings.debug)
						Debug.Log("UnityPurchasingAgent - Purchase Complete.");
#endif
				
				if (product.hasReceipt)
				{
					onProductBoughtCallback.Invoke(product.definition.id);
					if (onPurchaseCallBack != null)
						onPurchaseCallBack.Invoke(new PurchaseEvent(PurchaseEvent.Status.Success, product.definition.id));
				}
			}
			else if(onPurchaseCallBack != null)
				onPurchaseCallBack.Invoke(new PurchaseEvent(PurchaseEvent.Status.Fail, product.definition.id));

			return PurchaseProcessingResult.Complete;
		}

		public void OnPurchaseFailed ( Product product, PurchaseFailureReason failureReason )
		{
			Debug.LogError("UnityPurchasingAgent - Purchase failed. " + failureReason);
			if (onPurchaseCallBack != null)
				onPurchaseCallBack.Invoke(new PurchaseEvent(PurchaseEvent.Status.Fail, product.definition.id));
		}

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
			ApplicationManager.isPurchaseInitializEnd = true;

		}
	}

}

#endif