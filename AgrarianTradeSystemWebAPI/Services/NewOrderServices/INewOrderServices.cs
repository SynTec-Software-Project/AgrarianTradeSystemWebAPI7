using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Services.NewOrderServices
{
	public interface INewOrderServices
	{
		Task<Orders> CreateOrderAsync(OrderCreationDto orderCreateModel);
		Task<List<CourierListDto>> GetCourierListAsync();
		Task UpdateCourierIdAsync(int orderId, string newCourierId);
		Task<List<Notification>> getNotifications();
        Task<Notification> createtNotification(Notification notification);

        Task<List<Notification>> getNotification(string email);

    }
}
