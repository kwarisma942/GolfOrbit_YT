using Pinpin.Types;
using System;
using UnityEngine;

namespace Pinpin.UI
{

	public class LanguageButton : PushButton
	{

		[SerializeField] private Language m_language;
		public new Action<Language> onClick;
		
		protected override void OnClick ()
		{
			if (onClick != null)
				onClick.Invoke(m_language);
		}

	}

}