using System.Collections.Generic;
using System.Linq;
using FirstApp.BLL.Models;
using FirstApp.DAL.Entities;


namespace FirstApp.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppContext _db;

        public UserRepository(AppContext context)
        {
            _db = context;
        }
        public int AddUser(UserEntity userEntity)
        {
            var user = new User
            {
                Name = userEntity.Name,
                Email = userEntity.Email
            };

            _db.Users.Add(user);
            return _db.SaveChanges();
        }
        
        public User FindById(int id)
        {
            var user = _db.Users.Where(user=> user.Id == id);
            return user.FirstOrDefault();
        }

        public int UpdateUser(UserEntity userEntity)
        {
            var user = FindById(userEntity.Id);
            user.Name = userEntity.Name;
            return _db.SaveChanges();

        }

        public IEnumerable<User> FindAll()
        {
            var users = _db.Users.ToList();
            return users;
        }

        public int DeleteById(int id)
        {
            var user = FindById(id);
            _db.Remove(user);
            return _db.SaveChanges();
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