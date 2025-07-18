using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
using Pinpin;

public class QP_QATracker : MonoBehaviour
{
    public static QP_QATracker instance;
    public bool is_QA = false;

    public void Awake()
    {
        instance = this;
    }

    public void Parse_QA_IDS(string qa_ids)
    {
        JSONNode ids_tab = JSON.Parse(qa_ids);

        string ID = GetAndroidAdvertiserId();

        if (ids_tab == null)
        {
            // Handle the case when parsing fails (not valid JSON)
            Debug.LogError("QP_CheckQAIDS : failed to parse JSON");
            return;
        }

#if UNITY_IOS
        ID = UnityEngine.iOS.Device.advertisingIdentifier;

#endif

        print("QP_DEVICEID : " + ID);
        for (int i = 0; i < ids_tab.Count; i++)
        {
            print("QP_CheckQAIDS : " + ids_tab[i].ToString());
            if (ids_tab[i] == ID)
            {
                is_QA = true;
            }
        }
        if(is_QA)
        {
            ApplicationManager.datas.UnlockWorld(1);
            ApplicationManager.datas.UnlockWorld(2);
        }
        QuietFCM.Instance.InitializeFCM();
    }

    public static string GetAndroidAdvertiserId()
    {
        string advertisingID = "";
        try
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass client = new AndroidJavaClass("com.google.android.gms.ads.identifier.AdvertisingIdClient");
            AndroidJavaObject adInfo = client.CallStatic<AndroidJavaObject>("getAdvertisingIdInfo", currentActivity);

            advertisingID = adInfo.Call<string>("getId").ToString();
        }
        catch (Exception)
        {
        }
        return advertisingID;
    }
}
