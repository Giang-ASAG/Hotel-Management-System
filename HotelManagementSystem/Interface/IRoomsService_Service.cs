using DH52110843_web_quan_ly_khach_san_homestay.Service;
using DH52110843_web_quan_ly_khach_san_homestay.Models;
namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IRoomsService_Service
    {
        Task<IEnumerable<RoomsService>> getAllbyRoomServicebyUserIdAsync();
        Task<IEnumerable<RoomsService>> getAllbyRoomTypeIdAsync(int id);
        Task<bool> addRoomServiceAsync(RoomsService rooms);
        Task<bool> deleteRoomServiceAsync(int id);
        Task<bool> updateRoomServiceAsync(RoomsService rooms);
        Task<RoomsService> getRoomServiceAsync(int id);
        

    }
}
