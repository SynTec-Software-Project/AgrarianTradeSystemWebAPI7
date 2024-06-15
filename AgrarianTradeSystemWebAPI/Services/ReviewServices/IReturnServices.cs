using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Services.ReviewServices
{
	public interface IReturnServices
	{
		Task<List<Returns>> GetAllReturns();
		Task<Returns> AddReturn(Returns returnData);
		Task<List<ReturnOrderDto>> GetOrdersToReturn();
		Task<ReturnDto> GetDetailsByOrderId(int orderId);
    }
}
