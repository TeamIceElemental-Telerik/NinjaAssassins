namespace NinjaAssassins.Models
{
    using NinjaAssassins.Helper;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private static Random random = new Random();
        private Deck deck;
        private Player[] players;
        private Player playerInTurn;
        private Player nextPlayer;
        private Card currentCard;
        private bool played;
        private string log;

        public Game()
            :this (new Deck())
        {
        }

        public Game(Deck deck)
        {
            this.Deck = deck;
            this.Players = new Player[Constants.TotalPlayers];
            this.IsCardPlayed = false;
        }

        public GameState GameState { get; set; }

        public virtual Deck Deck
        {
            get
            {
                return this.deck;
            }

            set
            {
                this.deck = value;
            }
        }

        public virtual Player[] Players
        {
            get
            {
                return this.players;
            }

            set
            {
                this.players = value;
            }
        }

        public Player PlayerInTurn
        {
            get
            {
                return this.playerInTurn;
            }

            set
            {
                this.playerInTurn = value;
            }
        }

        public Player NextPlayer
        {
            get
            {
                return this.nextPlayer;
            }

            set
            {
                this.nextPlayer = value;
            }
        }

        public Card CurrentCard
        {
            get
            {
                return this.currentCard;
            }

            set
            {
                this.currentCard = value;
            }
        }

        public bool IsCardPlayed
        {
            get
            {
                return this.played;
            }

            set
            {
                this.played = value;
            }
        }

        public string Log
        {
            get
            {
                return this.log;
            }

            set
            {
                this.log = value;
            }
        }
    }
}
