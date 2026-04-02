namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
        public bool? Active { get; set; }
        public int? UserRoleId { get; set; }
    }
}
