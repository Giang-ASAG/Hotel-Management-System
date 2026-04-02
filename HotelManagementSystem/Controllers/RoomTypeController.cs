using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using System.Diagnostics;
using Humanizer;
using Microsoft.AspNetCore.Authorization;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class RoomTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApiServiceManager _apiService;
        public RoomTypeController(IMapper mapper, IApiServiceManager apiService)
        {
            _mapper = mapper;
            _apiService = apiService;

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
            var result = await _apiService.RoomService.getAllRoomTypebyHotelIdAsync(id);
            foreach (var item in result)
            {
                item.Hotel = await _apiService.HotelService.getHotel(item.HotelId);
                item.RoomImages = await _apiService.RoomImageService.GetRoomImagesbyTypeId(item.RoomTypeId) as List<RoomImage>;
                item.Rooms = await _apiService.RoomService.getAllRoombyRoomTypeId(item.RoomTypeId) as List<Room>;
                foreach (var i in item.RoomImages)
                {
                    i.Image = await _apiService.RoomImageStorageService.getImage(i.ImageId);
                }
            }

            return View(result);
        }
        public async Task<IActionResult> formAdd(int hotel)
        {
            //var iduser = HttpContext.Session.GetString("UserId");
            //ViewBag.DSKS = new SelectList(await _apiService.HotelService.getAllHotebyUserid(int.Parse(iduser)), "HotelId", "HotelName");
            ViewBag.DSKS = await _apiService.HotelService.getHotel(hotel);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addRoomType(RoomTypeDTO dTo)
        {
            try
            {

                //if (_mapper == null || _apiService?.RoomTypeService == null)
                //{
                //    TempData["ErrorMessage"] = "Dịch vụ hoặc mapper không được khởi tạo.";
                //    return StatusCode(500, "Service or mapper is null.");
                //}

                var room = _mapper.Map<RoomType>(dTo);
                Debug.WriteLine(room.ToString());
                var result = await _apiService.RoomTypeService.AddAsync(room);
                var imageUrls = await _apiService.RoomTypeService.UploadImagesAsync(dTo.Images, result.RoomTypeId);

                if (imageUrls.Any())
                {
                    foreach (var url in imageUrls)
                    {
                        var roomImage = _mapper.Map<RoomImagesStorage>(new RoomImagesStorageDTO { ImagePath = url });
                        var storageResult = await _apiService.RoomImageStorageService.addAsync(roomImage);

                        System.Diagnostics.Debug.WriteLine($"{storageResult.ImageId}\t{storageResult.ImagePath}");

                        if (storageResult == null)
                        {
                            TempData["ImageErrorMessage"] = "Lỗi khi lưu trữ hình ảnh.";
                            return RedirectToAction("formAdd", new { hotel = dTo.HotelId });
                        }

                        var roomImageLink = new RoomImage { RoomTypeId = result.RoomTypeId, ImageId = storageResult.ImageId };
                        if (!await _apiService.RoomImageService.addAsync(roomImageLink))
                        {
                            TempData["ImageErrorMessage"] = "Lỗi khi liên kết hình ảnh với phòng.";
                            return RedirectToAction("formAdd", new { hotel = dTo.HotelId });
                        }
                    }
                    TempData["ImageSuccessMessage"] = $"Tải lên thành công {imageUrls.Count} hình ảnh!";
                    TempData["SuccessMessage"] = $"Thêm loại phòng {room.TypeName} thành công!";
                    return RedirectToAction("Index", new { id = dTo.HotelId });
                }
                else
                {
                    TempData["ImageErrorMessage"] = TempData["ImageErrorMessage"]?.ToString() ?? "Không có hình ảnh nào được tải lên thành công.";
                    return RedirectToAction("formAdd", new { hotel = dTo.HotelId });
                }

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi thêm loại phòng: {ex.Message}";
                return RedirectToAction("formAdd", new { hotel = dTo.HotelId });
            }
        }
        public async Task<IActionResult> delete(int id, int hotel)
        {
            var result = await _apiService.RoomTypeService.deleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = $"Xóa thành công!";
                return RedirectToAction("Index", new { id = hotel });
            }
            else
            {
                TempData["SuccessMessage"] = $"Xóa thất bại";
                return RedirectToAction("Index", new { id = hotel });

            }
        }
        public async Task<IActionResult> formEdit(int id)
        {

           var r=  await _apiService.RoomService.getRoomTypebyid(id);
            ViewBag.DSKS = await _apiService.HotelService.getHotel(r.HotelId);
            //var iduser = HttpContext.Session.GetString("UserId");
            //ViewBag.DSKS = new SelectList(await _apiService.HotelService.getAllHotebyUserid(int.Parse(iduser)), "HotelId", "HotelName");
            r.RoomImages = await _apiService.RoomImageService.GetRoomImagesbyTypeId(r.RoomTypeId) as List<RoomImage>;
            foreach (var i in r.RoomImages)
            {
                i.Image = await _apiService.RoomImageStorageService.getImage(i.ImageId);
            }
            return View(r);
        }
        public async Task<IActionResult>update(RoomTypeDTO dTO)
        {
            var r = _mapper.Map<RoomType>(dTO);
            var result = await _apiService.RoomTypeService.updateAsync(r);
            if (result)
            {
                TempData["SuccessMessage"] = "Sửa thành công!";
                return RedirectToAction("Index", new { id = dTO.HotelId });
            }
            else
            {
                TempData["SuccessMessage"] = "Sửa thất bại!";
                return RedirectToAction("Index", new { id = dTO.HotelId });
            }
        }
        public async Task<IActionResult> deleteImage(int id, int a, int roomTypeId)
        {
            var result = await _apiService.RoomImageService.deleteAsync(id);
           // var result1 = await _apiService.RoomImageStorageService.deleteAsync(a);

            if (result /*&& result1*/)
            {
                TempData["SuccessMessage"] = "Xóa ảnh thành công!";
                return RedirectToAction("formEdit", new { id = roomTypeId });
            }

            TempData["ErrorMessage"] = "Xóa ảnh thất bại!";
            return RedirectToAction("formEdit", new { id = roomTypeId });
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(IFormFile[] Images, int id)
        {
            // Perform mapping in controller since service no longer has mapper
            var imageUrls = await _apiService.RoomTypeService.UploadImagesAsync(Images, id);

            if (imageUrls.Any())
            {
                foreach (var url in imageUrls)
                {
                    var roomImage = _mapper.Map<RoomImagesStorage>(new RoomImagesStorageDTO { ImagePath = url });
                    var storageResult = await _apiService.RoomImageStorageService.addAsync(roomImage);

                    System.Diagnostics.Debug.WriteLine($"{storageResult.ImageId}\t{storageResult.ImagePath}");

                    if (storageResult == null)
                    {
                        TempData["ImageErrorMessage"] = "Lỗi khi lưu trữ hình ảnh.";
                        return RedirectToAction("formEdit", new { id });
                    }

                    var roomImageLink = new RoomImage { RoomTypeId = id, ImageId = storageResult.ImageId };
                    if (!await _apiService.RoomImageService.addAsync(roomImageLink))
                    {
                        TempData["ImageErrorMessage"] = "Lỗi khi liên kết hình ảnh với phòng.";
                        return RedirectToAction("formEdit", new { id });
                    }
                }
                TempData["ImageSuccessMessage"] = $"Tải lên thành công {imageUrls.Count} hình ảnh!";
            }
            else
            {
                TempData["ImageErrorMessage"] = TempData["ImageErrorMessage"]?.ToString() ?? "Không có hình ảnh nào được tải lên thành công.";
            }

            return RedirectToAction("formEdit", new { id });
        }


        //private async Task<bool> ProcessImages(List<string> imageUrls, int roomTypeId)
        //{
        //    if (_apiService.RoomImageStorageService == null || _apiService.RoomImageService == null)
        //    {
        //        TempData["ImageErrorMessage"] = "Dịch vụ hình ảnh không được khởi tạo.";
        //        return false;
        //    }

        //    foreach (var url in imageUrls)
        //    {
        //        var roomImage = _mapper.Map<RoomImagesStorage>(new RoomImagesStorageDTO { ImagePath = url });
        //        var storageResult = await _apiService.RoomImageStorageService.addAsync(roomImage);

        //        if (storageResult == null)
        //        {
        //            TempData["ImageErrorMessage"] = "Lỗi khi lưu trữ hình ảnh.";
        //            return false;
        //        }

        //        var roomImageLink = new RoomImage { RoomTypeId = roomTypeId, ImageId = storageResult.ImageId };
        //        if (!await _apiService.RoomImageService.addAsync(roomImageLink))
        //        {
        //            TempData["ImageErrorMessage"] = "Lỗi khi liên kết hình ảnh với phòng.";
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }

}
