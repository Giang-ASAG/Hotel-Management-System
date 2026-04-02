using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> getAllHotebyUserid(int id);
        Task<IEnumerable<Hotel>> getAllHotel();
        Task <Hotel> getHotel(int id);
        Task<Hotel> addAsync(Hotel hotel);
        Task<bool> updateAsync(Hotel hotel);
        Task<bool> deleteAsync(int id);
        Task<JsonDocument> FetchJsonFromApi(string url);
        Task<List<object>> GetAddressSuggestionsAsync(string input);
        Task<(double, double)> GetPlaceDetailAsync(string placeId);

    }
}
