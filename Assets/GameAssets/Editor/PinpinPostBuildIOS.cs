#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
#if UNITY_2017_1_OR_NEWER
using UnityEditor.iOS.Xcode.Extensions;
#endif
using UnityEngine;

public static class PinpinPostBuildiOS
{
    private static readonly string[] PlatformLibs = { "libz.dylib", "libsqlite3.dylib", "libxml2.dylib" };

    [PostProcessBuild(150)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget.ToString() != "iOS" && buildTarget.ToString() != "iPhone") return;
		AddKeyToPlist(buildPath);
        PrepareProject(buildPath);
    }

	private static void AddKeyToPlist ( string buildPath )
	{
		// Get plist
		string plistPath = buildPath + "/Info.plist";
		PlistDocument plist = new PlistDocument();
		plist.ReadFromString(File.ReadAllText(plistPath));

		// Get root
		PlistElementDict rootDict = plist.root;

		// Change value of AppLovinSdkKey in Xcode plist
#if TEST_PINPIN
		rootDict.SetString("AppLovinSdkKey", "4FTlNLzt1e8_9fz_BSsq2epowBMk8cpCtHyFcw6eTFdhvbh53Xw6Bku6vFq1sdEkvAzV439cIoJCN_HRXBXivP");
#else
		rootDict.SetString("AppLovinSdkKey", "TZp7RLJvLtJ_LBDK4T1vXG5n2Gl73EkcoWkEt17CuQBEcPTgtaZPseAuwv_z7kZwFI86KGIDgURIv_znaWAQAK");
#endif
        // AdMob Key
        rootDict.SetString("GADApplicationIdentifier", "ca-app-pub-5473693186932777~2100947791");

        rootDict.SetString("NSCalendarsUsageDescription", "Adding events");
		rootDict.SetString("NSPhotoLibraryUsageDescription", "Taking selfies");
		rootDict.SetString("NSCameraUsageDescription", "Taking selfies");
		rootDict.SetString("NSMotionUsageDescription", "Interactive ad controls");

		// Write to file
		File.WriteAllText(plistPath, plist.WriteToString());
    }

    private static void PrepareProject(string buildPath)
    {
        var projPath = Path.Combine(buildPath, "Unity-iPhone.xcodeproj/project.pbxproj");
        var project = new PBXProject();
        project.ReadFromString(File.ReadAllText(projPath));
        var target = project.TargetGuidByName("Unity-iPhone");

        foreach (var lib in PlatformLibs)
        {
            string libGUID = project.AddFile("usr/lib/" + lib, "Libraries/" + lib, PBXSourceTree.Sdk);
            project.AddFileToBuild(target, libGUID);
        }

        bool emitWarning = true;
#if UNITY_2017_1_OR_NEWER
        var fileGuid = project.FindFileGuidByProjectPath("Frameworks/MoPub/Plugins/iOS/MoPubSDKFramework.framework")
                    // Unity 2018.3 leaves out the intermediate directories.
                    ?? project.FindFileGuidByProjectPath("Frameworks/MoPubSDKFramework.framework")
                    // Check legacy location in case post 5.4 file migration has not been done.
                    ?? project.FindFileGuidByProjectPath("Frameworks/Plugins/iOS/MoPubSDKFramework.framework");
        if (fileGuid != null)
        {
            project.AddFileToEmbedFrameworks(target, fileGuid);
            emitWarning = false;
        }
#endif
        if (emitWarning)
            Debug.LogWarning(
                "Unable to automatically add MoPubSDKFramework.framework to the Embedded Binaries list in the Xcode project.\n" +
                "Please add it manually in Xcode, under the General properties of the Unity-iPhone target, unless you\n" +
                "are building against the SDK source (via Cocoapods) or static library.");

        project.SetBuildProperty(
            target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");

        project.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
        project.AddBuildProperty(target, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
        project.AddBuildProperty(target, "CLANG_ENABLE_MODULES", "YES");
        project.AddBuildProperty(target, "ENABLE_BITCODE", "NO");

        File.WriteAllText(projPath, project.WriteToString());
    }

}
#endif
