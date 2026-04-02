namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IPaymentService
    {
        Task<int[]> GetPaymentSumByMonth();
    }
}
