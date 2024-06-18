using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AgrarianTradeSystemWebAPI.Hubs
{
    public class NotificationHub : Hub
    {

        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotification(NotificationDto notification)
        {
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
                return;
            }

            await Clients.User(userId).SendAsync("ReceiveMessage", message);

        }

    }
}

 