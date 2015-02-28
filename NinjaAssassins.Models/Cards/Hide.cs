namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Hide : Card
    {
        public Hide(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Hide, priority)
        {
        }

        public override void Action(Game game)
        {
            Player currentPlayer = game.PlayerInTurn;
            Player nextPlayer = game.NextPlayer;

            var card = CardFactory.Get(CardType.GreenNinja);

            if (nextPlayer.Hand.Contains(card))
            {
                nextPlayer.Hand.Remove(card);
                // TODO
                currentPlayer.Hand.Add(card);
            }
            else
            {
                if (game.CurrentCard.CardType == CardType.NinjaAssassin)
                {
                    game.PlayerInTurn.IsDead = true;
                }
                // Console.WriteLine("Sorry, no place to hide.");
                return;
            }
        }
    }
}
