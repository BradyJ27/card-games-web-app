using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controllers.NewBlackjack;
using Models.Blackjack.Enums;

namespace Blackjack.Test
{
    [TestClass]
    public class TestsForEachMethods
    {
        [TestMethod]
        public void StartShouldChangeGameStateToBetting()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            Assert.AreEqual(GameState.Betting, blackjack.Gamestate); 
        }

        [TestMethod]
        public void StartShouldClearPlayerHand()
        {
            var blackjack = new BlackjackController();
            blackjack.PlayerHand.Add("hello");
            blackjack.PlayerHand.Add("world");
            int countBeforeStart = blackjack.PlayerHand.Count;
            blackjack.Start();
            int countAfterStart = blackjack.PlayerHand.Count;
            Assert.AreNotEqual(countAfterStart, countBeforeStart);
        }

        [TestMethod]
        public void StartShouldClearDealerHand()
        {
            var blackjack = new BlackjackController();
            blackjack.DealerHand.Add("hello");
            blackjack.DealerHand.Add("world");
            int countBeforeStart = blackjack.DealerHand.Count;
            blackjack.Start();
            int countAfterStart = blackjack.DealerHand.Count;
            Assert.AreNotEqual(countAfterStart, countBeforeStart);
        }

        [TestMethod]
        public void BetShouldChangePlayerBet()
        { 
            var blackjack = new BlackjackController();
            blackjack.Start();
            int bet = 10;
            blackjack.Bet(bet);
            Assert.AreEqual(bet, blackjack.PlayerBet);
        }

        [TestMethod]
        public void BetShouldAddCardsToPlayerHand()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            int sizeOfPlayerHandBefore = blackjack.PlayerHand.Count;
            blackjack.Bet(10);
            int sizeOfPlayerHandAfter = blackjack.PlayerHand.Count;
            Assert.AreNotEqual(sizeOfPlayerHandBefore, sizeOfPlayerHandAfter);
        }

        [TestMethod]
        public void BetShouldAddCardsToDealerHand()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            int sizeOfDealerHandBefore = blackjack.DealerHand.Count;
            blackjack.Bet(10);
            int sizeOfDealerHandAfter = blackjack.DealerHand.Count;
            Assert.AreNotEqual(sizeOfDealerHandBefore, sizeOfDealerHandAfter);
        }

        [TestMethod]
        public void HitShouldAddCardToPlayerHand()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            int sizeOfPlayerHandBeforeHit = blackjack.PlayerHand.Count;
            blackjack.PlayerHit();
            int sizeOfPlayerHandAfterHit = blackjack.PlayerHand.Count;
            Assert.AreNotEqual(sizeOfPlayerHandBeforeHit, sizeOfPlayerHandAfterHit);
        }

        [TestMethod]
        public void HitShouldUpdateScore()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            int scoreBeforeHit = blackjack.PlayerScore;
            blackjack.PlayerHit();
            int scoreAfterHit = blackjack.PlayerScore;
            Assert.AreNotEqual(scoreBeforeHit, scoreAfterHit);
        }

        [TestMethod]
        public void StandShouldChangePlayerStoodVar()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            blackjack.PlayerStand();
            Assert.IsTrue(blackjack.PlayerStood);

        }

        [TestMethod]
        public void RevealDealerCardShouldChangeDealerHand()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            int scoreBeforeReveal = blackjack.DealerScore;
            blackjack.Game.Stand();
            blackjack.RevealDealerCard();
            int scoreAfterReveal = blackjack.DealerScore;
            Assert.AreNotEqual(scoreBeforeReveal, scoreAfterReveal);
        }

        [TestMethod]
        public void ExitShouldChangeGameState()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            blackjack.PlayerStand();
            blackjack.Exit();
            Assert.AreEqual(GameState.EndOfGame, blackjack.Gamestate);
        }

        [TestMethod]
        public void CheckForNaturalShouldChangeGameStateIfPlayerHasNatural()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            blackjack.PlayerState = "Natural";
            blackjack.DealerState = "numeric";
            blackjack.CheckForPlayerNatural(10);
            Assert.AreEqual(GameState.Payout, blackjack.Gamestate);
        }

        [TestMethod]
        public void GameStateChangesWhenPlayerGoesOver21()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            blackjack.PlayerScore = 22;
            blackjack.PlayerState = "bust";
            blackjack.HandleTurn();
            Assert.AreEqual(GameState.Payout, blackjack.Gamestate);
        }

        [TestMethod]
        public void WhenPlayerHits21BalanceShouldBeHigher()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            decimal currentBalance = blackjack.PlayerBalance;
            blackjack.PlayerScore = 21;
            blackjack.PlayerState = "blackjack";
            blackjack.HandleTurn();
            decimal newBalance = blackjack.PlayerBalance;
            Assert.AreNotEqual(currentBalance, newBalance);
        }

        [TestMethod]
        public void WhenPlayerPushesBalanceShouldBeTheSame()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            blackjack.PlayerScore = 17;
            blackjack.DealerScore = 17;
            blackjack.PlayerStood = true;
            blackjack.HandleStand();
            decimal newBalance = blackjack.PlayerBalance;
            Assert.AreEqual(100, newBalance);
        }

        [TestMethod]
        public void WhenDealerBustsPlayerBalanceShouldBeHigher()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            blackjack.Bet(10);
            decimal currentBalance = blackjack.PlayerBalance;
            blackjack.PlayerStood = true;
            blackjack.PlayerScore = 17;
            blackjack.DealerScore = 25;
            blackjack.HandleStand();
            decimal newBalance = blackjack.PlayerBalance;
            Assert.AreNotEqual(currentBalance, newBalance);
        }

        [TestMethod]
        public void WhenDealerIsHigherThanPlayerBalanceIsDifferent()
        {
            var blackjack = new BlackjackController();
            blackjack.Start();
            decimal currentBalance = blackjack.PlayerBalance;
            blackjack.Bet(10);
            blackjack.PlayerStood = true;
            blackjack.PlayerScore = 17;
            blackjack.DealerScore = 20;
            blackjack.HandleStand();
            decimal newBalance = blackjack.PlayerBalance;
            Assert.AreNotEqual(currentBalance, newBalance);
        }
    }
}
