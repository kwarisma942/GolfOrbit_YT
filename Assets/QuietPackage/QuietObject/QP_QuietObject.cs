using System;
using System.Collections;
using Pinpin;
using Proyecto26;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum BannerPosition
{
    Bottom,
    Top
}

public class QP_QuietObject : MonoBehaviour
{
    public static QP_QuietObject instance;

    [Header("UI")]
    [SerializeField]
    Canvas _mainCanvas, BannerCanvas;
    [SerializeField]
    GameObject _forceInternet, _forceUpdate, removeAds, removeAdsOffer, noAdsBTN, noAdsOfferBTN;
    [Header("Variables")]
    [SerializeField]
    bool forceInternetValue = false, forceUpdateValue = false, noads_warning = false, no_ads_popup_enabled = false, no_ads_popup_on_launch = false;
    int no_ads_popup_every_x_levels = 4;
    public bool check_internet_each_5sc; // or use QP_QuietObject.instance.CheckInternetConnection();
    [SerializeField]
    public BannerPosition banner_position = BannerPosition.Bottom;

    [Header("AdsSettings")]
    [SerializeField]
    private float banner_height = -1f;
    private float offsetYBanner = 100;
    private string inter_cooldown_sc = "5", inter_lastseen_date, banner_positionX, show_IS_at_level_X, show_IS_every_Y;

