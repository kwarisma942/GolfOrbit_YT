using UnityEngine;
using UnityEngine.UI;
public class PlayOnDemo : MonoBehaviour {
    private PlayOnSDK.Position popupLocation = (PlayOnSDK.Position) 8;
    private PlayOnSDK.Position adLocation = (PlayOnSDK.Position) 5;
    private PlayOnSDK.AdUnitType adType = PlayOnSDK.AdUnitType.AudioBannerAd;
    private PlayOnSDK.AdUnitActionButtonType bBehavior = PlayOnSDK.AdUnitActionButtonType.Mute;

    private AdUnit adUnit;

    public Button isAdAvailableImage;
    public Button showAdButton;
    public Button closeAdButton;
    private Color visualizationMainColor = Color.white;
    private Color visualizationBackgroundColor = Color.green;
    private Color adUnitProgressBarColor = Color.white;
    private float actionBtnDelayShowTime = 5f;
    private int logoAdSize = 100; //In pixels
    private int logoAdXOffset = 50; //In pixels
    private int logoAdYOffset = 50; //In pixels
    private int popupXOffset = 15; //In pixels
    private int popupYOffset = 15; //In pixels
    private string ApiKey = "4b5cf8a0-40ad-4e3d-8b39-875d7cfa5d48";
    private string storeID = "1111111111";

    private PlayOnSDK.AdUnitRewardType rewardType = PlayOnSDK.AdUnitRewardType.InLevel;
    
    public AdUnitAnchor adUnitAnchor;
    public Canvas canvas;
    public RectTransform rect;
    
    public void Initialize () {
        PlayOnSDK.OnInitializationFinished += OnInitializationFinished;
        PlayOnSDK.Initialize (ApiKey, storeID);
        PlayOnSDK.SetLogLevel(PlayOnSDK.LogLevel.Debug);

        //Wrapped native IOS requestTrackingAuthorization function, to be abble fetch advertiser id
        PlayOnSDK.RequestTrackingAuthorization();
    }

    public void SetAdType (int newType) {
        adType = (PlayOnSDK.AdUnitType) newType;
    }

    public void SetButtonBehavior(int butBeh){
        bBehavior = (PlayOnSDK.AdUnitActionButtonType)butBeh;
    }

    public void SetAdViewPosition (int index) {
        adLocation = (PlayOnSDK.Position) index;
    }

    public void SetPopupPosition (int index) {
        popupLocation = (PlayOnSDK.Position) index;
    }

    public Text logoAdSizeLabel;
    public void SetLogoAdSize (float size) {
        logoAdSize = (int)size;
        if(logoAdSizeLabel != null) {
            logoAdSizeLabel.text = "Logo Ad Size = " + size;
        }
    }

    public Text logoAdXOffsetLabel;
    public void SetLogoAdXOffset (float xOffset) {
        logoAdXOffset = (int)xOffset;
        if(logoAdXOffsetLabel != null) {
            logoAdXOffsetLabel.text = "Logo X Offset = " + xOffset;
        }
    }
    public Text logoAdYOffsetLabel;
    public void SetLogoAdYOffset (float yOffset) {
        logoAdYOffset = (int)yOffset;
        if(logoAdYOffsetLabel != null) {
            logoAdYOffsetLabel.text = "Logo Y Offset = " + yOffset;
        }
    }

    public Text popupXOffsetLabel;
    public void SetPopupXOffset (float xOffset) {
        popupXOffset = (int)xOffset;
        if(popupXOffsetLabel != null) {
            popupXOffsetLabel.text = "Popup X Offset = " + xOffset;
        }
    }

    public Text popupYOffsetLabel;
    public void SetPopupYOffset (float yOffset) {
        popupYOffset = (int)yOffset;
        if(popupYOffsetLabel != null) {
            popupYOffsetLabel.text = "Popup Y Offset = " + yOffset;
        }
    }
    
    public void SetRewardType(int value){
        rewardType = (PlayOnSDK.AdUnitRewardType)value;
    }

    public Image visualizationMainColorPicker;
    public void SetVisualizationMain(float value){
        visualizationMainColor = Color.HSVToRGB(value, 1f, 1f);
        if (adUnit != null){
            adUnit.SetVisualization(visualizationMainColor, visualizationBackgroundColor);
        }

        if(visualizationMainColorPicker != null){
            visualizationMainColorPicker.color = visualizationMainColor;
        }
    }

    public Image visualizationBackgroundColorPicker;
    public void SetVisualizationBackgroundColor(float value){
        visualizationBackgroundColor = Color.HSVToRGB(value, 1f, 1f);
        if (adUnit != null){
            adUnit.SetVisualization(visualizationMainColor, visualizationBackgroundColor);
        }

        if(visualizationBackgroundColorPicker != null){
            visualizationBackgroundColorPicker.color = visualizationBackgroundColor;
        }
    }

