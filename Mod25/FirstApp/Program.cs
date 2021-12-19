using FirstApp.PLL.Views;

namespace FirstApp
{
    class Program
    {

        private static readonly MainView MainView = new(); 
        static void Main(string[] args)
        {
            MainView.Show();

        }
    }
}