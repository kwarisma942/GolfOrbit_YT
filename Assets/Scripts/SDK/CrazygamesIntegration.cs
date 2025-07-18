
//using CrazyGames;
//using Pinpin.UI;
//using System.Collections;
//using UnityEngine;
//using Pinpin.Scene.MainScene.UI;
//using UnityEngine.SceneManagement;
//using Pinpin;

//public class CrazygamesIntegration : MonoBehaviour
//{
//    private void Awake() => DontDestroyOnLoad(gameObject);


//    private void Start()
//    {
//        if (CrazySDK.IsAvailable)
//        {
//            CrazySDK.Init(() =>
//            {
//                Debug.Log("CrazySDK initialized");
//                // SceneManager.LoadScene(1);
//            });
//        }
//        else
//        {
//            // SceneManager.LoadScene(1);
//        }
//    }


//    private void OnEnable()
//    {
//        EGameEvent.AddListener(EEvents.OnLevelStarted, OnLevelStartedFunc);
//        EGameEvent.AddListener(EEvents.OnLevelCompleted, OnLevelCompletedFunc);
//        EGameEvent.AddListener(EEvents.OnLevelFailed, OnLevelFailedFunc);

//        EGameEvent.AddListener(EEvents.RewardRespinWheel, OnRewardRespinWheel);
//        EGameEvent.AddListener(EEvents.RewardRandomChar, OnRewardRandomChar);
//        EGameEvent.AddListener(EEvents.RewardCollectRewardMultiply, OnRewardCollectRewardMultiply);
//        EGameEvent.AddListener(EEvents.RewardOneMoreChance, OnRewardOneMoreChance);
//        EGameEvent.AddListener(EEvents.RewardJackpot, OnRewardJackpot);
//        EGameEvent.AddListener(EEvents.RewardGoldenBall, OnRewardGoldenBall);
//        EGameEvent.AddListener(EEvents.RewardBonusGoldenBall, OnRewardBonusGoldenBall);
//        EGameEvent.AddListener(EEvents.RewardCollectOfflineRewardMultiply, OnRewardCollectOfflineRewardMultiply);
//        EGameEvent.AddListener(EEvents.RewardRVUpgrade, OnRewardRVUpgrade);
//        EGameEvent.AddListener(EEvents.RewardTripleCoins, OnRewardTripleCoins);

//        EGameEvent.AddListener(EEvents.RewardRedBall, OnRewardRedBall);
//        EGameEvent.AddListener(EEvents.RewardBlueBall, OnRewardBlueBall);
//        EGameEvent.AddListener(EEvents.RewardPurpleBall, OnRewardPurpleBall);
//        EGameEvent.AddListener(EEvents.RewardOrangeBall, OnRewardOrangeBall);

//    }

//    private void OnDisable()
//    {
//        EGameEvent.RemoveListener(EEvents.OnLevelStarted, OnLevelStartedFunc);
//        EGameEvent.RemoveListener(EEvents.OnLevelCompleted, OnLevelCompletedFunc);
//        EGameEvent.RemoveListener(EEvents.OnLevelFailed, OnLevelFailedFunc);

//        EGameEvent.RemoveListener(EEvents.RewardRespinWheel, OnRewardRespinWheel);
//        EGameEvent.RemoveListener(EEvents.RewardRandomChar, OnRewardRandomChar);
//        EGameEvent.RemoveListener(EEvents.RewardCollectRewardMultiply, OnRewardCollectRewardMultiply);
//        EGameEvent.RemoveListener(EEvents.RewardOneMoreChance, OnRewardOneMoreChance);
//        EGameEvent.RemoveListener(EEvents.RewardJackpot, OnRewardJackpot);
//        EGameEvent.RemoveListener(EEvents.RewardGoldenBall, OnRewardGoldenBall);
//        EGameEvent.RemoveListener(EEvents.RewardBonusGoldenBall, OnRewardBonusGoldenBall);
//        EGameEvent.RemoveListener(EEvents.RewardCollectOfflineRewardMultiply, OnRewardCollectOfflineRewardMultiply);
//        EGameEvent.RemoveListener(EEvents.RewardRVUpgrade, OnRewardRVUpgrade);
//        EGameEvent.RemoveListener(EEvents.RewardTripleCoins, OnRewardTripleCoins);

//        EGameEvent.RemoveListener(EEvents.RewardRedBall, OnRewardRedBall);
//        EGameEvent.RemoveListener(EEvents.RewardBlueBall, OnRewardBlueBall);
//        EGameEvent.RemoveListener(EEvents.RewardPurpleBall, OnRewardPurpleBall);
//        EGameEvent.RemoveListener(EEvents.RewardOrangeBall, OnRewardOrangeBall);


//    }



//    private void OnLevelStartedFunc(EData data)
//    {
//        //int level = data.Get<int>(0);
//        Debug.Log($" OnLevelStarted: ");

//        CrazySDK.Game.GameplayStart();
//    }

//    private void OnLevelCompletedFunc(EData data)
//    {
//        //int level = data.Get<int>(0);
//        Debug.Log($" OnLevelCompleted: ");

//        StartCoroutine(coro());
//        IEnumerator coro()
//        {
//            CrazySDK.Game.GameplayStop();
//            yield return new WaitForSeconds(.15f);
//            //if (!GameManager.isGamePanel)
//            //{
//            //    CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
//            //{
//            //    /** ad started */
//            //}, (error) =>
//            //{
//            //    /** ad error */
//            //}, () =>
//            //{
//            //    /** ad finished, rewarded players here for CrazyAdType.Rewarded */
//            //});
//            //}
//           // GameManager.isGamePanel = false;
//        }
//    }

