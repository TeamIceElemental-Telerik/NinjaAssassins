namespace NinjaAssassins.Helper
{
    using System;
    using System.Media;

    public class Sounds
    {
        public static void GameStartMenu(bool isOn) // GameMenu and GameIntro
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/01.MainWindow-Mortal Kombat-(with Fade).wav");
            if (!isOn)
            {
                player.Stop();
            }
            else
            {
                player.PlayLooping();
            }
        }

        public static void GameRulesMenu(bool isOn) // GameRules
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/Bground Music -DJ Sona Concussive .wav");
            if (!isOn)
            {
                player.Stop();
            }
            else
            {
                player.PlayLooping();
            }
        }

        public static void CARDorEND()// MODEL
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/02.Start - fight.waw");
            player.Play();
        }

        public static void GameBoardStart(bool isOn)
        {
            if (isOn)
            {
                SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/02.Start - fight.wav");
                player.Play();
            }
        }

        //CARDS
        public static void GreenNinjaCardSound()
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/03.01. Green Ninja-excelent.wav");
            player.Play();
        }

        public static void AttackCardSound()
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/03.02.Attack-Figh-getovert.wav");
            player.Play();
        }

        public static void EscapeCardSound()
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/03.03.Escape-toasty.wav");
            player.Play();
        }

        public static void HideCardSound()
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/03.04.Hide - friend.wav");
            player.Play();
        }

        public static void ShuffleCardSound()
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/03.05.Shuffling-cards.wav");
            player.Play();
        }

        public static void SkipTurnCardSound()
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/03.06.SkipTurn-mercy.wav");
            player.Play();
        }

        //Game End Sounds
        public static void GameOverLoseSound(bool isOn)
        {
            if (isOn)
            {
                SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/04.02-General- GameOver-Lost-fatality.wav");
                player.Play();
            }
        }

        public static void GameOverWinSound()
        {
            SoundPlayer player = new SoundPlayer(@"../../../NinjaAssassins.Models/Sounds/04.01.GameOver-Win-flawless.wav");
            player.Play();
        }

        public static void PlayCardSound(int cardType, bool isOn)
        {
            if (isOn)
            {
                switch (cardType)
                {
                    case 2:
                        ShuffleCardSound();
                        break;
                    case 3:
                        SkipTurnCardSound();
                        break;
                    case 4:
                        AttackCardSound();
                        break;
                    case 5:
                        HideCardSound();
                        break;
                    case 6:
                        EscapeCardSound();
                        break;
                    case 7:
                        CARDorEND();
                        break;
                    case 8:
                        GreenNinjaCardSound();
                        break;
                }
            }      
        }
    }
}