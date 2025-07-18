using Pinpin;
using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		string version = "v" + Application.version;
#if TEST_PINPIN
		version += ", pinpin";
#else
		version += ", gpp";
#endif

#if GPP_TEST_AD
		version += " test ads";
#endif

#if CAPTURE
		version += " capture";
#endif

#if !GPP_TEST_AD && !CAPTURE
		version += " prod";
#endif
		version += ". j:" + ApplicationManager.config.application.jsonID;
		GetComponent<Text>().text = version;
	}

}
