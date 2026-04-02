using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IHotelImageStorageService
    {
        Task<IEnumerable<HotelImagesStorageDTO>> getImagebyIdHotel(int id);
        Task<HotelImagesStorageDTO> addAysnc(HotelImagesStorageDTO hotelImage);
        Task<bool> deleteAysnc(int id);
        Task<HotelImagesStorage> getImage(int id);
    }
}
