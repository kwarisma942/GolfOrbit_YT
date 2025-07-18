using UnityEngine;
using System;
#if UNITY_EDITOR
using System.Collections;
using System.Dynamic;
using UnityEditor;
using Object = UnityEngine.Object;
#endif
using System.Runtime.InteropServices;
public class AdUnit : IDisposable
{

#if UNITY_ANDROID && !UNITY_EDITOR
    protected AndroidJavaObject client;
#endif

#if UNITY_EDITOR
    protected GameObject stubLogo;
    protected bool editorAdAvailable;
#endif
#if UNITY_IOS && !UNITY_EDITOR
    protected IntPtr client;

    [DllImport("__Internal")]
    public static extern IntPtr _playOnCreateAudioAdUnit(int adType);
    [DllImport("__Internal")]
    public static extern void _playOnShow(IntPtr client);
    [DllImport("__Internal")]
    public static extern void _playOnClose(IntPtr client);
    [DllImport("__Internal")]
    public static extern void _playOnSetLogo(IntPtr client, int position, int xOffset, int yOffset, int size);
    [DllImport("__Internal")]
    public static extern bool _playOnIsAdAvailable(IntPtr client);
    [DllImport("__Internal")]
    public static extern IntPtr _playOnSetListeners(IntPtr client, IntPtr callbackRef, AdListener.PlayOnNoArgsDelegateNative onShow, AdListener.PlayOnNoArgsDelegateNative onClose, AdListener.PlayOnNoArgsDelegateNative onClick, AdListener.PlayOnStateDelegateNative onAvailabilityChange, AdListener.PlayOnFloatDelegateNative onReward, AdListener.PlayOnDataDelegateNative onImpression);
    [DllImport("__Internal")]
    public static extern IntPtr _playOnCreateMutableArray();
    [DllImport("__Internal")]
    public static extern void _playOnAddToMutableArray(IntPtr dictionary, int item);
    [DllImport("__Internal")]
    public static extern void _playOnSetReward(IntPtr client, int rewardType, float value);
    [DllImport("__Internal")]
    public static extern void _playOnSetPopup(IntPtr client, int location, int xOffset, int yOffset);
    [DllImport("__Internal")]
    public static extern void _playOnSetBanner(IntPtr client, int location);
    [DllImport("__Internal")]
    public static extern void _playOnSetVisualization(IntPtr client, string tint, string backgound);
    [DllImport("__Internal")]
    public static extern void _playOnSetActionButton(IntPtr client, int actionType, float delayTime);
    [DllImport("__Internal")]
    public static extern void _playOnSetProgressBar(IntPtr client, string tint);
    [DllImport("__Internal")]
    public static extern void _playOnDestroyBridgeReference(IntPtr obj);
    [DllImport("__Internal")]
    public static extern void _playOnTrackRewardedOffer(IntPtr obj);
    [DllImport("__Internal")]
    public static extern void _playOnPause();
    [DllImport("__Internal")]
    public static extern void _playOnResume();
#endif
    private float fadeValue = 0.1f;
    private float sceneVolumeValue = 1f;
    private int _xOffset = 50;
    private int _yOffset = 50;
    private int _size = 70;
    private PlayOnSDK.Position _location = PlayOnSDK.Position.TopLeft;
    
    private AdListener adListener = new AdListener();
    public AdListener AdCallbacks
    {
        get
        {
            return adListener;
        }
    }

    public class ImpressionData
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaObject client;

        public ImpressionData(AndroidJavaObject ptr){
            client = ptr;
        }
#elif UNITY_IOS && !UNITY_EDITOR
        IntPtr client;

        [DllImport("__Internal")]
        public static extern string _playOnImpressionGetPlacementID(IntPtr obj);
            [DllImport("__Internal")]
        public static extern string _playOnImpressionGetSessionID(IntPtr obj);
            [DllImport("__Internal")]
        public static extern int _playOnImpressionGetAdType(IntPtr obj);
            [DllImport("__Internal")]
        public static extern string _playOnImpressionGetCountry(IntPtr obj);
            [DllImport("__Internal")]
        public static extern double _playOnGetRevenue(IntPtr obj);

        public ImpressionData(IntPtr ptr){
            client = ptr;
        }
#endif

        public string GetPlacementID(){
#if UNITY_ANDROID && !UNITY_EDITOR
            return client.Call<string>("getPlacementID");
#elif UNITY_IOS && !UNITY_EDITOR
            return _playOnImpressionGetPlacementID(client);
#endif
            return null;
        }

        public string GetSessionID(){
#if UNITY_ANDROID && !UNITY_EDITOR
            return client.Call<string>("getSessionID");
#elif UNITY_IOS && !UNITY_EDITOR
            return _playOnImpressionGetSessionID(client);
#endif
            return null;
        }

