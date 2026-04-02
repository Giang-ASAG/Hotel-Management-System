using AutoMapper;
using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<HotelsService, HotelsServiceDTO>().ReverseMap();
            CreateMap<RoomsService, RoomsServiceDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<RoomType, RoomTypeDTO>().ReverseMap();
            CreateMap<RoomImage, RoomImageDTO>().ReverseMap();
            CreateMap<RoomImagesStorage, RoomImagesStorageDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<UserDocument, UserDocumentDTO>().ReverseMap();
        }
    }
}
