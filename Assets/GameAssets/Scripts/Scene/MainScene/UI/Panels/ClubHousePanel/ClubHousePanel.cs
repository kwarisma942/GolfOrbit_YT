using DG.Tweening;
using Pinpin.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Homebrew;
using UnityEngine.Serialization;
using CardSate = Pinpin.Scene.MainScene.UI.AShopCard.State;
using System;
using Pinpin.Helpers;

namespace Pinpin.Scene.MainScene.UI
{

	public sealed class ClubHousePanel : AUIPanel
	{
		//[System.Serializable]
		//class ClubHouseShopLayout
		//{
		//	public PushButton m_openShopButton;
		//	public GameObject m_shopCardPrefab;
		//	public GameObject m_shopPanel;
		//	public Text m_shopUnlocksProgressionText;
		//	public Image m_shopUnlocksProgrsseionFillbar;
		//	public Image m_itemSelectedPreview;
		//	public ScrollRect m_characterScrollRect;
		//	public List<AShopCard> m_shopCards;
		//}

		public GameObject RondomCharButton;
		[SerializeField] private RectTransform m_motherPanel;
		[SerializeField] private Text m_coinCountText;
		[SerializeField] private Text m_diamondCountText;
		[SerializeField] private RectTransform m_shopButtonsOutline;
		[SerializeField] private Text m_missionText;
		[SerializeField] private PushButton m_shopButton;
		[SerializeField] private PushButton m_buyButton;
		[SerializeField] private PushButton m_tryButton;
		[SerializeField] private GridLayoutGroup[] m_grids;
		[SerializeField] private LayoutElement[] m_selectorLayouts;

		[Foldout("Character", true)]
		[SerializeField] private PushButton m_characterShopButton;
		[SerializeField] private SkinShopCardCharacter m_characterCardPrefab;
		[SerializeField] private GameObject m_characterPanel;
		[SerializeField] private Text m_characterProgressionText;
		[SerializeField] private Image m_characterProgressionFillBar;
		[SerializeField] private Image m_characterSelectedImage;
		[SerializeField] private ScrollRect m_characterScrollRect;
		[FormerlySerializedAs("m_FreeTryButton")]
		[SerializeField] private PushButton m_freeTryButton;
		private readonly List<SkinShopCardCharacter> m_characterCards = new List<SkinShopCardCharacter>();
		private readonly List<SkinShopCardCharacter> m_premiumCharacterCards = new List<SkinShopCardCharacter>();
		private SkinShopCardCharacter m_instagramCharacterCard;

		[Foldout("World", true)]
		[SerializeField] private PushButton m_worldShopButton;
		[SerializeField] private GameObject m_worldCardPrefab;
		[SerializeField] private GameObject m_soonWorldCardPrefab;
		[SerializeField] private GameObject m_worldPanel;
		[SerializeField] private Text m_worldProgressionText;
		[SerializeField] private Image m_worldProgressionFillBar;
		[SerializeField] private Image m_worldSelectedImage;
		[SerializeField] private ScrollRect m_worldScrollRect;
		[SerializeField] private PushButton m_travelButton;
		private readonly List<SkinShopCardWorld> m_worldCards = new List<SkinShopCardWorld>();

		[Foldout("Ball", true)]
		[SerializeField] private PushButton m_ballShopButton;
		[SerializeField] private GameObject m_ballCardPrefab;
		[SerializeField] private GameObject m_ballPanel;
		[SerializeField] private Image m_ballSelectedImage;
		[SerializeField] private Image m_teeImage;
		[SerializeField] private ScrollRect m_ballScrollRect;
		private readonly List<SkinShopCardBall> m_ballCards = new List<SkinShopCardBall>();

		private GameObject m_currentPanel;
		private int m_worldSelectedCard;
		private int m_characterSelectedCardId;
		private AShopCard.Type m_lastCharacterType;
		private int m_ballSelectedCard;
        private bool m_instagramOpened;

