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
        public async Task<IActionResult> SendNotification(NotificationDto notificationDto)
        {
            string userId = (notificationDto.OrderStatus == "ready to pickup") ? notificationDto.FarmerID : notificationDto.BuyerID;
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", "You have a new order update. Please confirm.");
            return Ok();
        }

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
    
}
}
