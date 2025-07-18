using UnityEngine.EventSystems;
namespace Pinpin.Scene.MainScene.UI
{

	public class SkinShopCardWorld : AShopCard
	{

		public override void Configure ( int skinId, Type type = Type.Normal )
		{
			m_id = skinId;
			m_skinImage.sprite = ApplicationManager.assets.planets[m_id].shopButtonPreview;
			
			State state = State.Locked;
			if(m_goldImage != null)
				m_goldImage.gameObject.SetActive(false);

			if (ApplicationManager.datas.selectedWorldId == m_id)
			{
				m_selectButton.Select();
				m_selectButton.OnSelect(new BaseEventData(ApplicationManager.currentSceneManager.GetComponent<EventSystem>()));
				if(m_goldImage != null)
					m_goldImage.gameObject.SetActive(true);
				state = State.Selected;
			}
			else if (ApplicationManager.datas.IsWorldUnlocked(m_id))
			{
				state = State.Unlocked;
			}
			SetState(state);
		}

	}

}
