using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IApiServiceManager _apiService;
        public BookingController(IApiServiceManager apiServiceManager)
        {
            _apiService = apiServiceManager;   
        }
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("UserId");
            var list = await _apiService.HotelService.getAllHotebyUserid(int.Parse(id));
            return View(list);
        }
        public async Task<IActionResult> Bookings(int id)
        {
            ViewBag.HotelId = id;
            var list = await _apiService.BookingService.getBookingbyIdHotel(id);
            var tasks = list.Select(async item =>
            {
                var roomsTask = _apiService.BookingService.GetBookingroomsbyIDBooking(item.BookingId);
                var userTask = _apiService.UserService.getUser(item.UserId);
                var roomTypeTask = _apiService.RoomService.getRoomTypebyid(item.roomTypeId);

                await Task.WhenAll(roomsTask, userTask, roomTypeTask);

                item.Bookingrooms = roomsTask.Result as List<BookingRoom>;
                item.User = userTask.Result;
                item.roomType = roomTypeTask.Result;
            });
            await Task.WhenAll(tasks);
            return View(list);
        }
        public async Task<IActionResult> updateStatus(int id,int hotelId,int num)
        {
            var result = await _apiService.BookingService.updateStatus(id,num);
            if (result)
            {
                TempData["SuccessMessage"] = "Cập nhật trạng thái thành công!";
                return RedirectToAction("Bookings", new { id = hotelId });
            }

            TempData["ErrorMessage"] = "Cập nhật trạng thái thất bại!";
            return RedirectToAction("Bookings", new { id = hotelId });
        }
    }
}
