using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using Pinpin.Helpers;

namespace Pinpin.UI
{

	[DisallowMultipleComponent, RequireComponent(typeof(Text))]
	public class UICurrencyUpdater: MonoBehaviour
	{

		public float animDuration = 1f;
		
		public event UnityAction onAnimationEndEvent;

		private ulong lastDisplayedValue { get; set; }
		private ulong toDisplayValue { get; set; }
		private ulong currentDisplayedValue { get; set; }

		[SerializeField] private bool m_animateOnEnable = true;

		private Text field { get; set; }
		private ulong currentDisplayed { get; set; }
		private YieldInstruction waitFrame { get; set; }
		private Coroutine currentCoroutine { get; set; }

		public void UpdateValue ( ulong from, ulong to, bool animate = true )
		{
			lastDisplayedValue = from;
			toDisplayValue = to;
			
			if (!animate)
				currentDisplayedValue = toDisplayValue;
		}
		
		public void TriggerUpdate (float duration)
		{
			animDuration = duration;
			UpdateValue();
		}

		private void Awake ()
		{
			this.field = this.GetComponent<Text>();
			this.waitFrame = new WaitForEndOfFrame();
		}

		private void OnEnable ()
		{
			if (currentDisplayedValue != toDisplayValue && m_animateOnEnable)
				this.UpdateValue();
			else
				this.SetAmount(toDisplayValue);
		}

		private void SetAmount ( ulong amount )
		{
			this.field.text = MathHelper.ConvertToEgineeringNotation(amount);//.ToString("n0", ApplicationManager.currentCulture.NumberFormat);
			this.currentDisplayed = amount;
			currentDisplayedValue = amount;
		}

		private void UpdateValue ()
		{
			if (this.isActiveAndEnabled == false)
				return ;
				
			if (currentDisplayedValue == toDisplayValue)
				return ;
				
			if (this.currentCoroutine == null)
				this.currentDisplayed = lastDisplayedValue;
			else
				this.StopCoroutine(this.currentCoroutine);

			ulong diff = toDisplayValue - this.currentDisplayed;
			if (diff == 0)
			{
				this.SetAmount(toDisplayValue);
				return;
			}

			this.currentCoroutine = this.StartCoroutine(this.AnimateValue(diff, animDuration));
		}
		
		private IEnumerator AnimateValue ( ulong diffValue, float speed )
		{
			double currentFloatValue = this.currentDisplayed;
			
			while (this.isActiveAndEnabled)
			{
				if (diffValue < 0) // - value
				{
					while ( this.currentDisplayed > toDisplayValue )
					{ 
						yield return ( this.waitFrame );
						currentFloatValue += (double)diffValue / speed * Time.deltaTime;
						if (this.currentDisplayed != (ulong)currentFloatValue)
							this.SetAmount((ulong)currentFloatValue);
					}
				}
				else // + value
				{
					while ( this.currentDisplayed < toDisplayValue )
					{ 
						yield return ( this.waitFrame );
						currentFloatValue += (double)diffValue / speed * Time.deltaTime;
						if (this.currentDisplayed != (ulong)currentFloatValue)
							this.SetAmount((ulong)currentFloatValue);
					}
				}
				this.SetAmount(toDisplayValue);
				break;
			}

			if (onAnimationEndEvent != null)
				onAnimationEndEvent.Invoke();

			this.currentCoroutine = null;
		}

	}

}