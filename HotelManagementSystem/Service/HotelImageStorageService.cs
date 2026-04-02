using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class HotelImageStorageService : IHotelImageStorageService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HotelImageStorageService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<HotelImagesStorageDTO> addAysnc(HotelImagesStorageDTO hotelImage)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(hotelImage), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("HotelImageStorage/addHotelImageStorage", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HotelImagesStorageDTO>();
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> deleteAysnc(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"HotelImageStorage/deleteHotelImageStorage/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<HotelImagesStorage> getImage(int id)
        {

            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"HotelImageStorage/getHotelImageStoragebyId/{id}");
            if (response.IsSuccessStatusCode)
            {
                var o = await response.Content.ReadFromJsonAsync<HotelImagesStorage>();
                return o;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<HotelImagesStorageDTO>> getImagebyIdHotel(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"HotelImage/getImagebyIdHotel/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<HotelImagesStorageDTO>>();
            }
            else
            {
                return null;
            }
        }
    }
}
