namespace NinjaAssassins.Models
{
    using System.Collections.Generic;

    public class Player
    {
        private ICollection<Card> hand;
        private int id;
        private string name;

        public Player(string name)
        {
            this.Id++;
            this.Name = name;
            this.hand = new List<Card>();
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            private set
            {
                this.id = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public virtual ICollection<Card> Hand { get; set; }

        public int Score { get; set; }
    }
}
