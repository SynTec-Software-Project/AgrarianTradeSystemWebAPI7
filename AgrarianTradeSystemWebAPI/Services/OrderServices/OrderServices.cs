// Update the class OrderServices
using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrarianTradeSystemWebAPI.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly DataContext _context;

        public OrderServices(DataContext context)
        {
            _context = context;
        }

        // Get courier's orders
        public async Task<List<Orders>> GetCourierOrders(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
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
        public async Task<List<Orders>> GetFarmerOrders(string farmerId)
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .Include(p => p.Buyer)
                .Include(o => o.Courier)
                .Where(o => o.Product.FarmerID == farmerId)
                .ToListAsync();
            return orders;
        }

        // Get details of a single order for a farmer by orderId
        public async Task<Orders> GetFarmerOrder(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .Include(p => p.Buyer)
                .Include(o => o.Courier)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);
            return order;
        }

        public async Task<Orders> GetCourierOrder(string userId,int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .Include(p => p.Buyer)
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.CourierID == userId);
            return order;
        }

        // Update order status
        public async Task UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
            if (order != null)
            {
                order.OrderStatus = newStatus; // Update the status
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            else
            {
                throw new ArgumentException("Order not found.");
            }
        }
    }
}
