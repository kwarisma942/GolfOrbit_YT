using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MobileJoyPad
{

	[DisallowMultipleComponent, RequireComponent(typeof(CanvasRenderer))]
	public class ShotArea : Graphic, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{

		[System.Serializable] public class MoveEvent : UnityEvent<Vector2> { }
		public UnityEvent onPress = new UnityEvent();
		public UnityEvent onRelease = new UnityEvent();
		public MoveEvent onValueChange = new MoveEvent();
		public MoveEvent onValueChangeScreen = new MoveEvent();


		public Vector2 axis { get; private set; }
		public Vector2 axisScreen { get; private set; }

		private int _touches = 0;
		private Vector2 _startPos;
		private Vector2 _lastPos;
		private Vector2 _startPosScreen;
		private Vector2 _lastPosScreen;


		public bool Moved
		{
			get { return (axis != Vector2.zero); }
		}

		public Vector2 StartPos
		{
			get { return (_startPos); }
		}

		public Vector2 LastPos
		{
			get { return (_lastPos); }
		}

		public Vector2 StartPosScreen
		{
			get { return (_startPosScreen); }
		}

		public Vector2 LastPosScreen
		{
			get { return (_lastPosScreen); }
		}

		public void OnPointerDown(PointerEventData ped)
		{
			_touches++;
			if (_touches > 1)
				return;
			_startPos = ped.position;
			_startPosScreen = new Vector2(ped.position.x / Screen.width, ped.position.y / Screen.height);
			this.onPress.Invoke();
		}

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.onPress.Invoke();
			}
        }

        public void OnDrag (PointerEventData ped)
		{
			_lastPos = ped.position;
			_lastPosScreen = new Vector2(ped.position.x / Screen.width, ped.position.y / Screen.height);
			axis = (_startPos - _lastPos);
			axisScreen = (_startPosScreen - _lastPosScreen);
			this.onValueChange.Invoke(this.axis);
			this.onValueChangeScreen.Invoke(this.axisScreen);
		}

		public void OnPointerUp (PointerEventData ped)
		{
			_touches--;
			if (_touches < 0)
				_touches = 0;
			if (_touches > 0)
				return;

			_lastPos = ped.position;
			_lastPosScreen = new Vector2(ped.position.x / Screen.width, ped.position.y / Screen.height);
			// Reset axis values
			axis = Vector2.zero;
			axisScreen = Vector2.zero;

			this.onRelease.Invoke();
		}

		private void OnApplicationPause (bool pause)
		{
			if (!pause)
				_touches = 0;
		}
	}

}
