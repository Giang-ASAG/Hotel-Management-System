namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int? BookingId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? Note { get; set; }

        public double TotalAmount { get; set; }

        public string? PaymentMethod { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
