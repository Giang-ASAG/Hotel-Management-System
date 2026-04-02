namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class HotelDTO
    {
        public int HotelId { get; set; }

        public int? UserId { get; set; }

        public string HotelName { get; set; } = null!;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Description { get; set; }

        public double? XCoordinate { get; set; }

        public double? YCoordinate { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public decimal? Star { get; set; }
        public IFormFile[] Images { get; set; }
    }
}
