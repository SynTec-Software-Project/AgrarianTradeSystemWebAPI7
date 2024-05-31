using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Services.ReviewServices
{
    public interface IReviewServices
    {
      
		Task<Review?> GetSingleReview(int id);

        Task<List<Review>> GetAllReview();

		Task<List<Review>> AddReview(Review review);

        Task<List<Review>?> UpdateReview(int id, Review request);

        Task<List<Review>?> DeleteReview(int id);
   
        Task<List<ReviewOrdersDto>> GetOrdersToReview();

        Task<Review> AddReviewReply(int id, string reply);

        Task<List<Review>> GetReviewsByProductID(int productId);
        List<ReviewDto> GetAllReviewDetails();
	}

}
