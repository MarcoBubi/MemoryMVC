namespace MemoryMVC.Classes
{
	public class Card
	{
		#region Public

		public Card(int value)
		{
			CardValue = value;
		}

		public int CardValue { get; set; }

		public bool CardStatus { get; set; } = false;

		#endregion
	}
}
