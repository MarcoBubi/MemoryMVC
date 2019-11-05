using System;
using System.Collections.Generic;
using MemoryMVC.Classes;

namespace MemoryMVC.Framework
{
	class BoardView
	{
		#region Members

		private const int EVEN_NUMBER_CHECK = 2;
		private const int MINIMUM_NUMBER_OF_CARDS = 2;

		private GameController _GameController;

		private double _VerticalMovement = 0;
		private double _HorizontalMovement = 1;
		private double _MaxRowNumbers = 0;
		private int _ActivePosition = 0;
		private int _CorrectNumberInput = 0;
		private bool _EndGame = false;

		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Public

		public BoardView()
		{
			int numberOfCards = _GetPlayerInput();
			_GameController = new GameController(numberOfCards);
			_SetMovementValues(numberOfCards);
			_Run();
		}

		#endregion
		// ----------------------------------------------------------------------------------------------------------------------------------
		#region Internal

		private void _Run()
		{
			while (!_EndGame)
			{
				_PrintCardList();
				_RegisterKeyInputs();
			}
			_ShouldRepeatGame();
		}

		private int _GetPlayerInput()
		{
			int NumberInput;
			string LineInput;
			Console.WriteLine("Insert an even number of cards:\n");
			LineInput = Console.ReadLine();
			NumberInput = _InputValueChecker(LineInput);
			return NumberInput;
		}

		private int _InputValueChecker(string lineInput)
		{
			bool ValidNumber;
			if (Int32.TryParse(lineInput, out int Number))
			{
				ValidNumber = _IsNumberOfCardsValid(Number);
				if (ValidNumber)
				{
					_CorrectNumberInput = Number;
				}
				else
				{
					Console.WriteLine("The input must be an even number bigger than zero! Please insert a valid number of cards:");
					lineInput = Console.ReadLine();
					_InputValueChecker(lineInput);
				}
			}
			else
			{
				Console.WriteLine("The input is not a number! Please insert a number of cards:");
				lineInput = Console.ReadLine();
				_InputValueChecker(lineInput);
			}
			return _CorrectNumberInput;
		}

		private bool _IsNumberOfCardsValid(int NumberInput)
		{
			return (NumberInput >= MINIMUM_NUMBER_OF_CARDS && _IsNumberEven(NumberInput));
		}

		private bool _IsNumberEven(int Number)
		{
			return (Number % EVEN_NUMBER_CHECK == 0);
		}

		private void _RegisterKeyInputs()
		{
			if (!_EndGame)
			{
				double? positionModifier = null;
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.Escape:
						_EndGame = true;
						break;
					case ConsoleKey.LeftArrow:
						positionModifier = -_HorizontalMovement;
						break;
					case ConsoleKey.A:
						positionModifier = -_HorizontalMovement;
						break;
					case ConsoleKey.UpArrow:
						positionModifier = -_VerticalMovement;
						break;
					case ConsoleKey.W:
						positionModifier = -_VerticalMovement;
						break;
					case ConsoleKey.RightArrow:
						positionModifier = _HorizontalMovement;
						break;
					case ConsoleKey.D:
						positionModifier = _HorizontalMovement;
						break;
					case ConsoleKey.DownArrow:
						positionModifier = _VerticalMovement;
						break;
					case ConsoleKey.S:
						positionModifier = _VerticalMovement;
						break;
					case ConsoleKey.Enter:
						_GameController.RevealCard(_ActivePosition);
						break;
					default:
						_RegisterKeyInputs();
						break;
				}
				if (positionModifier != null)
				{
					_ModifyPosition(positionModifier);
				}
			}
		}

		private void _PrintCardList(bool refreshValue = false)
		{
			if (!_EndGame)
			{
				Console.Clear();
				bool activePosition = false;

				List<Card> cardList = _GameController.GetCardList();


				_DrawSpacesBetweenCards();
				for (int i = 1; i <= cardList.Count; ++i)
				{
					activePosition = _ActivePosition == i - 1 ? true : false;
					_DisplayCard(cardList[i-1], activePosition);

					if (i % _MaxRowNumbers == 0 && i != 0)
					{
						_DrawSpacesBetweenCards();
					}
				}
				_DrawSpacesBetweenCards();

				if (_GameController.ShouldCheckForPairs)
				{
					_GameController.CheckForPairs();
					_EndGame = _GameController.WinCondition();
				}
			}
		}

		private void _SetMovementValues(int numberOfCards)
		{
			_MaxRowNumbers = Math.Round(Math.Sqrt(numberOfCards), 0);
			_VerticalMovement = _MaxRowNumbers;
		}

		private void _ShouldRepeatGame()
		{
			Console.Write("Congratulations, you win! Do you want to start a new game?Y/y\n");
			if (Console.ReadKey(true).Key == ConsoleKey.Y)
			{
				Program.Main(null);
			}
			else
			{
				Environment.Exit(0);
			}
		}

		private void _ModifyPosition(double? value)
		{
			_ActivePosition = _ActivePosition + (int)value;
			if (_ActivePosition > _GameController.NumberOfCards || _ActivePosition == _GameController.NumberOfCards)
			{
				_ActivePosition = _GameController.NumberOfCards - 1;
			}
			else if (_ActivePosition < 0 || _ActivePosition == 0)
			{
				_ActivePosition = 0;
			}
		}

		private void _DisplayCard(Card card, bool activePosition = false)
		{
			string activePositionStar = " ";
			string value = "X";

			if (activePosition)
			{
				activePositionStar = "*";
			}

			if (card.CardStatus)
			{
				value = card.CardValue.ToString();
			}
			string outputString = $"\t|  {value + activePositionStar} |\t";

			Console.Write(outputString);
		}

		private void _DrawSpacesBetweenCards()
		{
			Console.Write("\n");
			Console.Write("\n");
		}

		#endregion
	}
}
