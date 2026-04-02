using AutoMapper;
using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        public readonly IApiServiceManager apiService;
        private readonly IMapper _mapper;
        public RoomController(IApiServiceManager apiService, IMapper mapper)
        {
            this.apiService = apiService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.RT = await apiService.RoomTypeService.GetAsync(id);
            var list = await apiService.RoomService.getAllRoombyRoomTypeId(id);
            foreach (var item in list)
            {
                item.RoomType = await apiService.RoomService.getRoomTypebyid(item.RoomTypeId);
            }
            return View(list);
        }
        public async Task<IActionResult> ListRoom(int id)
        {
            var list = await apiService.RoomService.getAllRoombyRoomTypeId(id);
            return View(list);
        }
        //public Task<IActionResult> formEdit(int idhotel, int idroom) {

        //    return View();
        //}

        public async Task<IActionResult> FormAdd(int id)
        {
            //var roomTypes = await apiService.RoomService.getAllRoomTypebyHotelIdAsync(id);
            //ViewBag.DSRT = new SelectList(roomTypes, "RoomTypeId", "TypeName");
            ViewBag.RT = await apiService.RoomService.getRoomTypebyid(id);
            return View();

        }

        public async Task<IActionResult> AddRoom(RoomDTO dTO)
        {
            dTO.Status = false;
            var result = await apiService.RoomService.addAsync(_mapper.Map<Room>(dTO));
            var roomtype = await apiService.RoomService.getRoomTypebyid(dTO.RoomTypeId);
            if (result)
            {
                TempData["SuccessMessage"] = "Thêm phòng thành công!";
                return RedirectToAction("Index", new { id = roomtype.RoomTypeId }); // Sửa lại id truyền vào
            }
            else
            {
                TempData["SuccessMessage"] = "Thêm phòng thất bại"; // Thay đổi thành ErrorMessage
                return RedirectToAction("FormAdd", new { id = roomtype.RoomTypeId}); // Sửa lại id truyền vào

            }
        }
        public async Task<IActionResult> delete(int id, int roomtypeId)
        {

                var result = await apiService.RoomService.deleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa phòng thành công!";
                    return RedirectToAction("Index", new { id = roomtypeId });
                }
                else
                {
                    TempData["SuccessMessage"] = "Xóa phòng thất bại";
                    return RedirectToAction("Index", new { id = roomtypeId });
                }


        }
        public async Task<IActionResult> formEdit(int id, int a)
        {
            if (a > 0)
            {
                var roomTypes = await apiService.RoomService.getAllRoomTypebyHotelIdAsync(a);
                ViewBag.DSRT = new SelectList(roomTypes, "RoomTypeId", "TypeName");
                var r = await apiService.RoomService.getRoombyId(id);
                return View(r);
            }
            else
            {
                var r = await apiService.RoomService.getRoombyId(id);
                return View(r);
            }

        }
        public async Task<IActionResult> update(RoomDTO dTO)
        {
            var t = _mapper.Map<Room>(dTO);
            var result = await apiService.RoomService.updateAsync(t);
            var roomtype = await apiService.RoomService.getRoomTypebyid(dTO.RoomTypeId);
            if (result)
            {
                TempData["SuccessMessage"] = "Sửa phòng thành công!";
                return RedirectToAction("Index", new { id = roomtype.RoomTypeId });
            }
            else
            {
                TempData["SuccessMessage"] = "Sửa phòng thất bại";
                return RedirectToAction("Index", new { id = roomtype.RoomTypeId });
            }
        }
        public async Task<IActionResult> cancel(int id)
        {
            var result = await apiService.RoomService.cancelRoom(id);
            var room = await apiService.RoomService.getRoombyId(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Sửa phòng thành công!";
                return RedirectToAction("Index", new { id = room.RoomTypeId });
            }
            else
            {
                TempData["SuccessMessage"] = "Sửa phòng thất bại";
                return RedirectToAction("Index", new { id = room.RoomTypeId });
            }
        }
    }
}
