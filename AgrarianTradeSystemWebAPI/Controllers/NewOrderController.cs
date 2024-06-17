using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Services.NewOrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewOrderController : ControllerBase
	{
		private readonly INewOrderServices _orderService;

        public NewOrderController(INewOrderServices orderService)
        {
            _orderService = orderService;
        }
        
		//create new orders
		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] OrderCreationDto orderCreateDto)
		{
			try
			{
				var createdOrder = await _orderService.CreateOrderAsync(orderCreateDto);
				return Ok("order create successfully");
			}
			catch (Exception ex)
			{
				var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
				return BadRequest($"Failed to create order: {errorMessage}");
			}
		}

		//get all couriers
		[HttpGet]
		[Route("getcouriers")]
		public async Task<IActionResult> GetCouriers()
		{
			try
			{
				var couriers = await _orderService.GetCourierListAsync();
				return Ok(couriers);
			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to retrieve courier list: {ex.Message}");
			}
		}

		[HttpPut("update-courier/{orderId}")]
		public async Task<IActionResult> UpdateCourierId(int orderId, string courierID)
		{
			try
			{
				await _orderService.UpdateCourierIdAsync(orderId, courierID);
				return Ok("Courier ID updated successfully");
			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to update courier ID: {ex.Message}");
			}
		}
        //get all notifications
        [HttpGet]
        [Route("getnotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var notifications = await _orderService.getNotifications();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve courier list: {ex.Message}");
            }
        }
        //create new notification
        [HttpPost]
        [Route("Addnotification")]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            try
            {
                 var notifi=await _orderService.createtNotification(notification);
                return Ok("order create successfully");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest($"Failed to create order: {errorMessage}");
            }
        }

        [HttpGet]
        [Route("getnotification/{id}")]
        public async Task<IActionResult> GetNotifications(string id)
        {
            try
            {
                var notifications = await _orderService.getNotification(id);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve courier list: {ex.Message}");
            }
        }

    }
}
