namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class HotelImage
    {
        public int HotelImageId { get; set; }

        public int? HotelId { get; set; }

        public int ImageId { get; set; }

        public virtual Hotel? Hotel { get; set; }

        public virtual HotelImagesStorage? Image { get; set; }
    }
}
