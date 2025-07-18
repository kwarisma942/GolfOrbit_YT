using System;
using System.Collections;
using System.Collections.Generic;
using Pinpin;
using UnityEngine;

public class OdeeoManager : MonoBehaviour
{
    public static OdeeoManager Instance;
    public Coroutine Show;
    public float timeToCloseOAD;
    AdUnit adLogo;
    public bool adPlaying;
    bool countTime;
    float adCounter;
    bool isInitialized;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (countTime && adPlaying)
            adCounter += Time.deltaTime;
    }

    public void Init()
    {
        if (PlayOnSDK.IsInitialized())
        {
            Debug.LogWarning("Odeeo already initilized");
            return;
        }

        //Test  if (!FunGamesManager.UseAdLogo)
        //Test  {
        //Test  Debug.LogWarning("UseAdLogo " + FunGamesManager.UseAdLogo);
        //Test  return;
        //Test }
        return;
        Debug.Log("Init  Odeeo");
        if (PlayOnSDK.GetRegulationType() == PlayOnSDK.ConsentType.Gdpr)
        {
            if (PlayerPrefs.GetInt("GDPRAnswered") == 1)
            {
                PlayOnSDK.ForceGdprApply(true);               
            }
            else
            {
                PlayOnSDK.ForceGdprApply(false);
            }
        }
        PlayOnSDK.SetIsChildDirected(false);

        PlayOnSDK.OnInitializationFinished += OnInitialized;
        PlayOnSDK.Initialize("52eaad24-5449-4a13-81b6-0c268ca0fc97");
        
    }

    void OnInitialized()
    {
        Debug.Log("Initialized  Odeeo");
        InitLogo();
        
    }


    public void InitLogo()
    {
        Debug.Log("Initialized  AdLogo");

        PlayOnSDK.AdUnitType adType = PlayOnSDK.AdUnitType.AudioLogoAd;
        adLogo = new AdUnit(adType);

        Color adUnitProgressBarTint = Color.black;
        adLogo.SetProgressBar(adUnitProgressBarTint);

        int xOffset =0; //In pixels
        int yOffset =0; //In pixels
        int size = 80; //In pixels
        PlayOnSDK.Position adLocation = PlayOnSDK.Position.BottomRight;
        adLogo.SetLogo(adLocation, xOffset, yOffset, size);
        adLogo.AdCallbacks.OnClose += ADlogoCLoseCallBack;
        adLogo.AdCallbacks.OnShow += ADlogoShowCallBack;
    }

    public void ADlogoShowCallBack()
    {
        ApplicationManager.MuteSound();
        Debug.Log("OAD show: adPlaying " + adPlaying + "countTime " + countTime + " time since last ad " + adCounter);
        adPlaying = true;
        countTime = true;
        adCounter = 0;
    }

    public void ADlogoCLoseCallBack()
    {
        ApplicationManager.UnMuteSound();
        Debug.Log("OAD close: adPlaying " + adPlaying + "countTime " + countTime + " time since last ad " + adCounter);
        adPlaying = false;
        countTime = false;
        adCounter = 0;
    }


    public void ShowAdLogo()
    {
        if(!PlayOnSDK.IsInitialized())
        {
            Init();
            return;
        }

        Debug.Log("OAD try show AdLogo");
        return;
        //Test  if (FunGamesManager.UseAdLogo)
        //Test {
        if (adPlaying)
            {
                Debug.Log("OAD: time since last ad " + adCounter + " time To Close OAD " + timeToCloseOAD);
                if (adCounter < timeToCloseOAD)
                {           
                    Debug.Log("OAD still has to offer");
                    return;
                }
                CloseAdLogo();
            }
            Show = StartCoroutine(ShowAdLogoCorrutine());
        //Test }
    }

    public IEnumerator ShowAdLogoCorrutine()
    {
        if (adLogo != null)
        {
            while (!adLogo.IsAdAvailable())
            {
                //Debug.LogError(Time.time);
                yield return new WaitForSeconds(0);
            }
            adLogo.ShowAd();
            Debug.Log("OAD Showed");
        }
      
    }

    public void CloseAdLogo()
    {
        if (adLogo != null)
        {
            adLogo.CloseAd();
        }
        
        if(Show!=null)
            StopCoroutine(Show);    
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        PlayOnSDK.onApplicationPause(pauseStatus);
        Debug.Log("OAD pause: adPlaying " + adPlaying + "countTime " + countTime + " time since last ad " + adCounter);
        countTime = pauseStatus && adPlaying;
        Debug.Log("OAD pause: adPlaying " + adPlaying + "countTime " + countTime + " time since last ad " + adCounter);
    }
}
