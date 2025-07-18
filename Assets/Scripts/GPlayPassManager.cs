using System.Collections;
using System.Collections.Generic;
using Pinpin;
using UnityEngine;

public class GPlayPassManager : MonoBehaviour
{
    public static GPlayPassManager Instance;
    public static bool IsActive;


    public string Product_ID;


    void Awake()
    {
        
        if(Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void BuyIAP()
    {
        PurchasingManager.PurchaseProduct(Product_ID, this.OnIAPPurchaseComplete);
    }


	private void OnIAPPurchaseComplete(PurchasingManagement.PurchaseEvent IAPEvent)
	{

		if (IAPEvent.type == PurchasingManagement.PurchaseEvent.Status.Fail)
		{
			Debug.Log("Google Play Pass - Product [" + Product_ID + "] purchase failed.");
		}
		else
		{
			Debug.Log("Google Play Pass - Product [" + Product_ID + "] purchase success.");
            
		}
	}
}
