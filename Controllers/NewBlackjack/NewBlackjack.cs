using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackjackLibrary.API;
using BlackjackLibrary.EventArgs;
using BlackjackLibrary.Models;
using Models.Blackjack.Enums;

namespace Controllers.NewBlackjack
{
	public class Blackjack
	{
        public static GameState gamestate = GameState.NotStarted;
        public static BlackjackGame game;
        public static string startToPlay = "Click 'Start' if you would like to play or click 'How to play' for instructions!";
        public static string betQuestion = "How much do you want to bet?";
        public static List<string> playerHand = new List<string>();
        public static List<string> dealerHand = new List<string>();
        public static decimal playerBalance = 100;
        public static int playerScore = 0;
        public static int dealerScore = 0;
        public static string playerState;
        public static string dealerState;
        public static string result = null;
        public static int playerBet = 0;
        private static bool playerStood = false;

        public static void Start()
        {
            playerHand.Clear();
            dealerHand.Clear();
            game = new BlackjackGame(new AiDealer("dealer", 1000), new HumanPlayer("player", playerBalance), 6);
            gamestate = GameState.Betting;
        }

        public static void Bet(int num)
        {
            // Start round with a bet of user choice
            game.StartRound(num.ToString());

            gamestate = GameState.InProgress;
            playerBalance = (int)game.MainPlayer.Balance;
            playerBet = (int)game.MainPlayer.BetAmount;
            // Get hand info and store in a list
            foreach (var dealCard in game.DealerInfo.Hand)
            {
                if (dealCard.IsHidden == false)
                {
                    dealerHand.Add("/images/common/" + "card" + dealCard.Suite.ToString() + dealCard.Rank.ToString() + ".png");
                }
                else
                {
                    dealerHand.Add("/images/common/cardBack.png");
                }

            }
            dealerScore = game.GetHandScore(game.DealerInfo.Hand);
            dealerState = game.DealerInfo.Status.ToString();
            foreach (var card in game.MainPlayer.Hand)
            {
                playerHand.Add("/images/common/" + "card" + card.Suite.ToString() + card.Rank.ToString() + ".png");
            }
            playerScore = game.GetHandScore(game.MainPlayer.Hand);
            playerState = game.MainPlayer.Status.ToString();

            CheckForPlayerNatural(num);
        }

        public static void PlayerHit()
        {
            game.Hit();
            playerHand.Clear();
            foreach (var card in game.MainPlayer.Hand)
            {
                playerHand.Add("/images/common/" + "card" + card.Suite.ToString() + card.Rank.ToString() + ".png");
            }
            playerScore = game.GetHandScore(game.MainPlayer.Hand);
            playerState = game.MainPlayer.Status.ToString();
            HandleTurn();
        }

        public static void PlayerStand()
        {
            game.Stand();
            playerStood = true;
            RevealDealerCard();
            HandleStand();
        }

        private static void RevealDealerCard()
        {
            dealerHand.Clear();
            foreach (var dealCard in game.DealerInfo.Hand)
            {
                dealerHand.Add("/images/common/" + "card" + dealCard.Suite.ToString() + dealCard.Rank.ToString() + ".png");
                dealerScore = game.GetHandScore(game.DealerInfo.Hand);
            }
            dealerScore = game.GetHandScore(game.DealerInfo.Hand);
            dealerState = game.DealerInfo.Status.ToString();
        }

        private static void HandleTurn()
        {
            if (playerScore > 21 && playerState == "bust")
            {
                gamestate = GameState.Payout;
                result = $"You busted. You lost ${playerBet}";
                RevealDealerCard();
            }
            else if (playerScore == 21)
            {
                gamestate = GameState.Payout;
                RevealDealerCard();
                if (playerState == "blackjack" && dealerScore != 21)
                {
                    result = $"Congrats you won! You won ${playerBet * 2}";
                    playerBalance += (playerBet * 2);
                }
                else if (dealerScore == 21)
                {
                    result = $"You pushed. You got your money back";
                    playerBalance += playerBet;
                }
            }
        }

        private static void CheckForPlayerNatural(int bet)
        {
            if (playerState == "Natural" && dealerState != "Natural")
            {
                gamestate = GameState.Payout;
                result = $"Congrats you got a blackjack! You won ${bet * 2.5}";
                playerBalance += (decimal)(bet * 2.5);
            }
        }

        private static void HandleStand()
        {
            if (playerStood == true)
            {
                gamestate = GameState.Payout;
                if (playerScore < dealerScore && dealerScore <= 21)
                {
                    result = $"Dealer wins. You lost ${playerBet}";
                }
                else if (dealerScore > 21)
                {
                    result = $"Dealer busted. You won ${playerBet * 2}";
                    playerBalance += (playerBet * 2);

                }
                else if (playerScore > dealerScore && playerScore < 21)
                {
                    result = $"Congrats you won ${playerBet * 2}";
                    playerBalance += (playerBet * 2);
                }
                else if (playerScore == dealerScore)
                {
                    result = $"You pushed. You got your ${playerBet} back";
                    playerBalance += playerBet;
                }
            }
        }

        public static void Exit()
        {
            gamestate = GameState.EndOfGame;
            result = $"Thanks for playing! You are leaving with ${playerBalance}.";
        }

        public static void KeepPlaying()
        {
            gamestate = GameState.Betting;
            Start();
        }
    }
}