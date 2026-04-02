namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IStatisticService
    {
        Task<int[]> GetPaymentSumByUserId(int id);

        Task<int[]> GetPaymentSumByHotelId(int id);
    }
}
