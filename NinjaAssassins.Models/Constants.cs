namespace NinjaAssassins.Models
{
    using System.Collections.Generic;

    public abstract class Constants
    {
        public const string CardFilePath = "../../../NinjaAssassins.Models/CardFaces/";
        public const string CardFileExtension = ".txt";
        public const int CardsInDeck = 32;
        public const int TotalPlayers = 4;
        public const string HighScoreFilePath = "../../../NinjaAssassins.Models/HighScores.txt";
        public const string PlayerMoves = "../../../NinjaAssassins.Models/PlayerMoves.txt";
        public const string GameIntro = "../../../NinjaAssassins.Models/GameIntro.txt";
        public const string GameBoard = "../../../NinjaAssassins.Models/GameBoard.txt";

        public static readonly List<CardType> SaviourTypes = new List<CardType> { CardType.GreenNinja, CardType.Escape, CardType.Hide, CardType.Fight };

        public static readonly string[] ComputerPlayersNames = { "Pesho", "Geri", "Stamat" };
    }
}
