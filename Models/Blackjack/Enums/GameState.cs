using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Blackjack.Enums
{
    public enum GameState
    {
        NotStarted, //Before first hand
        Betting, //During the betting phase
        InProgress, //After the initial deal, but before bets are paid out or collected.
        Payout, //After the hand is over, during while bets are paid out or collected.
        EndOfGame, //Happens when the player runs out of money. 
    }   
}
