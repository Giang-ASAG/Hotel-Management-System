namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }
        public int roomTypeId { get; set; }
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public double TotalAmount { get; set; }

        public int? Status { get; set; }

        public virtual ICollection<BookingRoom> Bookingrooms { get; set; } = new List<BookingRoom>();

        //public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual User? User { get; set; }
        public virtual RoomType? roomType { get; set; }
    }
}
