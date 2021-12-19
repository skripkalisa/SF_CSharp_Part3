using System;

namespace FirstApp.PLL.Utils
{
    public class SuccessMessage
    {
        public static void Show(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }
    }
}