using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class User
    {
 
        public int UserId { get; set; }
  
        public string FullName { get; set; } = null!;

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public int? UserRoleId { get; set; }
        public bool? Active { get; set; }

        // public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public DateTime? CreationDate { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual UserRole? UserRole { get; set; }
    }
}
