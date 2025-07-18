using System.Collections;
using System.Collections.Generic;
using Pinpin;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValue : MonoBehaviour
{

    public static UpdateValue Instance;

    public Text Coins, Diamand;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateText()
    {
        Coins.text = ApplicationManager.datas.coins.ToString();
        Diamand.text = ApplicationManager.datas.diamonds.ToString();
    }
}
