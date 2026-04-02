namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class HotelsService
    {
        public int ServiceId { get; set; }

        public int HotelId { get; set; }

        public string ServiceName { get; set; } = null!;

        public string? ServiceInfo { get; set; }

        public virtual Hotel? Hotel { get; set; }
    }
}
