using System.Text;

namespace SimpleGameSnake.ConsoleUI
{
    public class ConsoleSettings
    {
        public ConsoleSettings()
        {
            FieldWidth = 80;
            FieldHeight = 25;
            ApplySettings();
        }

        public int FieldWidth { get; private set; }
        public int FieldHeight { get; private set; }

        public void ApplySettings()
        {
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(FieldWidth, FieldHeight + 4);
            Console.CursorVisible = false;
        }
    }
}
