using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Pinpin
{

	[CreateAssetMenu(fileName = "GameAssets", menuName = "Game/GameAssets", order = 1)]
	public class GameAssets: ScriptableObject
	{

		public AudioMixer	audioMixer;

		[Serializable]
		public class GolferData
		{
			public string name;
			public bool enabled = true;
			public Sprite shopPreview;
			public Golfer prefab;
			public Sprite shopHead;
		}

		[Serializable]
		public class WorldData
		{
			public string name;
			public Sprite shopPreview;
			public Sprite shopButtonPreview;
			public PlanetManager[] prefabs;
			public Sprite orbitButtonSprite;
			public Sprite orbitImage;
			public DaytimeMaterial dayTimeController;
			public Gradient[] landParticlesColor;
			public Gradient birdsColor;
			public AudioClip backgroundMusic;
			public string waterName;
		}

		[System.Serializable]
		public class IAPAsset
		{

			public enum Id
			{
				HandfullOfGems, HandfullOfGems2,
				BagOfGems, BagOfGems2,
				CupOfGems, CupOfGems2,
				ChestOfGems, ChestOfGems2,
				WeeklyVIP, WeeklyVIP2,
				MonthlyVIP, MonthlyVIP2,
				yearlyVIP, yearlyVIP2
			}

			public string name;
			public string description;
			public string productId;
			public int hardCurrencyAmount;
			public bool removeAds;
			public bool isMostpopular;
			public bool isBestOffer;
			public string fakeEUPrice;
			public string fakeUSPrice;
		}

		[System.Serializable]
		public class ShopItemAsset
		{

			public enum Id
			{
				ChestOfGold
			}

			public string name;
			public string description;
			public int hardCurrencyPrice;
			public int softCurrencyAmount;
		}

		[Serializable]
		public class BallData
		{
			public string name;
			public Color ballColor;
			public Sprite ballSprite;
			public string ballTooltip;
			public int cost;
		}

		public GolferData[] golfers;
		public GolferData[] premiumGolfers;
		public GolferData instagramGolfer;
		public WorldData[] planets;
		public BallData[] balls;
		public List<IAPAsset> inAppProducts;
		public List<ShopItemAsset> shopItems;
		public GameObject holePrefab;
		public GameObject oldHolePrefab;

	}

}