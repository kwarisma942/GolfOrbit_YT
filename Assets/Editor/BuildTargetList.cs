using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinpin
{

	[CreateAssetMenu(fileName = "BuildTargetList", menuName = "Game/BuildTargetList", order = 1)]
	public class BuildTargetList : ScriptableObject
	{
		public enum ProjectPhase
		{
			Prototyping,
			CPITest,
			Production
		}

		[Serializable]
		public class BuildTarget
		{
			public string buildName;
			public bool buildAndroidAAB;
			public List<string> defines = new List<string>();
		}

		public HashSet<string> allDefines = new HashSet<string>();
		public List<BuildTarget> buildTargets =  new List<BuildTarget>();
		public string buildPath = "./builds";
		public ProjectPhase m_projectPhase = ProjectPhase.Prototyping;

		public void OnValidate ()
		{
			allDefines.Clear();
			for (int i = 0; i < buildTargets.Count; i++)
			{
				for (int j = 0; j < buildTargets[i].defines.Count; j++)
				{
					allDefines.Add(buildTargets[i].defines[j]);
				}
			}
		}

	}

}