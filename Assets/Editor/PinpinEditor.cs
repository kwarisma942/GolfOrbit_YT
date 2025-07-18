using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;

public static class PinpinMenu
{

	[MenuItem("Pinpin/Remove TapNation SDK", false, 0)]
	static void RemoveTapNationSDK ()
	{
		FileUtil.DeleteFileOrDirectory("Assets/Packages");
		FileUtil.DeleteFileOrDirectory("Assets/FacebookSDK");
		FileUtil.DeleteFileOrDirectory("Assets/GameAnalytics");
		FileUtil.DeleteFileOrDirectory("Assets/FunGamesSdk");
		FileUtil.DeleteFileOrDirectory("Assets/ExternalDependencyManager");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins");
		FileUtil.DeleteFileOrDirectory("Assets/Resources");
		FileUtil.DeleteFileOrDirectory("Assets/Scripts");
		FileUtil.DeleteFileOrDirectory("Assets/Tenjin");
		FileUtil.DeleteFileOrDirectory("Assets/MaxSdk");
		RemoveDefineSymbol("TAPNATION");
	}

	public static void AddDefineSymbol ( string symbol )
	{
		string androidSymbolsString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
		List<string> androidSymbols = new List<string>(androidSymbolsString.Split(';'));
		if (!androidSymbols.Contains(symbol))
			androidSymbolsString += ";" + symbol;
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, androidSymbolsString);

		string iosSymbolsString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
		List<string> iosSymbols = new List<string>(iosSymbolsString.Split(';'));
		if (!iosSymbols.Contains(symbol))
			iosSymbolsString += ";" + symbol;
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, iosSymbolsString);
	}


	public static void RemoveDefineSymbol ( string symbol )
	{
		if (symbol.Length > 0)
		{
			string androidSymbolsString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			androidSymbolsString = androidSymbolsString.Replace(";" + symbol, "");
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, androidSymbolsString);

			string iosSymbolsString = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
			iosSymbolsString = iosSymbolsString.Replace(";" + symbol, "");
			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, iosSymbolsString);
		}
	}
	

}
