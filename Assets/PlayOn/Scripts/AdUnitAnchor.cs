using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
#endif
using UnityEngine;
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class AdUnitAnchor : MonoBehaviour {
    private RectTransform _transform;
    private Canvas _canvas;
#if UNITY_EDITOR
    private Vector3 oldPosition;
    private Dictionary<PlayOnSDK.Position, Vector3> positions = new Dictionary<PlayOnSDK.Position, Vector3>();
    private float maxPXSize;
    private float minPXSize;
    private PlayOnSDK.Position _location;

    private Vector2 savedSizeDelta = Vector2.zero;
    private Vector2 savedPosition;
    private Vector2 savedWindowSizeDelta = Vector2.zero;

    private bool positionSaved = false;
    private float delayBeforeSave = 1.5f;
    private bool checkSize = false;
    private void Init () {
        if (this.transform.parent != null)
            _canvas = this.transform.parent.GetComponent<Canvas>();
        else
        {
            ShowErrorInConsole();
            return;
        }
        if (_canvas == null)
        {
            ShowErrorInConsole();
            return;
        }
        _canvas.gameObject.SetActive(false);
        SetOptimalDPI();
        _transform = this.GetComponent<RectTransform>();
        _transform.pivot = new Vector2(0.5f, 0.5f);
        _canvas.gameObject.SetActive(true);
        if (!positionSaved)
        {
            savedPosition = _transform.position;
            positionSaved = true;
        }
 
        minPXSize = (((70 + 0.5f) * ((float) PlayOnSDK.GetUnityEditorDPI() / 160f)) / _canvas.scaleFactor);
        maxPXSize = (((120 + 0.5f) * ((float) PlayOnSDK.GetUnityEditorDPI() / 160f)) / _canvas.scaleFactor);
    }

    private void GetCorners () {
        Vector3[] v = new Vector3[4];
        this.transform.parent.GetComponent<RectTransform>().GetLocalCorners(v);
        positions = new Dictionary<PlayOnSDK.Position, Vector3>();
        positions.Add(PlayOnSDK.Position.CenterLeft, new Vector3(v[0].x + _transform.sizeDelta.x / 2, 0f, 0f));
        positions.Add(PlayOnSDK.Position.CenterRight, new Vector3(v[2].x - _transform.sizeDelta.x / 2, 0f, 0f));
        positions.Add(PlayOnSDK.Position.BottomCenter, new Vector3(0f, v[0].y + _transform.sizeDelta.x / 2, 0f));
        positions.Add(PlayOnSDK.Position.BottomLeft,
            new Vector3(v[0].x + _transform.sizeDelta.x / 2, v[0].y + _transform.sizeDelta.x / 2, 0f));
        positions.Add(PlayOnSDK.Position.BottomRight,
            new Vector3(v[2].x - _transform.sizeDelta.x / 2, v[0].y + _transform.sizeDelta.x / 2, 0f));
        positions.Add(PlayOnSDK.Position.TopCenter, new Vector3(0f, v[1].y - _transform.sizeDelta.x / 2, 0f));
        positions.Add(PlayOnSDK.Position.TopLeft,
            new Vector3(v[0].x + _transform.sizeDelta.x / 2, v[1].y - _transform.sizeDelta.x / 2, 0f));
        positions.Add(PlayOnSDK.Position.TopRight,
            new Vector3(v[2].x - _transform.sizeDelta.x / 2, v[1].y - _transform.sizeDelta.x / 2, 0f));
        positions.Add(PlayOnSDK.Position.Centered, Vector3.zero);
    }
    

    private void Update () {
        Init();
        if (checkSize == false)
        {
            WindowSizeChanged();
        }

        if (ObjectResized())
            _transform.position = savedPosition;
        if (!ObjectChanged())
            return;
        FixSize();
        StayInLines();
    }

    private void ShowErrorInConsole () {
        Debug.LogError("Put PlayonAdAnchor in Canvas");
    }

    private PlayOnSDK.Position GetClosest (Vector3 startPosition, Dictionary<PlayOnSDK.Position, Vector3> pickups) {
        PlayOnSDK.Position location = PlayOnSDK.Position.Centered;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (var potentialTarget in pickups)
        {
            Vector3 directionToTarget = potentialTarget.Value - startPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                location = potentialTarget.Key;
            }
        }

        return location;
    }

    private void StayInLines () {
        GetCorners();
        var location = GetClosest(_transform.localPosition, positions);
        var loc = positions[location];
        var xoffset = _transform.localPosition.x - loc.x;
        var yoffset = _transform.localPosition.y - loc.y;
        switch (location)
        {
            case PlayOnSDK.Position.Centered:
                break;
            case PlayOnSDK.Position.CenterRight:
                if (xoffset > 0)
                {
                    xoffset = 0;
                    this.transform.localPosition = new Vector3(positions[PlayOnSDK.Position.CenterRight].x, this.transform.localPosition.y, 0);
                }

                break;
            case PlayOnSDK.Position.CenterLeft:
                if (xoffset < 0)
                {
                    xoffset = 0;
                    this.transform.localPosition = new Vector3(positions[PlayOnSDK.Position.CenterLeft].x, this.transform.localPosition.y, 0);
                }

                break;
            case PlayOnSDK.Position.TopCenter:
                if (yoffset > 0)
                {
                    yoffset = 0;
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, positions[PlayOnSDK.Position.TopCenter].y, 0);
                }

                break;
            case PlayOnSDK.Position.TopRight:
                if (yoffset > 0)
                {
                    yoffset = 0;
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, positions[PlayOnSDK.Position.TopCenter].y, 0);
                }

                if (xoffset > 0)
                {
                    xoffset = 0;
                    this.transform.localPosition = new Vector3(positions[PlayOnSDK.Position.TopRight].x, this.transform.localPosition.y, 0);
                }

                break;
            case PlayOnSDK.Position.TopLeft:
                if (yoffset > 0)
                {
                    yoffset = 0;
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, positions[PlayOnSDK.Position.TopCenter].y, 0);
                }

                if (xoffset < 0)
                {
                    xoffset = 0;
                    this.transform.localPosition = new Vector3(positions[PlayOnSDK.Position.TopLeft].x, this.transform.localPosition.y, 0);
                }

                break;
            case PlayOnSDK.Position.BottomCenter:
                if (yoffset < 0)
                {
                    yoffset = 0;
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, positions[PlayOnSDK.Position.BottomCenter].y, 0);
                }

                break;
            case PlayOnSDK.Position.BottomRight:
                if (yoffset < 0)
                {
                    yoffset = 0;
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, positions[PlayOnSDK.Position.BottomRight].y, 0);
                }

                if (xoffset > 0)
                {
                    xoffset = 0;
                    this.transform.localPosition = new Vector3(positions[PlayOnSDK.Position.BottomRight].x, this.transform.localPosition.y, 0);
                }

                break;
            case PlayOnSDK.Position.BottomLeft:
                if (yoffset < 0)
                {
                    yoffset = 0;
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x, positions[PlayOnSDK.Position.BottomLeft].y, 0);
                }

                if (xoffset < 0)
                {
                    xoffset = 0;
                    this.transform.localPosition = new Vector3(positions[PlayOnSDK.Position.BottomLeft].x, this.transform.localPosition.y, 0);
                }

                break;
        }

        _transform.SetAsLastSibling();
    }

    private void SetOptimalDPI () {
        PlayOnSDK.SetUnityEditorDPI(96);
        if (_canvas.pixelRect.width >= 1440)
        {
            PlayOnSDK.SetUnityEditorDPI(440);
        }
        else if (_canvas.pixelRect.width >= 1080)
        {
            PlayOnSDK.SetUnityEditorDPI(323);
        }
        else if (_canvas.pixelRect.width >= 720)
        {
            PlayOnSDK.SetUnityEditorDPI(252);
        }
        else if (_canvas.pixelRect.width >= 480)
        {
            PlayOnSDK.SetUnityEditorDPI(170);
        }
    }


    private bool ObjectChanged () {
        if (ObjectMoved())
            return true;
        else if (ObjectResized())
            return true;
        else
            return false;
    }

    private bool ObjectMoved () {
        if (_transform == null)
            return true;
        if (_transform.position.x != savedPosition.x || _transform.position.y != savedPosition.y)
        {
            savedPosition = _transform.position;
            return true;
        }

        return false;
    }

    private bool ObjectResized () {
        if (_transform == null)
            return true;
        if (_transform.sizeDelta.x != savedSizeDelta.x || _transform.sizeDelta.y != savedSizeDelta.y)
        {
            return true;
        }

        return false;
    }
    
    private void WindowSizeChanged () {
        if (_canvas.pixelRect.width != savedWindowSizeDelta.x || _canvas.pixelRect.height != savedWindowSizeDelta.y)
        {
            PlayOnEditorCoroutinesManager.StartEditorCoroutine(SaveNewValue());
            checkSize = true;
        }
        else
        {
            checkSize = false;
        }
    }

    public IEnumerator SaveNewValue () {
        yield return new WaitForSeconds(delayBeforeSave);
        if (_canvas.pixelRect.width != savedWindowSizeDelta.x || _canvas.pixelRect.height != savedWindowSizeDelta.y)
        {
            savedWindowSizeDelta = new Vector2(_canvas.pixelRect.width, _canvas.pixelRect.height);
            FixSize();
        }

        checkSize = false;
        yield return null;
    }

    private void FixSize () {
        _transform.localScale = Vector3.one;
        if (_transform.sizeDelta.x > maxPXSize)
        {
            _transform.sizeDelta = new Vector2(maxPXSize, maxPXSize);
        }

        if (_transform.sizeDelta.x < minPXSize)
        {
            _transform.sizeDelta = new Vector2(minPXSize, minPXSize);
        }

        _transform.sizeDelta = new Vector2(_transform.sizeDelta.x, _transform.sizeDelta.x);
        savedSizeDelta = _transform.sizeDelta;
    }
