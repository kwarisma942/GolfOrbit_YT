using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AnchorOffset : MonoBehaviour
{
    private RectTransform uiElement;

    [SerializeField] private float portraitTopOffset;  // Adjustable top offset for portrait mode
    [SerializeField] private bool isOffsetX, isOffsetY;

    private void Start()
    {
        uiElement = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Check if the screen is in portrait mode
        if (Screen.height > Screen.width)
        {
            if (isOffsetX)
                uiElement.offsetMin = new Vector2(0, uiElement.offsetMin.y);

            if (isOffsetY)
                uiElement.offsetMin = new Vector2(uiElement.offsetMin.x, 0);

            // Set the Top offset based on the portraitTopOffset value
            uiElement.offsetMax = new Vector2(uiElement.offsetMax.x, portraitTopOffset);

            // Perform tasks for portrait mode
            //Debug.Log("Portrait Mode");

            // Add your portrait-specific task here
        }
        // Check if the screen is in landscape mode
        else if (Screen.width > Screen.height)
        {
            if (isOffsetX)
                uiElement.offsetMin = new Vector2(0, uiElement.offsetMin.y);

            if (isOffsetY)
                uiElement.offsetMin = new Vector2(uiElement.offsetMin.x, 0);

            // Set the Top value to 0 in landscape mode
            uiElement.offsetMax = new Vector2(uiElement.offsetMax.x, 0);

            // Perform tasks for landscape mode
           // Debug.Log("Landscape Mode");

            // Add your landscape-specific task here
        }
    }
}
