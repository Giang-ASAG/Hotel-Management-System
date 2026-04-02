namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class RoomImage
    {
        public int RoomImageId { get; set; }

        public int? RoomTypeId { get; set; }

        public int ImageId { get; set; }

        public virtual RoomImagesStorage? Image { get; set; }

        public virtual RoomType? RoomType { get; set; }
    }
}
