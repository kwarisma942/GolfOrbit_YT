using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class UIImageFitter : UIBehaviour, ILayoutSelfController
{
	#region Type declarations
	public enum ERatioMode
	{
		WidthControlsHeight,
		HeightControlsWidth
	}

	public enum ETargetType
	{
		Sprite,
		Texture,
		Custom
	}
	#endregion

	#region Private fields
	private DrivenRectTransformTracker __Tracker;

	private ERatioMode __RatioMode;
	#endregion

	#region Properties
	[SerializeField]
	private ETargetType __TargetType = ETargetType.Sprite;
	public ETargetType TargetType
	{
		get { return __TargetType; }
		set
		{
			ETargetType last;

			last = __TargetType;
			__TargetType = value;

			if (__TargetType != last)
				SetDirty();
		}
	}

	[SerializeField]
	private Vector2 __CustomSize = new Vector2(100, 100);
	public Vector2 CustomSize
	{
		get { return __CustomSize; }
		set { __CustomSize = value; }
	}
	#endregion

	#region Getters
	[NonSerialized]
	private RectTransform __CurrentTransform;
	public RectTransform CurrentTransform
	{
		get
		{
			if (__CurrentTransform == null)
				__CurrentTransform = GetComponent<RectTransform>();
			return __CurrentTransform;
		}
	}

	private Image __CurrentImage;
	public Image CurrentImage
	{
		get
		{
			if (__CurrentImage == null)
				__CurrentImage = GetComponent<Image>();
			return __CurrentImage;
		}
	}

	public Texture TargetTexture
	{
		get
		{
			if (__CurrentImage == null)
				return null;

			return __CurrentImage.mainTexture;
		}
	}

	public Sprite TargetSprite
	{
		get
		{
			if (CurrentImage == null)
				return null;

			return __CurrentImage.sprite;
		}
	}

	public float TargetRatio
	{
		get
		{
			switch (TargetType)
			{
				case ETargetType.Texture:
				{
					if (TargetTexture == null || TargetTexture.width * TargetTexture.height <= 0)
						return -1;
					return (float)TargetTexture.width / (float)TargetTexture.height;
				}
				case ETargetType.Sprite:
				{
					if (TargetSprite == null || TargetSprite.rect.width * TargetSprite.rect.height <= 0)
						return -1;
					return (float)TargetSprite.rect.width / (float)TargetSprite.rect.height;
				}
				case ETargetType.Custom:
				{
					if (CustomSize.x * CustomSize.y <= 0)
						return -1;
					return CustomSize.x / CustomSize.y;
				}
			}
			return -1;
		}
	}
	#endregion


	protected UIImageFitter() { }

	protected override void OnEnable()
	{
		base.OnEnable();
		SetDirty();
	}

	protected override void OnDisable()
	{
		//__Tracker.Clear();
		LayoutRebuilder.MarkLayoutForRebuild(CurrentTransform);

		base.OnDisable();
	}

	protected void SetDirty()
	{
		if (!IsActive())
			return;
		FitToTarget();
	}


#if UNITY_EDITOR
	protected override void OnValidate()
	{
		SetDirty();
	}
#endif

	protected override void OnRectTransformDimensionsChange()
	{
		FitToTarget();
	}

	public void FitToTarget()
	{
		if (!IsActive())
			return;


		Vector2 anchorSize;
		Vector2 targetSize = Vector2.zero;
		Vector2 currentSize;
		float anchorRatio;
		float currentRatio;

		if (!GetAnchorsSize(out anchorSize) || anchorSize.sqrMagnitude < 0 || TargetRatio <= 0)
			return;

		currentSize = CurrentTransform.rect.size;
		anchorRatio = anchorSize.x / (anchorSize.y > 0.0f ? anchorSize.y : 1.0f);
		currentRatio = (float)currentSize.x / (float)(currentSize.y > 0 ? currentSize.y : 1.0f);
		UpdateRatioMode(anchorSize, anchorRatio);

		if (Mathf.Approximately(currentSize[(int)__RatioMode], anchorSize[(int)__RatioMode]) &&
			Mathf.Approximately(currentRatio, TargetRatio))
			return;

		//__Tracker.Clear();

		switch (__RatioMode)
		{
			case ERatioMode.HeightControlsWidth:
			{
				targetSize.x = GetAnchoredDeltaSize(anchorSize.y * TargetRatio, anchorSize, 0);
				//__Tracker.Add(this, CurrentTransform, DrivenTransformProperties.SizeDelta);
				CurrentTransform.sizeDelta = targetSize;
				break;
			}
			case ERatioMode.WidthControlsHeight:
			{
				targetSize.y = GetAnchoredDeltaSize(anchorSize.x / TargetRatio, anchorSize, 1);
				//__Tracker.Add(this, CurrentTransform, DrivenTransformProperties.SizeDelta);
				CurrentTransform.sizeDelta = targetSize;
				break;
			}

		}
	}

	private void UpdateRatioMode(Vector2 anchorSize, float anchorRatio)
	{
		if (anchorSize.x == 0)
			__RatioMode = ERatioMode.HeightControlsWidth;
		else if (anchorSize.y == 0)
			__RatioMode = ERatioMode.WidthControlsHeight;
		else if (anchorRatio > TargetRatio)
			__RatioMode = ERatioMode.HeightControlsWidth;
		else if (anchorRatio < TargetRatio)
			__RatioMode = ERatioMode.WidthControlsHeight;
	}

	private float GetAnchoredDeltaSize(float size, Vector2 maxSize, int axis)
	{
		return size - maxSize[axis];
	}

	private bool GetAnchorsSize(out Vector2 anchorSize)
	{
		RectTransform parentTransform;
		Rect parentRect;

		anchorSize = -Vector2.one;
		parentTransform = transform.parent as RectTransform;
		if (parentTransform == null)
			return false;

		parentRect = parentTransform.rect;
		anchorSize = CurrentTransform.anchorMax - CurrentTransform.anchorMin;
		anchorSize.Scale(parentRect.size);

		if (anchorSize.x * anchorSize.y < 0)
			return false;

		return true;
	}

	public void SetLayoutHorizontal() { }

	public void SetLayoutVertical() { }
}