        public PlayOnSDK.AdUnitType GetAdType(){
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaObject enumAdType = client.Call<AndroidJavaObject>("getAdType");
            int typeIndex = enumAdType.Call<int> ("ordinal");
            return (PlayOnSDK.AdUnitType)typeIndex;
#elif UNITY_IOS && !UNITY_EDITOR
            return (PlayOnSDK.AdUnitType)_playOnImpressionGetAdType(client);
#endif
            return PlayOnSDK.AdUnitType.AudioLogoAd;
        }

        public string GetCountry(){
#if UNITY_ANDROID && !UNITY_EDITOR
            return client.Call<string>("getCountry");
#elif UNITY_IOS && !UNITY_EDITOR
            return _playOnImpressionGetCountry(client);
#endif
            return null;
        }

        public double GetRevenue(){
#if UNITY_ANDROID && !UNITY_EDITOR
            return client.Call<double>("getRevenue");
#elif UNITY_IOS && !UNITY_EDITOR
            return _playOnGetRevenue(client);
#endif
            return 0;
        }
    }

    public AdUnit(PlayOnSDK.AdUnitType adType)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass typeEnum = new AndroidJavaClass ("com.playon.bridge.AdUnit$AdUnitType");
        AndroidJavaObject curType = typeEnum.CallStatic<AndroidJavaObject> ("valueOf", adType.ToString ());
        client = new AndroidJavaObject ("com.playon.bridge.AdUnit", activity, curType, adListener);
#elif UNITY_IOS && !UNITY_EDITOR
        client = _playOnCreateAudioAdUnit((int)adType);
#endif
        SetAdListener();

        UnityMainThreadDispatcher.Instance();
#if UNITY_EDITOR
        UnityMainThreadDispatcher.Instance().Enqueue( () =>  AdCallbacks.OnAvailabilityChanged(true) );
#endif
        PlayOnSDK.onApplicationPause += OnApplicationPause;
    }

    protected void SetAdListener()
    {
#if UNITY_IOS && !UNITY_EDITOR
        adListener.adNativeListenerRef = _playOnSetListeners(client, (IntPtr)GCHandle.Alloc(adListener), AdListener.OnShowNative, AdListener.OnCloseNative, AdListener.OnClickNative, AdListener.OnAvailabilityChangedNative, AdListener.OnRewardNative, AdListener.OnImpressionNative);
#endif

        adListener.OnClose += onClose;
        adListener.OnShow += onShow;
    }

    public void ShowAd()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        client.Call ("showAd");
#endif

#if UNITY_IOS && !UNITY_EDITOR
        _playOnShow(client);
#endif
        
#if UNITY_EDITOR
        if (stubLogo == null)
        {
            var logoPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/PlayOn/Editor/Prefabs/PlayOnAD.prefab");
            stubLogo = Object.Instantiate(logoPrefab, Vector3.zero, Quaternion.identity);
            stubLogo.GetComponent<EditorAdUnit>().Init(this, _location, _xOffset, _yOffset, _size);
            Object.DontDestroyOnLoad(stubLogo);
            editorAdAvailable = false;
        }
#endif
    }

    [Obsolete("Function is out of date. Will be removed soon")]
    public void FadeSounds(bool isFade, float fadeValue = 0.2f)
    {

    }

    public void CloseAd()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        client.Call ("closeAd");
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnClose(client);
#elif UNITY_EDITOR
        editorAdAvailable = true;
#endif
    }

    public void SetBanner(PlayOnSDK.Position location)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass posEnum = new AndroidJavaClass ("com.playon.bridge.AdUnit$Position");
        AndroidJavaObject curPos = posEnum.CallStatic<AndroidJavaObject> ("valueOf", location.ToString ());

        client.Call ("setBanner", curPos);
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnSetBanner(client, (int)location);
#endif
    }

    public bool IsAdAvailable()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return client.Call<bool>("isAdAvailable");
#elif UNITY_IOS && !UNITY_EDITOR
        return _playOnIsAdAvailable(client);
#elif UNITY_EDITOR
        return editorAdAvailable;
#else
        return false;
#endif
    }

    public void SetReward(PlayOnSDK.AdUnitRewardType rewardType, float value){
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass en = new AndroidJavaClass ("com.playon.bridge.AdUnit$RewardType");
        AndroidJavaObject curValue = en.CallStatic<AndroidJavaObject> ("valueOf", rewardType.ToString ());
        client.Call ("setReward", curValue, value);
#elif UNITY_IOS && !UNITY_EDITOR
       _playOnSetReward(client, (int)rewardType, value);
#endif
    }

    public void SetPopup(PlayOnSDK.Position position, int xOffset, int yOffset)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass en = new AndroidJavaClass ("com.playon.bridge.AdUnit$Position");
        AndroidJavaObject curValue = en.CallStatic<AndroidJavaObject> ("valueOf", position.ToString ());
        client.Call ("setPopup", curValue, xOffset, yOffset);
