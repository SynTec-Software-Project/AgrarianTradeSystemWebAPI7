// IOrderServices interface
using AgrarianTradeSystemWebAPI.Models;
using MailKit.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgrarianTradeSystemWebAPI.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<List<Orders>> GetCourierOrders(string userId);
        Task<List<Orders>> GetBuyerOrders(string userId);
        Task<List<Orders>> GetFarmerOrders(string farmerId);
        Task UpdateOrderStatus(int orderId, string newStatus);
        Task<Orders> GetFarmerOrder(int orderId); // Added method for fetching a single farmer order
        Task<Orders> GetCourierOrder(string userId,int orderId);
    }
}
