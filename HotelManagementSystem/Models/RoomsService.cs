namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class RoomsService
    {
        public int ServiceId { get; set; }

        public string? ServiceName { get; set; }

        public int RoomTypeId { get; set; }

        public virtual RoomType? RoomType { get; set; }
    }
}
