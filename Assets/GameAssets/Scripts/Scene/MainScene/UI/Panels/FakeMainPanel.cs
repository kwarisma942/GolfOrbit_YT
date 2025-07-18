using Pinpin.UI;
using UnityEngine;
using PaperPlaneTools;
namespace Pinpin.Scene.MainScene.UI
{

	public class FakeMainPanel : AUIPanel
	{
		[SerializeField] private OfflineEarningsPopup m_offlineEarningsPopup;
		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		public override bool OnBackAction ()
		{
			if (m_offlineEarningsPopup.gameObject.activeInHierarchy)
				return false;
			if (AlertManager.Instance.isOpen)
			{
				UIManager.GetPopup<RatingPopup>().OnBackAction();
				return false;
			}
			return base.OnBackAction();
		}
	}

}
