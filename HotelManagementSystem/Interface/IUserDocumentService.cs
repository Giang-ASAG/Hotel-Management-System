using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IUserDocumentService
    {
        Task<UserDocumentDTO> addAsync(UserDocumentDTO user);
        Task<bool> getAsyncbyUserId(int id);

        Task<IEnumerable<UserDocument>> getAllAsync();
        Task<bool> updateActive(int userId, int number);
    }
}
