using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaAssassins.Helper
{
    public static class ExtensionMethods
    {
        public static void PrintOnPosition(this int x, int y, char symbol, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }

        public static void PrintOnPosition(this int x, int y, string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        public static void ClearConsolePart(this int x, int y, int width, int height)
        {
            int currentLineCursor = y;
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(new string(' ', width));
            }

            Console.SetCursorPosition(x, currentLineCursor);
        }

        public static void DisplayImageOnTheConsole(this string path)
        {
            Image Picture = Image.FromFile(path);
            Console.SetBufferSize((Picture.Width * 0x2), (Picture.Height * 0x2));
            FrameDimension Dimension = new FrameDimension(Picture.FrameDimensionsList[0x0]);
            int FrameCount = Picture.GetFrameCount(Dimension);
            int Left = Console.WindowLeft, Top = Console.WindowTop;
            char[] Chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };
            Picture.SelectActiveFrame(Dimension, 0x0);

            for (int i = 0x0; i < Picture.Height; i++)
            {
                for (int x = 0x0; x < Picture.Width; x++)
                {
                    Color Color = ((Bitmap)Picture).GetPixel(x, i);
                    int Gray = (Color.R + Color.G + Color.B) / 0x3;
                    int Index = (Gray * (Chars.Length - 0x1)) / 0xFF;
                    Console.Write(Chars[Index]);
                }

                Console.Write('\n');
            }

            Console.SetCursorPosition(Left, Top);
            Console.Read();
        }

        public static void HandleExceptions(this Exception e, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            ClearConsolePart(x, y, 40, 5);
            PrintOnPosition(x, y, "Uh oh... Something went wrong!", color);
            PrintOnPosition(x, y, e.Message, color);
        }
    }
}
