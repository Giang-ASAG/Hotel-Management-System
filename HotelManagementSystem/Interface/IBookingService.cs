using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>>getBookingbyIdHotel(int id);
        Task<IEnumerable<BookingRoom>> GetBookingroomsbyIDBooking(int id);
        Task<bool> updateStatus(int id,int num);
    }
}
