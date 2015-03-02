namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Shuffle : Card
    {
        public Shuffle(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Shuffle, priority, false)
        {
        }

        public override void Action(Game game)
        {
            var shuffledDeck = new Deck();
            shuffledDeck.Clear();
            var currentDeck = game.Deck.ToList();
            currentDeck = game.Deck.Shuffle();

            foreach (var card in currentDeck)
            {
                shuffledDeck.Add(card);
            }

            game.Deck = shuffledDeck;
            game.Log = "|The deck was shuffled. Beware!";
        }
    }
}
