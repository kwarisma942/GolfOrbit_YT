using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Pinpin.UI;
using Homebrew;
using PaperPlaneTools;

namespace Pinpin.Scene.MainScene.UI
{

	public class RatingPopup : AUIPopup {

		[Foldout("Like", true)]
		[SerializeField] private GameObject m_likeLayout;
		[SerializeField] private PushButton m_yesLikeButton;
		[SerializeField] private PushButton m_noLikeButton;
		[SerializeField] private ParticleSystem m_likeParticles;

		[Foldout("Rate", true)]
		[SerializeField] private GameObject m_topLayout;
		private Image[] m_stars;
		[SerializeField] private GameObject m_rateLayout;
		[SerializeField] private PushButton m_yesRateButton;
		[SerializeField] private PushButton m_noRateButton;

		Sequence seq;

		private void Start ()
		{
			m_yesLikeButton.onClick += OpenRatePanel;
			m_yesRateButton.onClick += YesRateButtonPressed;
			m_stars = m_topLayout.GetComponentsInChildren<Image>();
		}

		private void OnDestroy ()
		{
			m_yesLikeButton.onClick -= OpenRatePanel;
			m_yesRateButton.onClick -= YesRateButtonPressed;
		}

		private void YesRateButtonPressed ()
		{
		}

		public void OnBackAction ()
		{
			if (m_likeLayout.activeInHierarchy)
			{
				RateBox.Instance.AlertAdapter.InvokeNegativeButton();
			}
			else if (m_rateLayout.activeInHierarchy)
			{
				RateBox.Instance.AlertAdapter.InvokeNeutralButton();
			}
		}

		protected override void OnEnable ()
		{
			ApplicationManager.datas.rateboxShown++;
			m_noRateButton.text = I2.Loc.ScriptLocalization.no;
			m_noLikeButton.text = I2.Loc.ScriptLocalization.no;
			m_yesLikeButton.text = I2.Loc.ScriptLocalization.yes;
			m_yesRateButton.text = I2.Loc.ScriptLocalization.yes;

			m_likeLayout.SetActive(true);
			m_rateLayout.SetActive(false);
			m_likeParticles.gameObject.SetActive(true);
		}

		protected override void OnDisable ()
		{
			seq.Complete();
			seq.Kill();

			base.OnDisable();
		}

		private void OpenRatePanel ()
		{
			m_likeLayout.SetActive(false);
			m_rateLayout.SetActive(true);
			m_likeParticles.Stop();
			seq.Complete();
			seq.Kill();
			seq = DOTween.Sequence();

			for (int i = 0; i < m_stars.Length; i++)
			{
				seq.Insert(i * 0.05f, m_stars[i].rectTransform.DOScale(Vector3.zero, 0.1f));
				seq.Insert(0.1f + i * 0.05f, m_stars[i].rectTransform.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutElastic, 1.3f));
			}
			seq.AppendInterval(1.0f);
			seq.SetLoops(-1);
		}

	}

}

