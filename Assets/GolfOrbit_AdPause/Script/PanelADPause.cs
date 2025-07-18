using System.Collections;
using System.Collections.Generic;
using Pinpin;
using Pinpin.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Pinpin.Scene.MainScene.UI;

public class PanelADPause : MonoBehaviour
{
    public static PanelADPause Instance;

    public GameObject Panel;
    public GameObject WithMrec,WithoutMrec;
    public Text CoinsTxt;
    public Text RewardValue, FullRewardValue;
    public Text RewardValue2, FullRewardValue2;
    public Text RewardValueBtn, FullRewardValueBtn;
    public Text RewardValueBtn2, FullRewardValueBtn2;
    public List<UIShopButton> UiShops;
    public Image TimeImage;
    public Image TimeImage2;
    public Text TimeTxt;
    public Text TimeTxt2;
    [SerializeField] private ParticleSystem CoinsFx;
    [SerializeField] private ParticleSystem BigCoinsFX;


    public ulong Coins;
    Coroutine CloseCoroutine;

    void Start()
    {
        Instance = this;
    }

   


    public void Open()
    {
        Panel.SetActive(true);
        if (ExampleRemoteConfigABtests._instance.UseAdPauseMrec)
        {
            WithMrec.SetActive(true);
            WithoutMrec.SetActive(false);
        }
        else
        {
            WithMrec.SetActive(false);
            WithoutMrec.SetActive(true);
        }
        Coins = GetCoins();
        RewardValue.text = ((int)(Coins / 3)).ToString();
        RewardValue2.text = ((int)(Coins / 3)).ToString();
        RewardValueBtn.text = ((int)(Coins / 3)).ToString();
        RewardValueBtn2.text = ((int)(Coins / 3)).ToString();
        FullRewardValue.text = Coins.ToString();
        FullRewardValue2.text = Coins.ToString();
        FullRewardValueBtn.text = Coins.ToString();
        FullRewardValueBtn2.text = Coins.ToString();
        TimeImage.DOFillAmount(0f, 5);
        TimeImage2.DOFillAmount(0f, 5);
        CloseCoroutine = StartCoroutine(Close(5));
    }

    public IEnumerator Close(float time)
    {
        TimeTxt.text = "5";
        TimeTxt2.text = "5";
        yield return new WaitForSeconds(time/5);
        TimeTxt.text = "4";
        TimeTxt2.text = "4";
        yield return new WaitForSeconds(time / 5);
        TimeTxt.text = "3";
        TimeTxt2.text = "3";
        yield return new WaitForSeconds(time / 5);
        TimeTxt.text = "2";
        TimeTxt2.text = "2";
        yield return new WaitForSeconds(time / 5);
        TimeTxt.text = "1";
        TimeTxt2.text = "1";
        yield return new WaitForSeconds(time / 5);
        Collect();
    }

    public void Close()
    {
        Panel.SetActive(false);
    }

    public ulong GetCoins()
    {
        ulong res = 0;
        foreach(UIShopButton ui in UiShops)
        {
            if (res <= ui.cost)
            {
                res = ui.cost;
            }
        }
        return res*2;
    }


    public void TripleAction()
    {
        if(CloseCoroutine!=null)
             StopCoroutine(CloseCoroutine);
        //FGMediation.ShowRewarded((status) =>
        //{
        //  Time.timeScale = 1f;
        //  if (status)
        //  {
        //      ApplicationManager.datas.coins += Coins;
        //      CoinsTxt.text = ApplicationManager.datas.coins.ToString();
        //      BigCoinsFX.Play();
        //      Close();
        //  }
        //});
        EGameEvent.Invoke(EEvents.RewardTripleCoins, new EData(this));
    }

    public void OnRewardedTripleCoins(bool succeed)
    {
        if (succeed)
        {
            ApplicationManager.datas.coins += Coins;
            CoinsTxt.text = ApplicationManager.datas.coins.ToString();
            BigCoinsFX.Play();
            Close();
        }
    }

    public void Collect()
    {
        if (CloseCoroutine != null)
            StopCoroutine(CloseCoroutine);
         // FGMediation.ShowInterstitial("collect",(status) =>
         // {
         //   ApplicationManager.datas.coins += (Coins/3);
         //   CoinsTxt.text = ApplicationManager.datas.coins.ToString();
         //   CoinsFx.Play();
         //   Close();
         //});
    }
}
