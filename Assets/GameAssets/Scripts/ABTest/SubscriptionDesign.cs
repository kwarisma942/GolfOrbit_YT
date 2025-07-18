using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscriptionDesign : MonoBehaviour
{
    public GameObject PanelA, PanelB;


    public void OnEnable()
    {
        if (ExampleRemoteConfigABtests._instance.UseYearlyMainButton)
        {
            PanelA.SetActive(false);
            PanelB.SetActive(true);
        }
        else
        {
            PanelA.SetActive(true);
            PanelB.SetActive(false);
        }
    }
}
