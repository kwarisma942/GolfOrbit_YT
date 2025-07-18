using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordAnimator : MonoBehaviour
{
	[SerializeField] private Text m_letterPrefab;
	[SerializeField] private int fontSize;

	private Sequence m_sequence;

	private void Awake ()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	public void AnimateWordsSequence ( string[] words, Color wordColor, float timeBetweenLetters, float tweenDuration, out Sequence anim)
	{
		anim = DOTween.Sequence();
		float time = 0.0f;
		Text[] newText = new Text[words.Length];
		for (int i = 0; i < words.Length; i++)
		{
			newText[i] = Instantiate(m_letterPrefab, this.transform);
			newText[i].text = words[i];
			newText[i].rectTransform.localScale = Vector3.right + Vector3.forward;
			newText[i].color = wordColor;
			anim.Insert(time, newText[i].rectTransform.DOScaleY(1.0f, tweenDuration).SetEase(Ease.OutElastic));
			time += timeBetweenLetters;
		}
	}

	public void AnimateWords ( string[] words, Gradient color, float timeBetweenLetters, float tweenDuration)
	{
		Sequence anim = DOTween.Sequence();
		float time = 0.0f;
		Text[] newText = new Text[words.Length];
		for (int i = 0; i < words.Length; i++)
		{
			newText[i] = Instantiate(m_letterPrefab, this.transform);
			newText[i].text = words[i];
			newText[i].rectTransform.localScale = Vector3.right + Vector3.forward;
			newText[i].color = color.Evaluate((float)i / (words.Length - 1));
			anim.Insert(time, newText[i].rectTransform.DOScaleY(1.0f, tweenDuration).SetEase(Ease.OutElastic));
			time += timeBetweenLetters;
		}
	}

	private void OnDisable ()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
