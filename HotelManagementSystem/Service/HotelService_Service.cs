using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class HotelService_Service : IHotelService_Service
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HotelService_Service(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<HotelsService>> getAllHotelServicebyUserid()
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            var id = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response =await client.GetAsync("HotelService/getHotelServicebyUserId/"+ id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<HotelsService>>();
            }
            else
            {
                return Enumerable.Empty<HotelsService>();
            }
        }
        public async Task<bool> DeleteHotelServiceAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"HotelService/deleteHotelService/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<HotelsService> GetHotelServiceById(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"HotelService/gettHotelServicebyId/{id}");
            return await response.Content.ReadFromJsonAsync<HotelsService>();
        }

        public async Task<bool> updateHotelService(HotelsService hotels)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(hotels), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"HotelService/gettHotelServicebyId/{hotels.ServiceId}",
                content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> addHotelServiceAsync(HotelsService hotels)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString ("JWToken");
            var content = new StringContent(JsonSerializer.Serialize(hotels), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"HotelService/addHotelService",content);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<HotelsService>> getAllHotelServicebyHotelid(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("HotelService/getHotelServicebyHotelId/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<HotelsService>>();
            }
            else
            {
                return Enumerable.Empty<HotelsService>();
            }
        }
    }
}
