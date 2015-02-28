namespace NinjaAssassins.Models
{
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

        public Game()
            :this (new Deck())
        {
        }

        public Game(Deck deck)
        {
            this.Deck = deck;
            this.Players = new Player[Constants.TotalPlayers];
            //this.SetInitialGameState();
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

        //private void SetInitialGameState()
        //{
        //    int playerId = random.Next(1, Constants.TotalPlayers + 1);

        //    switch (playerId)
        //    {
        //        case 1:
        //        case 2:
        //        case 3:
        //            this.GameState = GameState.ComputerTurn;
        //            break;
        //        case 4:
        //            this.GameState = GameState.YourTurn;
        //            break;
        //    }

        //    this.PlayerInTurn = this.players[playerId];
        //    this.NextPlayer = playerId + 1 == this.players.Length ? this.players[0] : this.players[playerId + 1];
        //}
    }
}
