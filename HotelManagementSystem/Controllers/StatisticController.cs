using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class StatisticController : Controller
    {
        private readonly IApiServiceManager _apiService;
        public StatisticController(IApiServiceManager apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("UserId");
            var list = await _apiService.HotelService.getAllHotebyUserid(int.Parse(id));
            var statis = await _apiService.StatisticService.GetPaymentSumByUserId(int.Parse(id));
            var model = new HotelStatisticViewModel
            {
                Hotels = (List<Hotel>)list,
                MonthlyRevenue = statis
            };
            return View(model);
        }
        public async Task<IActionResult> Statistics(int id)
        {
            var statis = await _apiService.StatisticService.GetPaymentSumByHotelId(id);
            ViewBag.Id = id;
            return View(statis);
        }
        public async Task<IActionResult> ExportToExcelbyIdUser()
        {
            var id = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Login", "Auth");

            var statis = await _apiService.StatisticService.GetPaymentSumByUserId(int.Parse(id));

            var stream = new MemoryStream();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Doanh thu theo tháng");

                // Header
                sheet.Cells[1, 1].Value = "Tháng";
                sheet.Cells[1, 2].Value = "Doanh thu (VNĐ)";
                sheet.Row(1).Style.Font.Bold = true;

                // Dữ liệu doanh thu từ Tháng 1 đến Tháng 12
                for (int i = 0; i < 12; i++)
                {
                    sheet.Cells[i + 2, 1].Value = $"Tháng {i + 1}";
                    sheet.Cells[i + 2, 2].Value = statis[i];
                    sheet.Cells[i + 2, 2].Style.Numberformat.Format = "#,##0 ₫";
                }

                sheet.Cells.AutoFitColumns();
                package.Save();
            }

            stream.Position = 0;
            var now = DateTime.Now;
            string fileName = $"ThongKe_{now.Day}_{now.Month}_{now.Year}.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(stream, contentType, fileName);
        }
        public async Task<IActionResult> ExportToExcelbyIdHotel(int id)
        {

            var statisTask =  _apiService.StatisticService.GetPaymentSumByHotelId(id);
            var hotelTask =  _apiService.HotelService.getHotel(id);
            await Task.WhenAll(statisTask, hotelTask);
            var statis = statisTask.Result;
            var hotel = hotelTask.Result;
            var stream = new MemoryStream();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Doanh thu theo tháng");

                // Header
                sheet.Cells[1, 1].Value = "Tháng";
                sheet.Cells[1, 2].Value = "Doanh thu (VNĐ)";
                sheet.Row(1).Style.Font.Bold = true;

                // Dữ liệu doanh thu từ Tháng 1 đến Tháng 12
                for (int i = 0; i < 12; i++)
                {
                    sheet.Cells[i + 2, 1].Value = $"Tháng {i + 1}";
                    sheet.Cells[i + 2, 2].Value = statis[i];
                    sheet.Cells[i + 2, 2].Style.Numberformat.Format = "#,##0 ₫";
                }

                sheet.Cells.AutoFitColumns();
                package.Save();
            }

            stream.Position = 0;
            var now = DateTime.Now;
            string fileName = $"ThongKe_{hotel.HotelName}_{now.Day}_{now.Month}_{now.Year}.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(stream, contentType, fileName);
        }


    }
}
