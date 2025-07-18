#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class LargeTextureFinder
{
    private const long SIZE_THRESHOLD = 1024 * 1024; // 1 MB

    [MenuItem("Tools/Find Large Textures")]
    public static void FindLargeTextures()
    {
        string[] guids = AssetDatabase.FindAssets("t:Texture2D");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Length > SIZE_THRESHOLD)
            {
                Debug.Log($"Large Texture Found: {path} - {fileInfo.Length / (1024f * 1024f):F2} MB",
                          AssetDatabase.LoadAssetAtPath<Texture2D>(path));
            }
        }
        Debug.Log("Texture Scan Completed.");
    }
}
#endif
