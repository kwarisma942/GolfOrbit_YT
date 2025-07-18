using UnityEngine;
using Pinpin.UI;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

namespace Pinpin.Scene.MainScene.UI
{

	public sealed class MainSceneUIManager: AUIManager
	{
        public static MainSceneUIManager Instance { get; private set; }
        public new MainSceneManager	sceneManager
		{
			get { return (base.sceneManager as MainSceneManager); }
		}


        [Header("Panel Addressables Settings")]
        [SerializeField] private AssetLabelReference panelLabel;

        [Header("Panel Parent Settings")]
        [SerializeField] private Transform panelParentTransform;

        [Header("Loaded Panel References")]
        [SerializeField] private List<GameObject> loadedPanels = new List<GameObject>();

        [Header("Addressables Settings")]
        [SerializeField] private AssetLabelReference groupLabel;

        [Header("Parent Settings")]
        [SerializeField] private Transform PopupsparentTransform;

        [Header("Loaded Popup References")]
        [SerializeField] private List<GameObject> loadedInstances = new List<GameObject>();

        private bool isLoadingComplete = false; // Track loading status
        private bool isPanelLoadingComplete = false;
        [SerializeField]
        public GameObject StartLoadingSceen;

        protected override void Awake ()
		{
			base.Awake ();
		}

		protected override IEnumerator Start ()
		{
			base.Start();
            Instance = this;
            if (groupLabel == null)
            {
                Debug.LogError("Group label is not assigned in LoadAddressables!");
                yield return false;
            }

            if (PopupsparentTransform == null)
            {
                Debug.LogError("Parent transform is not assigned in LoadAddressables!");
                yield return false;
            }

            //if (PlayerPrefs.GetInt("FirstTimelevelPlaying") == 0)
                yield return new WaitForSeconds(2.5f);
            if (!isLoadingComplete)
                yield return StartCoroutine(LoadPopupsCoroutine());
            if (!isPanelLoadingComplete)
                yield return StartCoroutine(LoadPanelsCoroutine());
        }
        #region Panels
        public IEnumerator LoadPanelsCoroutine()
        {
            isPanelLoadingComplete = false;
            if (panelLabel == null)
            {
                Debug.LogError("Panel label is not assigned in LoadAddressables!");
                yield break;
            }

            if (panelParentTransform == null)
            {
                Debug.LogError("Panel parent transform is not assigned in LoadAddressables!");
                yield break;
            }

            // Show loading screen if assigned
            //if (loadingScreen != null)
            //    loadingScreen.SetActive(true);

            AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(panelLabel, OnPanelLoaded, true);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"{handle.Result.Count} Addressables AUIPanel assets loaded from label: {panelLabel.labelString}");

                ConvertAddressablesToUIPanels();
            }
            else
            {
                Debug.LogError($"Failed to load addressable panels from label: {panelLabel.labelString}");
            }

            // Hide loading screen when done
            //if (loadingScreen != null)
            //    loadingScreen.SetActive(false);

