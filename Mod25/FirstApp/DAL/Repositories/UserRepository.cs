using System.Collections.Generic;
using System.Linq;
using FirstApp.BLL.Models;
using FirstApp.DAL.Entities;


namespace FirstApp.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppContext db;

        public UserRepository(AppContext context)
        {
            db = context;
        }
        public int AddUser(UserEntity userEntity)
        {
            // using var db = new AppContext();
            var user = new User
            {
                Name = userEntity.Name,
                Email = userEntity.Email
            };

            db.Users.Add(user);
            return db.SaveChanges();
        }
        
        public User FindById(int id)
        {
            // using var db = new AppContext();
            var user = db.Users.Where(user=> user.Id == id);
            return user.FirstOrDefault();
        }

        public int UpdateUser(UserEntity userEntity)
        {
            // using var db = new AppContext();
            var user = FindById(userEntity.Id);
            user.Name = userEntity.Name;
            return db.SaveChanges();

        }

        public IEnumerable<User> FindAll()
        {
            // using var db = new AppContext();
            var users = db.Users.ToList();
            return users;
        }

        public int DeleteById(int id)
        {
            // using var db = new AppContext();
            var user = FindById(id);
            db.Remove(user);
            return db.SaveChanges();
        }
    }
    
    public interface IUserRepository
    {
        int AddUser(UserEntity userEntity);
        IEnumerable<User> FindAll();
        User FindById(int id);
        int UpdateUser(UserEntity userEntity);
        int DeleteById(int id);
    }
}