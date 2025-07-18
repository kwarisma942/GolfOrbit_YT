using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.UI
{
	public class VideoToVIPIconSwitch : MonoBehaviour
	{
		public Sprite vipIcon;
		public Sprite videoIcon;
		public Sprite androidVideoIcon;
		public List<Image> images;

		void Start ()
		{
			ApplyVIPIcon();
			ApplicationManager.onVipActivated += ApplyVIPIcon;
		}

		private void ApplyVIPIcon ()
		{
			for (int i = 0; i < images.Count; i++)
			{
#if UNITY_ANDROID
				images[i].sprite = ApplicationManager.datas.vip ? vipIcon : androidVideoIcon;
#else
				//images[i].sprite = ApplicationManager.datas.vip ? vipIcon : videoIcon;
#endif
			}
		}
	}
}
