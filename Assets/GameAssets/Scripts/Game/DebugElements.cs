using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugElements : MonoBehaviour {

	[SerializeField] private GameObject[] m_debugObjectsToDeactivate;
	[SerializeField] private GameObject[] m_captureObjectsToDeactivate;


	// Use this for initialization
	void Start () { 
		for (int i = 0; i < m_captureObjectsToDeactivate.Length; i++)
		{
#if CAPTURE
			m_captureObjectsToDeactivate[i].SetActive(true);
#else
			m_captureObjectsToDeactivate[i].SetActive(false);
#endif
		}

		for (int i = 0; i<m_debugObjectsToDeactivate.Length; i++)
		{
#if DEBUG
			m_debugObjectsToDeactivate[i].SetActive (true);
#else
			m_debugObjectsToDeactivate[i].SetActive(false);
#endif
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
