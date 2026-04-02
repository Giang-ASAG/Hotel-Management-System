using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IHotelService_Service
    {
        Task<IEnumerable<HotelsService>> getAllHotelServicebyUserid();
        Task<IEnumerable<HotelsService>> getAllHotelServicebyHotelid(int id);
        Task<bool> DeleteHotelServiceAsync(int id);
        Task<HotelsService> GetHotelServiceById(int id);
        Task<bool> updateHotelService(HotelsService hotels);
        Task<bool> addHotelServiceAsync(HotelsService hotels);
    }
}
