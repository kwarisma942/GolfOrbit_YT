using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinpin;
using System.IO;

[ExecuteInEditMode]
public class ScreenshotTaker : MonoBehaviour
{
    int screenshot = 0;
	public bool overrideExisting = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
			TakeScreen();
			Debug.Break();
        }
    }

    public void TakeScreen()
    {
		ScreenCapture.CaptureScreenshot("screenshot_" + screenshot++ + ".png");
	}
}


