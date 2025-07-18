using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {
	private int m_index;
	private int m_diamondTotal = 1;
	private Vector2 m_target;

	public Action AddDiamond;
	// Use this for initialization

	public void Initialize (int index, int totalDiamond, Vector2 target, Action callback)
	{
		m_index = index;
		m_diamondTotal = totalDiamond;
		m_target = target;
		AddDiamond += callback;
		this.transform.rotation = Quaternion.Euler(0f, 0f, (360.0f / m_diamondTotal) * m_index);
	}

	private void OnEnable ()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(this.transform.DOLocalMove(this.transform.right * 1.2f, 0.5f).SetEase(Ease.OutCubic));
		seq.AppendInterval((0.5f / m_diamondTotal) * m_index);
		seq.Append(this.transform.DOMove(m_target, 0.5f).SetEase(Ease.InCubic));
		seq.Insert(0, this.transform.DORotate(Vector3.forward * 700.0f, 1f + (0.5f / m_diamondTotal) * m_index, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic));
		
		seq.OnComplete(Explode);
	}

	void Explode ()
	{
		if(AddDiamond != null)
		{
			AddDiamond.Invoke();
		}
		AddDiamond = null;
		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
