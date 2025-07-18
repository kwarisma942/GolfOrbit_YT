using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinpin.UI;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using MoreMountains.NiceVibrations;

namespace Pinpin.Scene.MainScene.UI
{
	public class CharacterUnlockPopup : AClosablePopup
	{
		/*class CharacterPreviewInfo
		{
			public int position;
			public Image preview;

			public CharacterPreviewInfo ( int position, Image preview )
			{
				this.position = position;
				this.preview = preview;
			}
		}*/

		[SerializeField] private Image m_characterPreviewPrefab;
		[SerializeField] private RectTransform m_characterPreviewContainer;
		[SerializeField] private RectTransform m_parentContainer;
		[SerializeField] private ParticleSystem m_unlockedParticles;
		[SerializeField] private Material m_animationMaterial;
		[SerializeField] private WordAnimator m_wordAnimator;
		[SerializeField] private Gradient m_wordColor;
		[SerializeField] private PushButton m_chooseGolfer;
		[SerializeField] private PushButton m_keepGolfer;

		private Dictionary<int, Image> m_characterPreview;
		private int m_characterUnlocked;
		private bool m_isAnimationComplete;

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void Awake ()
		{
			m_chooseGolfer.onClick += ChooseGolfer;
			m_keepGolfer.onClick += this.Close;
			base.Awake();
		}

		public void Init () {
			m_characterPreview = new Dictionary<int, Image>();

			for (int i = 0; i < ApplicationManager.assets.golfers.Length; i++)
			{
				if (ApplicationManager.assets.golfers[i].enabled && !ApplicationManager.datas.IsCharacterUnlocked(i))
				{
					m_characterPreview.Add(i, Instantiate(m_characterPreviewPrefab, m_characterPreviewContainer));
					m_characterPreview[i].sprite = ApplicationManager.assets.golfers[i].shopPreview;
				}
			}
		}

		protected override void OnEnable ()
		{
			m_isAnimationComplete = false;
			base.OnEnable();
		}

		protected override void OnDestroy ()
		{
			m_chooseGolfer.onClick -= ChooseGolfer;
			m_keepGolfer.onClick -= this.Close;
			base.OnDestroy();
		}

		public void AnimateSlide ( int golferId )
		{
			StartCoroutine(AnimateSlideCoroutine(golferId));
		}

		public IEnumerator AnimateSlideCoroutine ( int golferId )
		{
			yield return new WaitForEndOfFrame();
			m_characterUnlocked = golferId;
			Image character = null;
			if (m_characterPreview.TryGetValue(m_characterUnlocked, out character))
			{
				m_characterPreviewContainer.anchoredPosition = Vector2.up * 0.5f;
				float position = character.rectTransform.anchoredPosition.x;
				m_characterPreviewContainer.DOAnchorPosX(-position, 2.0f).SetEase(Ease.InOutQuad).onComplete += SlideAnimationComplete;
				m_wordAnimator.AnimateWords(Regex.Split(I2.Loc.ScriptLocalization.new_character, string.Empty), m_wordColor, 0.03f, 0.7f);
			}
		}

		private void SlideAnimationComplete ()
		{
			m_isAnimationComplete = true;

			if (ApplicationManager.datas.isVibrationActive)
				MMVibrationManager.Haptic(HapticTypes.MediumImpact);

			m_characterPreview[m_characterUnlocked].material = m_animationMaterial;
			m_characterPreview[m_characterUnlocked].material.DOFloat(0f, "_EffectAmount", 1f);
			m_unlockedParticles.Play();
			UISounds.PlayCongratulationSound();
		}

		private void ChooseGolfer ()
		{
			if (m_characterUnlocked != ApplicationManager.datas.selectedCharacterId && m_characterUnlocked != -1)
			{
				ApplicationManager.datas.selectedCharacterId = m_characterUnlocked;
				ApplicationManager.datas.selectedCharacterType = (int)AShopCard.Type.Normal;
				UIManager.sceneManager.UpdateGolfer();
			}
			Close();
		}

		public override bool IsClosable ()
		{
			return m_isAnimationComplete;
		}

		protected override void OnDisable ()
		{
			if (m_characterPreview.ContainsKey(m_characterUnlocked))
			{
				Destroy(m_characterPreview[m_characterUnlocked].gameObject);
				m_characterPreview.Remove(m_characterUnlocked);
			}

			m_characterPreviewContainer.DOKill();

			/*float size = m_characterPreviewPrefab.GetComponent<RectTransform>().rect.width * m_characterPreview.Count;

			m_parentContainer.sizeDelta = new Vector2(size, m_parentContainer.sizeDelta.y);
			m_characterPreviewContainer.sizeDelta = new Vector2(size, m_characterPreviewContainer.sizeDelta.y);
			*/
			base.OnDisable();
		}

	}

}