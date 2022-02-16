using System.Threading.Tasks;

namespace CoreAppStart.Models
{
    public interface IUserInfoRepository
    {
        Task Add(UserInfo userInfo);
    }
}