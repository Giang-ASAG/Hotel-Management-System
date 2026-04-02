using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> getAllUser();
        Task<bool> addUser(User user);
        Task<bool> deleteUser(int id);
        Task<bool> updateUser(User user);
        Task<User> getUser(int id);
        Task<bool>Active(int id, bool active);
        Task<int[]> GetUserCountsByMonth();
        Task<bool> updatePermissionUser(int id);
    }
}