        private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void Awake ()
		{
			m_characterShopButton.onClick += InitializeCharacterPanel;
			m_worldShopButton.onClick += InitializeWorldPanel;
			m_ballShopButton.onClick += InitializeBallPanel;
			m_travelButton.onClick += TravelToPlanet;


			if (!ApplicationManager.config.application.enablePurchasing)
			{
				m_shopButton.gameObject.SetActive(false);
			}

			m_tryButton.onClick += OnFreeTryBallButtonPressed;
			m_buyButton.onClick += OnBuyBallClick;
			m_freeTryButton.onClick += OnFreeTryButtonPressed;
			EndGamePanel.onCharacterUnlocked += OnCharacterUnlocked;

			base.Awake();
		}

		private void Start ()
		{
			m_shopButton.onClick += UIManager.sceneManager.OpenShop;

            if (m_grids.Length > 0)
			{
				StartCoroutine(ResizeElements());
			}
		}

		protected override void OnDestroy ()
		{
			m_characterShopButton.onClick -= InitializeCharacterPanel;
			m_worldShopButton.onClick -= InitializeWorldPanel;
			m_ballShopButton.onClick -= InitializeBallPanel;
			m_travelButton.onClick -= TravelToPlanet;

			m_tryButton.onClick -= OnFreeTryBallButtonPressed;
			m_buyButton.onClick -= OnBuyBallClick;
			m_freeTryButton.onClick -= OnFreeTryButtonPressed;
			m_shopButton.onClick -= UIManager.sceneManager.OpenShop;
			ApplicationManager.onVipActivated -= OnVipActivated;
			EndGamePanel.onCharacterUnlocked -= OnCharacterUnlocked;

			base.OnDestroy();
		}
		

        private void OnApplicationPause(bool pause)
        {
            if (!pause && m_instagramOpened)
            {
                ApplicationManager.datas.isInstagramCharacterUnlocked = true;
                OnCharacterSelect(0, AShopCard.Type.Instagram, true);
            }
		}

		private IEnumerator ResizeElements ()
		{
			yield return new WaitForEndOfFrame();

			Vector2 cellSize = m_motherPanel.sizeDelta.y < 0 ? new Vector2(500f, 500f) : new Vector2(500f, 500f);

			for (int i = 0; i < m_grids.Length; i++)
			{
				m_grids[i].cellSize = cellSize;
			}

			float prefHeight = m_motherPanel.sizeDelta.y < 0 ? 1500 : 2230;

			for (int i = 0; i < m_selectorLayouts.Length; i++)
			{
				m_selectorLayouts[i].preferredHeight = prefHeight;
			}
		}

		public void Init ()
		{
			m_currentPanel = m_characterPanel;

			InitializeShops();
			ApplicationManager.onVipActivated += OnVipActivated;
		}

		public void GetRandomeChar()
		{
			EGameEvent.Invoke(EEvents.RewardRandomChar, new EData(this));

			//FGMediation.ShowRewarded((status) =>
			//	{
			//		Time.timeScale = 1f;
			//		if (status)
			//		{
			//			int id = Pinpin.ApplicationManager.datas.UnlockRandomCharacter();
			//			OnCharacterUnlocked(id);
			//			OnCharacterSelect(id, AShopCard.Type.Normal);
			//	}

			// });



		}

		public void OnRewardedRandomChar(bool status)
		{
			if (status)
			{
				int id = Pinpin.ApplicationManager.datas.UnlockRandomCharacter();
				OnCharacterUnlocked(id);
				OnCharacterSelect(id, AShopCard.Type.Normal);
			}
		}


