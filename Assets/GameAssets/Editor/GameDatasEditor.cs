using UnityEngine;
using UnityEditor;

namespace Pinpin.Editor
{
    [CustomEditor(typeof(GameDatas))]
    public class GameDatasEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Reset PlayerPrefs Data"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                Debug.Log("PlayerPrefs reset.");
            }

            if (GUILayout.Button("Force Save"))
            {
                ((GameDatas)target).ForceSave();
                Debug.Log("Forced save completed.");
            }

            //if (GUILayout.Button("Force Load"))
            //{
            //    ((GameDatas)target).ForceLoad(); // Optional: implement this method if needed
            //    Debug.Log("Forced load executed.");
            //}
        }
    }
}
