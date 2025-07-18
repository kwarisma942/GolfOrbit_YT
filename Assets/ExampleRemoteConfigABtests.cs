using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Firebase.Analytics;
//using Firebase.Extensions;
using UnityEngine;

public class ExampleRemoteConfigABtests : MonoBehaviour
{
    public float TaxIAP = 0;
    public bool UseNewIAP = false;
    public bool UseYearlyMainButton = false;
    public bool UseAdPause = false;
    public bool UseAdPauseMrec = false;
    public bool AdjustTrackIAP = false;
    public bool ShowUpgrade = false;
    public bool ShowRondomChar = false;
    public bool isFirebaseInitialized = false;
    public string user_cohort;

    public string qa_ids = "", app_version = "", IS_cooldown = "", banner_close_position = "", show_IS_at_level_X = "", show_IS_every_Y = "";
    public bool use_force_update = false, use_force_internet = false, no_ads_popup_enabled = false, no_ads_popup_on_launch = true;
    public int no_ads_popup_every_x_levels = 4;

    public static ExampleRemoteConfigABtests _instance;


    private void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //LoadRemoteVars();
    }




    // Initialize remote config, and set the default values.
    public void InitializeFirebase()
    {
        // [START set_defaults]
        System.Collections.Generic.Dictionary<string, object> defaults =
          new System.Collections.Generic.Dictionary<string, object>();

        // These are the values that are used if we haven't fetched data from the
        // server
        // yet, or if we ask for values that the server doesn't have:

        defaults.Add("TaxIAP", 20);
        defaults.Add("user_cohort", "non_configured");
        defaults.Add("ShowRondomChar", false);
        defaults.Add("ShowUpgrade", false);
        defaults.Add("UseNewIAP", false);
        defaults.Add("UseAdPause", false);
        defaults.Add("UseAdPauseMrec", false);
        defaults.Add("use_vipyearly_mainbutton", false);
        defaults.Add("AdjustTrackIAP", false);

        defaults.Add("qa_ids", "");
        defaults.Add("app_version", "");
        defaults.Add("use_force_update", false);
        defaults.Add("use_force_internet", false);
        defaults.Add("banner_close_position", "");
        defaults.Add("IS_cooldown", "");
        defaults.Add("show_IS_at_level_X", "");
        defaults.Add("show_IS_every_Y", "");

        defaults.Add("noads_warning", false);
        defaults.Add("no_ads_popup_enabled", false);
        defaults.Add("no_ads_popup_every_x_levels", 4);
        defaults.Add("no_ads_popup_on_launch", false);

        //Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
        //  .ContinueWithOnMainThread(task => {
        //      // [END set_defaults]
        //      isFirebaseInitialized = true;
        //      FetchDataAsync();
        //  });

    }

    //public Task FetchDataAsync()
    //{
    //    Debug.Log("Fetching data...");
    //    System.Threading.Tasks.Task fetchTask =
    //    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
    //    return fetchTask.ContinueWithOnMainThread(FetchComplete);

    //}
    //[END fetch_async]

    //void FetchComplete(Task fetchTask)
    //{
    //    if (fetchTask.IsCanceled)
    //    {
    //        Debug.Log("Fetch canceled.");
    //    }
    //    else if (fetchTask.IsFaulted)
    //    {
    //        Debug.Log("Fetch encountered an error.");
    //    }
    //    else if (fetchTask.IsCompleted)
    //    {
    //        Debug.Log("Fetch completed successfully!");
    //    }

    //    var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
    //    switch (info.LastFetchStatus)
    //    {
    //        case Firebase.RemoteConfig.LastFetchStatus.Success:
    //            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
    //            .ContinueWithOnMainThread(task => {
    //                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
    //                               info.FetchTime));
        
    //                _instance.TaxIAP = ToSingle(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("TaxIAP").DoubleValue);
    //                _instance.ShowRondomChar = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("ShowRondomChar").BooleanValue;
    //                _instance.ShowUpgrade = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("ShowUpgrade").BooleanValue;
    //                _instance.UseNewIAP = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("UseNewIAP").BooleanValue;
    //                _instance.UseAdPause = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("UseAdPause").BooleanValue;
    //                _instance.UseYearlyMainButton = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("use_vipyearly_mainbutton").BooleanValue;
    //                _instance.UseAdPauseMrec = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("UseAdPauseMrec").BooleanValue;
    //                _instance.AdjustTrackIAP = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("AdjustTrackIAP").BooleanValue;
    //                _instance.user_cohort = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("user_cohort").StringValue;

    //                _instance.qa_ids = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("qa_ids").StringValue;
    //                _instance.app_version = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("app_version").StringValue;
    //                _instance.use_force_internet = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("use_force_internet").BooleanValue;
    //                _instance.use_force_update = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("use_force_update").BooleanValue;
    //                _instance.IS_cooldown = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("IS_cooldown").StringValue;
    //                _instance.banner_close_position = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("banner_close_position").StringValue;
    //                _instance.show_IS_at_level_X = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("show_IS_at_level_X").StringValue;
    //                _instance.show_IS_every_Y = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("show_IS_every_Y").StringValue;


    //                _instance.no_ads_popup_enabled = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("no_ads_popup_enabled").BooleanValue;
    //                _instance.no_ads_popup_every_x_levels = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("no_ads_popup_every_x_levels").DoubleValue;
    //                _instance.no_ads_popup_on_launch = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("no_ads_popup_on_launch").BooleanValue;


    //                _instance.isFirebaseInitialized = true;

    //                QP_QuietObject.instance.Init(_instance.use_force_internet, _instance.use_force_update, _instance.app_version, _instance.IS_cooldown, _instance.banner_close_position,
    //                                    _instance.show_IS_at_level_X, _instance.show_IS_every_Y, false, _instance.no_ads_popup_enabled, _instance.no_ads_popup_every_x_levels, _instance.no_ads_popup_on_launch);
    //                QP_QATracker.instance.Parse_QA_IDS(_instance.qa_ids);
    //            });

    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Failure:
    //            _instance.user_cohort = "non_participant_failed";
    //            _instance.isFirebaseInitialized = true;

    //            switch (info.LastFetchFailureReason)
    //            {
    //                case Firebase.RemoteConfig.FetchFailureReason.Error:
    //                    Debug.Log("Fetch failed for unknown reason");
    //                    break;
    //                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
    //                    Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
    //                    break;
    //            }
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Pending:
    //            _instance.user_cohort = "non_participant_pending";
    //            _instance.isFirebaseInitialized = true;
    //            Debug.Log("Latest Fetch call still pending.");
    //            break;
    //    }
    //}

    //public  void CohortName(string name)
    //{
    //    FirebaseAnalytics.LogEvent("Cohort", "name", name);
    //}

    public static float ToSingle(double value)
    {
        return (float)value;
    }
}