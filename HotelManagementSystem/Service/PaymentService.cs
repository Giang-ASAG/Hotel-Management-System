using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using System.Net.Http.Headers;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int[]> GetPaymentSumByMonth()
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("Payment/GetPaymentSumByMonth");
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int[]>();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