		public void InitializeShops ()
		{
			for (int i = 0; i < ApplicationManager.assets.premiumGolfers.Length; i++)
			{
				if (ApplicationManager.assets.premiumGolfers[i].enabled)
				{
					SkinShopCardCharacter card = Instantiate(m_characterCardPrefab, m_characterScrollRect.content);
					
					m_premiumCharacterCards.Add(card);
					card.Configure(i, AShopCard.Type.Premium);
					card.onSelect += OnCharacterSelect;
				}
			}

			if (ApplicationManager.datas.isInstagramCharacterUnlocked)
			{
				SkinShopCardCharacter card = Instantiate(m_characterCardPrefab, m_characterScrollRect.content);

				m_instagramCharacterCard = card;
				card.Configure(0, AShopCard.Type.Instagram);
				card.onSelect += OnCharacterSelect;
			}

			for (int i = 0; i < ApplicationManager.assets.golfers.Length; i++)
			{
				if (ApplicationManager.assets.golfers[i].enabled)
				{
					SkinShopCardCharacter card = Instantiate(m_characterCardPrefab, m_characterScrollRect.content);
					m_characterCards.Add(card);
					card.Configure(i);
					card.onSelect += OnCharacterSelect;
				}
			}
			
			for (int i = 0; i < ApplicationManager.assets.balls.Length; i++) //Do not display normal ball
			{
				GameObject card = Instantiate(m_ballCardPrefab, m_ballScrollRect.content);
				m_ballCards.Add(card.GetComponent<SkinShopCardBall>());
				m_ballCards[i].Configure(i);
				m_ballCards[i].onSelect += OnBallSelect;
			}

			m_ballCards[0].gameObject.SetActive(false);
			
			for (int i = 0; i < ApplicationManager.assets.planets.Length; i++)
			{
				GameObject card = Instantiate(m_worldCardPrefab, m_worldScrollRect.content);
				m_worldCards.Add(card.GetComponent<SkinShopCardWorld>());
				m_worldCards[i].Configure(i);
				m_worldCards[i].onSelect += OnWorldSelect;
			}
			
			if(m_soonWorldCardPrefab != null)
				Instantiate(m_soonWorldCardPrefab, m_worldScrollRect.content);
			
			m_ballPanel.SetActive(false);
			gameObject.SetActive(false);
		}


		private void OnEnable ()
		{
			UpdateCoins();
			UpdateDiamonds();

			m_worldPanel.SetActive(false);
			m_worldSelectedImage.sprite = ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId].shopPreview;

			StartCoroutine(WaitToInitializePanel());

			if (GPlayPassManager.IsActive)
				RondomCharButton.SetActive(false);
			EnableRandomChar();
		}

		public void EnableRandomChar()
		{
			//if (ExampleRemoteConfigABtests._instance.ShowRondomChar&&!GPlayPassManager.IsActive)
			//	RondomCharButton.SetActive(true);
			//else
				//RondomCharButton.SetActive(false);
		}


        protected override void OnDisable()
        {
			base.OnDisable();

			//RondomCharButton.SetActive(false);
		}

        private IEnumerator WaitToInitializePanel ()
		{
			yield return new WaitForEndOfFrame();
			InitializeCharacterPanel();
		}

		#region Character

		private void OnFreeTryButtonPressed ()
		{
			SubscriptionPanel panel;
			if( ApplicationManager.config.game.subPopupABTest >= 0)
				panel = UIManager.OpenPanel<NewSubscriptionPanel>();
			else
				 panel = UIManager.OpenPanel<SubscriptionPanel>();
			panel.Setup(1, 1, 1);
		}

		private void OnVipActivated ()
		{
			for (int i = 0; i < m_premiumCharacterCards.Count; i++)
			{
				m_premiumCharacterCards[i].Configure(i, AShopCard.Type.Premium);
			}
		}

		private void OnCharacterUnlocked ( int characterId )
		{
			m_characterCards[characterId].SetState(CardSate.Unlocked);
		}

		private void OnCharacterSelect( int characterId, AShopCard.Type type)
		{
			OnCharacterSelect(characterId, type, false);
		}