    public Image adUnitProgressBarColorPicker;
    public void SetProgressBarColor(float value){
        adUnitProgressBarColor = Color.HSVToRGB(value, 1f, 1f);

        if(adUnitProgressBarColorPicker != null){
            adUnitProgressBarColorPicker.color = adUnitProgressBarColor;
        }
    }

    public Text actionBtnDelayLabel;
    public void SetActionButtonDelay(float value){
        actionBtnDelayShowTime = value;
        if(actionBtnDelayLabel != null) {
            actionBtnDelayLabel.text = "Action Button Delay " + value + " secs";
        }
    }

    public void CreateAd () {
        if (adUnit != null) adUnit.CloseAd ();

        adUnit = new AdUnit (adType);
        
        //if AudioLogoAd then that would be position of your logo
        //Three methods are used to set the position: LinkLogoToPrefab, SetLogo, LinkLogoToRectTransform
        //LinkLogoToPrefab - Set the AD Unit to the same position and size as the AdUnitAnchor object. Make sure the AdUnitAnchor object is a child of your canvas
        adUnit.LinkLogoToPrefab(adUnitAnchor);
        //SetLogo - Set the AD Unit position relative to the screen with offsets. Offsets and size must be specified in density pixels
        //adUnit.SetLogo(adLocation, logoAdXOffset, logoAdYOffset, logoAdSize);
        //LinkLogoToRectTransform - Set theAD Unit position in the RectTransform. PlayOnSDK.Position specifies the location inside RectTransform. Size must be specified in density pixels
        //adUnit.LinkLogoToRectTransform(PlayOnSDK.Position.BottomLeft, rect, canvas, logoAdSize);
        
        adUnit.SetVisualization(visualizationMainColor, visualizationBackgroundColor);
        //if rewarded ad type, turn on, turn off popup
        adUnit.SetPopup(popupLocation, popupXOffset, popupYOffset);
        adUnit.SetReward(rewardType, 100.0f);
        adUnit.SetProgressBar(adUnitProgressBarColor);
        adUnit.SetActionButton(bBehavior, actionBtnDelayShowTime);
        //if banner ad type
        adUnit.SetBanner(adLocation);
        
        //Adding callbacks
        adUnit.AdCallbacks.OnAvailabilityChanged += AdOnAvailabilityChanged;
        adUnit.AdCallbacks.OnShow += AdOnShow;
        adUnit.AdCallbacks.OnClose += AdOnClose;
        adUnit.AdCallbacks.OnClick += AdOnClick;

        //If rewarded ad type, rewarded callback
        adUnit.AdCallbacks.OnReward += AdOnReward;

        //If Impression turned on
        adUnit.AdCallbacks.OnImpression += AdOnImpression;

        isAdAvailableImage.GetComponent<Image>().color = Color.red;
        isAdAvailableImage.interactable = true;
    }

    public void IsAdAvailable () {
        if (adUnit != null){
            bool flag = adUnit.IsAdAvailable ();
            if (flag) {
                isAdAvailableImage.GetComponent<Image>().color = Color.green;
            } else {
                isAdAvailableImage.GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void ShowAd () {
        if (adUnit != null){
            adUnit.ShowAd ();
        }
    }

    public void CloseAd () {
        if (adUnit != null){
            adUnit.CloseAd ();
        }
    }


    public void OnInitializationFinished () {
        Debug.Log ("Unity PlayOnDemo OnInitializationFinished Callback");
    }

    public void AdOnAvailabilityChanged (bool flag) {
        Debug.Log ("Unity PlayOnDemo AdOnAvailabilityChanged Callback " + flag);
        if (flag) {
            isAdAvailableImage.GetComponent<Image>().color = Color.green;
            showAdButton.interactable = true;
        } else {
            isAdAvailableImage.GetComponent<Image>().color = Color.red;
            showAdButton.interactable = false;
        }
    }

    public void AdOnClick () {
        Debug.Log ("Unity PlayOnDemo AdOnClick Callback");
    }

    public void AdOnClose () {
        closeAdButton.interactable = false;
        Debug.Log ("Unity PlayOnDemo AdOnClose Callback");
    }

    public void AdOnShow () {
        closeAdButton.interactable = true;
        Debug.Log ("Unity PlayOnDemo AdOnShow Callback");
    }

    public void AdOnReward (float amount) {
        Debug.Log ("Unity PlayOnDemo AdOnReward Callback");
    }

    public void AdOnImpression(AdUnit.ImpressionData data){
        Debug.Log("Unity PlayOnDemo AdOnImpression Callback " + data.GetCountry());
    }

    private void OnApplicationPause (bool pauseStatus) {
        PlayOnSDK.onApplicationPause (pauseStatus);
    }
}