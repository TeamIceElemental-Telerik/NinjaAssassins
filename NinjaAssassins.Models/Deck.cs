namespace NinjaAssassins.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Deck : IEnumerable<Card>, IDeck
    {
        private const int CardsInDeck = 32;

        private static Random random = new Random();
        private List<Card> deck;

        public Deck()
        {
            this.deck = new List<Card>();
            this.FillDeck(CardsInDeck);
            this.deck = this.Shuffle();
        }

        public List<Card> Shuffle()
        {
            return (List<Card>)this.deck
                .OrderBy<Card, int>((item) => random.Next())
                .ToList();
        }

        public void FillDeck(int cardsInDeck)
        {
            int allCardTypesCount = Enum.GetValues(typeof(CardType)).Length;

            Card card;
            for (int i = 1; i <= allCardTypesCount; i++)
            {
                for (int j = 0; j < cardsInDeck / allCardTypesCount; j++)
                {
                    CardType cardType = (CardType)i;
                    card = new Card(cardType);
                    this.deck.Add(card);
                }
            }
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return deck.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return deck.GetEnumerator();
        }
    }
}
