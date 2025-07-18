using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{
	public class NewSubscriptionPanel : SubscriptionPanel
	{
		[SerializeField] Image[] m_backgrounds;
		[SerializeField] Image[] m_textGlowImages;
		[SerializeField] Graphic[] m_coloredGraphics;
		[SerializeField] Color m_darkColor;
		[SerializeField] Color[] m_glowColors;

		protected override void Start ()
		{
			base.Start();
			for (int i = 0; i < m_backgrounds.Length ; i++)
			{
				m_backgrounds[i].gameObject.SetActive(ApplicationManager.config.game.subPopupABTest == i);
			}
			for (int i = 0; i < m_textGlowImages.Length; i++)
			{
				m_textGlowImages[i].color = m_glowColors[ApplicationManager.config.game.subPopupABTest];
			}
			if (ApplicationManager.config.game.subPopupABTest == 2)
			{
				for (int i = 0; i < m_coloredGraphics.Length; i++)
				{
					float alpha = m_coloredGraphics[i].color.a;
					Color newColor = m_darkColor;
					newColor.a = alpha;
					m_coloredGraphics[i].color = newColor;
				}
			}
		}
	}
}
