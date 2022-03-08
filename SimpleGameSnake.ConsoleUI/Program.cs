using System;
using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        private const int FIELD_WIDTH = 80;
        private const int FIELD_HEIGHT = 25;

        private const string VERTICAL_WALL_SYMBOL = "│";
        private const string HORIZONTAL_WALL_SYMBOL = "──";
        private const string SNAKE_HEAD_SYMBOL = "v<>^";
        private const string SNAKE_BODY_SYMBOL = "o";

        static async Task Main(string[] args)
        {
            while (true)
            {
                ConsoleSettingUp();
                Console.Clear();
                ShowField();
                //Console.WriteLine(SYMBOLS);

                await Task.Delay(800);
            }
        }

        private static void ShowField()
        {
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    if (i == 0 || i == FIELD_HEIGHT - 1)
                        DisplaySymbol(j, i, HORIZONTAL_WALL_SYMBOL);

                    if (j == 0 || j == FIELD_WIDTH - 1)
                        DisplaySymbol(j, i, VERTICAL_WALL_SYMBOL);

                }
            }
        }
        private static void DisplaySymbol(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        private static void ConsoleSettingUp()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
        }
    }
}