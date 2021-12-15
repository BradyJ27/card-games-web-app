using System.Collections.Generic;

namespace Models.Poker
{
    public class Hand
    {
        internal class HandComparer : IComparer<Card>
        {
            public int Compare(Card a, Card b)
            {
                int res = a.Value.CompareTo(b.Value);
                return res;
            }
        }

        public List<Card> hand { get; set; } = new List<Card>();
        public HandType value { get; set; }
        
        

        public Hand(List<Card> playerHand, List<Card> communityCards)
        {
            foreach (Card card in playerHand)
            {
                this.hand.Add(card);
            }
            foreach (Card card in communityCards)
            {
                this.hand.Add(card);
            }

            SortCards();
            CalculateHand();
            
        }

        public enum HandType
        {
            RoyalFlush = 0,
            StraightFlush = 1,
            FourOfAKind = 2,
            FullHouse = 3,
            Flush = 4,
            Straight = 5,
            ThreeOfAKind = 6,
            TwoPairs = 7,
            Pair = 8,
            HighCard = 9,
        }

        public void CalculateHand()
        {
            Card previousCard = new Card();
            int straightcounter = 0;
            int paircounter = 0;
            int flushcounter = 0;
            bool possibletwopairs = false;
            bool jack = false;
            bool ace = false;

            foreach (Card card in hand)
            {
                

                if(previousCard.Value != default)
                {
               

                    if (previousCard.Value == card.Value - 1)
                    {
                        straightcounter++;
                    }
                    else if (previousCard.Value == card.Value)
                    {
                        paircounter++;
                    }

                    if (previousCard.Value != card.Value && paircounter == 2)
                    {
                        possibletwopairs = true;
                        paircounter = 0;
                    }

                    if (previousCard.Suit == card.Suit)
                    {
                        flushcounter++;
                    }

                    if (card.Value == Common.Enums.CardValue.Jack)
                    {
                        jack = true;
                    }

                    if (card.Value == Common.Enums.CardValue.Ace)
                    {
                        ace = true;
                    }

                }
                previousCard = card;

            }

            if(paircounter < 2 && straightcounter < 5 && flushcounter < 5)
            {
                this.value = HandType.HighCard;
            }
            else if(paircounter == 1 && straightcounter < 5 && flushcounter < 5)
            {
                this.value = HandType.Pair;
            }
            else if (paircounter == 2 && straightcounter < 5 && flushcounter < 5)
            {
                this.value = HandType.ThreeOfAKind;
            }
            else if (possibletwopairs == true && paircounter == 2 && straightcounter < 5 && flushcounter < 5)
            {
                this.value = HandType.TwoPairs;
            }
            else if (paircounter == 3 && straightcounter < 5 && flushcounter < 5)
            {
                this.value = HandType.ThreeOfAKind;
            }
            else if (straightcounter >= 5 && flushcounter < 5)
            {
                this.value = HandType.Straight;
            }
            else if (straightcounter < 5 && flushcounter >= 5)
            {
                this.value = HandType.Flush;
            }
            else if (possibletwopairs == true && paircounter >= 3 && straightcounter < 5 && flushcounter < 5)
            {
                this.value = HandType.FullHouse;
            }
            else if (paircounter == 4)
            {
                this.value = HandType.FourOfAKind;
            }
            else if(straightcounter >= 5 && flushcounter >= 5)
            {
                this.value = HandType.StraightFlush;
            }
            else if (straightcounter >= 5 && flushcounter >= 5)
            {
                this.value = HandType.StraightFlush;
            }
            else if (straightcounter >= 5 && flushcounter >= 5 && jack == true && ace == true)
            {
                this.value = HandType.StraightFlush;
            }
        }

        private void SortCards()
        {
            hand.Sort(0,hand.Count, new HandComparer());

        }

        
            
           
        
}
}
