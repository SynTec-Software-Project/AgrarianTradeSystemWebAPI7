using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<List<Orders>> GetCourierOrders(string userId);
        Task<List<Orders>> GetBuyerOrders(string userId);
        Task<List<Orders>> GetFarmerOrders(string farmerId);
        Task UpdateOrderStatus(int orderId, string newStatus);
        
    }
}
