namespace NinjaAssassins.Models
{
    using System.Collections.Generic;

    public class Player
    {
        private static int idCounter = 0;

        private IList<Card> hand;
        private int id;
        private string name;

        public Player(string name)
        {
            this.Id = idCounter++;
            this.Name = name;
            this.Hand = new List<Card>();
            this.Score = 0;
            this.SkipTurn = false;
            this.DrawDouble = false;
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

        public virtual IList<Card> Hand 
        {
            get
            {
                return this.hand;
            }

            set
            {
                this.hand = value;
            }
        }

        public int Score { get; set; }

        public bool SkipTurn { get; set; }

        public bool DrawDouble { get; set; }
    }
}