//    private void OnLevelFailedFunc(EData data)
//    {
//        //int level = data.Get<int>(0);
//        Debug.Log($" OnLevelFailed: " );

//        StartCoroutine(coro());
//        IEnumerator coro()
//        {
//            CrazySDK.Game.GameplayStop();
//            yield return new WaitForSeconds(.15f);
//            //if (!GameManager.isGamePanel)
//            //{
//                CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
//                {
//                    /** ad started */
//                }, (error) =>
//                {
//                    /** ad error */
//                }, () =>
//                {
//                    /** ad finished, rewarded players here for CrazyAdType.Rewarded */
//                });
//           // }

//           // GameManager.isGamePanel = false;
//        }
//    }

//    private void OnRewardRespinWheel(EData data)
//    {
//        GiftWheelPanel callback = data.Get<GiftWheelPanel>(0);
//        Debug.Log($" OnRewardRespinWheel: ");

//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedGiftWheel(false);
//        }, () =>
//        {
//            callback.OnRewardedGiftWheel(true);
//        });


//    }

//    private void OnRewardRandomChar(EData data)
//    {
//        ClubHousePanel callback = data.Get<ClubHousePanel>(0);
//        Debug.Log($" OnRewardRandomChar: ");

//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedRandomChar(false);
//        }, () =>
//        {
//            callback.OnRewardedRandomChar(true);
//        });

      
//    }

//    private void OnRewardCollectRewardMultiply(EData data)
//    {
//        EndGamePanel callback = data.Get<EndGamePanel>(0);
//        Debug.Log($" OnRewardCollectRewardMultiply: ");
//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnCollectRewardMultiply(false);
//        }, () =>
//        {
//            callback.OnCollectRewardMultiply(true);
//        });

      
//    }

//    private void OnRewardOneMoreChance(EData data)
//    {
//        GamePanel callback = data.Get<GamePanel>(0);
//        Debug.Log($" OnRewardOneMoreChance: ");
//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardOneMoreChance(false);
//        }, () =>
//        {
//            callback.OnRewardOneMoreChance(true);
//        });
//    }

//    private void OnRewardJackpot(EData data)
//    {
//        JackpotPanel callback = data.Get<JackpotPanel>(0);
//        Debug.Log($" OnRewardJackpot: ");

//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedJackpot(false);
//        }, () =>
//        {
//            callback.OnRewardedJackpot(true);
//        });
//    }

//    private void OnRewardGoldenBall(EData data)
//    {
//        MainPanel callback = data.Get<MainPanel>(0);
//        Debug.Log($" OnRewardGoldenBall: ");

//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedGoldenBall(false);
//        }, () =>
//        {
//            callback.OnRewardedGoldenBall(true);
//        });
//    }

//    private void OnRewardRedBall(EData data)
//    {
//        MainPanel callback = data.Get<MainPanel>(0);
//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () => { }, error => callback.OnRewardedRedBall(false), () => callback.OnRewardedRedBall(true));
//    }

//    private void OnRewardBlueBall(EData data)
//    {
//        MainPanel callback = data.Get<MainPanel>(0);
//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () => { }, error => callback.OnRewardedBlueBall(false), () => callback.OnRewardedBlueBall(true));
//    }

//    private void OnRewardPurpleBall(EData data)
//    {
//        MainPanel callback = data.Get<MainPanel>(0);
//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () => { }, error => callback.OnRewardedPurpleBall(false), () => callback.OnRewardedPurpleBall(true));
//    }

//    private void OnRewardOrangeBall(EData data)
//    {
//        MainPanel callback = data.Get<MainPanel>(0);
//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () => { }, error => callback.OnRewardedOrangeBall(false), () => callback.OnRewardedOrangeBall(true));
//    }


//    private void OnRewardBonusGoldenBall(EData data)
//    {
//        MainPanel callback = data.Get<MainPanel>(0);
//        Debug.Log($" OnRewardBonusGoldenBall: ");

//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedBonusGoldenBall(false);
//        }, () =>
//        {
//            callback.OnRewardedBonusGoldenBall(true);
//        });
//    }

//    private void OnRewardCollectOfflineRewardMultiply(EData data)
//    {
//        OfflineEarningsPopup callback = data.Get<OfflineEarningsPopup>(0);
//        Debug.Log($" OnRewardCollectOfflineRewardMultiply: ");

//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedCollectOfflineRewardMultiply(false);
//        }, () =>
//        {
//            callback.OnRewardedCollectOfflineRewardMultiply(true);
//        });
//    }

//    private void OnRewardRVUpgrade(EData data)
//    {
//        UIShopButton callback = data.Get<UIShopButton>(0);
//        Debug.Log($" OnRewardRVUpgrade: ");

//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedRVUpgrade(false);
//        }, () =>
//        {
//            callback.OnRewardedRVUpgrade(true);
//        });
//    }

//    private void OnRewardTripleCoins(EData data)
//    {
//        PanelADPause callback = data.Get<PanelADPause>(0);
//        Debug.Log($" OnRewardTripleCoins: ");
//        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
//        {
//            /** ad started */
//        }, (error) =>
//        {
//            callback.OnRewardedTripleCoins(false);
//        }, () =>
//        {
//            callback.OnRewardedTripleCoins(true);
//        });
//    }
//}
