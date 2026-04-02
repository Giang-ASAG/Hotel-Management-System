using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IApiServiceManager _apiService;
        public ReviewsController(IApiServiceManager apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("UserId");
            var list = await _apiService.HotelService.getAllHotebyUserid(int.Parse(id));
            return View(list);
        }
        public async Task<IActionResult> Reviews(int id)
        {
            var list = await _apiService.ReviewService.GetReviewsbyIdHotel(id);
            foreach (var item in list)
            {
                var u = await _apiService.UserService.getUser(item.UserId);
                item.sdt = u.PhoneNumber;
            }
            return View(list);
        }
    }
}