    public void Init(bool _forceInternetRC, bool _forceUpdateRC, string appversionRC, string _interCooldownSC, string _bannerPosition, string _show_IS_at_level_X, string _show_IS_every_Y, bool _noads_warning,bool _no_ads_popup_enabled,int _no_ads_popup_every_x_levels, bool _no_ads_popup_on_launch)
    {
        forceInternetValue = _forceInternetRC;
        forceUpdateValue = _forceUpdateRC;
        inter_cooldown_sc = _interCooldownSC;
        //banner_height = MaxSdkUtils.GetAdaptiveBannerHeight();
        banner_positionX = _bannerPosition;
        show_IS_at_level_X = _show_IS_at_level_X;
        show_IS_every_Y = _show_IS_every_Y;
        noads_warning = _noads_warning;
        no_ads_popup_enabled = _no_ads_popup_enabled;
        no_ads_popup_on_launch = _no_ads_popup_on_launch;
        no_ads_popup_every_x_levels = _no_ads_popup_every_x_levels;

        DebugAll();
        SetupUI();
        SetCurrentDate();
        if (forceInternetValue)
        {
            if (check_internet_each_5sc)
                StartCoroutine(CheckInternetConnectionRoutine());
            else
                CheckInternetConnection();
        }
        if (forceUpdateValue)
           Check_Force_Update(appversionRC);
        if (no_ads_popup_on_launch)
            Open_RemoveAds();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    public void SetupUI()
    {
        /****FORCE UPDATE****/
        _forceUpdate.GetComponent<VarsManager>().btn1.onClick.RemoveAllListeners();
       _forceUpdate.GetComponent<VarsManager>().btn1.onClick.AddListener(()=>{
#if UNITY_ANDROID
            // Open Play Store on Android
            Application.OpenURL("market://details?id=" + Application.identifier);
#elif UNITY_IOS
           // Open App Store on iOS
           //        Application.OpenURL("https://apps.apple.com/app/id" + ios_app_id); // Ex
           RestClient.Get("https://itunes.apple.com/lookup?bundleId=" + Application.identifier)
                   .Then(response =>
                   {
                       JSONNode node = JSON.Parse(response.Text);
                       JSONNode trackViewUrl = SimpleJsonHelpers.FindNode(node, "trackId");
                       string appID = trackViewUrl != null ? trackViewUrl.Value : String.Empty;
                       Application.OpenURL("https://apps.apple.com/app/id" + appID);
                   });
#endif
       });
        /****FORCE UPDATE****/

        /****NO ADS CONFIGURATION****/
        removeAds.GetComponent<VarsManager>().btn1.onClick.AddListener(() =>
        {
            Buy_No_Ads();
            
            //buy remove ads
        });

        removeAdsOffer.GetComponent<VarsManager>().btn1.onClick.AddListener(() =>
        {
           // IAPManager.instance.BuyCoinsNoAdsOffer(); //buy remove ads
        });
      
        /****NO ADS CONFIGURATION****/

    }
    #region Panels functions


    public void Close_BannerClose_UI()
    {
        BannerCanvas.GetComponent<VarsManager>().obj1.SetActive(false);
        BannerCanvas.GetComponent<VarsManager>().obj2.SetActive(false);
        BannerCanvas.GetComponent<VarsManager>().obj3.SetActive(false);
        BannerCanvas.GetComponent<VarsManager>().obj4.SetActive(false);
        Debug.Log("QP_BannerCloseOFF: " + banner_position);

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            Show_Banner_Close_UI();
    }
    public void Show_Banner_Close_UI()
    {
        Close_BannerClose_UI();
        if (banner_positionX == "null" || String.IsNullOrEmpty(banner_positionX))
        {
            Debug.Log("QP_BannerClose: NOT CONFIGURED");
            return;
        }
        Debug.Log("QP_BannerClose: " + banner_positionX + " " + banner_positionX.ToLower() + " banner h : " + banner_height);
        

        GameObject current_banner = null;
        int multiplier = 1;
        #if UNITY_IOS
            multiplier = 2;
        #endif

        switch (banner_position)
        {
            case BannerPosition.Top:
                if (banner_positionX.ToLower() == "right")
                {
                    current_banner = BannerCanvas.GetComponent<VarsManager>().obj1;
                    current_banner.SetActive(true);
                    current_banner.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, banner_height);
                }
                else if (banner_positionX.ToLower() == "left")
                {
                    current_banner = BannerCanvas.GetComponent<VarsManager>().obj2;
                    current_banner.SetActive(true);
                    current_banner.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, banner_height);

                }
                break;
            case BannerPosition.Bottom:
                if (banner_positionX.ToLower() == "right")
                {
                    current_banner = BannerCanvas.GetComponent<VarsManager>().obj3;
                    current_banner.SetActive(true);
                    current_banner.GetComponent<RectTransform>().position = new Vector2(current_banner.GetComponent<RectTransform>().position.x, (banner_height * multiplier) + offsetYBanner);
                }
                else if (banner_positionX.ToLower() == "left")
                {
                    current_banner = BannerCanvas.GetComponent<VarsManager>().obj4;
                    current_banner.SetActive(true);
                    current_banner.GetComponent<RectTransform>().sizeDelta = new Vector2(current_banner.GetComponent<RectTransform>().sizeDelta.x, (banner_height * multiplier) + offsetYBanner);
                }
                break;
        }
        Debug.Log("QP_BannerCloseON: " + banner_position + " " + banner_positionX);

    }

    public void Buy_No_Ads()
    {
        PurchasingManager.PurchaseProduct("noads", this.OnIAPPurchaseComplete);
      
    }

    private void OnIAPPurchaseComplete(PurchasingManagement.PurchaseEvent IAPEvent)
    {
        if (IAPEvent.type == PurchasingManagement.PurchaseEvent.Status.Fail)
        {
            Debug.Log("Google Play Pass - Product [noads] purchase failed.");
        }
        else
        {
            Debug.Log("Google Play Pass - Product [noads] purchase success.");
            PlayerPrefs.SetInt("NoAds", 1);
            Close_RemoveAds();
            Close_BannerClose_UI();
        }
    }

    public void Open_RemoveAds()
    {
        removeAds.SetActive(true);
        ShowCanvas();
        Start_Animation(true, removeAds);
    }

    public void Close_RemoveAds()
    {
        Start_Animation(false, removeAds);
    }

    public void Show_NoAdsBTN(bool visibility)
    {
        noAdsBTN.SetActive(visibility);
    }

    public void Show_NoAdsOfferBTN(bool visibility)
    {
        noAdsBTN.SetActive(visibility);
    }

    public void Check_Force_Update(string remote_version)
    {

        string appVersion = Application.version;

        int result = CompareVersions(appVersion, remote_version);
        Debug.Log("QP_Version: " + result);
        if (result < 0) // remote > current
            Open_Force_Update();
    }

    public void Open_RemoveAdsOffer()
    {
        removeAdsOffer.SetActive(true);
        ShowCanvas();
        Start_Animation(true, removeAdsOffer);
    }

    public void Close_RemoveAdsOffer()
    {
        Start_Animation(false, removeAdsOffer);
    }


    public void Open_Force_Internet()
    {
        if (_forceInternet.activeSelf)
            return;
        _forceInternet.SetActive(true);
        ShowCanvas();
        Start_Animation(true, _forceInternet);
    }

    public void Close_Force_Internet()
    {
        if (!_forceInternet.activeSelf)
            return;
        Start_Animation(false, _forceInternet);
    }

    public void Open_Force_Update()
    {
        if (_forceUpdate.activeSelf)
            return;
        ShowCanvas();
        _forceUpdate.SetActive(true);
        Start_Animation(true, _forceUpdate);
   
    }

    public void Close_Force_Update()
    {
        if (!_forceUpdate.activeSelf)
            return;
        Start_Animation(false, _forceUpdate);
    }

    public void ShowCanvas(bool show = true)
    {
        _mainCanvas.enabled = show;
    }
    #endregion

    #region Ads

    public bool Is_NoAds_Purchased()
    {
        return PlayerPrefs.GetInt("NoAds", 0)==1;

    }

    public bool Can_Show_NoAds_Popup()
    {
        if (no_ads_popup_enabled)
        {
            if (no_ads_popup_every_x_levels > 0 && ((Get_Actual_Level() - 1) % no_ads_popup_every_x_levels == 0))
            {
                return true;
            }
        }

        return false;
    }

    public bool QP_Can_Show_Inter()
    {
        bool cooldown_ready = false;
        int current_level = Get_Actual_Level();
        Debug.Log("QA_ShowInter_Condition1 : " + current_level + " / " + show_IS_at_level_X);

        // Check if show_IS_at_level_X is not null or empty before parsing
        if (!string.IsNullOrEmpty(show_IS_at_level_X) && current_level >= ParseInt(show_IS_at_level_X))
        {
            if (int.Parse(inter_cooldown_sc) > 0) // We have cooldown configured
            {
                // Check if inter_cooldown_sc is not null or empty before parsing
                if (Cooldown_ready(inter_lastseen_date, ParseInt(inter_cooldown_sc)))
                    cooldown_ready = true;
            }
            else
            {
                cooldown_ready = true;
            }

            // check for inter each x levels
            if (cooldown_ready)
            {
                if (int.Parse(show_IS_every_Y) > 0)
                {
                    Debug.Log("QA_ShowInter_Condition2 : " + Get_Actual_Level() + " / " + int.Parse(show_IS_every_Y) + " = " + Get_Actual_Level() % int.Parse(show_IS_every_Y));
                    if (Get_Actual_Level() % ParseInt(show_IS_every_Y) == 0)
                    {
                        SetCurrentDate();
                        return true;
                    }
                }
                else
                {
                    SetCurrentDate();
                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    #region Internet
    private IEnumerator CheckInternetConnectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Check every 5 seconds
            CheckInternetConnection();
        }
    }

    public void CheckInternetConnection()
    {
        StartCoroutine(IsInternetConnected());
    }

    private IEnumerator IsInternetConnected(int attempt = 0)
    {
        UnityWebRequest www = new UnityWebRequest("https://www.google.com");
        www.timeout = 5; // Set a timeout value for the request
        www.method = UnityWebRequest.kHttpVerbGET;

        var operation = www.SendWebRequest();

        // Wait for the request to complete
        while (!operation.isDone)
        {
            yield return null;
        }

        // Check the result after the request is done
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Internet is connected
            Close_Force_Internet();
        }
        else
        {
            // Internet is not connected
            if (attempt > 0)
            {
                print("QP_QuietCheckInternet : " + attempt + " :  open open");
                Open_Force_Internet();

            }
            else
            {
                print("QP_QuietCheckInternet :  " + attempt + " : wait 5 sec recheck");
                yield return new WaitForSeconds(5);
                StartCoroutine(IsInternetConnected(1));
            }
        }
    }
    #endregion

    #region Other

    public int Get_Actual_Level()
    {
        //return FGAdjust.Instance.CurrentLevel;
        return 0;
    }

    public void Start_Animation(bool open, GameObject obj)
    {
        if (open)
            obj.GetComponent<Animation>().Play("Bubble_Open");
        else
            obj.GetComponent<Animation>().Play("Bubble_Close");

    }

    public static int CompareVersions(string version1, string version2)
    {
        string[] components1 = version1.Split('.');
        string[] components2 = version2.Split('.');

        for (int i = 0; i < Math.Max(components1.Length, components2.Length); i++)
        {
            int part1 = (i < components1.Length) ? int.Parse(components1[i]) : 0;
            int part2 = (i < components2.Length) ? int.Parse(components2[i]) : 0;

            int result = part1.CompareTo(part2);
            if (result != 0)
            {
                return result;
            }
        }

        return 0; // Both versions are equal
    }

    public bool Cooldown_ready(string _date, int seconds)
    {
        Debug.Log("QP_CheckCooldown: " + _date);

        // Convert the input date string to DateTime
        DateTime inputDate;
        if (!DateTime.TryParseExact(_date, "yyyy-MM-dd-HH-mm-ss", null, System.Globalization.DateTimeStyles.None, out inputDate))
        {
            // Handle invalid date format
            throw new ArgumentException("Invalid date format");
        }

        // Calculate the target date by adding the specified seconds to the input date
        DateTime targetDate = inputDate.AddSeconds(seconds);

        // Get the current time
        DateTime currentTime = DateTime.Now;

        // Compare if the target date is greater than the current time
        bool isReady = targetDate < currentTime;
        Debug.Log("QP_CheckCooldown_isReady: " + isReady);

        return isReady;
    }

    public void SetCurrentDate()
    {
        string formattedDateTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        inter_lastseen_date = formattedDateTime;
        print("SET NEW INTER DATE : " +inter_lastseen_date); 
    }

    void DebugAll()
    {
        Debug.Log("QP_ForceUpdate " + forceUpdateValue);
        Debug.Log("QP_ForceInternet " + forceInternetValue);
        Debug.Log("QP_InterCooldown " + inter_cooldown_sc);
        Debug.Log("QP_BannerPositionX " + banner_positionX);
        Debug.Log("QP_BannerHeight " + banner_height);
        Debug.Log("QP_ShowInter_From " + show_IS_at_level_X);
        Debug.Log("QP_ShowNoAds_Warning " + noads_warning);
        Debug.Log("QP_no_ads_popup_enabled " + no_ads_popup_enabled);
        Debug.Log("QP_no_ads_popup_on_launch " + no_ads_popup_on_launch);
        Debug.Log("QP_no_ads_popup_every_x_levels " + no_ads_popup_every_x_levels);
    }

    public void End_Of_Level_Logic()
    {
        print("EOL");
        if (!check_internet_each_5sc && forceInternetValue)
            CheckInternetConnection();
        if (Can_Show_NoAds_Popup() && !Is_NoAds_Purchased())
        {
            Open_RemoveAds();
        }

    }
    private int ParseInt(string value)
    {
        int result = 0;
        if (!string.IsNullOrEmpty(value))
        {
            int.TryParse(value, out result);
        }
        return result;
    }

    public bool Get_Noads_Warning() { return noads_warning; }
    public GameObject Get_Noads_Offer_Button(){  return removeAdsOffer.GetComponent<VarsManager>().btn1.gameObject;}
    public GameObject Get_Noads_Button() { return removeAds.GetComponent<VarsManager>().btn1.gameObject; } 

    #endregion

}
