using System.Threading.Tasks;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Models
{
    public interface IBlogRepository
    {
               Task AddUser(User user);
    }
}