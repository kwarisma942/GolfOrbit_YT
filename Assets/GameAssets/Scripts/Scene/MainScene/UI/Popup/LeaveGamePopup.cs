using UnityEngine;
using Pinpin.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public sealed class LeaveGamePopup: AConfirmPopup
	{

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void OnAcceptButtonPressed ()
		{
			Close();
			ApplicationManager.Quit();
		}
		
	}

}