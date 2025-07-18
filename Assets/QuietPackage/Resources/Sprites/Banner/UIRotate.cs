using UnityEngine;
using System.Collections;

public class UIRotate : MonoBehaviour
{
	Vector3 angle;
    public float RotateMultiplier = 30;

	void Start()
	{
		angle = transform.eulerAngles;
	}

	void Update()
	{
		angle.z += Time.deltaTime * RotateMultiplier;
		transform.eulerAngles = angle;
	}

}
