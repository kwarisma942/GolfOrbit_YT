#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class IOSPostProcess
{
	[PostProcessBuild(200)]
	public static void OnPostprocessBuild ( BuildTarget buildTarget, string buildPath )
	{
		// BuiltTarget.iOS is not defined in Unity 4, so we just use strings here
		if (buildTarget.ToString() != "iOS" && buildTarget.ToString() != "iPhone") return;
		ChangeKeysToPlist(buildPath);
	}

	private static void ChangeKeysToPlist ( string buildPath )
	{
		// Get plist
		string plistPath = buildPath + "/Info.plist";
		PlistDocument plist = new PlistDocument();
		plist.ReadFromString(File.ReadAllText(plistPath));

		// Get root
		PlistElementDict rootDict = plist.root;
		
		rootDict.SetString("ITSAppUsesNonExemptEncryption", "NO");

		string exitsOnSuspendKey = "UIApplicationExitsOnSuspend";
		if (rootDict.values.ContainsKey(exitsOnSuspendKey))
		{
			rootDict.values.Remove(exitsOnSuspendKey);
		}

		// Write to file
		File.WriteAllText(plistPath, plist.WriteToString());
	}

}
#endif