using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
	[SerializeField] private MobileInputs m_mobileInputs;
	private static InputManager singleton = null;

	private bool m_active = true;
	private Vector2 m_lerpedPos;
	[SerializeField] private Canvas m_canvas;
	[SerializeField] private bool m_invertShootDirection = false;
	[SerializeField] private float m_lerpSpeed = 1f;

	private void Awake ()
	{
		if (singleton == null)
		{
			singleton = this;
		}
		else
			Destroy(this);
	}

	public static void SetActive (bool isActive)
	{
		singleton.m_active = isActive;
		singleton.m_mobileInputs.gameObject.SetActive(isActive);
	}

	public static bool InvertShootDirection
	{
		get { return (singleton.m_invertShootDirection); }
		set
		{
			singleton.m_invertShootDirection = value;
		}
	}

	public static bool IsMoving
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.IsMoving);
			return (false);
		}
	}

	public static Vector2 Axis
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.Axis * singleton.m_canvas.transform.localScale.x);
			return (Vector2.zero);
		}
	}

	public static Vector2 AxisRaw
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.Axis);
			return (Vector2.zero);
		}
	}

	public static Vector2 AxisScreen
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.AxisScreen);
			return (Vector2.zero);
		}
	}

	public static Vector2 StartPos
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.StartPos / singleton.m_canvas.transform.localScale.x);
			return (Vector2.zero);
		}
	}

	public static Vector2 StartPosRaw
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.StartPos);
			return (Vector2.zero);
		}
	}

	public static Vector2 StartPosScreen
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.StartPosScreen);
			return (Vector2.zero);
		}
	}

	public static Vector2 LastPos
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.LastPos / singleton.m_canvas.transform.localScale.x);
			return (Vector2.zero);
		}
	}

	public static Vector2 LastPosRaw
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.LastPos);
			return (Vector2.zero);
		}
	}

	public static Vector2 LastPosScreen
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_mobileInputs.LastPosScreen);
			return (Vector2.zero);
		}
	}

	public static Vector2 LerpedPos
	{
		get
		{
			if (singleton.m_active)
				return (singleton.m_lerpedPos);
			return (Vector2.zero);
		}
	}

	void Update ()
	{
		if (IsMoving)
		{
			// Update Visualization position (needed for aiming trajectory prediction);
			Vector2 lastPosition = LastPos;
			//finding lerp value

			m_lerpedPos = Vector3.Lerp(m_lerpedPos, lastPosition, Time.deltaTime * m_lerpSpeed);
		}
	}

}