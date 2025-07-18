using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoThumbMovement : MonoBehaviour {
	public bool isThumbDown;

	public void OnThumbUp()
	{
		isThumbDown = false;
	}

	public void OnThumbDown()
	{
		isThumbDown = true;
	}
}
