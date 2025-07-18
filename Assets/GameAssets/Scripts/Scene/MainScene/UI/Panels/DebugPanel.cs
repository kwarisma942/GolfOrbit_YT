using Pinpin.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public class DebugPanel : AUIPanel
	{
		[SerializeField] private PushButton m_buttonPrefab;
		[SerializeField] private GameObject m_panelsParent;
		[SerializeField] private GameObject m_popupsParent;
		[SerializeField] private RectTransform m_panelList;
		[SerializeField] private RectTransform m_popupList;

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		// Use this for initialization
		void Start ()
		{
			foreach(AUIPanel panel in m_panelsParent.GetComponentsInChildren<AUIPanel>(true))
			{
				if(panel != this)
				{
					PushButton button = Instantiate(m_buttonPrefab, m_panelList.transform);
					button.text = panel.gameObject.name;
					button.onClick += () => UIManager.OpenPanel(panel);
				}
			}

			foreach (AUIPopup popup in m_popupsParent.GetComponentsInChildren<AUIPopup>(true))
			{
				if(popup != this)
				{
					PushButton button = Instantiate(m_buttonPrefab, m_popupList.transform);
					button.text = popup.gameObject.name;
					button.onClick += () => UIManager.OpenPopup(popup);
				}
			}
		}

		// Update is called once per frame
		void Update ()
		{

		}
	}
}