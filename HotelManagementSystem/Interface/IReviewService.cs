using DH52110843_web_quan_ly_khach_san_homestay.DTO;
using DH52110843_web_quan_ly_khach_san_homestay.Models;

namespace DH52110843_web_quan_ly_khach_san_homestay.Interface
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewsbyIdHotel(int id);
        Task<IEnumerable<Review>> GetAllReviews();
        Task<bool> deleteReview(int id);
    }
}
