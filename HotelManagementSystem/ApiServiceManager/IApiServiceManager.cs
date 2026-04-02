using DH52110843_web_quan_ly_khach_san_homestay.Interface;

namespace DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager
{
    public interface IApiServiceManager
    {
        IUserService UserService { get; }
        IHotelService HotelService { get; }
        IRoomService RoomService { get; }
        IHotelService_Service HotelService_Service { get; }
        IRoomsService_Service RoomsService_Service { get; }
        IRoomTypeService RoomTypeService {  get; }
        IRoomImageService RoomImageService { get; }
        IRoomImageStorageService RoomImageStorageService { get; }
        IHotelImageService HotelImageService { get; }
        IHotelImageStorageService HotelImageStorageService { get; }
        IPaymentService PaymentService { get; }
        IReviewService ReviewService { get; }
        IBookingService BookingService { get; }
        IStatisticService StatisticService { get; }
        IEmailService EmailService { get; }
        IUserDocumentService UserDocumentService { get; }
    }
}
