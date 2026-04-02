using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Cloudinary _cloudinary;
        public RoomTypeService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor, Cloudinary cloudinary)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _cloudinary = cloudinary;
        }
        public async Task<RoomType> AddAsync(RoomType roomType)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(roomType), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("RoomType/addRoomType", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoomType>();
            }
            else return null;

        }

        public async Task<bool> deleteAsync(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"RoomType/deleteRoomType?id={id}");
            return response.IsSuccessStatusCode;

        }

        public async Task<RoomType> GetAsync(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("RoomType/getRoomTypebyId/"+id);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<RoomType>();
            }
            else return null;
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypesAsync()
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("RoomType");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoomType>>();
            }
            else
            {
                return Enumerable.Empty<RoomType>();
            }
        }

        public async Task<bool> updateAsync(RoomType roomType)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(roomType), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"RoomType/getRoomTypebyId/{roomType.RoomTypeId}",
                content);

            return response.IsSuccessStatusCode;
        }



        //Them hinh anh
        public async Task<List<string>> UploadImagesAsync(IFormFile[] images, int id)
        {
            string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            var imageUrls = new List<string>();

            foreach (var file in images)
            {
                if (file.Length == 0) continue;

                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension))
                {
                    continue; // Errors are handled in controller
                }

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = $"image_{id}_{Guid.NewGuid()}"
                };

                var uploadResult = await Task.Run(() => _cloudinary.Upload(uploadParams));
                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue; // Errors are handled in controller
                }

                imageUrls.Add(uploadResult.SecureUrl.ToString());
            }

            return imageUrls;
        }
    }
}
