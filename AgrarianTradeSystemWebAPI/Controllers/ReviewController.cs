﻿using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Services.ProductServices;
using AgrarianTradeSystemWebAPI.Services.ReviewServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{

		private readonly IReviewServices _reviewServices;
		private readonly IFileServices _fileServices;
		private const string AzureContainerName = "reviews";

		public ReviewController(IReviewServices reviewServices, IFileServices fileServices)
		{
			_reviewServices = reviewServices;
			_fileServices = fileServices;

		}

		[HttpGet("All")]
		public async Task<IActionResult> GetAllReviews()
		{
			var reviews = await _reviewServices.GetAllReview();
			return Ok(reviews);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<List<Review>>> GetSingleReview(int id)
		{
			var result = await _reviewServices.GetSingleReview(id);
			if (result is null)
				return NotFound("review is not found");
			return Ok(result);
		}

		[HttpPost("add-review")]
		public async Task<ActionResult<List<Review>>> AddReview([FromForm] AddReviewDto reviewDto, IFormFile file)
		{
			try
			{
				if (file == null || file.Length == 0)
				{
					return BadRequest("No file uploaded.");
				}

				var fileUrl = await _fileServices.Upload(file, AzureContainerName);

				var review = new Review
				{
					OrderID = reviewDto.OrderID,
					Comment = reviewDto.Comment,
					SellerRating = reviewDto.SellerRating,
					DeliverRating = reviewDto.DeliverRating,
					ProductRating = reviewDto.ProductRating,
					ReviewImageUrl = fileUrl
				};

				var result = await _reviewServices.AddReview(review);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred while processing the request." + ex.Message);
			}

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


		[HttpGet("to-review")]
		public async Task<IActionResult> GetOrdersWithStatusReview()
		{
			var orders = await _reviewServices.GetOrdersToReview();
			return Ok(orders);
		}

		[HttpPut("{id}/add-reply")]
		public async Task<IActionResult> UpdateReviewReply(int id, [FromBody] string reply)
		{
			var updatedReview = await _reviewServices.AddReviewReply(id, reply);

			if (updatedReview == null)
			{
				return NotFound();
			}

			return Ok(updatedReview);
		}

		[HttpGet("product/{productId}")]
		public async Task<IActionResult> GetReviewsByProductID(int productId)
		{
			var reviews = await _reviewServices.GetReviewsByProductID(productId);
			return Ok(reviews);
		}

		[HttpGet("get-history")]
		public IActionResult GetAllReviewDetails()
		{
			var reviewDetails = _reviewServices.GetAllReviewDetails();
			return Ok(reviewDetails);
		}
	}


}

