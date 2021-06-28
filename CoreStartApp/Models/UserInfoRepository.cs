using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreStartApp.Models
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly CoreStartAppContext _context;

        public UserInfoRepository(CoreStartAppContext context)
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