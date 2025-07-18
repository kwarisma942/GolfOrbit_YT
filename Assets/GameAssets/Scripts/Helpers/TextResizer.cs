using UnityEngine;
using UnityEngine.UI;


//This is used to resize the font depending of the ratio, in order to avoid to use best fit on texts
[System.Serializable]
public class TextResizer
{
	public Text[] texts;
	public int bigSize;
	public int smallSize;

	public void Resize ()
	{
		float ratio = (float)Screen.width / Screen.height;
		for (int i = 0; i < texts.Length; i++)
		{
			texts[i].fontSize = ratio < 1.5f ? smallSize : bigSize;
		}
	}
}