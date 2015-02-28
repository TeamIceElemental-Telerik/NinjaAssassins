namespace NinjaAssassins.Models
{
    using System.Collections.Generic;

    public interface IDeck
    {
        void FillDeck(int cardsInDeck);

        List<Card> Shuffle();

        void Add(Card card);

        void Clear();
    }
}
