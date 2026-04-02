using AutoMapper;
using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Interface;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        private readonly IApiServiceManager _apiService;
        private readonly IMapper _mapper;
        public HotelController(IApiServiceManager apiService,IMapper mapper) { _apiService = apiService;_mapper = mapper; }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _apiService.HotelService.getAllHotel());
        }
        [HttpGet]
        public async Task<IActionResult> listhotel(int id)
        {
            var ht = await _apiService.HotelService.getAllHotebyUserid(id);
            foreach (var item in ht)
            {
                item.HotelImages = await _apiService.HotelImageService.getImagebyIdHotel(item.HotelId) as List<HotelImage>;

                foreach (var i in item.HotelImages)
                {
                    i.Image = await _apiService.HotelImageStorageService.getImage(i.ImageId);
                }
            }

            if (ht != null)
            {
                return View(ht);
            }
            ViewBag.Error = "Người này không có khách sạn";
            return View(null);
        }
        [HttpGet]
        public async Task<IActionResult> GetAddressSuggestions(string input)
        {
            var suggestions= await _apiService.HotelService.GetAddressSuggestionsAsync(input);
            return Ok(suggestions);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetPlaceDetail(string input)
        //{

        //    var (x, y) = await _apiService.HotelService.GetPlaceDetailAsync(input);
        //    Debug.WriteLine($"{x}, {y}");
        //    return Ok(new { X = x, Y = y });
        //}
        [HttpGet]
        public async Task<IActionResult> GetPlaceDetail(string placeId)
        {
            var url = $"https://rsapi.goong.io/Place/Detail?place_id={placeId}&api_key={ApiKey}";
            var json = await _apiService.HotelService.FetchJsonFromApi(url);
            var location = json.RootElement.GetProperty("result")
                .GetProperty("geometry")
                .GetProperty("location");
            double lat = location.GetProperty("lat").GetDouble();
            double lng = location.GetProperty("lng").GetDouble();
            Debug.WriteLine($"{lat}, {lng}");
            return Json(new { lat, lng });
        }

        private const string ApiKey = "ljAHxZ7mRckiM7mbQQS8dPVd131TsvJc9fFrInM8";
        public IActionResult formAdd()
        {
            
            return View();
        }
        public async Task<IActionResult> addHotel(HotelDTO dTO)
        {
            var id = HttpContext.Session.GetString("UserId");
            dTO.UserId = int.Parse(id);
            dTO.Status = true;
            Debug.WriteLine(dTO.Status.ToString());
            var hotel = await _apiService.HotelService.addAsync(_mapper.Map<Hotel>(dTO));

                if (hotel != null)
                {
                
                    var imgUrls = await _apiService.RoomTypeService.UploadImagesAsync(dTO.Images, hotel.HotelId);

                    if (imgUrls.Any())
                    {
                        foreach (var item in imgUrls)
                        {
                        Debug.WriteLine(item.ToString());
                            var img = new HotelImagesStorageDTO
                            {
                                ImagePath = item
                            };
                            var ht = await _apiService.HotelImageStorageService.addAysnc(img);
                            if (ht == null)
                            {
                                TempData["ImageErrorMessage"] = "Lỗi khi lưu trữ hình ảnh.";
                                return RedirectToAction("formAdd");
                            }
                            var ImageLink = new HotelImage { HotelId = hotel.HotelId, ImageId = ht.ImageId };

                            if (!await _apiService.HotelImageService.addAysnc(ImageLink))
                            {
                                TempData["ImageErrorMessage"] = "Lỗi khi liên kết hình ảnh với phòng.";
                                return RedirectToAction("formAdd");
                            }
                        }
                        TempData["ImageSuccessMessage"] = $"Tải lên thành công {imgUrls.Count} hình ảnh!";
                        TempData["SuccessMessage"] = $"Thêm loại khach san {hotel.HotelName} thành công!";
                        return RedirectToAction("listhotel","home");
                    }
                    else
                    {
                        TempData["ImageErrorMessage"] = TempData["ImageErrorMessage"]?.ToString() ?? "Không có hình ảnh nào được tải lên thành công.";
                        return RedirectToAction("formAdd");
                    }
                }
            else
            {
                return RedirectToAction("formAdd");

            }
        }
        public async Task<IActionResult> formEdit(int id)
        {
            var hotel = await _apiService.HotelService.getHotel(id);
            hotel.HotelImages = await _apiService.HotelImageService.getImagebyIdHotel(id) as List<HotelImage>;
            foreach (var item in hotel.HotelImages)
            {
                item.Image = await _apiService.HotelImageStorageService.getImage(item.ImageId);
            }
            ViewBag.Star = hotel.Star;
            ViewBag.Status = hotel.Status;
            return View(hotel);
        }
        public async Task<IActionResult>delete(int id)
        {
            var result = await _apiService.HotelService.deleteAsync(id);
            if (result)
            {
                TempData["SuccessMessage"] = $"Xóa khách sạn thành công!";
                return RedirectToAction("listhotel", "home");
            }
            else
            {
                TempData["SuccessMessage"] = $"Xóa khách sạn thất bại!";
                return RedirectToAction("listhotel", "home");
            }
        }
        public async Task<IActionResult> updateHotel(HotelDTO dTO)
        {
            var hotel = _mapper.Map<Hotel>(dTO);
            var response = await _apiService.HotelService.updateAsync(hotel);
            if (response)
            {
                TempData["SuccessMessage"] = $"Sửa khách sạn thành công!";
                return RedirectToAction("listhotel", "home");
            }
            else
            {
                TempData["SuccessMessage"] = $"Sửa khách sạn thất bại!";
                return RedirectToAction("formEdit",new { id = dTO.HotelId });
            }
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
                    var image =new HotelImagesStorageDTO { ImagePath = url };
                    var storageResult = await _apiService.HotelImageStorageService.addAysnc(image);

                    System.Diagnostics.Debug.WriteLine($"{storageResult.ImageId}\t{storageResult.ImagePath}");

                    if (storageResult == null)
                    {
                        TempData["ImageErrorMessage"] = "Lỗi khi lưu trữ hình ảnh.";
                        return RedirectToAction("formEdit", new { id });
                    }

                    var ImageLink = new HotelImage { HotelId = id, ImageId = storageResult.ImageId };
                    if (!await _apiService.HotelImageService.addAysnc(ImageLink))
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
        public async Task<IActionResult> deleteImage(int id, int hotel, int a)
        {
            var result = await _apiService.HotelImageService.deleteAysnc(id);
            if (result)
            {
                var imgstr = await _apiService.HotelImageStorageService.deleteAysnc(a);
                if(imgstr) return RedirectToAction("formEdit", new { id = hotel });
                TempData["SuccessMessage"] = $"Xóa ảnh thành công!";
                return RedirectToAction("formEdit", new { id = hotel });
            }
            TempData["SuccessMessage"] = $"Xóa ảnh thất bại!";
            return RedirectToAction("formEdit", new { id = hotel });

        }

    }


}

