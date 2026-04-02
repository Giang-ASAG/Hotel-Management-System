using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IRoomImageService
    {
        Task<IEnumerable<RoomImage>>GetRoomImagesbyTypeId(int id);
        Task<bool> deleteAsync(int id);
        Task<bool> addAsync(RoomImage room);

    }
}
