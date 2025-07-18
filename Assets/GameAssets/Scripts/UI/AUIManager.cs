using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Pinpin.Scene;
using System.Collections;

namespace Pinpin.UI
{

	[DisallowMultipleComponent,
	RequireComponent(typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)),
	RequireComponent(typeof(EventSystem), typeof(StandaloneInputModule))]
	public abstract class AUIManager: MonoBehaviour
	{

		private class Stack<T> : IEnumerable
		{
			private List<T>	list { get; set; }

			public Stack ()
			{
				this.list = new List<T>();
			}
			
			public Stack ( Stack rhs )
			{
				foreach ( T item in rhs )
					this.list.Add(item);
			}

			public IEnumerator GetEnumerator()
			{
				return (this.list.GetEnumerator());
			}
			
			public T this [ int index ]
			{
				get { return (this.list[index]); }
			}

			public int Count
			{
				get { return (this.list.Count); }
			}

			public void Push ( T item )
			{
				this.list.Add(item);
			}
			
			public T Pop ()
			{
				if (this.list.Count == 0)
					return (default(T));
				int last = this.list.Count - 1;
				T item = this.list[last];
				this.list.RemoveAt(last);
				return (item);
			}

			public T Peek ()
			{
				if (this.list.Count == 0)
					return (default(T));
				return (this.list[this.list.Count - 1]);
			}

			public void Remove ( T item )
			{
				this.list.Remove(item);
			}

			public void Clear ()
			{
				this.list.Clear();
			}

		}

		[SerializeField] public List<AUIPanel>	m_panels;
		[SerializeField] public List<AUIPopup>	m_popups;
		[SerializeField] private ProcessingPopup m_processingPopup;

		public ASceneManager				sceneManager { get; private set; }
		private Dictionary<Type, AUIPanel>	panels { get; set; }
		private Stack<Type>					previousPanels { get; set; }
		public AUIPanel						currentPanel { get; private set; }
		public AUIPanel						rootPanel { get; private set; }
		private Dictionary<Type, AUIPopup>	popups { get; set; }
		protected AUIPopup					activePopup { get; private set; }
		public Canvas						canvas { get; private set; }

		#region MonoBehaviour

		protected virtual void Awake ()
		{
			#if DEBUG
				Debug.Log(this.GetType().Name + " - Awake() ");
			#endif

			this.SetupPanels();
			//this.SetupPopups();
			canvas = GetComponent<Canvas>();
		}

		protected virtual IEnumerator Start ()
		{
			#if DEBUG
				Debug.Log(this.GetType().Name + " - Start() ");
			#endif

			yield return null;
		}

		protected virtual void Update ()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				this.Back();
		}

		protected virtual void OnDestroy ()
		{
			AUIPopup.onPopupOpened -= this.SetActivePopup;
		}

        #endregion

        #region Init

        public void SetupPanels()
        {
            if (this.panels == null)
                this.panels = new Dictionary<Type, AUIPanel>();

            if (this.previousPanels == null)
                this.previousPanels = new Stack<Type>();

            foreach (AUIPanel panel in m_panels)
            {
                if (panel != null)
                {
                    Type panelType = panel.GetType();

                    if (!this.panels.ContainsKey(panelType))
                    {
                        this.panels.Add(panelType, panel);
                        panel.SetUIManager(this);
                        panel.gameObject.SetActive(false);
                    }
                    else
                    {
                        // Optional: update existing reference (e.g. from Addressables)
                        this.panels[panelType] = panel;
                    }
                }
            }
        }

        public void SetupPopups ()
		{
			this.popups = new Dictionary<Type, AUIPopup>();

			foreach ( AUIPopup popup in m_popups )
			{
				if (popup != null)
				{
					this.popups.Add(popup.GetType(), popup);
					popup.SetUIManager(this);
					popup.gameObject.SetActive(false);
				}
			}

			AUIPopup.onPopupOpened += this.SetActivePopup;
		}

		#endregion
		
		public void SetSceneManager ( ASceneManager sceneManager )
		{
			this.sceneManager = sceneManager;
		}

		public void Back ()
		{
			#if DEBUG
				Debug.Log(this.GetType().Name + " - Back()");	
			#endif
			if (this.activePopup != null)
				this.CloseActivePopup();
			else if (this.currentPanel.OnBackAction() == true)
			{
				if (previousPanels.Count == 0) 
				{
				//this.OnLeaveRootPanel();
				}
				else
					this.OnBackPreviousPanel();
			}
		}

		#region panels

		protected virtual void OnLeaveRootPanel ()
		{
			#if DEBUG
				Debug.Log("AUIManager - Leaving root panel... Quit");	
			#endif

			this.sceneManager.OnQuit();
		}

		protected virtual void OnBackPreviousPanel ()
		{
			if (this.currentPanel.OnBackAction() == true)
			{
				#if DEBUG
					Debug.Log("AUIManager - Reopening previous panel.");	
				#endif

				this.ReOpenPreviousPanel();
			}
		}

		public void SetCurrentPanelAsRoot ()
		{
			previousPanels.Clear();
			this.rootPanel = this.currentPanel;
		}

		public T OpenPanel<T> ( bool pushHistory = true, bool additive = false ) where T : AUIPanel
		{
			Type type = typeof(T);
			return (this.OpenPanel(type, pushHistory, additive) as T);
		}

		public AUIPanel OpenPanel ( AUIPanel panel, bool pushHistory = true, bool additive = false )
		{
			Type type = panel.GetType();
			return (this.OpenPanel(type, pushHistory, additive) as AUIPanel);
		}

		public T OpenPanelAllTime<T> () where T : AUIPanel
		{
			Type type = typeof(T);
			return (this.OpenPanelAllTime(type) as T);
		}

		private AUIPanel OpenPanel ( Type type, bool pushHistory, bool additive )
		{
			if (this.panels.ContainsKey(type) == false)
				throw new Exception(this.GetType().Name + " - Do not have panel [" + type.Name + "].");

			if (this.currentPanel != null)
			{
				if (!additive && pushHistory)
					this.currentPanel.gameObject.SetActive(false);
				if (pushHistory)
					this.previousPanels.Push(this.currentPanel.GetType());
			}
			else
				this.rootPanel = this.panels[type];

			this.panels[type].gameObject.SetActive(true);
			this.currentPanel = this.panels[type];

			#if DEBUG
				Debug.Log("AUIManager - Open panel [" + type.Name + "].");	
			#endif

			return (this.panels[type]);
		}

		private AUIPanel OpenPanelAllTime ( Type type )
		{
			if (this.panels.ContainsKey(type) == false)
				throw new Exception(this.GetType().Name + " - Do not have panel [" + type.Name + "].");
			
			this.panels[type].gameObject.SetActive(true);

#if DEBUG
			Debug.Log("AUIManager - Open panel [" + type.Name + "].");
#endif
			return (this.panels[type]);
		}

		public void ClosePanel<T> ()
		{
			Type type = typeof(T);
			this.ClosePanel(type);
		}

		private void ClosePanel ( Type type )
		{
			if (this.panels.ContainsKey(type) == false)
				throw new Exception(this.GetType().Name + " - Do not have panel [" + type.Name +"].");

			#if DEBUG
				Debug.Log("AUIManager - Closing panel [" + type.Name + "].");	
			#endif

			AUIPanel panel = this.panels[type];

			if (this.previousPanels.Count == 0 && this.currentPanel != null && panel == this.currentPanel)
			{
				Debug.LogWarning(this.GetType().Name + " - You attempting to close manually the root panel");
				return ;
			}

			if (panel.gameObject.activeSelf)
				panel.gameObject.SetActive(false);

			this.previousPanels.Remove(type);
		}

		private void ReOpenPreviousPanel ()
		{
			this.ClosePanel(this.currentPanel.GetType());

			AUIPanel panel = this.panels[this.previousPanels.Pop()];
			
			#if DEBUG
				Debug.Log("AUIManager - Reopenig panel [" + panel.GetType().Name + "].");	
			#endif

			if (!panel.gameObject.activeSelf)
				panel.gameObject.SetActive(true);
				
			this.currentPanel = panel;
		}

		public T GetPanel<T> () where T : AUIPanel
		{
			Type type = typeof(T);

			if (this.panels.ContainsKey(type) == false)
				throw new Exception(this.GetType().Name + " - Do not have panel [" + type.Name + "].");
			return (this.panels[type] as T);
		}

		#endregion

		#region popups

		public T OpenPopup<T> () where T : AUIPopup
		{
			Type type = typeof(T);
			return (this.OpenPopup(type) as T);
		}

		public AUIPopup OpenPopup (AUIPopup popup)
		{
			Type type = popup.GetType();
			return (this.OpenPopup(type) as AUIPopup);
		}

		private AUIPopup OpenPopup ( Type type )
		{
			AUIPopup popup;

			#if DEBUG
				Debug.Log("AUIManager - Opening popup: " + type.Name);	
			#endif

			if (this.popups.ContainsKey(type) == false)
				throw new Exception(this.GetType().Name + " - Do not have popup [" + type.Name + "].");

			popup = this.popups[type];
			if (!popup.gameObject.activeSelf)
				popup.gameObject.SetActive(true);
			else
			{
				#if DEBUG
					Debug.LogWarning("AUIManager - Popup [" + type.Name + "] is already opened.");	
				#endif
			}
			return (popup);
		}

		public void ClosePopup<T> () where T : AUIPopup
		{
			Type type = typeof(T);
			this.ClosePopup(type);
		}

		private void ClosePopup ( Type type )
		{
			AUIPopup popup;

			#if DEBUG
				Debug.Log("AUIManager - Closing popup: [" + type.Name + "].");	
			#endif

			if (this.popups.ContainsKey(type) == false)
				throw new Exception(this.GetType().Name + " - Do not have popup [" + type.Name + "].");

			popup = this.popups[type];
			if (popup.gameObject.activeSelf)
				popup.Close();
			else
			{
				#if DEBUG
					Debug.LogWarning("AUIManager - Popup [" + type.Name + "] is already closed.");	
				#endif
			}

			if (this.activePopup != null && popup == this.activePopup)
				this.activePopup = null;
		}

		public T GetPopup<T> () where T : AUIPopup
		{
			Type type = typeof(T);

			if (this.popups.ContainsKey(type) == false)
				throw new Exception(this.GetType().Name + " - Do not have popup [" + type.Name + "].");
			return (this.popups[type] as T);
		}
	
		public void CloseActivePopup ( bool forceClose = false)
		{
			if (this.activePopup == null)
				return ;
		
			#if DEBUG
				Debug.Log("AUIManager - Closing active popup: [" + this.activePopup.GetType().Name + "].");	
			#endif

			if (!forceClose && !this.activePopup.IsClosable())
			{
				#if DEBUG
					Debug.LogWarning("AUIManger - Current popup cannot be closed from user Input");
				#endif
				return ;
			}
			AUIPopup popupToClose = this.activePopup;
			this.activePopup = null;
			popupToClose.Close();
		}

		private void SetActivePopup ( AUIPopup popup )
		{
			if (this.activePopup == popup)
				return ;

			if (this.activePopup != null)
				this.activePopup.Close();

			this.activePopup = popup;
			this.activePopup.onPopupClosed += OnPopupClosed;
		}

		private void OnPopupClosed ( AUIPopup popup )
		{
			if (this.activePopup != null && this.activePopup == popup)
				this.activePopup = null;

			#if DEBUG
				Debug.Log("AUIManager - Popup closed: [" + popup.GetType().Name + "].");	
			#endif

			popup.gameObject.SetActive(false);
			if (this.currentPanel.gameObject.activeSelf == false)
				this.currentPanel.gameObject.SetActive(true);

			popup.onPopupClosed -= OnPopupClosed;
		}

		public void OpenProcessingPopup ( string message )
		{
			m_processingPopup.gameObject.SetActive(true);
			m_processingPopup.message = message;
		}

		public void CloseProcessingPopup ()
		{
			m_processingPopup.gameObject.SetActive(false);
		}

		#endregion

	}

}