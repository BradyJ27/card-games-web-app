using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Poker
{
    public class Player : Person
    {
        public decimal Funds { get; set; } = 200M;

        public decimal Bet { get; set; }

        public decimal Change { get; set; }

        public bool HasFolded { get; set; }


        public void Collect()
        {
            Funds += Change;
            Change = 0M;
            
        }
    }
}
