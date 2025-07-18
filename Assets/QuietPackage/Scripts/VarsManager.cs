using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class VarsManager : MonoBehaviour
{
    public TextMeshProUGUI txt1, txt2, txt3, txt4, txt5;
    public Image img1, img2, img3;
    public Button btn1, btn2, btn3; 
    public string txt, str2, str3,str4;
    public int nb;
    public GameObject obj1, obj2, obj3, obj4, content;
    public Scrollbar sb1;
    public List<TMP_Dropdown> dropdowns = new List<TMP_Dropdown>();
    public List<TMP_InputField> fields = new List<TMP_InputField>();
    public List<string> strList = new List<string>();
    public Slider slider;
    public bool boolean = false, boolean2 = false;
    public Animation anim1, anim2;

    public void Close_QuietObject_Canvas()
    {
        this.gameObject.SetActive(false);
        QP_QuietObject.instance.ShowCanvas(false);
    }

}
 