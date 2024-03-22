using AgrarianTradeSystemWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Services.ReviewServices
{
    public interface IReviewServices
    {
        Task<List<Review>> GetAllReview();
        Task<Review?> GetSingleReview(int id);

        Task<List<Review>> AddReview(Review review);

        Task<List<Review>?> UpdateReview(int id, Review request);

        Task<List<Review>?> DeleteReview(int id);
    }
}
