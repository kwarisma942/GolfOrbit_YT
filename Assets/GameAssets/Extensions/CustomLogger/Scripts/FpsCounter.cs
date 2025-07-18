using UnityEngine;

namespace Monitoring
{

	[AddComponentMenu("Monitoring/Fps Counter")]
	public class FpsCounter: MonoBehaviour
	{

		public enum Anchor
		{
			TopLeft,
			TopCenter,
			TopRight,
			BottomLeft,
			BottomRight
		}

		private int						m_fps = 0;
		private int						m_fpsCounter = 0;
		private float					m_timer = 0f;
		[SerializeField] private Anchor	m_anchor;
		private int						m_boxHeight = 30;
		private int						m_boxWidth = 85;

		private void Awake ()
		{
			#if !DEBUG
				this.enabled = false;
			#endif
		}

		private void Update ()
		{
			m_fpsCounter++;
			m_timer += Time.deltaTime;

			if (m_timer >= 1f)
			{
				m_timer = 0f;
				m_fps = m_fpsCounter;
				m_fpsCounter = 0;
			}
		}

		private void OnGUI ()
		{
			Rect box;
			switch (m_anchor)
			{
				case Anchor.BottomLeft:
					box = new Rect(0, Screen.height - m_boxHeight, m_boxWidth, m_boxHeight); 
					break;

				case Anchor.BottomRight:
					box = new Rect(Screen.width - m_boxWidth , Screen.height - m_boxHeight, m_boxWidth, m_boxHeight); 
					break;

				case Anchor.TopLeft:
					box = new Rect(0, 0, m_boxWidth, m_boxHeight);
					break;

				case Anchor.TopCenter:
					box = new Rect(Screen.width / 2 - m_boxWidth / 2f, 0, m_boxWidth, m_boxHeight);
					break;

				case Anchor.TopRight:
					box = new Rect(Screen.width - m_boxWidth , 0, m_boxHeight, m_boxHeight);
					break;

				default:
					box = new Rect();
					break;
			}

			GUI.skin.label.fontSize = 18;
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;

			GUI.Box(box, "");
			GUI.Label(box, "FPS: " + m_fps.ToString());

		}
	}

}
