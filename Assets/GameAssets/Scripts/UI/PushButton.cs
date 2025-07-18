using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Pinpin.UI
{

	[DisallowMultipleComponent]
	public class PushButton : Button, IPointerDownHandler
	{
		[SerializeField] private Image m_disableImage;
		[SerializeField] private Text m_text = null;
		[SerializeField] private Image m_icon = null;

		public bool m_isShining;
		[SerializeField] private Image m_shineImage = null;
		[SerializeField] private float m_shineStartDelay = 0.5f;
		[SerializeField] private float m_shinePauseDuration = 2.0f;
		[SerializeField] private float m_shineDuration = 2.0f;

		public bool m_isBouncing;
		[Tooltip("Duration between each bounce")]
		[SerializeField] private float m_bounceStartDelay = 0.5f;
		[SerializeField] private float m_bouncePauseDuration = 2.0f;
		[SerializeField] private float m_bounceDuration = 0.4f;

		public bool m_bounceOnClick;
		[SerializeField] private bool m_repeatOnHold;
		[SerializeField] private float m_holdFrequency;
		[SerializeField] private float m_holdDelay;
		private float m_holdTimer;
		private float m_holdFrequencyTimer;
		private bool m_isHolding;
		private bool m_isClicked;

		[SerializeField] private bool m_adButton;

		private CanvasGroup m_canvasGroup;

		private Sequence m_shineSequence;
		private Sequence m_shineStartDelaySequence;
		private Sequence m_bounceSequence;
		private Sequence m_bounceSequenceOnPointer;
		private Sequence m_bounceSequenceOnClick;
		private Sequence m_bounceStartDelaySequence;

		public new event UnityAction onClick;
		private bool m_isInteractable = true;
		private bool m_isAdAvailable = false;

		public bool isInteractable
		{
			get { return interactable; }
			set
			{
				m_isInteractable = value;
				UpdateInteractableState();
			}
		}
		public bool isAdButton
		{
			get { return m_adButton; }
		}

		public void UpdateInteractableState()
		{
			bool value = m_isInteractable && (m_adButton && !ApplicationManager.datas.vip ? m_isAdAvailable : true);
			if (value != interactable)
			{
				interactable = value;
				OnInteractableChange(value);
			}
		}

		public void Update ()
		{
			if (m_repeatOnHold && m_isHolding)
			{
				m_holdTimer += Time.deltaTime;
				if (m_holdTimer >= m_holdDelay)
				{
					m_holdFrequencyTimer += Time.deltaTime;
					if (m_holdFrequencyTimer > m_holdFrequency)
					{
						m_holdFrequencyTimer = 0;
						OnClick();
					}
				}
			}
		}

		public string text
		{
			get { return (m_text.text); }
			set { m_text.text = value; }
		}

		public Color textColor
		{
			get { return (m_text.color); }
			set { m_text.color = value; }
		}

		public Sprite icon
		{
			get { return (m_icon.sprite); }
			set
			{
				m_icon.sprite = value;
				if (value)
					m_icon.gameObject.SetActive(true);
				if (value == null)
					m_icon.gameObject.SetActive(false);
			}
		}

		public Color iconColor
		{
			get { return (m_icon.color); }
			set { m_icon.color = value; }
		}

		protected override void Awake ()
		{
			base.Awake();
			base.onClick.AddListener(this.OnClick);
			if (m_adButton)
			{
				ApplicationManager.onRewardedVideoAvailabilityChange += OnRewardedVideoAvailabilityChange;
			}

			if (GetComponent<CanvasGroup>() != null)
			{
				m_canvasGroup = GetComponent<CanvasGroup>();
			}
		}

		void OnRewardedVideoAvailabilityChange (bool value)
		{
#if TAPNATION
			m_isAdAvailable = ApplicationManager.canWatchRewardedVideo;
#endif
			UpdateInteractableState();
		}

		protected override void OnEnable ()
		{
			if (m_adButton)
			{
#if TAPNATION
				m_isAdAvailable = ApplicationManager.canWatchRewardedVideo;
#endif
				UpdateInteractableState();
			}
			else
			{
				OnInteractableChange(interactable);
			}
			base.OnEnable();
		}

		protected override void Start ()
		{
			base.Start();
		}

		protected override void OnDestroy ()
		{
			base.onClick.RemoveListener(this.OnClick);
			if (m_adButton)
			{
				ApplicationManager.onRewardedVideoAvailabilityChange -= OnRewardedVideoAvailabilityChange;
			}
			base.OnDestroy();
		}

		protected override void OnDisable ()
		{
			if (interactable)
			{
				if (m_isShining)
				{
					StopShining();
				}
				if (m_isBouncing)
				{
					StopBouncing();
				}
				if (m_repeatOnHold)
					m_isHolding = false;
			}
			base.OnDisable();
		}

		private void OnApplicationPause (bool pause)
		{
			if (pause)
			{
				if (m_repeatOnHold)
					m_isHolding = false;
			}
		}

		public override void OnPointerDown (PointerEventData eventData)
		{
			m_isClicked = false;
			if (interactable)
			{
				if (m_isBouncing)
				{
					StopBouncing();
				}
				if (m_bounceOnClick)
				{
					m_bounceSequenceOnPointer = DOTween.Sequence();
					m_bounceSequenceOnPointer.Append(this.transform.DOScale(Vector3.one * 0.85f, 0.1f).SetEase(Ease.OutCubic));
				}
				if (m_repeatOnHold)
				{
					m_isHolding = true;
					m_holdTimer = 0;
					m_holdFrequencyTimer = m_holdFrequency;
				}
			}
		}

		public override void OnPointerUp (PointerEventData eventData)
		{
			if (m_repeatOnHold)
			{
				m_isHolding = false;
			}
			if (m_isBouncing && interactable)
			{
				StartBouncing();
			}
			if (m_bounceOnClick)
			{
				m_bounceSequenceOnPointer = DOTween.Sequence();
				if (m_isClicked)
				{
					m_bounceSequenceOnPointer.Append(this.transform.DOScale(Vector3.one * 1.1f, 0.1f).SetEase(Ease.OutCubic));
				}
				m_bounceSequenceOnPointer.Append(this.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBounce));
			}
			m_isClicked = false;
			base.OnPointerUp(eventData);
		}

		protected virtual void OnClick ()
		{
			m_isClicked = true;
			if (m_bounceOnClick)
			{
				m_bounceSequenceOnClick = DOTween.Sequence();
				m_bounceSequenceOnClick.Append(this.transform.DOScale(Vector3.one * 1.1f, 0.05f).SetEase(Ease.OutCubic));
				m_bounceSequenceOnClick.Append(this.transform.DOScale(Vector3.one, 0.05f).SetEase(Ease.OutBounce));
			}
			UISounds.PlayClickSound();
			if (this.onClick != null)
				this.onClick.Invoke();

		}

		public void ResetOnClick ()
		{
			this.onClick = null;
		}

		public void InvokeOnClick ()
		{
			if (onClick != null)
				onClick.Invoke();
		}


		private void OnInteractableChange (bool value)
		{
			if (value)
			{
				if (m_isShining)
				{
					StartShining();
				}
				if (m_isBouncing)
				{
					StartBouncing();
				}
				if (m_disableImage != null)
				{
					m_disableImage.gameObject.SetActive(false);
				}
			}
			else
			{
				if (m_isShining)
				{
					StopShining();
				}
				if (m_isBouncing)
				{
					StopBouncing();
				}
				if (m_disableImage != null)
				{
					m_disableImage.gameObject.SetActive(true);
				}
				if (m_isHolding)
				{
					m_isHolding = false;
				}
			}
		}

		protected override void DoStateTransition (Selectable.SelectionState state, bool instant)
		{
			base.DoStateTransition(state, instant);
		}

		public void Hide (float fadeTime)
		{
			this.isInteractable = false;
			if (image != null)
			{
				image.DOComplete();
				image.DOKill();
				image.DOFade(0f, fadeTime).SetEase(Ease.InSine);
			}
			if (m_text != null)
			{
				m_text.DOComplete();
				m_text.DOKill();
				m_text.DOFade(0f, fadeTime).SetEase(Ease.InSine);
			}
			if (m_icon != null)
			{
				m_icon.DOComplete();
				m_icon.DOKill();
				m_icon.DOFade(0f, fadeTime).SetEase(Ease.InSine);
			}
			if (m_disableImage != null)
			{
				m_disableImage.DOComplete();
				m_disableImage.DOKill(true);
				m_disableImage.DOFade(0f, fadeTime).SetEase(Ease.InSine).OnComplete(() => SetDisableImageRaycast(false));
			}

			this.targetGraphic.raycastTarget = false;
		}

		public void Show (float fadeTime)
		{
			this.isInteractable = true;
			if (image != null)
			{
				image.DOComplete();
				image.DOKill();
				image.DOFade(1f, fadeTime).SetEase(Ease.InSine);
			}
			if (m_text != null)
			{
				m_text.DOComplete();
				m_text.DOKill();
				m_text.DOFade(1f, fadeTime).SetEase(Ease.InSine);
			}
			if (m_icon != null)
			{
				m_icon.DOComplete();
				m_icon.DOKill();
				m_icon.DOFade(1f, fadeTime).SetEase(Ease.InSine);
			}
			if (m_disableImage != null)
			{
				m_disableImage.DOComplete();
				m_disableImage.DOKill(true);
				m_disableImage.DOFade(.5f, fadeTime).SetEase(Ease.InSine).OnComplete(() => SetDisableImageRaycast(true));
			}
			this.targetGraphic.raycastTarget = true;
		}

		private void SetDisableImageRaycast (bool value)
		{
			m_disableImage.raycastTarget = value;
		}

		private void StartBouncing ()
		{
			if (m_bounceStartDelaySequence != null)
				m_bounceStartDelaySequence.Kill(false);

			m_bounceStartDelaySequence = DOTween.Sequence();
			m_bounceStartDelaySequence.SetDelay(m_bounceStartDelay);
			m_bounceStartDelaySequence.AppendCallback(ActivateBouncing);
		}

		private void StartShining ()
		{
			if (m_shineStartDelaySequence != null)
				m_shineStartDelaySequence.Kill(false);

			m_shineStartDelaySequence = DOTween.Sequence();
			m_shineStartDelaySequence.SetDelay(m_shineStartDelay);
			m_shineStartDelaySequence.AppendCallback(ActivateShining);
		}

		private void ActivateShining ()
		{
			if (m_shineImage != null)
			{
				StopShining();
				float shineImageDestination = m_shineImage.rectTransform.rect.size.x + m_shineImage.rectTransform.localPosition.x + GetComponent<RectTransform>().rect.size.x;

				if (m_shineSequence == null)
				{
					m_shineSequence = DOTween.Sequence();
					m_shineSequence.SetRecyclable(false);
					m_shineSequence.Append(m_shineImage.rectTransform.DOLocalMoveX(shineImageDestination, m_shineDuration).SetEase(Ease.InOutQuad)).AppendInterval(m_shinePauseDuration).SetLoops(-1);
				}
				else
				{
					m_shineSequence.Restart(false);
				}
			}
		}

		private void ActivateBouncing ()
		{
			StopBouncing();
			if (m_bounceSequence == null)
			{
				m_bounceSequence = DOTween.Sequence();
				m_bounceSequence.SetRecyclable(false);
				m_bounceSequence.Append(transform.DOPunchScale(Vector3.one * 0.1f, m_bounceDuration, 10, 0.6f)).AppendInterval(m_bouncePauseDuration).SetLoops(-1);
			}
			else
			{
				m_bounceSequence.Restart(false);
			}
		}


		private void StopShining ()
		{
			if (m_shineStartDelaySequence != null)
				m_shineStartDelaySequence.Kill(false);
			if (m_shineImage != null)
			{
				m_shineSequence.Kill(true);
				m_shineSequence = null;
				m_shineImage.rectTransform.anchoredPosition = -m_shineImage.rectTransform.rect.size / 2f;
			}
		}

		private void StopBouncing ()
		{
			if (m_bounceStartDelaySequence != null)
				m_bounceStartDelaySequence.Kill(false);

			m_bounceSequence.Kill(true);
			m_bounceSequence = null;
			this.transform.localScale = Vector3.one;
		}

		public void Fade (float fadeValue, float time)
		{
			if (m_canvasGroup != null)
			{
				m_canvasGroup.DOKill();
				m_canvasGroup.DOFade(fadeValue, time);
			}
		}
	}

}