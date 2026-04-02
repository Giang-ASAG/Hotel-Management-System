using DH52110843_web_quan_ly_khach_san_homestay.Interface;

namespace DH52110843_web_quan_ly_khach_san_homestay.ApiServiceManager
{
    public class ApiServiceManager : IApiServiceManager
    {
        public IUserService UserService { get; }

        public IHotelService HotelService { get; }

        public IRoomService RoomService { get; }

        public IHotelService_Service HotelService_Service { get; }

        public IRoomsService_Service RoomsService_Service { get; }

        public IRoomTypeService RoomTypeService { get; }

        public IRoomImageService RoomImageService { get; }

        public IRoomImageStorageService RoomImageStorageService { get; }

        public IHotelImageService HotelImageService { get; }

        public IHotelImageStorageService HotelImageStorageService { get; }

        public IPaymentService PaymentService {  get; }

        public IReviewService ReviewService { get; }

        public IBookingService BookingService { get; }

        public IStatisticService StatisticService { get; }

        public IEmailService EmailService { get; }

        public IUserDocumentService UserDocumentService { get; }

        public ApiServiceManager(IUserService UserService, IHotelService HotelService, 
            IRoomService RoomService, IHotelService_Service hotelService_Service, 
            IRoomsService_Service roomsService, IRoomTypeService roomTypeService, IRoomImageService roomImageService,
            IRoomImageStorageService roomImageStorageService,
            IHotelImageService hotelImageService, IHotelImageStorageService hotelImageStorageService,
            IPaymentService paymentService, IReviewService reviewService, IBookingService bookingService,
            IStatisticService statisticService, IEmailService emailService, IUserDocumentService userDocumentService)
        {
            this.UserService = UserService;
            this.HotelService = HotelService;
            this.RoomService = RoomService;
            HotelService_Service = hotelService_Service;
            RoomsService_Service = roomsService;
            RoomTypeService = roomTypeService;
            RoomImageStorageService = roomImageStorageService;
            RoomImageService = roomImageService;
            HotelImageService = hotelImageService;
            HotelImageStorageService = hotelImageStorageService;
            PaymentService = paymentService;
            ReviewService = reviewService;
            BookingService = bookingService;
            StatisticService = statisticService;
            EmailService = emailService;
            UserDocumentService = userDocumentService;
        }
    }
}
