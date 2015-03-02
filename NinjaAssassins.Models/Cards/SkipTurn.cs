namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SkipTurn : Card
    {
        public SkipTurn(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.SkipTurn, priority, false)
        {
        }

        public override void Action(Game game)
        {
            Player currentPlayer = game.PlayerInTurn;
            currentPlayer.SkipTurn = true;
        }
    }
}
