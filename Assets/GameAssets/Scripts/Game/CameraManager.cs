using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private Camera m_camera;
	[SerializeField] private Ball m_ball;
	[SerializeField] private float m_ballStartHeight;
	[SerializeField] private Transform m_cameraPivot;

	private bool canZoom = true;
	[Header("Zoom")]
	[SerializeField, Range(0.01f, 1f)] private float m_zoomSpeed = 0.9f;
	private float m_baseZoomSpeed;
	[SerializeField] private float m_minOrthographicSize = 5;
	[SerializeField] private float m_maxOrthographicSize = 15;
	[SerializeField] private float m_minHeightForDezoom = 5;
	[SerializeField] private float m_ortographicSizePerHeight;

	private bool canMove = true;
	private bool m_followBall = true;
	[Header("Position")]
	[SerializeField, Range(0.01f, 1f)] private float m_translationSpeed;
	private float m_baseTranslationSpeed;
	[SerializeField, Range(0.01f, 1f)] private float m_rotationSpeed;
					 private float m_cameraHeightStart = 500.0f;
	[SerializeField] private float m_cameraHeightStartLandScape = 500.0f;
	[SerializeField] private float m_cameraHeightStartPortrait = 500.0f;
	[SerializeField] private float m_cameraHeightGame = 498.0f;

	private float m_targetPerfectZoomDelta = 0.0f;
	private float m_targetPerfectYDelta = 0.0f;
	private float m_cameraHeight;
	public float targetOrthographicSize { get; private set; }
	private Vector3 m_targetPosition;
	public float targetAngle { get; private set; }
	public float currentAngle { get; private set; }

	private void Awake ()
	{
		m_camera = Camera.main;
		m_baseTranslationSpeed = m_translationSpeed;
		m_baseZoomSpeed = m_zoomSpeed;
	}

	private void Start ()
	{
		ComputeCameraZoom();

		m_camera.orthographicSize = targetOrthographicSize;
        AdjustTheOrientation();
        //m_cameraHeight = m_cameraHeightStart;
        m_ballStartHeight = m_ball.height;
	}

	public void StartGame ()
	{
        AdjustTheOrientation();
        //m_cameraHeight = m_cameraHeightGame;
		m_followBall = true;
	}


	// Update is called once per frame
	void FixedUpdate ()
	{
		if (canMove)
		{
			if (m_followBall)
			{
				ComputeCameraPosition();
			}
			currentAngle =  Mathf.Lerp(currentAngle, targetAngle, m_rotationSpeed);
			m_cameraPivot.rotation = Quaternion.Euler(0f,0f, currentAngle);
			m_camera.transform.localPosition = Vector3.Lerp(m_camera.transform.localPosition, m_targetPosition, m_translationSpeed);
		}

		if (canZoom)
		{
			if (m_followBall)
			{
				ComputeCameraZoom();
			}
			m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, targetOrthographicSize, m_zoomSpeed);
		}
	}

	private void Update()
	{

		// Check the device's screen orientation
		AdjustTheOrientation();
        m_cameraHeight = m_cameraHeightStart;

    }

	void AdjustTheOrientation() 
	{
        if (Screen.width > Screen.height) // Landscape
        {
            m_minOrthographicSize = 4;
			m_cameraHeightStart = m_cameraHeightStartLandScape;

        }
        else // Portrait
        {
            m_minOrthographicSize = 7;
			m_cameraHeightStart = m_cameraHeightStartPortrait;	
        }
    }

    public void SetTargetAngle (float angle)
	{
		if (m_followBall)
		{
			targetAngle = angle;
		}
	}

	private void ComputeCameraZoom ()
	{
		//Choose the maximum zoom value with velocity or height
		this.targetOrthographicSize = Mathf.Max((m_ball.height - m_ballStartHeight) * 1.1f - m_minHeightForDezoom, 0.0f) * m_ortographicSizePerHeight + m_minOrthographicSize;
		this.targetOrthographicSize = Mathf.Clamp(this.targetOrthographicSize, m_minOrthographicSize, m_maxOrthographicSize);
		this.targetOrthographicSize += m_targetPerfectZoomDelta;
	}

	public void SetPerfectZoomDelta ( float delta, float duration)
	{
		DOTween.To(SetZoomDelta, 0, delta, duration).SetEase(Ease.OutQuad);
	}

	private void SetZoomDelta(float value )
	{
		m_targetPerfectZoomDelta = value;
	}

	private void ComputeCameraPosition ()
	{
		float targetHeight = m_cameraHeight + Mathf.Max(m_ball.height, m_ballStartHeight) / 2f;
		this.m_targetPosition = new Vector3((targetHeight - m_cameraHeight - 1.7f), targetHeight + m_targetPerfectYDelta, m_camera.transform.localPosition.z);
	}

	public void SetPerfectYDelta ( float delta, float duration )
	{
		float currentY = m_targetPerfectYDelta;
		DOTween.To(SetYDelta, currentY, delta, duration).SetEase(Ease.OutQuad);
	}

	private void SetYDelta ( float value )
	{
		m_targetPerfectYDelta = value;
	}

	public void ResetGame ()
	{
		m_followBall = false;
		m_zoomSpeed = m_baseZoomSpeed;
		m_translationSpeed = m_baseTranslationSpeed;
		m_cameraHeight = m_cameraHeightStart;
		targetOrthographicSize = m_minOrthographicSize;
		targetAngle = 0f;
		m_targetPerfectZoomDelta = 0.0f;
		m_targetPerfectYDelta = 0.0f;
	}

	public void SetStartView ()
	{
		StartCoroutine(WaitFollowBall());
	}

	IEnumerator WaitFollowBall ()
	{
		yield return new WaitForSeconds(0.2f);

		m_followBall = true;
	}

	public void SetGreenView (Vector2 position, float angle, float width)
	{
		m_zoomSpeed *= 0.3f;
		m_translationSpeed *= 0.1f;
		m_followBall = false;
		m_targetPosition = new Vector3(0f, m_cameraHeight + 2.0f, m_camera.transform.localPosition.z);;
		targetAngle = angle;
		targetOrthographicSize = (width / 2.0f) * ((float)m_camera.scaledPixelHeight / (float)m_camera.scaledPixelWidth);
		targetOrthographicSize += targetOrthographicSize * 0.1f;
	}

	public void StopFollowingBall ()
	{
		canMove = false;
		canZoom = false;
	}

	public void StartFollowingBall ()
	{
		canMove = true;
		canZoom = true;
	}

}
