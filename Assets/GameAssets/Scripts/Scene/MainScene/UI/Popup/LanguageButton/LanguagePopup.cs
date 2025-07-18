using DG.Tweening;
using Pinpin.Types;
using Pinpin.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{
	public class LanguagePopup : AClosablePopup
	{
		[SerializeField] private PushButton m_closeAnimButton;
		[SerializeField] protected RectTransform m_panel;
		[SerializeField] protected Image m_background;
		[SerializeField] private LanguageButton[] m_languageButtons;
		[SerializeField] private Sprite m_onSprite;
		[SerializeField] private Sprite m_offSprite;

		protected float m_panelContainerScreenYDelta;
		private float m_panelContainerBaseYPosition;
		private Language m_openedLanguage = Language.English;

		protected new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void Awake ()
		{
			m_panelContainerBaseYPosition = m_panel.localPosition.y;
			m_panelContainerScreenYDelta = -m_panel.rect.height;
		}

		protected virtual void Start ()
		{
			m_closeAnimButton.onClick += OnCloseClick;
			m_panelContainerScreenYDelta = -GetComponent<RectTransform>().rect.height;
			m_panel.localPosition = new Vector3(m_panel.localPosition.x, m_panelContainerScreenYDelta, 0f);
			for (int i = 0; i < m_languageButtons.Length; i++)
			{
				m_languageButtons[i].onClick += OnLanguageClic;
				m_languageButtons[i].image.sprite = i == (int)ApplicationManager.language ? m_onSprite : m_offSprite;
			}
		}

		protected override void OnEnable ()
		{
			m_panel.localPosition = new Vector3(m_panel.localPosition.x, m_panelContainerScreenYDelta, 0f);
			m_panel.DOLocalMoveY(m_panelContainerBaseYPosition, 0.5f).SetEase(Ease.OutBack, 1.0f);
			m_openedLanguage = ApplicationManager.language;
		}

		protected override void OnDestroy ()
		{
			m_closeAnimButton.onClick -= OnCloseClick;
			for (int i = 0; i < m_languageButtons.Length; i++)
			{
				m_languageButtons[i].onClick -= OnLanguageClic;
			}
		}

		protected virtual void OnCloseClick ()
		{
			m_panel.DOLocalMoveY(m_panelContainerScreenYDelta * 1f, 0.5f).SetEase(Ease.InBack).onComplete += Close;
			if(m_openedLanguage != ApplicationManager.language)
			{
				ApplicationManager.LoadScene(ASceneManager.Scene.MainScene);
			}
		}

		private void OnLanguageClic ( Language language )
		{
			m_languageButtons[(int)ApplicationManager.language].image.sprite = m_offSprite;
			ApplicationManager.language = language;
			m_languageButtons[(int)language].image.sprite = m_onSprite;
		}

	}

}
