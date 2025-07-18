using System.Collections;
using System.Collections.Generic;
using Pinpin.Scene.MainScene.UI;
using UnityEngine;

public class UpdateStore : MonoBehaviour
{
    public static UpdateStore Instance;
    public ShopPanel ShopPanel;

    void Awake()
    {
        Instance = this;
    }

    
}
