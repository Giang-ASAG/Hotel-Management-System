using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using System.Net.Http.Headers;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class StatisticService : IStatisticService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StatisticService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<int[]> GetPaymentSumByHotelId(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Payment/getSumbyHotelId/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int[]>();
            }
            else
            {
                return null;
            }
        }

        public async Task<int[]> GetPaymentSumByUserId(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Payment/getSumbyUserId/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int[]>();
            }
            else
            {
                return null;
            }
        }
    }
}
