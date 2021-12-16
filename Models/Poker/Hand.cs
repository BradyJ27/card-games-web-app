using Models.Common.Enums;
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
        public CardValue highCard { get; set; }
        public HandType value { get; set; }
        public CardValue pairValue { get; set; }
        public List<Card> cardsOnPerson { get; set; }
        

        public Hand(List<Card> playerHand, List<Card> communityCards)
        {
            this.cardsOnPerson = playerHand;

            

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
            HighCardInOwnHand();
        }

        public void HighCardInOwnHand()
        {
            if (this.cardsOnPerson[0].Value > this.cardsOnPerson[1].Value)
            {
                this.highCard = this.cardsOnPerson[0].Value;

            }
            else
            {
                this.highCard = this.cardsOnPerson[1].Value;
            }
        }

        public enum HandType
        {
            RoyalFlush = 9,
            StraightFlush = 8,
            FourOfAKind = 7,
            FullHouse = 6,
            Flush = 5,
            Straight = 4,
            ThreeOfAKind = 3,
            TwoPairs = 2,
            Pair = 1,
            HighCard = 0,
        }

        public void CalculateHand()
        {
            Card previousCard = new Card();
            int straightcounter = 1;
            int paircounter = 0;
            int flushcounter = 1;
            bool possibletwopairs = false;
            bool jack = false;
            bool ace = false;
            bool twopairs = false;
            bool singlepair = false;

            int spadecount = 0;
            int clubcount = 0;
            int heartcount = 0;
            int diamondcount = 0;
            

            foreach (Card card in hand)
            {
                if(card.Suit == CardSuit.Hearts)
                {
                    heartcount++;
                }
                else if(card.Suit == CardSuit.Diamonds)
                {
                    diamondcount++;
                }
                else if(card.Suit == CardSuit.Clubs)
                {
                    clubcount++;
                }
                else if(card.Suit == CardSuit.Spades)
                {
                    spadecount++;
                }



                if(previousCard.Value != default)
                {
               

                    if (previousCard.Value == card.Value - 1)
                    {
                        straightcounter++;
                    }

                    if (previousCard.Value != card.Value - 1 && straightcounter < 5)
                    {
                        straightcounter = 0;
                    }

                    if (previousCard.Value == card.Value)
                    {
                        paircounter++;
                        this.pairValue = previousCard.Value;
                        if (possibletwopairs == true)
                        {
                            twopairs = true;
                            this.pairValue = previousCard.Value;
                        }
                    }

                    if(paircounter == 1 && previousCard.Value != card.Value)
                    {
                        possibletwopairs = true;
                        singlepair = true;
                        this.pairValue = previousCard.Value;
                        paircounter = 0;
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

           

            if(paircounter == 0)
            {
                this.value = HandType.HighCard;
            }
            if(singlepair)
            {
                this.value = HandType.Pair;
            }
            if (paircounter == 2)
            {
                this.value = HandType.ThreeOfAKind;
            }
            if (twopairs)
            {
                this.value = HandType.TwoPairs;
            }
            if (straightcounter >= 5)
            {
                this.value = HandType.Straight;
            }
            if (diamondcount >= 5 || heartcount >= 5 || spadecount >= 5 || clubcount >= 5)
            {
                this.value = HandType.Flush;
            }
            if (possibletwopairs == true && paircounter >= 2)
            {
                this.value = HandType.FullHouse;
            }
            if (paircounter == 3)
            {
                this.value = HandType.FourOfAKind;
            }
            if(straightcounter >= 5 && flushcounter >= 5)
            {
                this.value = HandType.StraightFlush;
            }
            if (straightcounter >= 5 && this.value == HandType.Flush && jack == true && ace == true)
            {
                this.value = HandType.RoyalFlush;
            }

        }

        private void SortCards()
        {
            hand.Sort(0,hand.Count, new HandComparer());

        }

        
            
           
        
}
}
