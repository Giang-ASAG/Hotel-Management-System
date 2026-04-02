using DH52110843_web_quan_ly_khach_san_homestay.Models;
using Microsoft.AspNetCore.Mvc;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> getAllRoombyidHotel(int id);
        Task<IEnumerable<Room>> getAllRoombyRoomTypeId(int id);
        Task<RoomType> getRoomTypebyid(int id);
        Task<Room> getRoombyId(int id);

        Task<bool> addAsync(Room room);
        Task<bool> updateAsync(Room room);
        Task<bool> deleteAsync(int id);


        Task<bool> cancelRoom(int id);
        Task<IEnumerable<RoomType>> getAllRoomTypebyUserIdAsync(int id);
        Task<IEnumerable<RoomType>> getAllRoomTypebyHotelIdAsync(int id);
    }
}
