using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class HotelStatisticViewModel
    {
        public List<Hotel> Hotels { get; set; }
        public int[] MonthlyRevenue { get; set; }
    }
}
