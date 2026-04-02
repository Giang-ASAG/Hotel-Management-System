namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class BookingRoom
    {
        public int BkroomsId { get; set; }

        public int BookingId { get; set; }

        public int RoomId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Booking Booking { get; set; } = null!;

        public virtual Room Room { get; set; } = null!;
    }
}
