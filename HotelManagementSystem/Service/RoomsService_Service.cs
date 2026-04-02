using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class RoomsService_Service : IRoomsService_Service
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoomsService_Service(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<RoomsService>> getAllbyRoomServicebyUserIdAsync()
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var userid = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            var response =await client.GetAsync($"RoomService/getAllbyUserId/{Int32.Parse(userid)}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoomsService>>();
            }
            else
            {
                return Enumerable.Empty<RoomsService>();
            }

        }
        public async Task<bool> addRoomServiceAsync(RoomsService rooms)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(rooms), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("RoomService/addRoomService", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> deleteRoomServiceAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"RoomService/deleteRoomService/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateRoomServiceAsync(RoomsService rooms)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(rooms), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"RoomService/updateRoomService/{rooms.ServiceId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<RoomsService> getRoomServiceAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"RoomService/updateRoomService/{id}");
            return await response.Content.ReadFromJsonAsync<RoomsService>();
        }

        public async Task<IEnumerable<RoomsService>> getAllbyRoomTypeIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"RoomService/getAllbyRoomTypeId?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoomsService>>();
            }
            else
            {
                return Enumerable.Empty<RoomsService>();
            }
        }
    }
}
