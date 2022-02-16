using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreAppStart.Models
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly CoreAppStartContext _context;
        
        public UserInfoRepository(CoreAppStartContext context)
        {
            _context = context;
        }
        
        public async Task Add(UserInfo userInfo)
        {
            var entry = _context.Entry(userInfo);
            if (entry.State == EntityState.Detached)
                await _context.UserInfos.AddAsync(userInfo);
        
            await _context.SaveChangesAsync();
        }
    }
}