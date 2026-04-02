using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<bool> addUser(User user)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("User/AddUser", content);
            return response.IsSuccessStatusCode;

        }

        public async Task<bool> deleteUser(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"User/DeleteUser/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateUser(User user)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsync($"User/GetUserbyId/{user.UserId}",content);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<User>> getAllUser()
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("User/getAllUser");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
            }
            else
            {
                return Enumerable.Empty<User>();
            }
        }

        public async Task<User> getUser(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"User/GetUserbyId/{id}");
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                return null;
            }
        }

        public async Task<int[]> GetUserCountsByMonth()
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("User/GetUserCount");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int[]>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> updatePermissionUser(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            var content = new StringContent("", Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsync($"User/UpdatePermission/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Active(int id, bool active)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            //var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PutAsJsonAsync($"User/Active?id={id}&active={active}","");
            return response.IsSuccessStatusCode;
        }
    }
}
