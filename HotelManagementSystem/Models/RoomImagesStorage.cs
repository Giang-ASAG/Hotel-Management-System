namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class RoomImagesStorage
    {
        public int ImageId { get; set; }

        public string ImagePath { get; set; } = null!;

        public virtual ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
    }
}
