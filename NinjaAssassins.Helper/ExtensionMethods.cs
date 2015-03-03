namespace NinjaAssassins.Helper
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Text;

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
            Image picture = Image.FromFile(path);
            Console.SetBufferSize((picture.Width * 0x2), (picture.Height * 0x2));
            FrameDimension Dimension = new FrameDimension(picture.FrameDimensionsList[0x0]);
            int frameCount = picture.GetFrameCount(Dimension);
            int left = Console.WindowLeft;
            int top = Console.WindowTop;
            char[] chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };
            picture.SelectActiveFrame(Dimension, 0x0);

            for (int i = 0x0; i < picture.Height; i++)
            {
                for (int x = 0x0; x < picture.Width; x++)
                {
                    Color Color = ((Bitmap)picture).GetPixel(x, i);
                    int Gray = (Color.R + Color.G + Color.B) / 0x3;
                    int Index = (Gray * (chars.Length - 0x1)) / 0xFF;
                    Console.Write(chars[Index]);
                }

                Console.Write('\n');
            }

            Console.SetCursorPosition(left, top);
            Console.Read();
        }

        public static void HandleExceptions(this Exception e, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            ClearConsolePart(x, y, 40, 5);
            PrintOnPosition(x, y, "Uh oh... Something went wrong!", color);
            PrintOnPosition(x, y, e.Message, color);
        }

        public static void PrintMatrix(this char[,] matrix, int x, int y, ConsoleColor color)
        {
            StringBuilder sb = new StringBuilder(); 
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    sb.AppendFormat("{0}", matrix[row, col]);
                }

                PrintOnPosition(x, y + row, sb.ToString(), color);
                sb.Clear();
            } 
        }
    }
}
