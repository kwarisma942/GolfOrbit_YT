using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimIntro : MonoBehaviour {

	public void OnanimationEnd ()
	{
		Destroy(this.gameObject);
	}
}
