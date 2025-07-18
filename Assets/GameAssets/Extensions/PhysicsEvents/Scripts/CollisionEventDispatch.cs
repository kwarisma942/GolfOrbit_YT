using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Event/Collision Event Dispatch")]
public class CollisionEventDispatch: MonoBehaviour
{

	[SerializeField] private List<GameObject>	listeners;

	public void OnCollisionEnter ( Collision collision )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnCollisionEnter", collision as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnCollisionStay ( Collision collision )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnCollisionStay", collision as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnCollisionExit ( Collision collision )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnCollisionExit", collision as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnTriggerEnter ( Collider other )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnTriggerEnter", other as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnTriggerStay ( Collider other )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnTriggerStay", other as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnTriggerExit ( Collider other )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnTriggerExit", other as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnCollisionEnter2D ( Collision2D collision )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnCollisionEnter2D", collision as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnCollisionStay2D ( Collision2D collision )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnCollisionStay2D", collision as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnCollisionExit2D ( Collision2D collision )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnCollisionExit2D", collision as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnTriggerEnter2D ( Collider2D other )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnTriggerEnter2D", other as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnTriggerStay2D ( Collider2D other )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnTriggerStay2D", other as object, SendMessageOptions.DontRequireReceiver);
	}

	public void OnTriggerExit2D ( Collider2D other )
	{
		foreach ( GameObject unityGameObject in listeners )
			unityGameObject.SendMessageUpwards("OnTriggerExit2D", other as object, SendMessageOptions.DontRequireReceiver);
	}

}
