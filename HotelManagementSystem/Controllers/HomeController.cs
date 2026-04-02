using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public readonly IApiServiceManager apiService;
        public HomeController(IApiServiceManager apiService)
        {
            this.apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> listhotel()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("Login", "Auth"); // hoặc trả Unauthorized
            }

            int userId = int.Parse(userIdStr);
            var hotels = await apiService.HotelService.getAllHotebyUserid(userId);

            var hotelTasks = hotels.Select(async hotel =>
            {
                var hotelImages = await apiService.HotelImageService.getImagebyIdHotel(hotel.HotelId)
                                    ?? new List<HotelImage>();

                var imageTasks = hotelImages.Select(async img =>
                {
                    img.Image = await apiService.HotelImageStorageService.getImage(img.ImageId);
                    return img;
                });

                hotel.HotelImages = (await Task.WhenAll(imageTasks)).ToList();
                return hotel;
            });

            var result = await Task.WhenAll(hotelTasks);
            return View(result);
        }



    }
}
