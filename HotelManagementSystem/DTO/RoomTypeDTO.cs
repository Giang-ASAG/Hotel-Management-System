namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class RoomTypeDTO
    {
        public int RoomTypeId { get; set; }

        public string TypeName { get; set; } = null!;

        public string? RoomInfo { get; set; }

        public double Price { get; set; }

        public int Capacity { get; set; }

        public int HotelId { get; set; }
        public IFormFile[] Images { get; set; }
    }
}
