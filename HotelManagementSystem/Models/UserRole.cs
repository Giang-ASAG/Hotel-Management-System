namespace DH52110843_web_quan_ly_khach_san_homestay.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; }

        public string RoleName { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
