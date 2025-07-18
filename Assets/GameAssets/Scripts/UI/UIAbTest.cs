using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.UI
{

	public class UIAbTest : MonoBehaviour
	{

		[Serializable]
		public class SpriteToChange
		{
			public Sprite sprite;
			public List<Image> images;

			public void ChangeSprites ()
			{
				for (int i = 0; i < images.Count; i++)
				{
					images[i].sprite = sprite;
				}
			}
		}

		public List<SpriteToChange> spritesToChange;

		private void Start ()
		{
#if UNITY_ANDROID
			if (ApplicationManager.config.game.useNewVideoIcon)
			{
				for (int i = 0; i < spritesToChange.Count; i++)
				{
					spritesToChange[i].ChangeSprites();
				}
			}
#endif
		}

	}

}