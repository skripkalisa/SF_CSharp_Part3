using System;
using AppContext = FirstApp.DAL.AppContext;
namespace FirstApp.PLL.Views
{
    public  class MainView
    {

        private static readonly AppContext Context = new();
        private readonly ManageBooksView _booksView = new(Context);
        private readonly ManageUsersView _usersView = new(Context);

        public void Show()
        {

            while(true)
            {
                Console.WriteLine("rd - Reset Database");
                Console.WriteLine("1 - Manage Users");
                Console.WriteLine("2 - Manage Books");
                Console.WriteLine("0 - Exit");
                switch (Console.ReadLine())
                {
                    case "rd":
                    {
                        // Пересоздаем базу
                        Context.Database.EnsureDeleted();
                        Context.Database.EnsureCreated();
                        break;
                    }
                    case "1":
                    {
                        _usersView.ManageUsers();
                        break;
                    }

                    case "2":
                    {
                        _booksView.ManageBooks();
                        break;
                    }
                    case "0":
                    {
                        return;
                    }
                }
            }
        }
    }
}