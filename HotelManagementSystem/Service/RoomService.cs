using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class RoomService : IRoomService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoomService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<IEnumerable<Room>> getAllRoombyidHotel(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("Room/GetAllRoomByHotelId/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Room>>();
            }
            else
            {
                return Enumerable.Empty<Room>();
            }
        }
        public async Task<IEnumerable<Room>> getAllRoombyRoomTypeId(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("Room/getAllRoombyRoomTypeId/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Room>>();
            }
            else
            {
                return Enumerable.Empty<Room>();
            }
        }

        public async Task<IEnumerable<RoomType>> getAllRoomTypebyHotelIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("RoomType/getAllRoomTypebyHotelId/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoomType>>();
            }
            else
            {
                return Enumerable.Empty<RoomType>();
            }
        }

        public async Task<IEnumerable<RoomType>> getAllRoomTypebyUserIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("RoomType/getAllRoomTypebyUserId/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoomType>>();
            }
            else
            {
                return Enumerable.Empty<RoomType>();
            }
        }

        public async Task<RoomType> getRoomTypebyid(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("RoomType/getRoomTypebyId/" + id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoomType>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> addAsync(Room room)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(room), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Room/addRoom", content);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> deleteAsync(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"Room/deleteRoom/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> updateAsync(Room room)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(room), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Room/getRoombyId/{room.RoomId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<Room> getRoombyId(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Room/getRoombyId/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Room>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> cancelRoom(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsJsonAsync($"Room/cancelRoom/{id}", "");
            return response.IsSuccessStatusCode;

        }
    }
}
