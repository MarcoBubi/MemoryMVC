namespace MemoryMVC.Classes
{
	public class Card
	{
		#region Members

		private int _Value;
		private bool _Status = false;

		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Public

		public Card(int value)
		{
			_Value = value;
		}

		public int CardValue
		{
			get { return _Value; }
			set { this._Value = value; }
		}

		public bool CardStatus
		{
			get { return _Status; }
			set { this._Status = value; }
		}

		#endregion
	}
}
