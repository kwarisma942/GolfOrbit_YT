using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Event/Collision Event Trigger")]
public class CollisionEventTrigger: MonoBehaviour
{

	[Serializable] public class CollisionEvent: UnityEvent<Collision> {}
	[Serializable] public class CollisionEvent2D: UnityEvent<Collision2D> {}
	[Serializable] public class ColliderEvent: UnityEvent<Collider> {}
	[Serializable] public class ColliderEvent2D: UnityEvent<Collider2D> {}

	[SerializeField] private CollisionEvent	onCollisionEnter;
	[SerializeField] private CollisionEvent	onCollisionStay;
	[SerializeField] private CollisionEvent	onCollisionExit;
	[SerializeField] private ColliderEvent	onTriggerEnter;
	[SerializeField] private ColliderEvent	onTriggerStay;
	[SerializeField] private ColliderEvent	onTriggerExit;

	[SerializeField] private CollisionEvent2D	onCollisionEnter2D;
	[SerializeField] private CollisionEvent2D	onCollisionStay2D;
	[SerializeField] private CollisionEvent2D	onCollisionExit2D;
	[SerializeField] private ColliderEvent2D	onTriggerEnter2D;
	[SerializeField] private ColliderEvent2D	onTriggerStay2D;
	[SerializeField] private ColliderEvent2D	onTriggerExit2D;

	[SerializeField, HideInInspector] private int showedEventsMask;

	public void OnCollisionEnter ( Collision collision ) { this.onCollisionEnter.Invoke(collision); }
	public void OnCollisionStay ( Collision collision ) { this.onCollisionStay.Invoke(collision); }
	public void OnCollisionExit ( Collision collision ) { this.onCollisionExit.Invoke(collision); }
	public void OnTriggerEnter ( Collider other ) { this.onTriggerEnter.Invoke(other); }
	public void OnTriggerStay ( Collider other ) { this.onTriggerStay.Invoke(other); }
	public void OnTriggerExit ( Collider other ) { this.onTriggerExit.Invoke(other); }

	public void OnCollisionEnter2D ( Collision2D collision ) { this.onCollisionEnter2D.Invoke(collision); }
	public void OnCollisionStay2D ( Collision2D collision ) { this.onCollisionStay2D.Invoke(collision); }
	public void OnCollisionExit2D ( Collision2D collision ) { this.onCollisionExit2D.Invoke(collision); }
	public void OnTriggerEnter2D ( Collider2D other ) { this.onTriggerEnter2D.Invoke(other); }
	public void OnTriggerStay2D ( Collider2D other ) { this.onTriggerStay2D.Invoke(other); }
	public void OnTriggerExit2D ( Collider2D other ) { this.onTriggerExit2D.Invoke(other); }

}
