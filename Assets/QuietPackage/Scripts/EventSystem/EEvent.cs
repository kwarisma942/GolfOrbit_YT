
public enum EEvents
{
    // Level Events :
    OnLevelStarted,             // Data => 
    OnLevelCompleted,           // Data => 
    OnLevelFailed,              // Data => 

    // Reward Events
    RewardRespinWheel,                  // Data => GiftWheelPanel:callback=>OnRewardedGiftWheel(bool)
    RewardRandomChar,                   // Data => ClubHousePanel:callback=>OnRewardedRandomChar(bool)
    RewardCollectRewardMultiply,        // Data => EndGamePanel:callback=>OnCollectRewardMultiply(bool)
    RewardOneMoreChance,                // Data => GamePanel:callback=>OnRewardOneMoreChance(bool)
    RewardJackpot,                      // Data => JackpotPanel:callback=>OnRewardedJackpot(bool)
    RewardGoldenBall,                   // Data => MainPanel:callback=>OnRewardedGoldenBall(bool)
    RewardRedBall,                   // Data => MainPanel:callback=>OnRewardedGoldenBall(bool)
    RewardBlueBall,                   // Data => MainPanel:callback=>OnRewardedGoldenBall(bool)
    RewardPurpleBall,                   // Data => MainPanel:callback=>OnRewardedGoldenBall(bool)
    RewardOrangeBall,                   // Data => MainPanel:callback=>OnRewardedGoldenBall(bool)
    RewardBonusGoldenBall,              // Data => MainPanel:callback=>OnRewardedBonusGoldenBall(bool)
    RewardCollectOfflineRewardMultiply, // Data => OfflineEarningsPopup:callback=>OnRewardedCollectOfflineRewardMultiply(bool)
    RewardRVUpgrade,                    // Data => UIShopButton:callback=>OnRewardedRVUpgrade(bool)
    RewardTripleCoins,                  // Data => PanelADPause:callback=>OnRewardedTripleCoins(bool)
}
