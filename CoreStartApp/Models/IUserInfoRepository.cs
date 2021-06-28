using System.Threading.Tasks;

namespace CoreStartApp.Models
{
    public interface IUserInfoRepository
    {
        Task Add(UserInfo userInfo);
    }
}