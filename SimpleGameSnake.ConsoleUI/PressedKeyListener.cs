using SimpleGameSnake.GameLibrary;

namespace SimpleGameSnake.ConsoleUI
{
    public class PressedKeyListener
    {
        private static PressedKeyListener _instance;

        private PressedKeyListener() { }

        public static PressedKeyListener Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PressedKeyListener();
                return _instance;
            }
        }

        public Task ListenKeys(GameManager game, Snake snake)
        {
            while (!game.IsGameOver)
            {
                var pressedKey = Console.ReadKey();
                snake.ChangeDirection(GetDirectionByConsoleKey(pressedKey.Key));
                Task.Delay(120).Wait();
            }

            return Task.CompletedTask;
        }

        private Direction GetDirectionByConsoleKey(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right
            };
        }
    }
}