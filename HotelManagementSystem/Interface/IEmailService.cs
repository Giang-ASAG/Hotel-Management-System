using DH52110843_web_quan_ly_khach_san_homestay.DTO;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IEmailService
    {
        Task<bool> SendOTPAsync(string email);
        Task<bool> VerifyOTPAsync(VerifyOTP verifyOTP);
        Task<bool> checkEmailAsync(string email);
        Task<bool>SendToUser(string email);
        Task<bool>SendToUserFail(string email);
        Task<bool>SendToHost(int id);
    }
}
