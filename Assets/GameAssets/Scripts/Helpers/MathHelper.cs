using UnityEngine;
using Pinpin.Types;
using System;

namespace Pinpin.Helpers
{

	public static class MathHelper
	{
		public static float MapBetween ( float value, float valueMin, float valueMax, float resultMin, float resultMax )
		{
			return (resultMax - resultMin) / (valueMax - valueMin) * (value - valueMax) + resultMax;
		}

		public static string ConvertToEgineeringNotation ( ulong value )
		{
			if (value < 1e3)
				return value.ToString();
			if (value < 1e4)
				return (value / 1e3).ToString("n2", ApplicationManager.currentCulture) + "K";
			if (value < 1e5)
				return (value / 1e3).ToString("n1", ApplicationManager.currentCulture) + "K";
			if (value < 1e6)
				return (value / 1e3).ToString("n0", ApplicationManager.currentCulture) + "K";
			if (value < 1e7)
				return (value / 1e6).ToString("n2", ApplicationManager.currentCulture) + "M";
			if (value < 1e8)
				return (value / 1e6).ToString("n1", ApplicationManager.currentCulture) + "M";
			if (value < 1e9)
				return (value / 1e6).ToString("n0", ApplicationManager.currentCulture) + "M";
			if (value < 1e10)
				return (value / 1e9).ToString("n2", ApplicationManager.currentCulture) + "B";
			if (value < 1e11)
				return (value / 1e9).ToString("n1", ApplicationManager.currentCulture) + "B";
			if (value < 1e12)
				return (value / 1e9).ToString("n0", ApplicationManager.currentCulture) + "B";
			if (value < 1e13)
				return (value / 1e12).ToString("n2", ApplicationManager.currentCulture) + "T";
			if (value < 1e14)
				return (value / 1e12).ToString("n1", ApplicationManager.currentCulture) + "T";
			if (value < 1e15)
				return (value / 1e12).ToString("n0", ApplicationManager.currentCulture) + "T";
			if (value < 1e16)
				return (value / 1e15).ToString("n2", ApplicationManager.currentCulture) + "Qa";
			if (value < 1e17)
				return (value / 1e15).ToString("n1", ApplicationManager.currentCulture) + "Qa";
			if (value < 1e18)
				return (value / 1e15).ToString("n0", ApplicationManager.currentCulture) + "Qa";
			return value.ToString();
		}

		public static int GetRandomWeighted ( int[] weights )
		{
			int valueCount = weights.Length;
			int[] values = new int[valueCount];
			for (int i = 0; i < valueCount; i++)
			{
				values[i] = (i == 0 ? 0 : values[i - 1]) + weights[i];
			}

			int randValue = UnityEngine.Random.Range(0, values[valueCount - 1]);
			for (int i = 0; i < valueCount; i++)
			{
				if (randValue < values[i])
					return i;
			}
			return 0;
		}

	}

}