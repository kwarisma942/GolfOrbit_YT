
using System.Collections.Generic;
#if TAPNATION
//using Facebook.Unity.Settings;
//using GameAnalyticsSDK.Editor;
using Unity.EditorCoroutines.Editor;
#endif
using Pinpin;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Pinpin.Helpers;
using System.IO;
using System.Collections;
using System;
using UnityEngine.Networking;
using System.Text;
using System.Xml;
using System.Net;
//enkins token : "11dfea40e7b809e37ba9c5157cdb012c5e"
public class PinpinStatusWindow : EditorWindow
{
	[MenuItem("Pinpin/Show status Window", false, 0)]
	static void ShowStatusWindow ()
	{
		GetWindow(typeof(PinpinStatusWindow));
	}

	private Pinpin.BuildTargetList m_buildTargetList;

	private Vector2 m_scrollPos;
	private string m_googleSheetUrl;
	string m_androidAdjustToken = "";
	string m_iOSAdjustToken = "";

	private void Awake ()
	{
		Loadbuildist();
	}

	private void OnFocus ()
	{
		Loadbuildist();
	}

	private void Loadbuildist ()
	{
		if (m_buildTargetList == null)
		{
			m_buildTargetList = AssetDatabase.LoadAssetAtPath<Pinpin.BuildTargetList>("Assets/Objects/BuildTargetList.asset");
		}


	}



