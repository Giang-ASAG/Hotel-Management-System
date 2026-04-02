using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IHotelImageService
    {
        Task<HotelImage>getImage(int id);
        Task<bool> addAysnc(HotelImage hotelImage);
        Task<bool> deleteAysnc(int id);
        Task<IEnumerable<HotelImage>> getImagebyIdHotel(int id);

    }
}
