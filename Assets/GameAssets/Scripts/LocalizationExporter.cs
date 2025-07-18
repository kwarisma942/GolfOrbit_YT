using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

[CreateAssetMenu(fileName = "LocalizationExporter", menuName = "Lcoalization/Exporter", order = 2)]
public class LocalizationExporter : ScriptableObject {

	[SerializeField] private string m_jsonPath;

	public void CreateJSON ()
	{
		string csv = (Resources.Load("Localization/CSV/Localization") as TextAsset).text;
		string[] lines = csv.Split(new string[] { "\n" }, System.StringSplitOptions.None);
		string line;
		string separator = "\":\"";
		string jsonPath = Application.dataPath + m_jsonPath;
		Debug.Log(lines[0]);
		StreamWriter json;
		if (!File.Exists(jsonPath))
		{
			File.Create(jsonPath);
		}
		json = new StreamWriter(jsonPath);

		for (int i = 1; i < lines.Length; i++)
		{
			string[] linecsv = lines[i].Split(new string[] { "," }, System.StringSplitOptions.None);
			Debug.Log(lines[i]);
			Debug.Log(linecsv.Length);
			line = "\"";
			line += linecsv[0];
			line += separator;
			line += linecsv[4];
			if (i < lines.Length - 1)
				line += "\",\n";
			else
				line += "\"";

			json.WriteLine(line);
		}

	}

}

public static class LocaleParser
{
	public static Dictionary<string, string> ParseLocaleFile ( string filePath )
	{
		string json = (Resources.Load(filePath) as TextAsset).text;
		Dictionary<string, string> locales = new Dictionary<string, string>();

		bool eof = false;

		int emergency_count = 911;
		int quoteIndex = 0;
		int commaIndex = 0;
		bool parsingKey = true;

		string key = "";

		while (!eof)
		{
			if (emergency_count-- == 0)
			{
				Debug.LogError("ParseLocale was in infinite loop, abort");
				break;
			}

			quoteIndex = json.IndexOf('"', commaIndex + 1);

			commaIndex = json.IndexOf(parsingKey ? "\":\"" : "\",", commaIndex + 1);

			if (commaIndex < 0)
			{
				// Last value to parse
				commaIndex = json.IndexOf('"', quoteIndex + 1);
				eof = true;
			}

			int l = commaIndex - (quoteIndex + 1);
			string parsed = json.Substring(quoteIndex + 1, l);

			if (parsingKey)
				key = parsed;
			else
				locales.Add(key, parsed);

			// Key, then value, then key, then value...
			parsingKey = !parsingKey;
		}

		return locales;
	}
}

