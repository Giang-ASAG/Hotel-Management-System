namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class RoomDTO
    {
        public int RoomId { get; set; }

        //public int? HotelId { get; set; }

        public int RoomTypeId { get; set; }

        public string RoomNumber { get; set; } = null!;

        public bool Status { get; set; }
        public bool Active { get; set; }
    }
}
