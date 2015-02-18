namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Escape : Card
    {
        public Escape(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Escape, priority)
        {
        }

        public override void Action()
        {

        }
    }
}
