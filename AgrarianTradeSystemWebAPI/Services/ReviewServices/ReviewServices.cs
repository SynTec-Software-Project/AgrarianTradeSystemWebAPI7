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
		public ReviewServices(DataContext context)
		{
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

		 
		//update review
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

		//delete review
		public async Task<List<Review>?> DeleteReview(int id)
		{
			var review = await _context.Reviews.FindAsync(id); //find data from db
			if (review == null)
				return null;

			_context.Reviews.Remove(review);

			await _context.SaveChangesAsync();
			return await _context.Reviews.ToListAsync();
		}
		//get orders to review
		public async Task<List<ReviewOrdersDto>> GetOrdersToReview()
		{
			var reviewOrders = await _context.Orders
				.Where(o => o.OrderStatus == "review")
				.Select(o => new ReviewOrdersDto
				{
					OrderID = o.OrderID,
					ProductID = o.ProductID,
					ProductName = o.Product.ProductTitle,
					ProductDescription = o.Product.ProductDescription,
					ProductImageUrl = o.Product.ProductImageUrl
				})
				.ToListAsync();

			return reviewOrders;
		}

		public async Task<Review> AddReviewReply(int id, string reply)
		{
			var review = await _context.Reviews.FindAsync(id);

			if (review == null)
			{
				return null; // Return null if review with the given ID is not found
			}

			review.Reply = reply;
			await _context.SaveChangesAsync();

			return review;
		}

		public async Task<List<Review>> GetReviewsByProductID(int productId)
		{
			return await _context.Reviews
				.Where(r => r.Orders != null && r.Orders.ProductID == productId)
				.ToListAsync();
		}
	}

}
