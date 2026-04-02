namespace DH52110843_web_quan_ly_khach_san_homestay.DTO
{
    public class UserDocumentDTO
    {
        public string CccdNumber { get; set; } = null!;

        public string? TaxCode { get; set; }

        public byte[]? ImageBase64 { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? BankName { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public int Active { get; set; }

    }
}
