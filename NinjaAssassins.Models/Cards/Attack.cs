namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Attack : Card
    {
        public Attack(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Attack, priority, false)
        {
        }

        public override void Action(Game game)
        {
            Player nextPlayer = game.NextPlayer;
            nextPlayer.DrawDouble = true;
            game.Log = game.PlayerInTurn + "| attacked " + game.NextPlayer + ". \n" + game.NextPlayer + "| needs to draw two cards.";
        }
    }
}
