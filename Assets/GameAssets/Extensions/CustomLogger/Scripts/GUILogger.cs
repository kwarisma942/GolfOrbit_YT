using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CustomLogger
{

	[AddComponentMenu("Monitoring/GUI Logger"), DefaultExecutionOrder(100)]
	public class GUILogger: MonoBehaviour
	{

		public enum Anchor
		{
			Top,
			Bottom,
		}

		[SerializeField] private Anchor	m_anchor;
		[SerializeField] private int	m_historyLimit = 100;

		private Queue<string>	m_logMessages = new Queue<string>();
		private bool			m_expanded = false;
		private Vector2			m_scrollPosition = new Vector2();
		private string			m_lastMessage = "";

		private float m_height;

		private void OnEnable ()
		{
			Application.logMessageReceived += this.HandleLogs;
		}

		private void OnDisable ()
		{
			Application.logMessageReceived -= this.HandleLogs;
		}
	
		private void HandleLogs ( string message, string trace, LogType type )
		{
			string content = "[" + type.ToString() + "] " + message;
			m_logMessages.Enqueue(content);
			if (m_logMessages.Count > m_historyLimit)
				m_logMessages.Dequeue();

			m_lastMessage = content;
		}

		#if DEBUG

		private void OnGUI ()
		{
			GUI.depth = -100;
		
			m_height = Mathf.Min(Screen.height, Screen.width) / 12;

			GUI.skin.label.wordWrap = false;
			GUI.skin.label.alignment = TextAnchor.MiddleLeft;
			GUI.skin.label.fontSize = (int)(m_height * 0.5);;
			GUI.skin.button.fontSize = (int)(m_height * 0.9);
			
			if (m_anchor == Anchor.Top)
			{
				if (m_expanded == false)
					GUI.Box( new Rect(0,0, Screen.width, m_height), "");
				else
					GUI.Box( new Rect(0,0, Screen.width, Screen.height), "");
				if (GUI.Button( new Rect(5, 5, m_height - 10, m_height - 10), m_expanded ? "-" : "+"))
					m_expanded = !m_expanded;
				GUI.Label( new Rect(m_height + 10, 0, Screen.width - 60, m_height), m_lastMessage);
			}
			else
			{
				if (m_expanded == false)
					GUI.Box( new Rect(0, Screen.height - m_height, Screen.width, m_height), "");
				else
					GUI.Box( new Rect(0,0, Screen.width, Screen.height), "");
				if (GUI.Button( new Rect(5, Screen.height - (m_height - 5), m_height - 10, m_height - 10), m_expanded ? "-" : "+"))
					m_expanded = !m_expanded;
				GUI.Label( new Rect(m_height + 10, Screen.height - m_height, Screen.width - 60, m_height), m_lastMessage);
			}

			if (m_expanded)
			{
				Rect RectView = new Rect(0, 0, Screen.width, m_logMessages.Count * (m_height *  0.8f));

				if (m_anchor == Anchor.Top)
				{
					m_scrollPosition = GUI.BeginScrollView( new Rect(0, m_height, Screen.width, Screen.height - m_height), m_scrollPosition, RectView, false, true);

					int y = 0;

					foreach ( string msg in m_logMessages.ToList() )
					{
						GUI.Label(new Rect(0, y, Screen.width, m_height * 0.8f), msg);
						y += (int)(m_height *  0.8f);
					}

					GUI.EndScrollView();
				}
				else
				{
					m_scrollPosition = GUI.BeginScrollView( new Rect(0, 0, Screen.width, Screen.height - m_height), m_scrollPosition, RectView, false, true);

					int y = 0;

					foreach ( string msg in m_logMessages.ToList() )
					{
						GUI.Label(new Rect(0, y, Screen.width, m_height * 0.8f), msg);
						y += (int)(m_height *  0.8f);
					}

					GUI.EndScrollView();
				}
			}
		}
		#endif

	}

}