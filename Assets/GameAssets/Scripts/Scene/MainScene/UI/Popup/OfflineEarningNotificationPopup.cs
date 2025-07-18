using UnityEngine;
using Pinpin.UI;
namespace Pinpin.Scene.MainScene.UI
{

	public sealed class OfflineEarningNotificationPopup : AConfirmPopup
	{

		private new MainSceneUIManager UIManager
		{
			get { return (base.UIManager as MainSceneUIManager); }
		}

		protected override void OnAcceptButtonPressed ()
		{
			ApplicationManager.datas.enableOENotifications = true;
#if UNITY_IOS
			UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Sound);
#endif
			base.OnAcceptButtonPressed();
			this.Close();
		}
		
	}

}