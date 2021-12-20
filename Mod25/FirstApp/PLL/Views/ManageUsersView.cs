using System;
using FirstApp.DAL.Entities;
using FirstApp.DAL.Repositories;
using AppContext = FirstApp.DAL.AppContext;

namespace FirstApp.PLL.Views
{
    public class ManageUsersView
    {
        private readonly UserRepository _userRepository;

        public ManageUsersView(AppContext context)
        {
            _userRepository = new(context);

        }
        public void ManageUsers()
        {
            while (true)
            {
                Console.WriteLine("1 - Add new User");
                Console.WriteLine("2 - Update User");
                Console.WriteLine("3 - Delete User");
                Console.WriteLine("4 - View all users");
                Console.WriteLine("0 - Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        var userEntity = CreateUserEntity();
                        _userRepository.AddUser(userEntity);
                        break;

                    case "2":
                        GetUsers(_userRepository);
                        Console.WriteLine("Enter user ID");
                        var id = GetInteger();
                        userEntity = CreateUserEntity();
                        if (id != 0) userEntity.Id = id;
                        _userRepository.UpdateUser(userEntity);
                        break;

                    case "3":
                        Console.WriteLine("Find user Id in the list");
                        GetUsers(_userRepository);
                        Console.WriteLine("Enter user ID");
                        id = GetInteger();
                        if (id != 0) _userRepository.DeleteById(id);
                        break;
                
                    case "4":
                        GetUsers(_userRepository);
                        break;            
                
                    case "0":
                        return;
                }
            }
        }
                                               
        private int GetInteger()
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch (NotFiniteNumberException e)
            {
                Console.WriteLine($"Cannot convert your input to Number: {e.Message}");
            }

            return id;
        }

        private static void GetUsers(UserRepository userRepository)
        {
            var users = userRepository.FindAll();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");
            }
        }

        private UserEntity CreateUserEntity()
        {
    
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();


            return new UserEntity
            {
                Name = name,
                Email = email
            };
        }
    }
}