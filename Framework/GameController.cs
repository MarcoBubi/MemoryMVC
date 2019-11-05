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
		public bool ShouldCheckForPairs = false;

		private bool _IsFirstCardSelected = false;
		private int _FirstCardSelected = 0;
		private int _SecondCardSelected = 0;

		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Public

		public GameController(int numberInput)
		{
			Board = new Board(numberInput);

			NumberOfCards = Board.NumberOfCards;
			Board.InitializeBoard(NumberOfCards);
		}

		public void RevealCard(int activePosition)
		{
			List<Card> cardList = GetCardList();

			if (cardList[activePosition].CardStatus == false && !ShouldCheckForPairs)
			{
				if (!_IsFirstCardSelected)
				{
					_FirstCardSelected = activePosition;
					_IsFirstCardSelected = Board.ShouldDisplayCard(_FirstCardSelected);
				}
				else
				{
					_SecondCardSelected = activePosition;
					ShouldCheckForPairs = Board.ShouldDisplayCard(_SecondCardSelected);
				}
			}
		}

		public void CheckForPairs()
		{
			List<Card> cardList = GetCardList();

			if (cardList[_FirstCardSelected].CardValue != cardList[_SecondCardSelected].CardValue)
			{
				cardList[_FirstCardSelected].CardStatus = false;
				cardList[_SecondCardSelected].CardStatus = false;
			}
			_IsFirstCardSelected = false;
			ShouldCheckForPairs = false;
		}

		public bool WinCondition()
		{
			bool winConditionMet = true;
			List<Card> cardList = GetCardList();

			foreach (var card in cardList)
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
			return Board.GetCardList();
		}

		#endregion
	}
}
