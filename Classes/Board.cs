using System;
using System.Collections.Generic;
using System.Linq;
using MemoryMVC.Framework;

namespace MemoryMVC.Classes
{
	public class Board
	{
		#region Members

		public List<Card> CardList;

		private List<int> _CardValueList;
		private int _SplitCardNumber = 2;
		private int _NumberOfCardPairs = 0;

		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Public

		public Board(int numberOfCards)
		{
			SetNumberOfCards(numberOfCards);
		}

		public int NumberOfCards
		{
			get; private set;
		}

		public void SetNumberOfCards(int cards)
		{
			NumberOfCards = cards;
		}

		public void InitializeBoard(int splitCardNumber, int numberOfCardPairs)
		{
			CardList = new List<Card>();
			_CardValueList = new List<int>();

			_SplitCardNumber = splitCardNumber;
			_NumberOfCardPairs = numberOfCardPairs;
			_GenerateCardNumbers();
			_CardValueList = _ShuffleList();
			_CreateCardList();
		}

		public List<Card> GetShuffledCardList()
		{
			return CardList;
		}

		public Card GetCardAtPosition(int position)
		{
			return CardList[position];
		}
		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Internal

		private void _CreateCardList()
		{
			foreach (int value in _CardValueList)
			{
				CardList.Add(new Card(value));
			}
			_CardValueList = null;
		}

		private void _GenerateCardNumbers()
		{
			for (int i = 0; i < _SplitCardNumber; ++i)
			{
				for (int j = 0; j < _NumberOfCardPairs; ++j)
				{
					_CardValueList.Add(j);
				}
			}
		}

		private List<int> _ShuffleList()
		{
			List<int> newList = new List<int>();
			Random randomPick = new Random();
			int randomIndex = 0;

			while (_CardValueList.Count > 0)
			{
				randomIndex = randomPick.Next(0, _CardValueList.Count);
				newList.Add(_CardValueList[randomIndex]);
				_CardValueList.RemoveAt(randomIndex);
			}

			return newList;
		}

		#endregion
	}
}