#endif

    public AdUnitAnchorData GetDPPositionData () {
        AdUnitAnchorData data;
        _transform = this.GetComponent<RectTransform>();
        if (this.transform.parent != null)
            _canvas = this.transform.parent.GetComponent<Canvas>();
        if (_canvas == null)
        {
            data.size = 70;
            data.xOffset = 50;
            data.yOffset = 50;
            data.location = PlayOnSDK.Position.TopLeft;
            return data;
        }
        _transform.pivot = new Vector2(0.5f, 0.5f);
        Rect rr = RectTransformExtension.GetScreenRect(_transform, _canvas);
#if UNITY_IOS
        var x = ((rr.xMin - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f));
        var y = 0.1f;
	    if(Screen.safeArea.height == Screen.height){
            y = (Screen.height - rr.yMax - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f);
	    }
	    else{
            y = ((Screen.height - rr.yMax - Screen.safeArea.y )- 0.5f  ) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f);	
	    }
        var s = (_transform.sizeDelta.x * _canvas.scaleFactor - 0.5f) / Mathf.Round(PlayOnSDK.GetUnityEditorDPI() / 160f);
#else
        var x = ((rr.xMin - 0.5f) / (PlayOnSDK.GetUnityEditorDPI() / 160f));
        var y = ((( Screen.height - rr.yMax) - 0.5f) / (PlayOnSDK.GetUnityEditorDPI() / 160f));
        var s = (_transform.sizeDelta.x * _canvas.scaleFactor - 0.5f) / (PlayOnSDK.GetUnityEditorDPI() / 160f);
#endif
        data.size = (int) s;
        data.xOffset = (int) x;
        data.yOffset = (int) y;
        data.location = PlayOnSDK.Position.BottomLeft;
        return data;
    }

    private void Start () {
        if (Application.isPlaying)
            this.gameObject.SetActive(false);
    }
}

public struct AdUnitAnchorData {
    public int xOffset;
    public int yOffset;
    public int size;
    public PlayOnSDK.Position location;
}