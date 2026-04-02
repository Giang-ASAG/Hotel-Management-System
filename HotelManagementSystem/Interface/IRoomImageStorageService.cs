using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IRoomImageStorageService
    {
        Task<RoomImagesStorage> getImage(int id);
        Task<bool> deleteAsync(int id);
        Task<RoomImagesStorage> addAsync(RoomImagesStorage room);
    }
}
