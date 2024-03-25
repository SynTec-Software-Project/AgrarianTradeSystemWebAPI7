using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgrarianTradeSystemWebAPI.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly DataContext _context;

        public OrderServices(DataContext context)
        {
            _context = context;
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
    }
}
