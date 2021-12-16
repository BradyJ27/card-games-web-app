using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Poker
{
    public class CommunityCards
{
        public List<Card> Cards { get; set; } = new List<Card>();

        public async Task AddCard(Card card)
        {
            Cards.Add(card);
            await Task.Delay(300);
        }

        public void ClearHand()
        {
            Cards.Clear();
        }
    }
}
