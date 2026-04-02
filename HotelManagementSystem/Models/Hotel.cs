using DH52110843_web_quan_ly_khach_san_homestay.Service;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class Hotel
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

        public virtual ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();
        public virtual ICollection<HotelsService> HotelServices { get; set; } = new List<HotelsService>();

        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<RoomType> RoomTypes { get; set; } = new List<RoomType>();

        public virtual User? User { get; set; }
    }
}