		private void OnCharacterSelect (int characterId, AShopCard.Type type, bool forceSelect)
		{
			/*if (m_characterSelectedCardId >= m_characterCards.Count || characterId >= m_characterCards.Count)
				return;*/

			if (characterId != m_characterSelectedCardId || type != m_lastCharacterType || forceSelect)
			{
				switch (m_lastCharacterType)
				{
					case AShopCard.Type.Normal:
						if (ApplicationManager.datas.IsCharacterUnlocked(m_characterSelectedCardId))
						{
							m_characterCards[m_characterSelectedCardId].SetState(CardSate.Unlocked);
						}
						else
						{
							m_characterCards[m_characterSelectedCardId].SetState(CardSate.Locked);
						}
						break;

					case AShopCard.Type.Premium:
						if (ApplicationManager.datas.vip)
						{
							m_premiumCharacterCards[m_characterSelectedCardId].SetState(CardSate.Unlocked);
						}
						else
						{
							m_premiumCharacterCards[m_characterSelectedCardId].SetState(CardSate.Locked);
						}
						break;

					case AShopCard.Type.Instagram:
						if (ApplicationManager.datas.isInstagramCharacterUnlocked)
						{
							m_instagramCharacterCard.SetState(CardSate.Unlocked);
						}
						else
						{
							m_instagramCharacterCard.SetState(CardSate.Locked);
						}
						break;
				}

				m_freeTryButton.gameObject.SetActive(false);
				m_missionText.gameObject.SetActive(false);
				switch (type)
				{
					case AShopCard.Type.Normal:

						if (ApplicationManager.datas.IsCharacterUnlocked(characterId))
						{
							ApplicationManager.datas.selectedCharacterId = characterId;
							ApplicationManager.datas.selectedCharacterType = (int)type;
							m_characterCards[ApplicationManager.datas.selectedCharacterId].SetState(CardSate.Selected);
							UIManager.sceneManager.UpdateGolfer();

							m_missionText.gameObject.SetActive(false);
						}
						else
						{
							m_characterCards[characterId].SetState(CardSate.LockedSelected);
							m_missionText.gameObject.SetActive(true);
						}
						m_characterSelectedCardId = characterId;
						m_characterSelectedImage.sprite = ApplicationManager.assets.golfers[m_characterSelectedCardId].shopPreview;
						MoveScrollBar(m_characterScrollRect, -m_characterCards[m_characterSelectedCardId].GetComponent<RectTransform>().anchoredPosition.y);

						break;
					case AShopCard.Type.Premium:
						if (ApplicationManager.datas.vip)
						{
							ApplicationManager.datas.selectedCharacterId = characterId;
							ApplicationManager.datas.selectedCharacterType = (int)type;
							m_premiumCharacterCards[ApplicationManager.datas.selectedCharacterId].SetState(CardSate.Selected);
							UIManager.sceneManager.UpdateGolfer();
						}
						else
						{
							m_premiumCharacterCards[characterId].SetState(CardSate.LockedSelected);
							m_freeTryButton.gameObject.SetActive(true);
						}
						m_characterSelectedCardId = characterId;
						m_characterSelectedImage.sprite = ApplicationManager.assets.premiumGolfers[m_characterSelectedCardId].shopPreview;
						MoveScrollBar(m_characterScrollRect, -m_premiumCharacterCards[m_characterSelectedCardId].GetComponent<RectTransform>().anchoredPosition.y);
						break;

					case AShopCard.Type.Instagram:
						if (ApplicationManager.datas.isInstagramCharacterUnlocked)
						{
							ApplicationManager.datas.selectedCharacterId = characterId;
							ApplicationManager.datas.selectedCharacterType = (int)type;
							m_instagramCharacterCard.SetState(CardSate.Selected);
							UIManager.sceneManager.UpdateGolfer();
						}
						else
						{
							m_instagramCharacterCard.SetState(CardSate.LockedSelected);
						}
						m_characterSelectedCardId = characterId;
						m_characterSelectedImage.sprite = ApplicationManager.assets.instagramGolfer.shopPreview;
						MoveScrollBar(m_characterScrollRect, -m_instagramCharacterCard.GetComponent<RectTransform>().anchoredPosition.y);
						break;
				}

				m_lastCharacterType = type;

			}
		}

		public void InitializeCharacterPanel ()
		{
			m_worldSelectedImage.sprite = ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId].shopPreview;
			m_ballSelectedImage.gameObject.SetActive(false);
			m_teeImage.gameObject.SetActive(false);
			m_characterSelectedImage.gameObject.SetActive(true);
			m_missionText.gameObject.SetActive(false);
			m_freeTryButton.gameObject.SetActive(false);
			m_travelButton.gameObject.SetActive(false);
			m_currentPanel.SetActive(false);
			m_characterPanel.SetActive(true);
			m_currentPanel = m_characterPanel;
			m_shopButtonsOutline.position = m_characterShopButton.transform.position;

