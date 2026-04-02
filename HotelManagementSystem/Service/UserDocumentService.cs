using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class UserDocumentService : IUserDocumentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserDocumentService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserDocumentDTO> addAsync(UserDocumentDTO user)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("UserDocument", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDocumentDTO>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserDocument>> getAllAsync()
        {
            var client = _clientFactory.CreateClient("MyApi");
            var response = await client.GetAsync("UserDocument/getalldocument");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserDocument>>();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> getAsyncbyUserId(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var response = await client.GetAsync($"UserDocument?id={id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> updateActive(int userId, int number)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var response = await client.PutAsJsonAsync($"UserDocument/updateActive?id={userId}&num={number}", "");
            return response.IsSuccessStatusCode;
        }
    }
}
