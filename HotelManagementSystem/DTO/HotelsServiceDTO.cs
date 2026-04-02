using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class HotelsServiceDTO
    {
        public int ServiceId { get; set; }

        public int HotelId { get; set; }

        public string ServiceName { get; set; } = null!;

        public string? ServiceInfo { get; set; }

    }
}
