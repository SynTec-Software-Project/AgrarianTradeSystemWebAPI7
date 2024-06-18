using AgrarianTradeSystemWebAPI.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("sendNotification")]
        public async Task<IActionResult> SendNotificationToClient(string userId, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", userId, message);
            return Ok();
        }
    }
}
