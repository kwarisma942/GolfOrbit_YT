using UnityEngine;
using YTGameSDK;

public class AudioFocusWebGL : MonoBehaviour
{
    private YTGameWrapper ytGameWrapper;
    private void Start()
    {
        ytGameWrapper = GetComponent<YTGameWrapper>();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (ytGameWrapper.IsYTGameAudioEnabled())
        {
            // If YT game audio is enabled, adjust the volume based on tab focus
            AudioListener.volume = focus ? 1f : 0f;
            Debug.Log("Tab Focus: " + (focus ? "Focused" : "Not Focused"));
        }

    }

}
