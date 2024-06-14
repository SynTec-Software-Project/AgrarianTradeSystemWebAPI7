using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using AgrarianTradeSystemWebAPI.Hubs;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AgrarianTradeSystemWebAPI.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly DataContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        private static ConcurrentDictionary<int, TaskCompletionSource<bool>> _confirmationTasks = new ConcurrentDictionary<int, TaskCompletionSource<bool>>();

        public OrderServices(DataContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }


        //get courier's order
        public async Task<List<Orders>> GetCourierOrders(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Product )
                .ThenInclude(p => p.Farmer)
                .Include(o => o.Buyer)
                .Include(o => o.Courier)
                .Where(o => o.CourierID == userId)
                .ToListAsync();
            return orders;
        }

        // Get buyer's orders
        public async Task<List<Orders>> GetBuyerOrders(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Farmer)
                .Include(o => o.Courier)
                .Where(o => o.BuyerID == userId)
                .ToListAsync();
            return orders;
        }

        // Get farmer's orders
        public async Task<List<Orders>> GetFarmerOrders(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .Include(p => p.Buyer)
                .Include(o => o.Courier)
                .Where(o => o.Product.FarmerID == userId)
                .ToListAsync();
            return orders;
        }

        /*
        //update order status
        public async Task UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
            if (order != null)
            {
             
                    order.OrderStatus = newStatus; // Update the status to "Picked up"
                    await _context.SaveChangesAsync(); // Save changes to the database
             
            }
            else
            {
                throw new ArgumentException("Order not found.");
            }
        }
        */

        // Update order status
        public async Task<bool> UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
            if (order != null)
            {
                string? userId = (order.OrderStatus?.ToLower() == "ready to pickup") ? order.Product?.FarmerID : order.BuyerID;
                string role = (order.OrderStatus.ToLower() == "ready to pickup") ? "farmer" : "buyer";

                if (_hubContext.Clients != null && _hubContext.Clients.User(userId) != null)
                {
                    await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
                    {
                        OrderId = orderId,
                        NewStatus = newStatus,
                        Role = role
                    });
                }

                // Wait for confirmation
                bool isConfirmed = await WaitForConfirmation(orderId);

                if (isConfirmed)
                {
                    order.OrderStatus = newStatus; // Update the status
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return true;
                }
                return false;
            }
            else
            {
                throw new ArgumentException("Order not found.");
            }
        }

        private async Task<bool> WaitForConfirmation(int orderId)
        {
            var tcs = new TaskCompletionSource<bool>();
            _confirmationTasks[orderId] = tcs;
            // Here you can set a timeout if needed
            return await tcs.Task;
        }


        // Method to handle confirmation
        public async Task ConfirmOrderStatus(int orderId, string newStatus)
        {
            // Assuming you have logic to confirm the order status update
            await _hubContext.Clients.All.SendAsync("OrderStatusConfirmed", orderId, newStatus);
        }

        //get courier's order details
        public async Task<List<CourierOrderDto>> GetCourierOrderDetails(int orderId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Product)
                    .ThenInclude(p => p.Farmer)
                    .Include(o => o.Buyer)
                    .Include(o => o.Courier)
                    .Where(o => o.OrderID == orderId)
                    .ToListAsync();

                var orderDtos = orders.Select(order => new CourierOrderDto
                {
                    OrderID = order.OrderID,
                    ProductTitle = order.Product?.ProductTitle,
                    ProductImageUrl = order.Product?.ProductImageUrl,
                    OrderStatus = order.OrderStatus,
                    TotalQuantity = order.TotalQuantity,
                    CustomerFName = order.Buyer?.FirstName,
                    CustomerLName = order.Buyer?.LastName,
                    CustomerAddL1 = order.Buyer?.AddL1,
                    CustomerAddL2 = order.Buyer?.AddL2,
                    CustomerAddL3 = order.Buyer?.AddL3,
                    CustomerPhoneNumber = order.Buyer.PhoneNumber,
                    FarmerFName = order.Product.Farmer?.FirstName,
                    FarmerLName = order.Product.Farmer?.LastName,
                    FarmerAddL1 = order.Product.Farmer?.AddL1,
                    FarmerAddL2 = order.Product.Farmer?.AddL2,
                    FarmerAddL3 = order.Product.Farmer?.AddL3,
                    FarmerPhoneNumber = order.Product.Farmer?.PhoneNumber
                }).ToList();

                return orderDtos;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Failed to get courier order details.", ex);
            }
        }

        //get farmer's order details
        public async Task<List<FarmerOrderDto>> GetFarmerOrderDetails(int orderId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Product)
                    .Include(p => p.Buyer)
                    .Include(o => o.Courier)
                    .Where(o => o.OrderID == orderId)
                    .ToListAsync();

                var orderDtos = orders.Select(order => new FarmerOrderDto
                {
                    OrderID = order.OrderID,
                    ProductTitle = order.Product.ProductTitle,
                    ProductImageUrl = order.Product.ProductImageUrl,
                    OrderStatus = order.OrderStatus,
                    TotalQuantity = order.TotalQuantity,
                    CourierFName = order.Courier?.FirstName,
                    CourierLName = order.Courier?.LastName,
                    CourierAddL1 = order.Courier?.AddL1,
                    CourierAddL2 = order.Courier?.AddL2,
                    CourierAddL3 = order.Courier?.AddL3,
                    CourierPhoneNumber = order.Courier?.PhoneNumber,
                    CustomerFName = order.Buyer?.FirstName,
                    CustomerLName = order.Buyer?.LastName,
                    CustomerAddL1 = order.Buyer?.AddL1,
                    CustomerAddL2 = order.Buyer?.AddL2,
                    CustomerAddL3 = order.Buyer?.AddL3,
                    CustomerPhoneNumber = order.Buyer?.PhoneNumber
                }).ToList();

                return orderDtos;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Failed to get farmer order details.", ex);
            }
        }

        //get buyer's order details
        public async Task<List<BuyerOrderDto>> GetBuyerOrderDetails(int orderId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Product)
                    .ThenInclude(p => p.Farmer)
                    .Include(o => o.Courier)
                    .Where(o => o.OrderID == orderId)
                    .ToListAsync();

                var orderDtos = orders.Select(order => new BuyerOrderDto
                {
                    OrderID = order.OrderID,
                    ProductTitle = order.Product?.ProductTitle,
                    ProductImageUrl = order.Product.ProductImageUrl,
                    OrderStatus = order.OrderStatus,
                    TotalQuantity = order.TotalQuantity,
                    FarmerFName = order.Product.Farmer?.FirstName,
                    FarmerLName = order.Product.Farmer?.LastName,
                    FarmerAddL1 = order.Product.Farmer?.AddL1,
                    FarmerAddL2 = order.Product.Farmer?.AddL2,
                    FarmerAddL3 = order.Product.Farmer?.AddL3,
                    FarmerPhoneNumber = order.Product.Farmer?.PhoneNumber,
                    CourierFName = order.Courier?.FirstName,
                    CourierLName = order.Courier?.LastName,
                    CourierAddL1 = order.Courier?.AddL1,
                    CourierAddL2 = order.Courier?.AddL2,
                    CourierAddL3 = order.Courier?.AddL3,
                    CourierPhoneNumber = order.Courier?.PhoneNumber
                }).ToList();

                return orderDtos;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Failed to get buyer order details.", ex);
            }
        }

    }
}

