using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackjackLibrary.API;
using BlackjackLibrary.EventArgs;
using BlackjackLibrary.Models;
using Models.Blackjack.Enums;

namespace Controllers.BlackjackController
{
	public class BlackjackController
	{
        public GameState Gamestate { get; set; } = GameState.NotStarted;
        public BlackjackGame Game { get; set;  }
        public List<string> PlayerHand { get; set; } = new List<string>();
        public List<string> DealerHand { get; set; } = new List<string>();
        public decimal PlayerBalance { get; set; } = 100;
        public int PlayerScore { get; set; } = 0;
        public int DealerScore { get; set; } = 0;
        public string PlayerState { get; set; }
        public string DealerState { get; set; }
        public string Result { get; set; } = null;
        public int PlayerBet { get; set; } = 0;
        public bool PlayerStood { get; set; } = false;

        public void Start()
        {
            PlayerHand.Clear();
            DealerHand.Clear();
            Game = new BlackjackGame(new AiDealer("dealer", 1000), new HumanPlayer("player", PlayerBalance), 6);
            Gamestate = GameState.Betting;
        }

        public void Bet(int num)
        {
            // Start round with a bet of user choice
            Game.StartRound(num.ToString());

            Gamestate = GameState.InProgress;
            PlayerBalance = (int)Game.MainPlayer.Balance;
            PlayerBet = (int)Game.MainPlayer.BetAmount;
            // Get hand info and store in a list
            foreach (var dealCard in Game.DealerInfo.Hand)
            {
                if (dealCard.IsHidden == false)
                {
                    DealerHand.Add("/images/common/" + "card" + dealCard.Suite.ToString() + dealCard.Rank.ToString() + ".png");
                }
                else
                {
                    DealerHand.Add("/images/common/cardBack.png");
                }

            }
            DealerScore = Game.GetHandScore(Game.DealerInfo.Hand);
            DealerState = Game.DealerInfo.Status.ToString();
            foreach (var card in Game.MainPlayer.Hand)
            {
                PlayerHand.Add("/images/common/" + "card" + card.Suite.ToString() + card.Rank.ToString() + ".png");
            }
            PlayerScore = Game.GetHandScore(Game.MainPlayer.Hand);
            PlayerState = Game.MainPlayer.Status.ToString();

            CheckForPlayerNatural(num);
        }

        public void PlayerHit()
        {
            Game.Hit();
            PlayerHand.Clear();
            foreach (var card in Game.MainPlayer.Hand)
            {
                PlayerHand.Add("/images/common/" + "card" + card.Suite.ToString() + card.Rank.ToString() + ".png");
            }
            PlayerScore = Game.GetHandScore(Game.MainPlayer.Hand);
            PlayerState = Game.MainPlayer.Status.ToString();
            HandleTurn();
        }

        public void PlayerStand()
        {
            Game.Stand();
            PlayerStood = true;
            RevealDealerCard();
            HandleStand();
        }

        public void RevealDealerCard()
        {
            DealerHand.Clear();
            foreach (var dealCard in Game.DealerInfo.Hand)
            {
                DealerHand.Add("/images/common/" + "card" + dealCard.Suite.ToString() + dealCard.Rank.ToString() + ".png");
            }
            DealerScore = Game.GetHandScore(Game.DealerInfo.Hand);
            DealerState = Game.DealerInfo.Status.ToString();
        }

        public void HandleTurn()
        {
            if (PlayerScore > 21 && PlayerState == "bust")
            {
                Gamestate = GameState.Payout;
                Result = $"You busted. You lost ${PlayerBet}";
                RevealDealerCard();
            }
            else if (PlayerScore == 21)
            {
                Gamestate = GameState.Payout;
                RevealDealerCard();
                if (PlayerState == "blackjack" && DealerScore != 21)
                {
                    Result = $"Congrats you won! You won ${PlayerBet * 2}";
                    PlayerBalance += (PlayerBet * 2);
                }
                else if (DealerScore == 21)
                {
                    Result = $"You pushed. You got your money back";
                    PlayerBalance += PlayerBet;
                }
            }
        }

        public void CheckForPlayerNatural(int bet)
        { 
            if (PlayerState == "Natural" && DealerState != "Natural")
            {
                Result = $"Congrats you got a blackjack! You won ${bet * 1.5}";
                double sum = (bet * 1.5);
                PlayerBalance = PlayerBalance + (decimal)sum;
                Gamestate = GameState.Payout;
            }
        }

        public void HandleStand()
        {
            if (PlayerStood == true)
            {
                Gamestate = GameState.Payout;
                if (PlayerScore < DealerScore && DealerScore <= 21)
                {
                    Result = $"Dealer wins. You lost ${PlayerBet}";
                }
                else if (DealerScore > 21)
                {
                    Result = $"Dealer busted. You won ${PlayerBet * 2}";
                    PlayerBalance += (PlayerBet * 2);

                }
                else if (PlayerScore > DealerScore && PlayerScore < 21)
                {
                    Result = $"Congrats you won ${PlayerBet * 2}";
                    PlayerBalance += (PlayerBet * 2);
                }
                else if (PlayerScore == DealerScore)
                {
                    Result = $"You pushed. You got your ${PlayerBet} back";
                    PlayerBalance += PlayerBet;
                }
            }
        }

        public void Exit()
        {
            Gamestate = GameState.EndOfGame;
            Result = $"Thanks for playing! You are leaving with ${PlayerBalance}.";
        }

        public void KeepPlaying()
        {
            Gamestate = GameState.Betting;
            Start();
        }
    }
}