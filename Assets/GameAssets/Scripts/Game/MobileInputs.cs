using UnityEngine;
using UnityEngine.Events;

using MobileJoyPad;

public class MobileInputs : MonoBehaviour
{
	[SerializeField] private ShotArea _shotArea;

	public bool IsMoving
	{
		get { return (_shotArea.Moved); }
	}

	public Vector2 Axis
	{
		get { return (_shotArea.axis); }
	}

	public Vector2 AxisScreen
	{
		get { return (_shotArea.axisScreen); }
	}

	public Vector2 StartPos
	{
		get { return (_shotArea.StartPos); }
	}

	public Vector2 LastPos
	{
		get { return (_shotArea.LastPos); }
	}

	public Vector2 StartPosScreen
	{
		get { return (_shotArea.StartPosScreen); }
	}

	public Vector2 LastPosScreen
	{
		get { return (_shotArea.LastPosScreen); }
	}

}
