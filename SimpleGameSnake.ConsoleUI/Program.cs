﻿using SimpleGameSnake.GameLibrary;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    internal class Program
    {
        private const int UPDATEPERSECONDS = 100;

        private const char VERTICAL_WALL_SYMBOL = '│';
        private const char HORIZONTAL_WALL_SYMBOL = '─';
        private const char SNAKE_HEAD_UP_SYMBOL = '^';
        private const char SNAKE_HEAD_DOWN_SYMBOL = 'v';
        private const char SNAKE_HEAD_LEFT_SYMBOL = '<';
        private const char SNAKE_HEAD_RIGHT_SYMBOL = '>';
        private const char SNAKE_BODY_SYMBOL = 'o';
        private const char FOOD_SYMBOL = '@';

        private static GameManager _game;

        static async Task Main(string[] args)
        {
            Snake snake = new Snake(ConsoleSettings.Instance.FieldWidth / 2, ConsoleSettings.Instance.FieldHeight / 2);
            _game = new GameManager();

            ConsoleSettings.Instance.SettingUpConsole();

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            Console.Clear();
            ShowField();
            ShowSnake(snake);

            Task.Run(() => PressedKeyListener.Instance.ListenKeys(_game, snake));

            while (!_game.IsGameOver)
            {
                if (_game.IsFoodOnField == false)
                    GenerateFood();

                DisplaySymbol(_game.Food.Position.X, _game.Food.Position.Y, FOOD_SYMBOL);

                MoveSnake(snake);

                ShowScore();

                await Task.Delay(UPDATEPERSECONDS);
            }
        }

        private static void GenerateFood()
        {
            _game.CreateFood(1, ConsoleSettings.Instance.FieldWidth - 1, 1, ConsoleSettings.Instance.FieldHeight - 1);
        }



        private static void ShowField()
        {
            for (int i = 0; i < ConsoleSettings.Instance.FieldHeight; i++)
            {
                for (int j = 0; j < ConsoleSettings.Instance.FieldWidth; j++)
                {
                    if (i == 0 || i == ConsoleSettings.Instance.FieldHeight - 1)
                        DisplaySymbol(j, i, HORIZONTAL_WALL_SYMBOL);

                    if (j == 0 || j == ConsoleSettings.Instance.FieldWidth - 1)
                        DisplaySymbol(j, i, VERTICAL_WALL_SYMBOL);

                }
            }
        }

        private static void DisplaySymbol(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        private static void ShowSnake(Snake snake)
        {
            DisplaySymbol(snake.Head.X, snake.Head.Y, GetHeadSymbolByDirection(snake.CurrentDirection));

            for (int i = 1; i <= snake.TailLength; i++)
                DisplaySymbol(snake.Head.X - i, snake.Head.Y, SNAKE_BODY_SYMBOL);
        }

        private static char GetHeadSymbolByDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => SNAKE_HEAD_UP_SYMBOL,
                Direction.Down => SNAKE_HEAD_DOWN_SYMBOL,
                Direction.Left => SNAKE_HEAD_LEFT_SYMBOL,
                Direction.Right => SNAKE_HEAD_RIGHT_SYMBOL,
                _ => throw new Exception(),
            };
        }

        private static void MoveSnake(Snake snake)
        {
            snake.MoveSnake();

            ScreenEdgeCheck(snake);

            SelfBodyCheck(snake);

            var foodHasBeenEaten = FoodCheck(snake);


            if (_game.IsGameOver)
            {
                DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, GetHeadSymbolByDirection(snake.CurrentDirection));
                return;
            }

            DisplaySymbol(snake.Head.X, snake.Head.Y, GetHeadSymbolByDirection(snake.CurrentDirection));
            DisplaySymbol(snake.PrevHead.X, snake.PrevHead.Y, SNAKE_BODY_SYMBOL);
            if (!foodHasBeenEaten)
                DisplaySymbol(snake.LastPart.X, snake.LastPart.Y, ' ');
        }

        private static void ScreenEdgeCheck(Snake snake)
        {
            if (snake.Head.X == 0 || snake.Head.X == ConsoleSettings.Instance.FieldWidth - 1)
                _game.StopGame();

            if (snake.Head.Y == 0 || snake.Head.Y == ConsoleSettings.Instance.FieldHeight - 1)
                _game.StopGame();
        }

        private static void SelfBodyCheck(Snake snake)
        {
            if (snake.Body.Contains(snake.Head))
                _game.StopGame();
        }

        private static bool FoodCheck(Snake snake)
        {
            if (snake.Head.X == _game.Food.Position.X && snake.Head.Y == _game.Food.Position.Y)
            {
                _game.PutAwayFood();
                snake.Eat();

                return true;
            }

            return false;
        }

        private static void ShowScore()
        {
            Console.SetCursorPosition(3, ConsoleSettings.Instance.FieldHeight + 1);
            Console.WriteLine($"Score - {_game.Score}");
        }
    }
}