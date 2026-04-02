using DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Diagnostics;

namespace DH52110843_web_quan_ly_khach_san_homestay.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IApiServiceManager _apiService;
        
        public AdminController(IApiServiceManager apiService) { _apiService = apiService; }

        public async Task<IActionResult> Index()
        {
            var userCounts = await _apiService.UserService.GetUserCountsByMonth();
            foreach (var item in userCounts)
            {
                Debug.WriteLine(item);
            }
            return View(userCounts);
        }
        [HttpGet("dashboardPayment")]
        public async Task<IActionResult> dashboardPayment()
        {
            var paymentSum = await _apiService.PaymentService.GetPaymentSumByMonth();
            return View(paymentSum);
        }
        public async Task<IActionResult> ExportToExcelAll()
        {

            var statis = await _apiService.PaymentService.GetPaymentSumByMonth();
            var stream = new MemoryStream();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Doanh thu theo tháng");

                // Header
                sheet.Cells[1, 1].Value = "Tháng";
                sheet.Cells[1, 2].Value = "Doanh thu (VNĐ)";
                sheet.Cells[1, 3].Value = "Lợi nhuận (VNĐ)";
                sheet.Row(1).Style.Font.Bold = true;
                double sum = 0;
                double sum1 = 0;
                // Dữ liệu doanh thu từ Tháng 1 đến Tháng 12
                for (int i = 0; i < 12; i++)
                {
                    sheet.Cells[i + 2, 1].Value = $"Tháng {i + 1}";
                    sheet.Cells[i + 2, 2].Value = statis[i];
                    sum += statis[i];
                    sheet.Cells[i + 2, 2].Style.Numberformat.Format = "#,##0 ₫";
                    sheet.Cells[i + 2, 3].Value = statis[i] *0.3;
                    sum1 += statis[i] * 0.3;
                    sheet.Cells[i + 2, 3].Style.Numberformat.Format = "#,##0 ₫";
                }
                sheet.Cells[14, 1].Value = $"Tổng tiền";
                sheet.Cells[14, 2].Style.Numberformat.Format = "#,##0 ₫";
                sheet.Cells[14, 3].Style.Numberformat.Format = "#,##0 ₫";
                sheet.Cells[14, 2].Value = sum;
                sheet.Cells[14, 3].Value = sum1;
                sheet.Cells.AutoFitColumns();
                package.Save();
            }

            stream.Position = 0;
            var now = DateTime.Now;
            string fileName = $"ThongKeDoanhThu_{now.Day}_{now.Month}_{now.Year}.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(stream, contentType, fileName);
        }
        public async Task<IActionResult> Reviews(int? star)
        {
            var review = await _apiService.ReviewService.GetAllReviews();
            foreach (var item in review)
            {
                item.User = await _apiService.UserService.getUser(item.UserId);
                item.Hotel = await _apiService.HotelService.getHotel(item.HotelId);
            }
            if (star.HasValue)
            {
                review = review.Where(r => r.StarRating == star.Value).ToList();
            }
            return View(review);
        }
        public async Task<IActionResult> delete(int id,int ? star)
        {
            var result = await _apiService.ReviewService.deleteReview(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa thành công!";
                return RedirectToAction("Reviews", "Admin", new { star = star });
            }
            else
            {
                TempData["SuccessMessage"] = "Xóa thất bại!";
                return RedirectToAction("Reviews","Admin", new { star = star });
            }
        }

    }
}
