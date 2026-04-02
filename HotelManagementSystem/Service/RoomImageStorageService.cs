using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class RoomImageStorageService : IRoomImageStorageService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoomImageStorageService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RoomImagesStorage> addAsync(RoomImagesStorage room)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            var content = new StringContent(JsonSerializer.Serialize(room), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"RoomImagesStorage/addRoomImagesStorage", content);
            if (response.IsSuccessStatusCode)
            {
                var savedRoomImage = await response.Content.ReadFromJsonAsync<RoomImagesStorage>();
                return savedRoomImage;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> deleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"RoomImagesStorage/deleteRoomImagesStorage/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<RoomImagesStorage> getImage(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"RoomImagesStorage/getImageRoombyId/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoomImagesStorage>();
            }
            else
            {
                return null;
            }
        }

        
    }
}
