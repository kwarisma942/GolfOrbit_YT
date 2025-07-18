namespace PurchasingManagement
{

	public struct PurchaseEvent
	{
		
		public enum Status
		{
			Success,
			Fail
		}
	
		public Status type { get; private set; }
		public string productId { get; private set; }

		public PurchaseEvent ( Status type, string productId )
		{
			this.type = type;
			this.productId = productId;
		}

	}

}