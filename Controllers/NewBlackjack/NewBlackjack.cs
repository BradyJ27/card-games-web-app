using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackjackLibrary.API;
using BlackjackLibrary.EventArgs;
using BlackjackLibrary.Models;

namespace Controllers.NewBlackjack
{
	public class NewBlackjack
	{
		public static BlackjackGame game;
		public void Brady(string[] args)
		{
			game = new BlackjackGame(new AiDealer("dealer", 1000), new HumanPlayer("player", 50), 6);

			while (game.AppRunning)
			{
				while (game.GameRunning)
				{
					Console.WriteLine("test");
				}
			}

			//game.GameOver += OnGameOver; // runs whenever the game is over
			//game.RoundEvaluated += OnRoundEvaluated; // runs whenever the round has been evaluated
			//game.DealerInfo.CardDealt += OnCardDealt; // runs whenever a card is dealt to anyone
			//game.OnValidationError += OnValidationError; // runs whenever there is a validation error
		}
	}
}