	private void OnGUI ()
	{
		GUIStyle errorStyle = new GUIStyle(EditorStyles.label);
		errorStyle.normal.textColor = new Color(0.8627452f, 0.2f, 0.1882353f);
		errorStyle.font = EditorStyles.boldFont;

		GUIStyle goodStyle = new GUIStyle(EditorStyles.label);
		goodStyle.normal.textColor = new Color(48f / 255f, 209f / 255f, 88f / 255f);
		goodStyle.font = EditorStyles.boldFont;


		EditorGUILayout.BeginVertical(EditorStyles.helpBox);

		EditorGUILayout.BeginHorizontal();

		GUILayout.Label("Project phase :");
		EditorGUI.BeginChangeCheck();
		m_buildTargetList.m_projectPhase = (BuildTargetList.ProjectPhase)EditorGUILayout.Popup((int)m_buildTargetList.m_projectPhase, Enum.GetNames(typeof(BuildTargetList.ProjectPhase)), GUILayout.ExpandWidth(false));

		if (EditorGUI.EndChangeCheck())
		{
			EditorUtility.SetDirty(m_buildTargetList);
		}

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();

		GUILayout.Label("Bundle Identifier :");

		if (PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS) == PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android))
		{
			if (string.IsNullOrEmpty(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS)))
				GUILayout.Label("Not Set", errorStyle, GUILayout.ExpandWidth(false));
			else if (PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS) == "com.pinpinteam.gametemplate")
				GUILayout.Label(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS), errorStyle, GUILayout.ExpandWidth(false));
			else
				GUILayout.Label(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS), goodStyle, GUILayout.ExpandWidth(false));
		}
		else
		{
			GUILayout.Label("iOS : ", GUILayout.ExpandWidth(false));
			if (string.IsNullOrEmpty(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS)))
				GUILayout.Label("Not Set", errorStyle, GUILayout.ExpandWidth(false));
			else if (PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS) == "com.pinpinteam.gametemplate")
				GUILayout.Label(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS), errorStyle, GUILayout.ExpandWidth(false));
			else
				GUILayout.Label(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS), goodStyle, GUILayout.ExpandWidth(false));

			GUILayout.Label("Android : ", GUILayout.ExpandWidth(false));
			if (string.IsNullOrEmpty(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android)))
				GUILayout.Label("Not Set", errorStyle, GUILayout.ExpandWidth(false));
			else if (PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android) == "com.pinpinteam.gametemplate")
				GUILayout.Label(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android), errorStyle, GUILayout.ExpandWidth(false));
			else
				GUILayout.Label(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android), goodStyle, GUILayout.ExpandWidth(false));
		}
		EditorGUILayout.EndHorizontal();


		if (m_buildTargetList.m_projectPhase > BuildTargetList.ProjectPhase.Prototyping)
		{
			EditorGUILayout.BeginHorizontal();

			GUILayout.Label("Unity Services :");
			if (string.IsNullOrEmpty(CloudProjectSettings.projectId))
				GUILayout.Label("Not Connected", errorStyle, GUILayout.ExpandWidth(false));
			else
			{
				GUILayout.Label("Linked, ", goodStyle, GUILayout.ExpandWidth(false));
				if (CloudProjectSettings.projectName == PlayerSettings.productName)
					GUILayout.Label(CloudProjectSettings.projectName, goodStyle, GUILayout.ExpandWidth(false));
				else
				{
					GUILayout.Label("Names are not mathching : ", errorStyle, GUILayout.ExpandWidth(false));
					GUILayout.Label(CloudProjectSettings.projectName + "/" + PlayerSettings.productName, errorStyle, GUILayout.ExpandWidth(false));
				}
			}

			EditorGUILayout.EndHorizontal();

			bool tapNationLoaded = AssetDatabase.IsValidFolder("Assets/FunGamesSdk");
			if (!tapNationLoaded)
			{
				EditorGUILayout.BeginHorizontal();

				GUILayout.Label("TAPNATION Package :");
				GUILayout.Label("Not Loaded", errorStyle, GUILayout.ExpandWidth(false));
				if (GUILayout.Button("Import It", GUILayout.ExpandWidth(false)))
				{
					AssetDatabase.ImportPackage(Application.dataPath + "/Packages/TapNationPackage.unitypackage", false);
					//PinpinMenu.AddDefineSymbol("TAPNATION");
				}
				EditorGUILayout.EndHorizontal();
			}


			if (tapNationLoaded)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("TAPNATION Define symbol :");

				string androidSymbolsString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
				string iosSymbolsString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
				if (!androidSymbolsString.Contains(";TAPNATION") || !iosSymbolsString.Contains(";TAPNATION"))
				{
					GUILayout.Label("Missing", errorStyle, GUILayout.ExpandWidth(false));
					if (tapNationLoaded && GUILayout.Button("Add It", GUILayout.ExpandWidth(false)))
					{
						PinpinMenu.AddDefineSymbol("TAPNATION");
					}
				}
				else
				{
					GUILayout.Label("Defined", goodStyle, GUILayout.ExpandWidth(false));
				}
				EditorGUILayout.EndHorizontal();



				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Facebook App ID :");

				EditorGUILayout.EndHorizontal();


				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Game Analytics Settings :");


				EditorGUILayout.EndHorizontal();
			}
		}

		EditorGUILayout.EndVertical();

		if (m_buildTargetList != null)
		{
			errorStyle.fontSize = 20;
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			GUILayout.Label("Current Build Target : " + PlayerPrefs.GetString("CurrentTarget", "None"), errorStyle);
			EditorGUILayout.BeginHorizontal();

			EditorGUI.BeginChangeCheck();

			m_buildTargetList.buildPath = EditorGUILayout.TextField(m_buildTargetList.buildPath);
			if (GUILayout.Button("Select folder", GUILayout.ExpandWidth(false)))
			{
				string newFolder = EditorUtility.OpenFolderPanel("Select build folder", m_buildTargetList.buildPath, "");
				if (newFolder.Length > 0)
					m_buildTargetList.buildPath = newFolder;
			}
			EditorGUILayout.EndHorizontal();


			EditorGUILayout.EndVertical();

			m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, false, GUILayout.ExpandHeight(true));

			GUILayout.BeginVertical();
			for (int i = 0; i < m_buildTargetList.buildTargets.Count; i++)
			{
				if (DrawBuildTarget(m_buildTargetList.buildTargets[i]))
				{
					m_buildTargetList.buildTargets.RemoveAt(i);
					i--;
				}
			}
			GUILayout.EndVertical();
			if (GUILayout.Button("Add build target"))
			{
				m_buildTargetList.buildTargets.Add(new BuildTargetList.BuildTarget());
			}
			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(m_buildTargetList);
			}
			GUILayout.EndScrollView();
		}
		else
		{
			GUILayout.Label("Error : Missing BuildTargetList.asset", errorStyle);
			if (GUILayout.Button("Create It", GUILayout.ExpandWidth(true)))
			{
				m_buildTargetList = ScriptableObject.CreateInstance<Pinpin.BuildTargetList>();

				AssetDatabase.CreateFolder("Assets", "Objects");
				AssetDatabase.CreateAsset(m_buildTargetList, "Assets/Objects/BuildTargetList.asset");
				AssetDatabase.SaveAssets();

			}
		}
	}

	private bool DrawBuildTarget ( BuildTargetList.BuildTarget buildTarget )
	{
		bool destroy = false;
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		{
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("Build target name : ");
				if (GUILayout.Button("Delete build target", GUILayout.ExpandWidth(false)))
				{
					destroy = true;
				}
			}
			EditorGUILayout.EndHorizontal();

			buildTarget.buildName = GUILayout.TextField(buildTarget.buildName);
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("Defines : ", GUILayout.ExpandWidth(false));
				EditorGUILayout.BeginVertical();
				{
					for (int i = 0; i < buildTarget.defines.Count; i++)
					{
						EditorGUILayout.BeginHorizontal();
						{
							buildTarget.defines[i] = EditorGUILayout.TextField(buildTarget.defines[i], GUILayout.ExpandWidth(true));
							if (GUILayout.Button("-", GUILayout.ExpandWidth(false)))
							{
								buildTarget.defines.RemoveAt(i);
								i--;
							}
						}
						EditorGUILayout.EndHorizontal();
					}
				}
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label("Build Android AAB : ", GUILayout.ExpandWidth(false));
				buildTarget.buildAndroidAAB = EditorGUILayout.Toggle(buildTarget.buildAndroidAAB, GUILayout.ExpandWidth(true));
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Add define", GUILayout.ExpandWidth(true)))
				{
					buildTarget.defines.Add("");
				}
				if (GUILayout.Button("Select Target", GUILayout.ExpandWidth(true)))
				{
					SetupTarget(buildTarget);
				}
				if (GUILayout.Button("Build", GUILayout.ExpandWidth(true)))
				{
					BuildTarget(buildTarget);
				}
			}
			EditorGUILayout.EndHorizontal();

		}
		GUILayout.EndVertical();

		return destroy;
	}

	private void SetupTarget ( Pinpin.BuildTargetList.BuildTarget buildTarget )
	{
		m_buildTargetList.OnValidate();
		foreach (string define in m_buildTargetList.allDefines)
		{
			PinpinMenu.RemoveDefineSymbol(define);
		}

		for (int i = 0; i < buildTarget.defines.Count; i++)
		{
			PinpinMenu.AddDefineSymbol(buildTarget.defines[i]);
		}
		PlayerPrefs.SetString("CurrentTarget", buildTarget.buildName);
	}

	private void BuildTarget ( Pinpin.BuildTargetList.BuildTarget buildTarget )
	{
		if (EditorUtility.DisplayDialog("Build", "Build current target : " + PlayerPrefs.GetString("CurrentTarget", "None"), "yes", "no"))
		{
			SetupTarget(buildTarget);
			List<string> scenes = new List<string>();
			for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
			{
				if (EditorBuildSettings.scenes[i].enabled)
				{
					scenes.Add(EditorBuildSettings.scenes[i].path);
				}
			}
			EditorUserBuildSettings.buildAppBundle = false;
			BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
			buildPlayerOptions.scenes = scenes.ToArray();
			buildPlayerOptions.locationPathName = m_buildTargetList.buildPath + "/"
												+ Application.productName + "-"
												+ buildTarget.buildName + "-"
												+ Application.version + "-"
												+ (EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS ? PlayerSettings.iOS.buildNumber : PlayerSettings.Android.bundleVersionCode.ToString())
												+ (EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android ? ".apk" : "");

			buildPlayerOptions.target = EditorUserBuildSettings.activeBuildTarget;
			buildPlayerOptions.options = BuildOptions.None;

			BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
			BuildSummary summary = report.summary;

			if (summary.result == BuildResult.Succeeded)
			{
				Debug.Log("Build succeeded: " + summary.totalSize + " bytes");

				if (buildTarget.buildAndroidAAB && EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
				{
					EditorUserBuildSettings.buildAppBundle = true;
					buildPlayerOptions.locationPathName = m_buildTargetList.buildPath + "/"
														+ Application.productName + "-"
														+ buildTarget.buildName + "-"
														+ Application.version + "-"
														+ PlayerSettings.Android.bundleVersionCode.ToString()
														+ ".aab";
					report = BuildPipeline.BuildPlayer(buildPlayerOptions);
					summary = report.summary;

					if (summary.result == BuildResult.Succeeded)
					{
						Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
						if (EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
							PlayerSettings.Android.bundleVersionCode++;
						OpenInFileBrowser.Open(report.summary.outputPath);
					}

					if (summary.result == BuildResult.Failed)
					{
						Debug.Log("Build failed");
					}

				}
				else
				{
					if (EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
						PlayerSettings.Android.bundleVersionCode++;
					OpenInFileBrowser.Open(report.summary.outputPath);
				}
			}

			if (summary.result == BuildResult.Failed)
			{
				Debug.Log("Build failed");
			}
		}

	}

}
