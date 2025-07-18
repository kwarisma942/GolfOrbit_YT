using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public class SkinShopCardCharacter : AShopCard
	{

		public override void Configure (int skinId, Type type = Type.Normal)
		{
			m_type = type;
			m_id = skinId;
			State state = State.Locked;
			switch (type)
			{
				case Type.Normal:
					m_skinImage.sprite = ApplicationManager.assets.golfers[m_id].shopHead;
					if (ApplicationManager.datas.selectedCharacterId == m_id && (Type)ApplicationManager.datas.selectedCharacterType == Type.Normal)
					{
						state = State.Selected;
					}
					else if (ApplicationManager.datas.IsCharacterUnlocked(m_id))
					{
						state = State.Unlocked;
					}
					break;

				case Type.Premium:
					m_skinImage.sprite = ApplicationManager.assets.premiumGolfers[m_id].shopHead;
					if (ApplicationManager.datas.selectedCharacterId == m_id && (Type)ApplicationManager.datas.selectedCharacterType == Type.Premium)
					{
						state = State.Selected;
					}
					else if (ApplicationManager.datas.vip)
					{
						state = State.Unlocked;
					}
					break;

				case Type.Instagram:
					m_skinImage.sprite = ApplicationManager.assets.instagramGolfer.shopHead;
					if (ApplicationManager.datas.selectedCharacterId == m_id && (Type)ApplicationManager.datas.selectedCharacterType == Type.Instagram)
					{
						state = State.Selected;
					}
					else if (ApplicationManager.datas.isInstagramCharacterUnlocked)
					{
						state = State.Unlocked;
					}
					break;
				default:
					break;
			}

			SetState(state);
		}
	}

}