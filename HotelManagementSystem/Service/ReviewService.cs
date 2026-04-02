using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using System.Net.Http.Headers;

namespace DH52110843_web_quan_ly_khach_san_homestay.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewService(IHttpClientFactory clientFactor, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactor;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> deleteReview(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"Review/deleteReview/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Review");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Review>>();
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsbyIdHotel(int id)
        {
            var client = _clientFactory.CreateClient("MyApi");
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"Review/getAllReviewbyHotelId/{id}");
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ReviewDTO>>();
            }
            throw new NotImplementedException();
        }
    }
}
