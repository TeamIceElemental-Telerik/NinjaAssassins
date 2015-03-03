namespace NinjaAssassins.Helper
{
    using System;

    public abstract class Constants
    {
        public const int CardsInDeck = 32;
        public const int TotalPlayers = 4;
        public const int HandCount = 3;

        public const string CardFilePath = "../../../NinjaAssassins.Models/CardFaces/";
        public const string CardFileExtension = ".txt";

        public const string HighScoreFilePath = "../../../NinjaAssassins.Models/Files/HighScores.txt";
        public const string PlayerMoves = "../../../NinjaAssassins.Models/Files/PlayerMoves.txt";
        public const string GameIntro = "../../../NinjaAssassins.Models/Files/GameIntro.txt";
        public const string GameBoard = "../../../NinjaAssassins.Models/Files/GameBoard.txt";
        public const string GameMenuIntro = "../../../NinjaAssassins.Models/Files/GameMenuIntro.txt";


        public static readonly string[] ComputerPlayersNames = { "Pesho", "Geri", "Stamat" };

        public const int HighScoresCount = 10;
        public const int PlayerMovesCount = 13;

        public const int RightBorderX = 81;

        public const int GameStateX = RightBorderX + 3;
        public const int GameStateY = 1;

        public const int PlayerMovesX = RightBorderX + 3;
        public const int PlayerMovesY = GameStateY + 7;

        public const int CardX = 30;
        public const int CardY = 7;

        public const int PlayersChoiceOptionsX = RightBorderX + 3;
        public const int PlayersChoiceOptionsY = 27;

        public const int ExceptionMessageX = RightBorderX + 3;
        public static readonly int ExceptionMesssageY = Console.WindowHeight - 9;
        public static readonly int ExceptionMesssageWrongChoiceY = Console.WindowHeight - 4;

        public const int IntroX = 25;
        public const int IntroY = 5;

        public const int InitialMenuX = 55;
        public const int InitialMenuY = 25;

        public static readonly int EndGameX = Console.WindowWidth / 2 - 10;

        public static readonly int GoBackX = Console.WindowWidth / 2 - 15;
        public static readonly int GoBackY = Console.WindowHeight - 9;

        public static readonly char[,] DeadMessage = new char[,]
        {
            {'_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', ' ', ' ', '_', '_', '_', ' ', ' '},
            {'|', '|', ' ', '\\', '\\', ' ', '|', '|', ' ', ' ', ' ', ' ', '/', '/', ' ', '\\', '\\', ' ', '|', '|', ' ', '\\', '\\'},
            {'|', '|', ' ', ' ', '|', ' ', '|', '|', '=', '=', ' ', ' ', '|', '|', '=', '|', '|', ' ', '|', '|', ' ', ' ', '|'},
            {'|', '|', ' ', '/', '/', ' ', '|', '|', '_', '_', '_', ' ', '|', '|', ' ', '|', '|', ' ', '|', '|', ' ', '/', '/'},
        };

        public static readonly char[,] ByeByeMessage = new char[,] 
        {
            {'_', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', }, 
            {'|', '|', ' ', ' ', '|', ' ', '\\', '\\', ' ', ' ', '/', '/', ' ', '|', '|', ' ', ' ', ' ', ' ', ' ', ' ', '|', '|', ' ', ' ', '|', ' ', '\\', '\\', ' ', ' ', '/', '/', ' ', '|', '|', ' ', ' ', ' ', },
            {'|', '|', '=', '=', ' ', ' ', ' ', '\\', '\\', '/', '/', ' ', ' ', '|', '|', '=', '=', ' ', ' ', ' ', ' ', '|', '|', '=', '=', ' ', ' ', ' ', '\\', '\\', '/', '/', ' ', ' ', '|', '|', '=', '=', ' ', },
            {'|', '|', '_', '_', '_', '|', ' ', ' ', '|', '|', ' ', ' ', ' ', '|', '|', '_', '_', '_', ' ', ' ', ' ', '|', '|', '_', '_', '_', '|', ' ', ' ', '|', '|', ' ', ' ', ' ', '|', '|', '_', '_', '_', },
        };

        public static readonly char[,] HighScoresMessage = new char[,]
        {
            {'_', '_', ' ', ' ', '_', '_', ' ', '_', '_', ' ', ' ', ' ', '_', '_', '_', ' ', ' ', '_', '_', ' ', ' ', '_', '_', ' ', ' ', ' ', ' ', ' ', '_', '_', ' ', ' ', ' ', ' ', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', '_', ' ', ' ', '_', '_', ' '},
            {'|', '|', ' ', ' ', '|', '|', ' ', '|', '|', ' ', ' ', '/', '/', ' ', '\\', '\\', ' ', '|', '|', ' ', ' ', '|', '|', ' ', ' ', ' ', ' ', '(', '(', ' ', '\\', ' ', ' ', '/', '/', ' ', ' ', ' ', ' ', '/', '/', ' ', '\\', '\\', ' ', ' ', '|', '|', ' ', '\\', '\\', ' ', '|', '|', ' ', ' ', ' ', ' ', '(', '(', ' ', '\\'},
            {'|', '|', '=', '=', '|', '|', ' ', '|', '|', ' ', '(', '(', ' ', '_', '_', '_', ' ', '|', '|', '=', '=', '|', '|', ' ', ' ', ' ', ' ', ' ', '\\', '\\', ' ', ' ', '(', '(', ' ', ' ', ' ', ' ', '(', '(', ' ', ' ', ' ', ')', ')', ' ', '|', '|', '_', '/', '/', ' ', '|', '|', '=', '=', ' ', ' ', ' ', '\\', '\\', ' '},
            {'|', '|', ' ', ' ', '|', '|', ' ', '|', '|', ' ', ' ', '\\', '\\', '_', '|', '|', ' ', '|', '|', ' ', ' ', '|', '|', ' ', ' ', ' ', ' ', '\\', '_', ')', ')', ' ', ' ', '\\', '\\', '_', '_', ' ', ' ', '\\', '\\', '_', '/', '/', ' ', ' ', '|', '|', ' ', '\\', '\\', ' ', '|', '|', '_', '_', '_', ' ', '\\', '_', ')', ')'},
        };

        public static readonly char[,] GameOverMessage = new char[,]
        {
            {' ', ' ', '_', '_', '_', ' ', ' ', ' ', '_', '_', '_', ' ', ' ', '_', '_', '_', ' ', ' ', '_', '_', '_', ' ', ' ', '_', '_', '_', '_', ' ', ' ', ' ', ' ', ' ', ' ', '_', '_', '_', ' ', ' ', ' ', '_', '_', ' ', '_', '_', ' ', ' ', '_', '_', '_', '_', ' ', '_', '_', '_', '_', ' ', },
            {' ', '/', '/', ' ', '\\', '\\', ' ', '/', '/', ' ', '\\', '\\', ' ', '|', '|', '\\', '\\', '/', '/', '|', '|', ' ', '|', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '/', '/', ' ', '\\', '\\', ' ', ' ', '|', '|', ' ', '|', '|', ' ', '|', '|', ' ', ' ', ' ', ' ', '|', '|', ' ', '\\', '\\', },
            {'(', '(', ' ', '_', '_', '_', ' ', '|', '|', '=', '|', '|', ' ', '|', '|', ' ', '\\', '/', ' ', '|', '|', ' ', '|', '|', '=', '=', ' ', ' ', ' ', ' ', ' ', '(', '(', ' ', ' ', ' ', ')', ')', ' ', '\\', '\\', ' ', '/', '/', ' ', '|', '|', '=', '=', ' ', ' ', '|', '|', '_', '/', '/', },
            {' ', '\\', '\\', '_', '|', '|', ' ', '|', '|', ' ', '|', '|', ' ', '|', '|', ' ', ' ', ' ', ' ', '|', '|', ' ', '|', '|', '_', '_', '_', ' ', ' ', ' ', ' ', ' ', '\\', '\\', '_', '/', '/', ' ', ' ', ' ', '\\', 'V', '/', ' ', ' ', '|', '|', '_', '_', '_', ' ', '|', '|', ' ', '\\', '\\', },
        };
    }
}
