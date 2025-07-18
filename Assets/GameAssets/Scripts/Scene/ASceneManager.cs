using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Pinpin.UI;

namespace Pinpin.Scene
{

	[DisallowMultipleComponent]
	public abstract partial class ASceneManager: MonoBehaviour
	{

		[SerializeField] private AUIManager	m_UIManager;

		public static event	UnityAction	onSceneReady;

		protected virtual AUIManager UI
		{
			get { return (m_UIManager); }
		}

		protected virtual void Awake ()
		{
			#if DEBUG
				Debug.Log(this.GetType().Name + " - Awake() ");
			#endif

			if (!ApplicationManager.isInitialized)
			{
				#if DEBUG
					Debug.Log(this.GetType().Name + " - Loading Init Scene. ");
				#endif

				SceneManager.LoadScene(0); // Load InitScene (Must be the scene 0)
			}
			else
			{
				ApplicationManager.SetCurrentSceneManager(this);
				if (m_UIManager != null)
					m_UIManager.SetSceneManager(this);
			}
		}

		protected virtual void Start ()
		{
			#if DEBUG
				Debug.Log(this.GetType().Name + " - Start() ");
			#endif
		}

		// Tell to the game Manager the scene was ready to be displayed
		protected void SetSceneReady ()
		{
			#if DEBUG
				Debug.Log("ASceneManager - SetSceneReady()");
			#endif

			ASceneManager.onSceneReady?.Invoke();
		}

		public virtual void OnQuit ()
		{
			#if DEBUG
				Debug.Log(this.GetType().Name + " - OnQuit() ");
			#endif
		}

	}

}