using AutoMapper;
using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class HotelServiceController : Controller
    {
        private readonly IApiServiceManager _apiService;
        private readonly IMapper _mapper;

        public HotelServiceController(IApiServiceManager apiServiceManager, IMapper mapper)
        {
            _apiService = apiServiceManager;
            _mapper = mapper;
        }


        public async Task<IActionResult> Hotels()
        {
            var id = HttpContext.Session.GetString("UserId");
            var list = await _apiService.HotelService.getAllHotebyUserid(int.Parse(id));
            return View(list);
        }



        public async Task<IActionResult> Index(int id)
        {
            ViewBag.HotelId = id;
            var list = await _apiService.HotelService_Service.getAllHotelServicebyHotelid(id);
            foreach (var item in list)
            {
                item.Hotel = await _apiService.HotelService.getHotel(id);
            }
            return View(list.ToList());
        }


        public async Task<IActionResult> Delete(int id,int hotel) {
            var result = await _apiService.HotelService_Service.DeleteHotelServiceAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa dịch vụ thành công!";
                return RedirectToAction("Index", new { id = hotel });
            }
            TempData["SuccessMessage"] = "Xóa dịch vụ thất bại";
            return RedirectToAction("Index", new { id = hotel });
        }


        public async Task<IActionResult> formEdit(int id)
        {
            var ser = await _apiService.HotelService_Service.GetHotelServiceById(id);
            ViewBag.DSKS = await _apiService.HotelService.getHotel(ser.HotelId);
            //var iduser = HttpContext.Session.GetString("UserId");
            //ViewBag.DSKS = new SelectList(await _apiService.HotelService.getAllHotebyUserid(int.Parse(iduser)), "HotelId", "HotelName");
            return View(ser);
        }


        public async Task<IActionResult>updateHotelService(HotelsServiceDTO hotels)
        {
            var h = _mapper.Map<HotelsService>(hotels);
            var result = await _apiService.HotelService_Service.updateHotelService(h);
            if (result)
            {
                TempData["SuccessMessage"] = "Chỉnh sửa dịch vụ thành công!";
                return RedirectToAction("Index", new { id = hotels.HotelId });
            }
            TempData["SuccessMessage"] = "Sửa dịch vụ thất bại";
            return RedirectToAction("formEdit", new { id = hotels.ServiceId });
        }



        public async Task<IActionResult> formAdd(int hotel)
        {
            ViewBag.DSKS = await _apiService.HotelService.getHotel(hotel);
            //var iduser = HttpContext.Session.GetString("UserId");
            //ViewBag.DSKS = new SelectList(await _apiService.HotelService.getAllHotebyUserid(int.Parse(iduser)), "HotelId", "HotelName");
            return View();
        }


        public async Task<IActionResult> addeHotelService(HotelsServiceDTO hotels)
        {
            var h = _mapper.Map<HotelsService>(hotels);
            var result = await _apiService.HotelService_Service.addHotelServiceAsync(h);
            if (result)
            {
                TempData["SuccessMessage"] = "Thêm dịch vụ thành công!";
                return RedirectToAction("Index", new { id = hotels.HotelId });
            }
            TempData["SuccessMessage"] = "Thêm dịch vụ thất bại";
            return RedirectToAction("formAdd", new { hotel = hotels.HotelId });
        }
    }
}
