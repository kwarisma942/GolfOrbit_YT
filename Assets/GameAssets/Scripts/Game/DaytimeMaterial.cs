using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaytimeMaterial : MonoBehaviour
{
	[System.Serializable]
	public enum DayTime
	{
		None,
		Day,
		Night,
		SunSet,
		Grey
	}
	
	[SerializeField] private Material bgEarthMat;
	[SerializeField] private Material bgfloorMat;
	[SerializeField] private Material hillsMat;
	[SerializeField] private Material seaMat;
	[SerializeField] private Material montainsMat;
	[SerializeField] private Material skyMat;
	[SerializeField] private Material cloudsMat;
	[SerializeField] private Material fairMat;
	[SerializeField] private Material obstacleWaterMat;
	[SerializeField] private Material obstacleSandMat;
	[SerializeField] private Material obstacleGreenMat;
	[SerializeField] private Material poundSparkleMat;
	[SerializeField] private Material sandSparkleMat;
	[SerializeField] private Material universeBorderMat;
	[SerializeField] private Material rocksMat;


	//day
	[SerializeField] private Color dayEarthColor;
	[SerializeField] private Color dayFloorColor;
	[SerializeField] private Texture dayHills;
	[SerializeField] private Texture daySea;
	[SerializeField] private Texture daySkyRepeat;
	[SerializeField] private Texture dayFair;
	[SerializeField] private Texture dayObstacleWater;
	[SerializeField] private Texture dayObstacleSand;
	[SerializeField] private Texture dayObstacleGreen;
	[SerializeField] private Color dayPoundSparkleColor;
	[SerializeField] private Color daySandSparkleColor;
	[SerializeField] private Texture dayUniverseBorder;
	[SerializeField] private Texture dayRocks;

	//night
	[SerializeField] private Color nightEarthColor;
	[SerializeField] private Color nightFloorColor;
	[SerializeField] private Texture nightHills;
	[SerializeField] private Texture nightSea;
	[SerializeField] private Texture nightSkyRepeat;
	[SerializeField] private Texture nightFair;
	[SerializeField] private Texture nightObstacleWater;
	[SerializeField] private Texture nightObstacleSand;
	[SerializeField] private Texture nightObstacleGreen;
	[SerializeField] private Color nightPoundSparkleColor;
	[SerializeField] private Color nightSandSparkleColor;
	[SerializeField] private Texture nightUniverseBorder;
	[SerializeField] private Texture nightRocks;

	//sunset
	[SerializeField] private Color sunsetEarthColor;
	[SerializeField] private Color sunsetFloorColor;
	[SerializeField] private Texture sunsetHills;
	[SerializeField] private Texture sunsetSea;
	[SerializeField] private Texture sunsetSkyRepeat;
	[SerializeField] private Texture sunsetFair;
	[SerializeField] private Texture sunsetObstacleWater;
	[SerializeField] private Texture sunsetObstacleSand;
	[SerializeField] private Texture sunsetObstacleGreen;
	[SerializeField] private Color sunsetPoundSparkleColor;
	[SerializeField] private Color sunsetSandSparkleColor;
	[SerializeField] private Texture sunsetUniverseBorder;
	[SerializeField] private Texture sunsetRocks;

	//Grey
	[SerializeField] private Color greyEarthColor;
	[SerializeField] private Color greyFloorColor;
	[SerializeField] private Texture greyHills;
	[SerializeField] private Texture greySea;
	[SerializeField] private Texture greySkyRepeat;
	[SerializeField] private Texture greyFair;
	[SerializeField] private Texture greyObstacleWater;
	[SerializeField] private Texture greyObstacleSand;
	[SerializeField] private Texture greyObstacleGreen;
	[SerializeField] private Color greyPoundSparkleColor;
	[SerializeField] private Color greySandSparkleColor;
	[SerializeField] private Texture greyUniverseBorder;
	[SerializeField] private Texture greyRocks;


	[SerializeField] private Material greyScaleMat;
	[SerializeField] private Material spriteDefault;

	private DayTime lastDayTime = DayTime.None;

	public void ChangeDaytime(DayTime dayTime, PlanetManager planet)
	{
		if (dayTime == lastDayTime)
			return;

		if (dayTime != DayTime.Grey)
		{
			SpriteRenderer[] spriteRenderers = planet.GetComponentsInChildren<SpriteRenderer>();
			for (int i = 0; i < spriteRenderers.Length; i++)
			{
				spriteRenderers[i].material = spriteDefault;
			}
			Pinpin.ApplicationManager.datas.dayTime = dayTime;
		}

		SpriteRenderer[] allGrass = planet.decorationParent.GetComponentsInChildren<SpriteRenderer>();
		Color topGrassColor = dayFloorColor;
		Color outerGrassColor = dayEarthColor;

		switch (dayTime)
		{
			case DayTime.Day:
				bgEarthMat.color = dayEarthColor;
				bgfloorMat.color = dayFloorColor;
				hillsMat.mainTexture = dayHills;
				seaMat.mainTexture = daySea;
				montainsMat.mainTexture = daySkyRepeat;
				skyMat.mainTexture = daySkyRepeat;
				cloudsMat.mainTexture = daySkyRepeat;
				fairMat.mainTexture = dayFair;
				obstacleWaterMat.mainTexture = dayObstacleWater;
				obstacleSandMat.mainTexture = dayObstacleSand;
				obstacleGreenMat.mainTexture = dayObstacleGreen;
				poundSparkleMat.color = dayPoundSparkleColor;
				sandSparkleMat.color = daySandSparkleColor;
				universeBorderMat.mainTexture = dayUniverseBorder;
				topGrassColor = dayFloorColor;
				outerGrassColor = dayEarthColor;
				break;

			case DayTime.Night:
				bgEarthMat.color = nightEarthColor;
				bgfloorMat.color = nightFloorColor;
				hillsMat.mainTexture = nightHills;
				seaMat.mainTexture = nightSea;
				montainsMat.mainTexture = nightSkyRepeat;
				skyMat.mainTexture = nightSkyRepeat;
				cloudsMat.mainTexture = nightSkyRepeat;
				fairMat.mainTexture = nightFair;
				obstacleWaterMat.mainTexture = nightObstacleWater;
				obstacleSandMat.mainTexture = nightObstacleSand;
				obstacleGreenMat.mainTexture = nightObstacleGreen;
				poundSparkleMat.color = nightPoundSparkleColor;
				sandSparkleMat.color = nightSandSparkleColor;
				universeBorderMat.mainTexture = nightUniverseBorder;
				topGrassColor = nightFloorColor;
				outerGrassColor = nightEarthColor;
				break;

			case DayTime.SunSet:
				bgEarthMat.color = sunsetEarthColor;
				bgfloorMat.color = sunsetFloorColor;
				hillsMat.mainTexture = sunsetHills;
				seaMat.mainTexture = sunsetSea;
				montainsMat.mainTexture = sunsetSkyRepeat;
				skyMat.mainTexture = sunsetSkyRepeat;
				cloudsMat.mainTexture = sunsetSkyRepeat;
				fairMat.mainTexture = sunsetFair;
				obstacleWaterMat.mainTexture = sunsetObstacleWater;
				obstacleSandMat.mainTexture = sunsetObstacleSand;
				obstacleGreenMat.mainTexture = sunsetObstacleGreen;
				poundSparkleMat.color = sunsetPoundSparkleColor;
				sandSparkleMat.color = sunsetSandSparkleColor;
				universeBorderMat.mainTexture = sunsetUniverseBorder;
				topGrassColor = sunsetFloorColor;
				outerGrassColor = sunsetEarthColor;
				break;

			case DayTime.Grey:
				bgEarthMat.color = greyEarthColor;
				bgfloorMat.color = greyFloorColor;
				hillsMat.mainTexture = greyHills;
				seaMat.mainTexture = greySea;
				montainsMat.mainTexture = greySkyRepeat;
				skyMat.mainTexture = greySkyRepeat;
				cloudsMat.mainTexture = greySkyRepeat;
				fairMat.mainTexture = greyFair;
				obstacleWaterMat.mainTexture = greyObstacleWater;
				obstacleSandMat.mainTexture = greyObstacleSand;
				obstacleGreenMat.mainTexture = greyObstacleGreen;
				poundSparkleMat.color = greyPoundSparkleColor;
				sandSparkleMat.color = greySandSparkleColor;
				universeBorderMat.mainTexture = greyUniverseBorder;
				rocksMat.mainTexture = greyRocks;
				topGrassColor = greyFloorColor;
				outerGrassColor = greyEarthColor;

				SpriteRenderer[] spriteRenderers = planet.GetComponentsInChildren<SpriteRenderer>();
				for (int i = 0; i < spriteRenderers.Length; i++)
				{
					spriteRenderers[i].material = greyScaleMat;
				}
				break;
			default:
				break;
		}

		for (int i = 0; i < allGrass.Length; i++)
		{
			string name = allGrass[i].transform.parent.name;
			switch(name)
			{
				case "InnerGrass(Clone)":
				case "TopGrass1(Clone)":
				case "TopGrass2(Clone)":
					allGrass[i].color = topGrassColor;
					break;
				case "OuterGrass1(Clone)":
				case "OuterGrass2(Clone)":
					allGrass[i].color = outerGrassColor;
					break;
			}
		}
	}


}
