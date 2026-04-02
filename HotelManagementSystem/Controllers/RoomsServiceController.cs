using AutoMapper;
using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class RoomsServiceController : Controller
    {
        private readonly IApiServiceManager _apiService;
        private readonly IMapper _mapper;

        public RoomsServiceController(IMapper mapper,IApiServiceManager apiService)
        {
            _mapper = mapper;
            _apiService = apiService;
        }
        public async Task<IActionResult> Index(int id)//Id room type
        {
            ViewBag.RT = await _apiService.RoomService.getRoomTypebyid(id);
            var list =await _apiService.RoomsService_Service.getAllbyRoomTypeIdAsync(id);
            foreach (var item in list)
            {
                item.RoomType = await _apiService.RoomService.getRoomTypebyid(item.RoomTypeId);
            }
            return View(list);
        }
        public async Task<IActionResult> formAdd(int id) {

            ViewBag.RT = await _apiService.RoomService.getRoomTypebyid(id);
            return View();
        }
        public async Task<IActionResult> formEdit(int id)
        {
            var r = await _apiService.RoomsService_Service.getRoomServiceAsync(id);
            ViewBag.RT = await _apiService.RoomService.getRoomTypebyid(r.RoomTypeId);
            //var iduser = HttpContext.Session.GetString("UserId");
            //ViewBag.DSRT = new SelectList(await _apiService.RoomService.getAllRoomTypebyHotelIdAsync(a), "RoomTypeId", "TypeName");
            return View(r);
        }
        public async Task<IActionResult> addRoomService(RoomsServiceDTO dTO)
        {
            var room= _mapper.Map<RoomsService>(dTO);
            var result = await _apiService.RoomsService_Service.addRoomServiceAsync(room);
            if (result)
            {
                TempData["SuccessMessage"] = "Thêm dịch vụ thành công!";
                return RedirectToAction("Index", new { id = dTO.RoomTypeId });
            }
            else
            {
                TempData["SuccessMessage"] = "Thêm dịch vụ thất bại";
                return RedirectToAction("formAdd", new { id = dTO.RoomTypeId });
            }

        }

        public async Task<IActionResult>delete(int id, int roomtypeId)
        {
            var result = await _apiService.RoomsService_Service.deleteRoomServiceAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa dịch vụ thành công!";
                return RedirectToAction("Index", new { id = roomtypeId });
            }
            else
            {
                TempData["SuccessMessage"] = "Thêm dịch vụ thất bại";
                return RedirectToAction("Index", new { id = roomtypeId });
            }
        }
        public async Task<IActionResult>update(RoomsServiceDTO dto)
        {
            var r = _mapper.Map<RoomsService>(dto);
            var result = await _apiService.RoomsService_Service.updateRoomServiceAsync(r);
            if (result)
            {
                TempData["SuccessMessage"] = "Chỉnh sửa dịch vụ thành công!";
                return RedirectToAction("Index", new { id = dto.RoomTypeId });
            }
            else
            {
                TempData["SuccessMessage"] = "Chỉnh sửa dịch vụ thất bại";
                return RedirectToAction("Index", new { id = dto.RoomTypeId });
            }

        }
    }
}
