//using CrazyGames;
using Pinpin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using GameScene = Pinpin.Scene.ASceneManager.Scene;


public class LoadLibrary_Addressables : MonoBehaviour
{

    public GameAssets GameAssetsAddressables { get; private set; }
    [SerializeField] private GameScene m_sceneToLoadbeforeInit;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Debug.LogError(" session count "+m_gameDatas.sessionCount); 
        if (PlayerPrefs.GetInt("itsFirstTime") > 0)
        {
            yield return LoadGameAssetLibraryAddressable();
        }
        else
        {
            LoadScene();
        }
    }

    void LoadScene() 
    {
        m_sceneToLoadbeforeInit = GameScene.InitScene;
        ApplicationManager.LoadScene(m_sceneToLoadbeforeInit);
    }

    #region Addressables
    public IEnumerator LoadGameAssetLibraryAddressable()
    {
        if (PlayerPrefs.GetInt("itsFirstTime") > 0)
        {
        Debug.Log("Loading Desinger Library");
            if (GameAssetsAddressables == null)
            {
                string label = "Library";
                Debug.Log($"Loading AddressableDesignerLibrary from Addressables with label: {label}");
                AsyncOperationHandle<GameAssets> handle = Addressables.LoadAssetAsync<GameAssets>(label);
                yield return handle;

                yield return new WaitUntil(() => handle.IsDone);
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Successfully loaded AccessoriesLibrary from Addressables.");
                    GameAssetsAddressables = handle.Result;
                    StaticVariables.sv_GameAsset = handle.Result;
                }
                else
                {
                    Debug.LogError("Failed to load AccessoriesLibrary from Addressables.");
                }

                LoadScene();
            }
        }
    }
    #endregion

}