			m_characterProgressionText.text = string.Format("{0} / {1}", ApplicationManager.datas.unlockedCharacterCount, m_characterCards.Count);
			m_characterProgressionFillBar.fillAmount = (float)(ApplicationManager.datas.unlockedCharacterCount) / m_characterCards.Count;


			if (ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) >= ApplicationManager.config.game.characterUnlockDistance.Length)
			{
				if (!UIManager.sceneManager.IsLastPlanet() && !ApplicationManager.datas.IsWorldUnlocked(ApplicationManager.datas.selectedWorldId + 1))
				{
					m_missionText.text = I2.Loc.ScriptLocalization.mission_orbit;
				}
				else
				{
					m_missionText.text = "Open the World Tab to travel to an other planet !";
				}
			}
			else
			{
				string distance = ((int)ApplicationManager.config.game.characterUnlockDistance[Mathf.Min(ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId), ApplicationManager.config.game.characterUnlockDistance.Length - 1)]).ToString();
				if (ApplicationManager.datas.GetWorldCurrentGoal(ApplicationManager.datas.selectedWorldId) == UIManager.sceneManager.GetMaxCharacterUnlock())
				{
					m_missionText.text = String.Format(I2.Loc.ScriptLocalization.mission_new_planet, distance);
				}
				else
				{
					m_missionText.text = String.Format(I2.Loc.ScriptLocalization.mission_new_golfer, distance);
				}
			}
			m_missionText.color = Color.black;

			OnCharacterSelect(ApplicationManager.datas.selectedCharacterId, (AShopCard.Type)ApplicationManager.datas.selectedCharacterType);
			
			switch (m_lastCharacterType)
			{
				case AShopCard.Type.Normal:
					m_characterSelectedImage.sprite = ApplicationManager.assets.golfers[m_characterSelectedCardId].shopPreview;
					MoveScrollBar(m_characterScrollRect, -m_characterCards[m_characterSelectedCardId].GetComponent<RectTransform>().anchoredPosition.y);
					break;
				case AShopCard.Type.Premium:
					m_characterSelectedImage.sprite = ApplicationManager.assets.premiumGolfers[m_characterSelectedCardId].shopPreview;
					MoveScrollBar(m_characterScrollRect, -m_premiumCharacterCards[m_characterSelectedCardId].GetComponent<RectTransform>().anchoredPosition.y);
					break;
				case AShopCard.Type.Instagram:
					m_characterSelectedImage.sprite = ApplicationManager.assets.instagramGolfer.shopPreview;
					MoveScrollBar(m_characterScrollRect, -m_instagramCharacterCard.GetComponent<RectTransform>().anchoredPosition.y);
					break;
				default:
					break;
			}
		}

		private void MoveScrollBar ( ScrollRect scrollRect, float cardAnchorPosition )
		{
			float relativePosition = cardAnchorPosition - scrollRect.viewport.rect.height / 2f;
			relativePosition = Mathf.Clamp(relativePosition, 0f, scrollRect.content.rect.height - scrollRect.viewport.rect.height);
			scrollRect.content.DOAnchorPosY(relativePosition, 0.5f);
		}

		#endregion

		#region World

		private void TravelToPlanet ()
		{
			UIManager.sceneManager.OnWorldChange(m_worldSelectedCard);
		}

		private void OnWorldSelect ( int worldId, AShopCard.Type type )
		{
			if (worldId != m_worldSelectedCard)
			{
				if (ApplicationManager.datas.IsWorldUnlocked(worldId))
				{
					if (ApplicationManager.datas.selectedWorldId != worldId)
						m_travelButton.gameObject.SetActive(true);
					else
						m_travelButton.gameObject.SetActive(false);

					m_missionText.gameObject.SetActive(false);
				}
				else
				{
					m_travelButton.gameObject.SetActive(false);
					m_missionText.gameObject.SetActive(true);
				}

				if (ApplicationManager.datas.IsWorldUnlocked(m_worldSelectedCard))
				{
					m_worldCards[m_worldSelectedCard].SetState(CardSate.Unlocked);
				}
				else
				{
					m_worldCards[m_worldSelectedCard].SetState(CardSate.Locked);
				}

				if (ApplicationManager.datas.IsWorldUnlocked(worldId))
				{
					m_worldCards[worldId].SetState(CardSate.Selected);
				}
				else
				{
					m_worldCards[worldId].SetState(CardSate.LockedSelected);
				}
				m_worldSelectedImage.sprite = ApplicationManager.assets.planets[worldId].shopPreview;
				m_worldSelectedCard = worldId;
			}
		}

		public void OnWorldUpdate ()
		{
			if (m_worldSelectedCard != ApplicationManager.datas.selectedWorldId)
			{
				m_worldCards[m_worldSelectedCard].SetState(CardSate.Unlocked);
				m_worldSelectedCard = ApplicationManager.datas.selectedWorldId;
				m_worldCards[m_worldSelectedCard].SetState(CardSate.Selected);
				m_worldSelectedImage.sprite = ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId].shopPreview;
				UpdateCoins();
			}
		}

		private void InitializeWorldPanel ()
		{
			m_ballSelectedImage.gameObject.SetActive(false);
			m_teeImage.gameObject.SetActive(false);
			m_characterSelectedImage.gameObject.SetActive(false);
			m_currentPanel.SetActive(false);
			m_worldPanel.SetActive(true);
			m_travelButton.gameObject.SetActive(false);
			m_currentPanel = m_worldPanel;
			m_shopButtonsOutline.position = m_worldShopButton.transform.position;
			m_worldSelectedCard = ApplicationManager.datas.selectedWorldId;
			m_missionText.text = I2.Loc.ScriptLocalization.mission_orbit_clubhouse;

			m_worldProgressionText.text = string.Format("{0} / {1}", ApplicationManager.datas.unlockedWorldCount, ApplicationManager.assets.planets.Length);
			m_worldProgressionFillBar.fillAmount = (float)(ApplicationManager.datas.unlockedWorldCount) / ApplicationManager.assets.planets.Length;

			m_missionText.gameObject.SetActive(false);
			m_missionText.color = Color.black;

			for (int i = 0; i < m_worldCards.Count; i++)
			{
				m_worldCards[i].Configure(i);
			}
			m_worldSelectedImage.sprite = ApplicationManager.assets.planets[m_worldSelectedCard].shopPreview;
		}

		#endregion

		#region Ball

		private void OnBallSelect ( int ballId, AShopCard.Type type )
		{
			if (ballId != m_ballSelectedCard)
			{
				m_tryButton.gameObject.SetActive(false);
				m_buyButton.gameObject.SetActive(false);

				if (ApplicationManager.datas.IsBallUnlocked(m_ballSelectedCard) || ApplicationManager.datas.GetBallCount(m_ballSelectedCard) > 0 || m_ballSelectedCard == 1 && ApplicationManager.datas.vip)
				{
					m_ballCards[m_ballSelectedCard].SetState(CardSate.Unlocked);
				}
				else
				{
					m_ballCards[m_ballSelectedCard].SetState(CardSate.Locked);
				}

				m_buyButton.text = ApplicationManager.config.game.ballCosts[ballId].gemPrice.ToString();
				m_ballCards[ballId].SetState(CardSate.Selected);
				if (ballId == 1)
				{
					if (ApplicationManager.datas.vip)
					{
						ApplicationManager.datas.selectedBallId = ballId;
						UIManager.sceneManager.UpdateBall();
					}
					else
					{
						m_tryButton.gameObject.SetActive(true);
						m_ballCards[ballId].SetState(CardSate.LockedSelected);
					}
				}
				else
				{
					if (ApplicationManager.datas.IsBallUnlocked(ballId))
					{
						ApplicationManager.datas.selectedBallId = ballId;
						UIManager.sceneManager.UpdateBall();
					}
					else if (ApplicationManager.datas.GetBallCount(ballId) > 0)
					{
						ApplicationManager.datas.selectedBallId = ballId;
						UIManager.sceneManager.UpdateBall();
						m_buyButton.gameObject.SetActive(true);
					}
					else
					{
						m_buyButton.gameObject.SetActive(true);
						m_ballCards[ballId].SetState(CardSate.LockedSelected);
					}
				}

				m_ballSelectedCard = ballId;
				m_ballSelectedImage.sprite = ApplicationManager.assets.balls[m_ballSelectedCard].ballSprite;
				m_missionText.text = I2.Loc.LocalizationManager.GetTermTranslation(ApplicationManager.assets.balls[ballId].ballTooltip);
				m_missionText.color = ApplicationManager.assets.balls[m_ballSelectedCard].ballColor;

				MoveScrollBar(m_ballScrollRect, -m_ballCards[m_ballSelectedCard].GetComponent<RectTransform>().anchoredPosition.y);
			}
		}

		private void InitializeBallPanel ()
		{
			m_worldSelectedImage.sprite = ApplicationManager.assets.planets[ApplicationManager.datas.selectedWorldId].shopPreview;
			m_ballSelectedImage.gameObject.SetActive(true);
			m_teeImage.gameObject.SetActive(true);
			m_characterSelectedImage.gameObject.SetActive(false);
			m_missionText.gameObject.SetActive(true);
			m_travelButton.gameObject.SetActive(false);
			m_currentPanel.SetActive(false);
			m_ballPanel.SetActive(true);
			m_currentPanel = m_ballPanel;
			m_shopButtonsOutline.position = m_ballShopButton.transform.position;

			m_tryButton.gameObject.SetActive(false);
			m_buyButton.gameObject.SetActive(false);

			for (int i = 0; i < m_ballCards.Count; i++)
			{
				m_ballCards[i].Configure(i);
			}

			if (ApplicationManager.datas.selectedBallId == 0)
			{
				m_missionText.text = "";
				return;
			}
			
			m_ballSelectedCard = ApplicationManager.datas.selectedBallId;
			m_ballSelectedImage.sprite = ApplicationManager.assets.balls[m_ballSelectedCard].ballSprite;
			m_missionText.text = I2.Loc.LocalizationManager.GetTermTranslation(ApplicationManager.assets.balls[m_ballSelectedCard].ballTooltip);
			m_missionText.color = ApplicationManager.assets.balls[m_ballSelectedCard].ballColor;

			MoveScrollBar(m_ballScrollRect, -m_ballCards[m_ballSelectedCard].GetComponent<RectTransform>().anchoredPosition.y);
		}

		private void OnFreeTryBallButtonPressed ()
		{
			SubscriptionPanel panel;
			if (ApplicationManager.config.game.subPopupABTest >= 0)
				panel = UIManager.OpenPanel<NewSubscriptionPanel>();
			else
				panel = UIManager.OpenPanel<SubscriptionPanel>();
			panel.Setup(1, 1, 0);
		}

		private void OnBuyBallClick ()
		{
			int cost = ApplicationManager.config.game.ballCosts[m_ballSelectedCard].gemPrice;
			if (cost <= ApplicationManager.datas.diamonds)
			{
				ApplicationManager.datas.diamonds -= cost;
				ApplicationManager.datas.UnlockBall(m_ballSelectedCard);
				ApplicationManager.datas.selectedBallId = m_ballSelectedCard;
				ApplicationManager.datas.SaveDatas();
				m_ballCards[m_ballSelectedCard].SetState(CardSate.Selected);
				m_buyButton.gameObject.SetActive(false);
				UIManager.sceneManager.UpdateBall();
				UpdateDiamonds();
				string ballName = "";
				switch (m_ballSelectedCard)
				{
					case 2:
						ballName = "red";
						break;
					case 3:
						ballName = "purple";
						break;
					case 4:
						ballName = "blue";
						break;
					case 5:
						ballName = "orange";
						break;
				}
			}
			else
			{
				UIManager.sceneManager.OpenShop();
			}
		}

		#endregion

		private void UpdateDiamonds ()
		{
			m_diamondCountText.text = MathHelper.ConvertToEgineeringNotation((ulong)ApplicationManager.datas.diamonds);//.ToString("n0", ApplicationManager.currentCulture);
		}

		private void UpdateCoins ()
		{
			m_coinCountText.text = MathHelper.ConvertToEgineeringNotation(ApplicationManager.datas.coins);//.ToString("n0", ApplicationManager.currentCulture);
		}

	}

}