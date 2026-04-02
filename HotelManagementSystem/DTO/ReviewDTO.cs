namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        public int? HotelId { get; set; }

        public int UserId { get; set; }
        public int StarRating { get; set; }
        public string? Description { get; set; }
        public string Username { get; set; }
        public string sdt { get; set; }
    }

}
