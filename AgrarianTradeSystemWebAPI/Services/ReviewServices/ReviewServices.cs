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
        //get orders to return
        public async Task<List<ReviewOrdersDto>> GetOrdersToReview(string buyerId)
        {
            var reviewOrders = await _context.Orders
                .Where(o => o.OrderStatus == "review" && o.BuyerID == buyerId)
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

        public async Task<List<ProductReviewDto>> GetReviewsByProductID(int productId)
        {
            // Step 1: Retrieve OrderIDs for the given ProductID
            var orderIds = await _context.Orders
                .Where(o => o.ProductID == productId)
                .Select(o => o.OrderID)
                .ToListAsync();

            // Step 2: Fetch Reviews using the OrderIDs
            var reviews = await _context.Reviews
                .Where(r => orderIds.Contains(r.OrderID))
                .Include(r => r.Orders) // Include Orders for mapping
                .ThenInclude(o => o.Buyer) // Include Buyer for mapping
                .ToListAsync();

            // Step 3: Map Reviews to ProductReviewDto
            var productReviewDtos = reviews.Select(r => new ProductReviewDto
            {
                ReviewId = r.ReviewId,
                OrderID = r.OrderID,
                BuyerFirstName = r.Orders?.Buyer?.FirstName ?? "Unknown",
                BuyerLastName = r.Orders?.Buyer?.LastName ?? "Unknown",
                Comment = r.Comment,
                ReviewImageUrl = r.ReviewImageUrl,
                ReviewDate = r.ReviewDate,
                SellerRating = r.SellerRating,
                DeliverRating = r.DeliverRating,
                ProductRating = r.ProductRating,
                Reply = r.Reply
            }).ToList();

            // Step 4: Return the list of ProductReviewDto
            return productReviewDtos;
        }



        public async Task<List<ReviewHistoryDto>> GetAllReviewHistory(string buyerId)
        {
            var reviewDetails = await _context.Reviews
                .Include(r => r.Orders)
                .ThenInclude(o => o.Product)
                .Where(r => r.Orders.BuyerID == buyerId)
                .Select(r => new ReviewHistoryDto
                {
                    ReviewId = r.ReviewId,
                    OrderID = r.OrderID,
                    ProductTitle = r.Orders.Product != null ? r.Orders.Product.ProductTitle : null,
                    ProductDescription = r.Orders.Product != null ? r.Orders.Product.ProductDescription : null,
                    ProductImageUrl = r.Orders.Product != null ? r.Orders.Product.ProductImageUrl : null,
                    Comment = r.Comment,
                    ReviewImageUrl = r.ReviewImageUrl,
                    ReviewDate = r.ReviewDate,
                    SellerRating = r.SellerRating,
                    DeliverRating = r.DeliverRating,
                    ProductRating = r.ProductRating
                })
                .ToListAsync();

            return reviewDetails;
        }


    }

}
