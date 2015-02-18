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
            : base(filePath, rank, CardType.Shuffle, priority)
        {
        }

        public override void Action()
        {

        }
    }
}
