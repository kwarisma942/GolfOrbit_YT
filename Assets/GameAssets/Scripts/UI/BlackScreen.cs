using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.UI
{

	public class BlackScreen : MonoBehaviour
	{

		private static BlackScreen singleton { get; set; }

		private Image m_image;
		private System.Action onShowComplete;

		private void Awake ()
		{
			if (singleton != null)
			{
				GameObject.Destroy(this.gameObject);
				return;
			}

			singleton = this;
		}


		// Use this for initialization
		void Start ()
		{
			m_image = GetComponentInChildren<Image>(true);
			m_image.color = new Color(0f, 0f, 0f, 0f);
			m_image.gameObject.SetActive(false);
		}

		public static void Show(float fadeTime)
		{
			singleton.m_image.gameObject.SetActive(true);
			singleton.m_image.DOFade(1f, fadeTime);
		}

		private void OnShowComplete ()
		{
			if(onShowComplete != null)
			{
				onShowComplete.Invoke();
			}
			singleton.onShowComplete = null;
		}

		public static void Hide ( float fadeTime )
		{
			singleton.m_image.DOFade(0f, fadeTime).onComplete += singleton.OnHideComplete;
		}

		private void OnHideComplete ()
		{
			m_image.gameObject.SetActive(false);
		}

	}

}
