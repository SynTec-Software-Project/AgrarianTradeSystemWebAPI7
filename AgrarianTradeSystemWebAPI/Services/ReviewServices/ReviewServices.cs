using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgrarianTradeSystemWebAPI.Services.ReviewServices
{
    public class ReviewServices : IReviewServices
    {
        private readonly DataContext _context;
        public ReviewServices(DataContext context) {
            _context = context;
        }
        public DataContext Context { get; }


        //get all data
        public async Task<List<Review>> GetAllReview()
        {

            var reviews = await _context.Reviews.ToListAsync(); //retrieve data from db
            return reviews;
        }

        //get single data by id
        public async Task<Review?> GetSingleReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);  //find data from db
            if (review == null)
                return null;
            return review;
        }


        //add
        public async Task<List<Review>> AddReview(Review review)
{
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return await _context.Reviews.ToListAsync();
        }


        //update
        public async Task<List<Review>?> UpdateReview(int id, Review request)
        {
            var review = await _context.Reviews.FindAsync(id); //find data from db
            if (review == null)
                return null;

            review.Comment = request.Comment;
            review.SellerRating = request.SellerRating;
            review.ReviewImageUrl = request.ReviewImageUrl;
            review.ProductRating = request.ProductRating;
            review.DeliverRating = request.DeliverRating;
            

            await _context.SaveChangesAsync();


            return await _context.Reviews.ToListAsync();
        }

        //delete
        public async Task<List<Review>?> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id); //find data from db
            if (review == null)
                return null;

            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();
            return await _context.Reviews.ToListAsync();
        }

        public ReviewDto GetReviewDetailsById(int id)
		{
			var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == id);

			if (review == null)
			{
				return null; // Return null if review with the given ID is not found
			}

			// Map Review entity to ReviewDto object
			var reviewDto = new ReviewDto
			{
				ReviewId = review.ReviewId,
				OrderID = review.OrderID,
				Comment = review.Comment,
				ReviewImageUrl = review.ReviewImageUrl,
				ReviewDate = review.ReviewDate,
				SellerRating = review.SellerRating,
				DeliverRating = review.DeliverRating,
				ProductRating = review.ProductRating
				// Map other properties as needed
			};

			return reviewDto;
		}

		public IEnumerable<ReviewDto> GetAllReviewDetails()
		{
			var reviews = _context.Reviews.Include(r => r.Orders).ToList();
			// Map Review entities to ReviewDto objects here
			// You can use AutoMapper or manually map properties
			return reviews.Select(review => new ReviewDto
			{
				ReviewId = review.ReviewId,
				OrderID = review.OrderID,
				ProductTitle = review.Orders.Product.ProductTitle,
				ProductDescription = review.Orders.Product.ProductDescription,
				ProductImageUrl = review.Orders.Product.ProductImageUrl,
				Comment = review.Comment,
				ReviewImageUrl = review.ReviewImageUrl,
				ReviewDate = review.ReviewDate,
				SellerRating = review.SellerRating,
				DeliverRating = review.DeliverRating,
				ProductRating = review.ProductRating
			});
		}
	}
}
