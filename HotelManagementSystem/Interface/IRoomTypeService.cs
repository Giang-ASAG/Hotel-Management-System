using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetRoomTypesAsync();
        Task<RoomType> AddAsync(RoomType roomType);
        Task<RoomType> GetAsync(int id);
        Task<bool> deleteAsync(int id);
        Task<bool> updateAsync(RoomType roomType);
        Task<List<string>> UploadImagesAsync(IFormFile[] images, int id);


    }
}
