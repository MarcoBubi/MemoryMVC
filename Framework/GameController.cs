using MemoryMVC.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMVC.Framework
{
	public class GameController
	{
		#region Members

		public Board Board;
		public int NumberOfCards = 0;
		public bool ClicksRegistered = false;

		private readonly int _SplitCardNumber = 2;
		private bool _ChangeTurn = false;
		private int _FirstCardSelected = 0;
		private int _SecondCardSelected = 0;

		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Public

		public GameController(int numberInput)
		{
			Board = new Board(numberInput);

			NumberOfCards = Board.NumberOfCards;
			Board.InitializeBoard(_SplitCardNumber, (NumberOfCards / _SplitCardNumber));
		}

		public void RevealCard(int activePosition)
		{
			if (Board.GetShuffledCardList()[activePosition].CardStatus == false && !ClicksRegistered)
			{
				if (!_ChangeTurn)
				{
					_FirstCardSelected = activePosition;
					_ShouldDisplayCard(_FirstCardSelected, activePosition, true);
				}
				else
				{
					_SecondCardSelected = activePosition;
					_ShouldDisplayCard(_SecondCardSelected, activePosition, false);
					ClicksRegistered = true;
				}
			}
		}

		public void EndGame(bool endGame = false)
		{
			if (endGame)
			{
				Environment.Exit(0);
			}
		}

		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Internal

		private void _ShouldDisplayCard(int cardSelected, int activePosition, bool changeTurn = false)
		{
			Board.GetCardAtPosition(cardSelected).CardStatus = true;
			cardSelected = activePosition;
			_ChangeTurn = changeTurn;
		}

		public void CheckForPairs()
		{

			if (Board.GetCardAtPosition(_FirstCardSelected).CardValue != Board.GetCardAtPosition(_SecondCardSelected).CardValue)
			{
				Board.GetCardAtPosition(_FirstCardSelected).CardStatus = false;
				Board.GetCardAtPosition(_SecondCardSelected).CardStatus = false;
			}
			ClicksRegistered = false;
		}

		public bool WinCondition()
		{
			bool winConditionMet = true;

			foreach (var card in Board.GetShuffledCardList())
			{
				if (!card.CardStatus)
				{
					winConditionMet = false;
				}
			}
			return winConditionMet;
		}

		public List<Card> GetCardList()
		{
			return Board.GetShuffledCardList();
		}

		public Card GetCardAtPosition(int cardPosition)
		{
			return Board.GetCardAtPosition(cardPosition);
		}

		#endregion
	}
}
