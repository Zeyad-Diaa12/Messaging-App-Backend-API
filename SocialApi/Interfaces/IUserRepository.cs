using SocialApi.DTOs;
using SocialApi.Models;

namespace SocialApi.Interfaces
{
    public interface IUserRepository
    {
        void UpdateUser(AppUser user);
        Task<bool> SaveAllChanges();
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUserNameAsync(string username);
        Task<MemberDto> GetMemberByUserNameAsync(string username);
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
    }
}
