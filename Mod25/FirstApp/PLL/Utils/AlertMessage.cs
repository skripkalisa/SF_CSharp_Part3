using System;

namespace FirstApp.PLL.Utils
{
    public class AlertMessage
    {
                public static void Show(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }
    }
}