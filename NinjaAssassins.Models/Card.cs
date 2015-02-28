namespace NinjaAssassins.Models
{
    using System;
    using System.IO;

    public abstract class Card : ICard
    {
        private static int idCounter = 0;

        private string filePath;
        private int rank;
        private CardType cardType;
        private int priority;

        public Card(string filePath, int rank, CardType cardType, int priority)
        {
            this.Id = idCounter++;
            this.CardType = cardType;
            this.FilePath = filePath;
            this.Rank = rank;
            this.Priority = priority;
        }

        public int Id { get; private set; }

        public CardType CardType
        {
            get
            {
                return this.cardType;
            }

            private set
            {
                this.cardType = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }

            protected set
            {
                this.filePath = value;
            }
        }

        public int Rank
        {
            get
            {
                return this.rank;
            }

            protected set
            {
                this.rank = value;
            }
        }

        public int Priority
        {
            get
            {
                return this.priority;
            }

            protected set
            {
                this.priority = value;
            }
        }

        public abstract void Action(Game game);

        public override string ToString()
        {
            // TODO
            return this.CardType.ToString();
        }
    }
}
