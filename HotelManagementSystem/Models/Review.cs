namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int HotelId { get; set; }

        public int UserId { get; set; }

        public string? Description { get; set; }

        public int StarRating { get; set; }

        public  Hotel? Hotel { get; set; }

        public  User? User { get; set; }
    }
}
