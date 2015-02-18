namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GreenNinja : Card
    {
        public GreenNinja(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.GreenNinja, priority)
        {
        }

        public override void Action()
        {

        }
    }
}
