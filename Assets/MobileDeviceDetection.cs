using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileDeviceDetection : MonoBehaviour
{
    [SerializeField] private List<GameObject> ChildList = new List<GameObject>();
    private void OnEnable()
    {
        if (!WebGLDeviceHelper.IsMobile())
        {
            for (int i = 0; i < ChildList.Count; i++)
            {
                ChildList[i].SetActive(true);
            }
        }
    }
}
