using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Hubs;
using AgrarianTradeSystemWebAPI.Services.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IOrderServices _orderService;

        public NotificationController(IHubContext<NotificationHub> hubContext, IOrderServices orderService)
        {
            _hubContext = hubContext;
            _orderService = orderService;
        }
        

        [HttpPost("send-notification")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDto notification)
        {
            if (notification == null)
            {
                return BadRequest("Notification data is null.");
            }

            string message;
            string userId;

            if (notification.OrderStatus.ToLower() == "ready to pickup")
            {
                userId = notification.FarmerID;
                message = $"Hey, {notification.FarmerFName} {notification.FarmerLName}!\r\n\r\nYour order #{notification.OrderID} is ready to pickup.\r\n";
            }
            else if (notification.OrderStatus.ToLower() == "picked up")
            {
                userId = notification.BuyerID;
                message = $"Hey, {notification.CustomerFName} {notification.CustomerLName}!\r\n\r\nYour order #{notification.OrderID} has been picked up.\r\n";
            }
            else
            {
                // Handle other statuses if needed
                return BadRequest("Invalid order status.");
            }

            try
            {
                await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", message);
                return Ok("Notification sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /*
        [HttpPost("confirm-update")]
        public async Task<IActionResult> ConfirmUpdate(ConfirmUpdateDto confirmUpdateDto)
        {
            // Update the order status in the database if confirmed
            // Assuming you have a service to handle order updates
            bool isUpdated = await _orderService.UpdateOrderStatus(confirmUpdateDto.OrderID, confirmUpdateDto.NewStatus);
            if (isUpdated)
            {
                return Ok();
            }
            return BadRequest("Failed to update order status.");
        }
    */
        [HttpPost("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(string orderId, [FromBody] OrderStatusUpdate update)
        {
            if (update == null)
            {
                return BadRequest("Invalid payload");
            }

            // Assume you have a way to get the userId and farmerId based on the order
            var userId = "userId_based_on_order";
            var farmerId = "farmerId_based_on_order";

            // Update the order status logic here...

            var message = $"Hey, {userId} your order {orderId} is {update.OrderStatus}. Please confirm or decline.";

            if (update.OrderStatus == "Picked Up")
            {
                await _hubContext.Clients.User(farmerId).SendAsync("ReceiveNotification", message);
            }
            else if (update.OrderStatus == "Delivered")
            {
                await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
            }

            return Ok();
        }
        public class OrderStatusUpdate
        {
            public string OrderStatus { get; set; }
        }

    }
}
