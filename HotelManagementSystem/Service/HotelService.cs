using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class HotelService : IHotelService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _apiKey;
        public HotelService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _apiKey = configuration["Goong:ApiKey"] ?? throw new ArgumentNullException("Goong:ApiKey khong nhan duoc.");
        }

        public async Task<Hotel> addAsync(Hotel hotel)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            var content = new StringContent(JsonSerializer.Serialize(hotel), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"Hotel/AddHotel", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Hotel>();
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                // Ghi lại thông tin lỗi hoặc xử lý theo cách bạn muốn
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorResponse}");
                return null;
            }
        }

        public async Task<IEnumerable<Hotel>> getAllHotebyUserid(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var response = await client.GetAsync("Hotel/getHotelbyIdUser/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Hotel>>();
            }
            else
            {
                return Enumerable.Empty<Hotel>();
            }
        }

        public async Task<IEnumerable<Hotel>> getAllHotel()
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("Hotel/getAllHotel");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Hotel>>();
            }
            else
            {
                return Enumerable.Empty<Hotel>();
            }
        }

        public async Task<Hotel> getHotel(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("Hotel/getHotelbyId/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Hotel>();
            }
            else
            {
                return null;
            }
        }
        public async Task<JsonDocument> FetchJsonFromApi(string url)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(content);
        }

        public async Task<List<object>> GetAddressSuggestionsAsync(string input)
        {
            var url = $"https://rsapi.goong.io/Place/AutoComplete?api_key={_apiKey}&input={Uri.EscapeDataString(input)}";
            var json = await FetchJsonFromApi(url);
            return json.RootElement.GetProperty("predictions")
                .EnumerateArray()
                .Select(p => new
                {
                    description = p.GetProperty("description").GetString(),
                    place_id = p.GetProperty("place_id").GetString()
                })
                .Cast<object>()
                .ToList();
        }

        public async Task<(double,double)> GetPlaceDetailAsync(string placeId)
        {
            var url = $"https://rsapi.goong.io/Place/Detail?place_id={placeId}&api_key={_apiKey}";
            var json = await FetchJsonFromApi(url);
            var location = json.RootElement.GetProperty("result")
                .GetProperty("geometry")
                .GetProperty("location");
            double x = location.GetProperty("lat").GetDouble();
            double y = location.GetProperty("lng").GetDouble();
            Debug.WriteLine($"Ben Service{x}, {y}");
            return (x, y);
        }

        public async Task<bool> updateAsync(Hotel hotel)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(hotel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Hotel/getHotelbyId/{hotel.HotelId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> deleteAsync(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"Hotel/deleteHotel/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
