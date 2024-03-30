using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Services.ReviewServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetAllReview()
        {

            return await _reviewServices.GetAllReview();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Review>>> GetSingleReview(int id)
        {
            var result = await _reviewServices.GetSingleReview(id);
            if (result is null)
                return NotFound("review is not found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Review>>> AddReview(Review review)
        {
            var result = await _reviewServices.AddReview(review);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Review>>> UpdateReview(int id, Review request)
        {

            var result = await _reviewServices.UpdateReview(id, request);
            if (result is null)
                return NotFound("review is not found");

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Review>>> DeleteReview(int id)
        {

            var result = await _reviewServices.DeleteReview(id);
            if (result is null)
                return NotFound("hero is not found");
            return Ok(result);
        }

		[HttpGet("all-details")]
		public IActionResult GetReviews()
		{
			try
			{
				var reviews = _reviewServices.GetAllReviewDetails();
				return Ok(reviews);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// GET: api/Review/{id}
		[HttpGet("details/{id}")]
		public IActionResult GetReviewById(int id)
		{
			try
			{
				var review = _reviewServices.GetReviewDetailsById(id);
				if (review == null)
				{
					return NotFound("Review not found");
				}
				return Ok(review);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}


	}
}
