using UnityEngine;
using Pinpin.UI;

[RequireComponent(typeof(PushButton))]
public class UISettingsButton : MonoBehaviour {

	[SerializeField] private Sprite m_enabledImage;
	[SerializeField] private Sprite m_disableImage;
	[SerializeField] private string m_enableTooltip;
	[SerializeField] private string m_disableTooltip;

	private PushButton m_button;

	private void Awake ()
	{
		m_button = GetComponent<PushButton>();
	}

	public void SetState( bool enabled )
	{
		if (enabled)
		{
			m_button.icon = m_enabledImage;
			m_button.text = I2.Loc.LocalizationManager.GetTermTranslation(m_enableTooltip);
		}
		else
		{
			m_button.icon = m_disableImage;
			m_button.text = I2.Loc.LocalizationManager.GetTermTranslation(m_disableTooltip);
		}
	}
}
