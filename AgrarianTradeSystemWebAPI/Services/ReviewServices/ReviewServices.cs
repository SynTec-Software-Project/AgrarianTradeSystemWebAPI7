using AgrarianTradeSystemWebAPI.Data;
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
    }
}
