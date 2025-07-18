using Pinpin.InitScene;
using Pinpin.UI;
using System;
using UnityEngine;

public class GDPRPanel : MonoBehaviour
{

	[SerializeField] private InitSceneManager m_initSceneManager;
	[SerializeField] private PushButton m_acceptButton;
		
	// Use this for initialization
	void Start ()
	{
		m_acceptButton.onClick += OnAcceptButtonPressed;
	}

	protected void OnDestroy ()
	{
		m_acceptButton.onClick -= OnAcceptButtonPressed;
	}

	private void OnAcceptButtonPressed()
	{
		m_initSceneManager.GDPRPopupAccepted();
	}

}
