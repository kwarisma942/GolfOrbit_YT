using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class CloudBuildHelper : MonoBehaviour
{
#if UNITY_CLOUD_BUILD
        public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest)
        {
            string build = manifest.GetValue("buildNumber", null);
            PlayerSettings.iOS.buildNumber = build;
           
            int buildNo = 1;
            int.TryParse(build, out buildNo);
           
            PlayerSettings.Android.bundleVersionCode = buildNo;
        }
#endif
}