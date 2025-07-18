using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{
	public class DayTimeToggle : Toggle
	{

		public static Action<DaytimeMaterial.DayTime> onDayTimeSelect;
		public DaytimeMaterial.DayTime dayTime;

		protected override void Start ()
		{
			onValueChanged.AddListener(OnValueChanged);
			base.Start();
		}

		protected override void OnDestroy ()
		{
			onValueChanged.RemoveListener(OnValueChanged);
			base.OnDestroy();
		}

		private void OnValueChanged ( bool value )
		{
			if (value)
			{
				if (onDayTimeSelect != null)
					onDayTimeSelect.Invoke(dayTime);
			}
		}

	}
}