            isPanelLoadingComplete = true;

        }

        private void OnPanelLoaded(GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("Null AUIPanel found in addressables.");
                return;
            }

            GameObject instance = Instantiate(obj, panelParentTransform);
            instance.gameObject.SetActive(false); // Optional: keep inactive
            loadedPanels.Add(instance);
        }
        public List<AUIPanel> GetLoadedPanels()
        {
            List<AUIPanel> panelInstances = new List<AUIPanel>();

            foreach (GameObject instance in loadedPanels)
            {
                AUIPanel panel = instance.GetComponent<AUIPanel>();
                if (panel != null)
                {
                    panelInstances.Add(panel);
                }
                else
                {
                    Debug.LogWarning($"GameObject {instance.name} does not have an AUIPanel component!");
                }
            }

            return panelInstances;
        }

        public IEnumerator WaitForPanelsToLoad()
        {
            yield return new WaitUntil(() => isPanelLoadingComplete);
        }


        #endregion
        #region Popups
        public IEnumerator LoadPopupsCoroutine()
        {
            isLoadingComplete = false;

            // Show loading screen if assigned
            //if (loadingScreen != null)
            //    loadingScreen.SetActive(true);

            AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(groupLabel, OnPopupsLoaded, true);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"{handle.Result.Count} Addressables AUIPopup assets loaded from label: {groupLabel.labelString}");

                ConvertAddressablesToUIPopup();
            }
            else
            {
                Debug.LogError($"Failed to load addressable assets from label: {groupLabel.labelString}");
            }

            // Hide loading screen when done
            //if (loadingScreen != null)
            //    loadingScreen.SetActive(false);

            isLoadingComplete = true;
        }


        private void OnPopupsLoaded(GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogWarning("Null AUIPopup found in addressables.");
                return;
            }

            GameObject instance = Instantiate(obj, PopupsparentTransform);
            instance.gameObject.SetActive(false); // Keep it inactive after instantiation
            loadedInstances.Add(instance);
        }

        public IEnumerator WaitForAssetsToLoad()
        {
            // Wait until loading is complete before proceeding
            yield return new WaitUntil(() => isLoadingComplete);
        }

        public List<AUIPopup> GetLoadedPopups()
        {
            List<AUIPopup> popupInstances = new List<AUIPopup>();

            foreach (GameObject instance in loadedInstances)
            {
                AUIPopup popup = instance.GetComponent<AUIPopup>();
                if (popup != null)
                {
                    popupInstances.Add(popup);
                }
                else
                {
                    Debug.LogWarning($"GameObject {instance.name} does not have an AUIPopup component!");
                }
            }

            return popupInstances;
        }
        #endregion

        void ConvertAddressablesToUIPopup()
        {
            //  Wait until the Addressables are fully loaded
            // yield return StartCoroutine(WaitForAssetsToLoad());
            //  Get loaded popups and add them to m_popups after Addressables are loaded
            List<AUIPopup> loadedPopups = GetLoadedPopups();
            AddPopupsToList(loadedPopups); // Add and call SetupPopups after
        }
        void ConvertAddressablesToUIPanels()
        {
            // Wait until the panels are fully loaded
            // yield return StartCoroutine(WaitForPanelsToLoad());

            // Get loaded panels and add them to your list or do something with them
            List<AUIPanel> loadedPanels = GetLoadedPanels();
            AddPanelsToList(loadedPanels); // Your method to handle AUIPanel setup
        }
        public void AddPopupsToList(List<AUIPopup> loadedPopups)
        {
            if (loadedPopups == null || loadedPopups.Count == 0)
            {
                Debug.LogWarning("No popups to add to m_popups!");
                return;
            }

            foreach (AUIPopup popup in loadedPopups)
            {
                if (popup != null && !m_popups.Contains(popup))
                {
                    m_popups.Add(popup);
                    popup.SetUIManager(this);
                    popup.gameObject.SetActive(false); // Disable after adding
                }
            }

            Debug.Log($"{loadedPopups.Count} popups added to m_popups.");

            //  Call SetupPopups after adding to m_popups
            SetupPopups();
            InitPopups();
        }

        void InitPopups() 
        {

			sceneManager.UI.GetPopup<CharacterUnlockPopup>().Init();
        }
        void InitPanels()
        {
            sceneManager.UI.GetPanel<ClubHousePanel>().Init();
        }
        public void AddPanelsToList(List<AUIPanel> loadedPanels)
        {
            if (loadedPanels == null || loadedPanels.Count == 0)
            {
                Debug.LogWarning("No panels to add to m_panels!");
                return;
            }

            foreach (AUIPanel panel in loadedPanels)
            {
                if (panel != null && !m_panels.Contains(panel))
                {
                    m_panels.Add(panel);
                    panel.SetUIManager(this);
                    panel.gameObject.SetActive(false); // Disable after adding
                }
            }

            Debug.Log($"{loadedPanels.Count} panels added to m_panels.");

            // Call SetupPanels after adding to m_panels
            SetupPanels();
            InitPanels();
        }
        protected override void OnLeaveRootPanel ()
		{
#if DEBUG
			Debug.Log("MainMenuUIManager - Leave root panel");
#endif
			this.OpenPopup<LeaveGamePopup>();
		}

	}

}