using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pinpin.Scene.MainScene.UI
{

	public class SkinShopCardBall : AShopCard
	{
		public override void Configure ( int skinId, Type type = Type.Normal )
		{
			m_id = skinId;
			m_skinImage.sprite = ApplicationManager.assets.balls[m_id].ballSprite;

			State state = State.Locked;
			if (ApplicationManager.datas.selectedBallId == m_id)
			{
				state = State.Selected;
			}
			else if (ApplicationManager.datas.IsBallUnlocked(m_id) || ApplicationManager.datas.GetBallCount(m_id) > 0 || m_id == 1 && ApplicationManager.datas.vip)
			{
				state = State.Unlocked;
			}
			SetState(state);
		}
	}

}