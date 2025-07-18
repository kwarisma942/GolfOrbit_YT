using System.Collections;
using System.Collections.Generic;
//using Firebase;
//using Firebase.Analytics;
//using Firebase.Extensions;
using UnityEngine;

public class QuietFCM : MonoBehaviour
{
    public static QuietFCM Instance;
    string fcm_token= "";
    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
    }

    //private void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    //{
    //    // Handle token received
    //    Debug.Log($"Received Registration Token: {token.Token}");
    //    fcm_token = token.Token;

    //}

    public void InitializeFCM()
    {
        Debug.Log("InitializeFCM");
        //StartCoroutine(GetTokenAsync());
    }

    public void InitializeFCM2()
    {
        Debug.Log("InitializeFCM2");
        //StartCoroutine(GetTokenAsync2());
    }


    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        InitializeFCM2();
    //    }
    //}
    //private IEnumerator GetTokenAsync()
    //{
    //    print("QP_isQA : " + QP_QATracker.instance.is_QA);

    //    if (fcm_token == "")
    //    {
    //        Debug.Log("Start GetTokenAsync");
    //        var task = Firebase.Messaging.FirebaseMessaging.GetTokenAsync();
    //        Debug.Log("Start task");
    //        while (!task.IsCompleted)
    //            yield return new WaitForEndOfFrame();

    //        fcm_token = task.Result;
    //        //Debug.Log("GET TOKEN ASYNC " + token);
    //    }

    //    FirebaseAnalytics.SetUserProperty("installation_id", fcm_token);

    //    if (Debug.isDebugBuild || QP_QATracker.instance.is_QA)
    //    {
    //        UniClipboard.SetText(fcm_token);
    //    }
    //}
    //private IEnumerator GetTokenAsync2()
    //{
    //    print("QP_isQA2 : " + QP_QATracker.instance.is_QA);

    //    Debug.Log("Start GetTokenAsync");
    //    var task = Firebase.Messaging.FirebaseMessaging.GetTokenAsync();
    //    Debug.Log("Start task");
    //    while (!task.IsCompleted)
    //        yield return new WaitForEndOfFrame();


    //    FirebaseAnalytics.SetUserProperty("installation_id", fcm_token);

    //    if (Debug.isDebugBuild || QP_QATracker.instance.is_QA)
    //    {
    //        UniClipboard.SetText(fcm_token);
    //    }
    //}

}