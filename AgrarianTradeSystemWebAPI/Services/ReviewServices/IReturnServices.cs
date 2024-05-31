using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Services.ReviewServices
{
	public interface IReturnServices
	{
		Task<Returns> CreateReturn(Returns returnData);
	}
}