#elif UNITY_IOS && !UNITY_EDITOR
       _playOnSetPopup(client, (int)position, xOffset, yOffset);
#endif
    }

    public void SetProgressBar(Color progressBarColor){
#if UNITY_ANDROID && !UNITY_EDITOR
        string hexProgressBarColor = ColorUtility.ToHtmlStringRGBA(progressBarColor);
        hexProgressBarColor = "#" + hexProgressBarColor.Substring(6) + hexProgressBarColor.Remove(6);

        client.Call ("setProgressBar", hexProgressBarColor);
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnSetProgressBar(client, "#"+ColorUtility.ToHtmlStringRGB(progressBarColor));
#endif
    }

    public void SetActionButton(PlayOnSDK.AdUnitActionButtonType actionType, float delayTime){
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass en = new AndroidJavaClass ("com.playon.bridge.AdUnit$ActionButtonType");
        AndroidJavaObject cur = en.CallStatic<AndroidJavaObject> ("valueOf", actionType.ToString ());

        client.Call ("setActionButton", cur, delayTime);
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnSetActionButton(client, (int)actionType, delayTime);
#endif
    }
    
    public void LinkLogoToRectTransform(PlayOnSDK.Position position, RectTransform rectTransform, Canvas canvas, int size)
    {

        Rect rect = RectTransformExtension.GetScreenRect(rectTransform, canvas);
#if UNITY_IOS
        var positinDP = ConvertRectPositionToDPForIOS(rect, position, size);
#else
        var positinDP = ConvertRectPositionToDP(rect, position, size);
     #endif
        int xOffset = (int) positinDP.x;
        int yOffset = (int) positinDP.y;
        SetLogo(PlayOnSDK.Position.BottomLeft, xOffset, yOffset, size);
    }
    
    private Vector2 ConvertRectPositionToDPForIOS(Rect rect, PlayOnSDK.Position position, int size) {
        Vector2 result = Vector2.zero;
        var screenHeight = Screen.height;
        if (Screen.safeArea.height == Screen.height)
        {
            switch (position)
        {
            case PlayOnSDK.Position.Centered:
                result.x = (rect.center.x - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((screenHeight - rect.center.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                break;
            case PlayOnSDK.Position.BottomLeft:
                result.x = (rect.xMin - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                result.y = ((screenHeight - rect.yMax) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.BottomRight:
                result.x = (rect.xMax - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                result.y = ((screenHeight - rect.yMax) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.TopLeft:
                result.x = (rect.xMin - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                result.y = ((screenHeight - rect.yMin) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f) - size;
                break;
            case PlayOnSDK.Position.TopRight:
                result.x = (rect.xMax - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                result.y = ((screenHeight - rect.yMin) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                break;
            case PlayOnSDK.Position.CenterLeft:
                result.x = (rect.xMin - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f);
                result.y = ((screenHeight- rect.center.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                break;
            case PlayOnSDK.Position.CenterRight:
                result.x = (rect.xMax - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f) - size;
                result.y = ((screenHeight - rect.center.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f) - size / 2;
                ;
                break;
            case PlayOnSDK.Position.BottomCenter:
                result.x = (rect.center.x - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((screenHeight - rect.yMax) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.TopCenter:
                result.x = (rect.center.x - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((screenHeight - rect.yMin) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                break;
        }
        }
        else
        {
            switch (position) {
            case PlayOnSDK.Position.Centered:
                result.x = (rect.center.x - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((screenHeight - rect.center.y - Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                break;
            case PlayOnSDK.Position.BottomLeft:
                result.x = (rect.xMin - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                result.y = ((screenHeight - rect.yMax - Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.BottomRight:
                result.x = (rect.xMax - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                result.y = ((screenHeight - rect.yMax - Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.TopLeft:
                result.x = (rect.xMin - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                result.y = ((screenHeight - rect.yMin - Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f) - size;
                break;
            case PlayOnSDK.Position.TopRight:
                result.x = (rect.xMax - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                result.y = ((screenHeight - rect.yMin- Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                break;
            case PlayOnSDK.Position.CenterLeft:
                result.x = (rect.xMin - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f);
                result.y = ((screenHeight- rect.center.y- Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                break;
            case PlayOnSDK.Position.CenterRight:
                result.x = (rect.xMax - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f) - size;
                result.y = ((screenHeight - rect.center.y- Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f) - size / 2;
                ;
                break;
            case PlayOnSDK.Position.BottomCenter:
                result.x = (rect.center.x - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((screenHeight - rect.yMax- Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.TopCenter:
                result.x = (rect.center.x - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((screenHeight - rect.yMin- Screen.safeArea.y) - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                break;
            }
        }
        
        return result;
    }
    
     private Vector2 ConvertRectPositionToDP(Rect rect, PlayOnSDK.Position position, int size) {
        Vector2 result = Vector2.zero;
         switch (position)
        {
            case PlayOnSDK.Position.Centered:
                result.x = (rect.center.x - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((Screen.height - rect.center.y) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                break;
            case PlayOnSDK.Position.BottomLeft:
                result.x = (rect.xMin - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f);
                result.y = ((Screen.height - rect.yMax) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.BottomRight:
                result.x = (rect.xMax - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                result.y = ((Screen.height - rect.yMax) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.TopLeft:
                result.x = (rect.xMin - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f);
                result.y = ((Screen.height - rect.yMin) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI() / 160f) - size;
                break;
            case PlayOnSDK.Position.TopRight:
                result.x = (rect.xMax - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                result.y = ((Screen.height - rect.yMin) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                break;
            case PlayOnSDK.Position.CenterLeft:
                result.x = (rect.xMax - 0.5f) / (PlayOnSDK.GetUnityEditorDPI() / 160f) - size;
                result.y = ((Screen.height - rect.center.y) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI() / 160f) - size / 2;
                break;
            case PlayOnSDK.Position.CenterRight:
                result.x = (rect.xMin - 0.5f) / (PlayOnSDK.GetUnityEditorDPI() / 160f);
                result.y = ((Screen.height - rect.center.y) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                ;
                break;
            case PlayOnSDK.Position.BottomCenter:
                result.x = (rect.center.x - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((Screen.height - rect.yMax) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f);
                break;
            case PlayOnSDK.Position.TopCenter:
                result.x = (rect.center.x - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size / 2;
                result.y = ((Screen.height - rect.yMin) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI()  / 160f) - size;
                break;
        }
        return result;
    }
     

    public void LinkLogoToPrefab(AdUnitAnchor adUnitAnchor)
    {
        var data = adUnitAnchor.GetDPPositionData();
        SetLogo(data.location, data.xOffset, data.yOffset, data.size);
    }
    public void SetLogo(PlayOnSDK.Position position, int xOffset, int yOffset, int size) {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass posEnum = new AndroidJavaClass ("com.playon.bridge.AdUnit$Position");
        AndroidJavaObject curPos = posEnum.CallStatic<AndroidJavaObject> ("valueOf", position.ToString ());

        client.Call ("setLogo", curPos, xOffset, yOffset, size);
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnSetLogo(client, (int)position, xOffset, yOffset, size);
#endif

#if UNITY_EDITOR
        this._xOffset = xOffset;
        this._yOffset = yOffset;
        this._size = size;
        this._location = position;
#endif
    }
    
    public void SetVisualization(Color tint, Color background)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string hexTintColor = ColorUtility.ToHtmlStringRGBA(tint);
        hexTintColor = "#" + hexTintColor.Substring(6) + hexTintColor.Remove(6);
        string hexBackgroundColor = ColorUtility.ToHtmlStringRGBA(background);
        hexBackgroundColor = "#" + hexBackgroundColor.Substring(6) + hexBackgroundColor.Remove(6);
        client.Call ("setVisualization", hexTintColor, hexBackgroundColor);
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnSetVisualization(client, "#"+ColorUtility.ToHtmlStringRGB(tint), "#"+ColorUtility.ToHtmlStringRGB(background));
#endif
    }

    public void TrackRewardedOffer()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        client.Call ("trackRewardedOffer");
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnTrackRewardedOffer(client);
#endif
    }

    void onShow()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => {
            sceneVolumeValue = AudioListener.volume;
            AudioListener.volume = fadeValue;
        });
    }

    void onClose()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => AudioListener.volume = sceneVolumeValue);
    }

    private void OnApplicationPause(bool isPause)
    {
        if (isPause) onPause();
        else onResume();
    }

    private void onPause()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        client.Call ("onPause");
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnPause();
#endif
    }

    private void onResume()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        client.Call ("onResume");
#elif UNITY_IOS && !UNITY_EDITOR
        _playOnResume();
#endif
    }

    public void Dispose()
    {
#if UNITY_IOS && !UNITY_EDITOR
        _playOnDestroyBridgeReference(client);
        _playOnDestroyBridgeReference(adListener.adNativeListenerRef);
#endif
    }
}