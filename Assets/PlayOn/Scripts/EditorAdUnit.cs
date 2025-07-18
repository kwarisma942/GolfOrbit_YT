using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EditorAdUnit : MonoBehaviour {
    private Text timer;
    public int playLength = 8;
    public int getRevardTime = 0;
    public RectTransform rect;
    public Canvas canvas;
    private int _xOffset;
    private int _yOffset;
    private int _size;
    private PlayOnSDK.Position _location;
    private AdUnit _adUnit;

    public void Init (AdUnit adUnit, PlayOnSDK.Position location, int xOffset, int yOffset, int size) {
        _location = location;
        _adUnit = adUnit;
        _xOffset = xOffset;
        _yOffset = yOffset;
        _size = size;
        timer = this.GetComponentInChildren<Text>();
        StartCoroutine(Timer());
        SetPosition();
    }

    private IEnumerator Timer () {
        int time = playLength;
        while (time > 0)
        {
            timer.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
            if (time == getRevardTime)
            {
                _adUnit.AdCallbacks.OnReward.Invoke(0f);
                Debug.Log("<color=green>OnReward fire </color>");
            }
        }
        _adUnit.CloseAd();
        Destroy(this.gameObject);
        yield return null;
    }
    
    private void SetPosition ()
    {
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2((_size + 0.5f) * (PlayOnSDK.GetUnityEditorDPI() / 160f) / canvas.scaleFactor, (_size + 0.5f) * (PlayOnSDK.GetUnityEditorDPI() / 160f) / canvas.scaleFactor);
        var xPos = (_xOffset * ((float) PlayOnSDK.GetUnityEditorDPI() / 160f)) + 0.5f;
        var yPos = (_yOffset * ((float) PlayOnSDK.GetUnityEditorDPI() / 160f) + 0.5f);
        switch (_location)
        {
            case PlayOnSDK.Position.Centered:
                rect.pivot = new Vector2(0.5f, 0.5f);
                yPos = canvas.pixelRect.height / 2 - yPos;
                xPos = canvas.pixelRect.width / 2 - xPos;
                break;
            case PlayOnSDK.Position.BottomLeft:
                rect.pivot = Vector2.zero;
                break;
            case PlayOnSDK.Position.BottomRight:
                rect.pivot = new Vector2(1f, 0f);
                xPos = canvas.pixelRect.width - xPos;
                break;
            case PlayOnSDK.Position.TopLeft:
                rect.pivot = new Vector2(0f, 1f);
                yPos = canvas.pixelRect.height - yPos;
                break;
            case PlayOnSDK.Position.TopRight:
                rect.pivot = new Vector2(1f, 1f);
                yPos = canvas.pixelRect.height - yPos;
                xPos = canvas.pixelRect.width - xPos;
                break;
            case PlayOnSDK.Position.CenterLeft:
                rect.pivot = new Vector2(1f, 0.5f);
                yPos = canvas.pixelRect.height / 2 - yPos;
                xPos = canvas.pixelRect.width - xPos;
                break;
            case PlayOnSDK.Position.CenterRight:
                rect.pivot = new Vector2(1f, 0.5f);
                yPos = canvas.pixelRect.height / 2 - yPos;
                xPos = canvas.pixelRect.width - xPos;
                break;
            case PlayOnSDK.Position.BottomCenter:
                rect.pivot = new Vector2(0.5f, 0f);
                xPos = canvas.pixelRect.width / 2 - xPos;
                break;
            case PlayOnSDK.Position.TopCenter:
                rect.pivot = new Vector2(0.5f, 1f);
                xPos = canvas.pixelRect.width / 2 - xPos;
                yPos = canvas.pixelRect.height - yPos;
                break;
        }
        rect.position = new Vector3(xPos, yPos, 0);
    }
}