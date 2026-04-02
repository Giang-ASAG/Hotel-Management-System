using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class BookingService : IBookingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        public BookingService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<Booking>> getBookingbyIdHotel(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _contextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Booking/GetAllbyHotelID/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Booking>>();
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookingRoom>> GetBookingroomsbyIDBooking(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _contextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Booking/getBookingRoom/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<BookingRoom>>();
            }
            throw new NotImplementedException();
        }

        public async Task<bool> updateStatus(int id,int num)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _contextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsync($"Booking/updateStatus?id={id}&num={num}", new StringContent(""));
            return response.IsSuccessStatusCode;
        }
    }
}
