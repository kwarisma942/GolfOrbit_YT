using UnityEngine;

namespace PurchasingManagement
{

	[CreateAssetMenu(fileName="UnityPurchasingSettings", menuName="Assets/UnityPurchasing/Settings", order=0),
	System.Serializable]
	public class UnityPurchasingSettings: ScriptableObject
	{
	
		[SerializeField] private string m_androidProductName = "";
		[SerializeField] private string m_iosProductName = "";
		[SerializeField] private bool m_debugMode = true;
	
		public string androidProductName
		{
			get { return (m_androidProductName); }
		}
		
		public string iosProductName
		{
			get { return (m_iosProductName); }
		}
		
		public bool debug
		{
			get { return (m_debugMode); }
		}
	
	}

}