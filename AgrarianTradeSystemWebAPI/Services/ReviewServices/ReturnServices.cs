using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgrarianTradeSystemWebAPI.Services.ReviewServices
{
	public class ReturnServices : IReturnServices
	{
		private readonly DataContext _context;

        public ReturnServices(DataContext context)
        {
            _context = context;
        }

		public async Task<Returns> CreateReturn(Returns returnData)
		{
			if (returnData == null)
			{
				throw new ArgumentNullException(nameof(returnData));
			}

			try
			{
				_context.Returns.Add(returnData);
				await _context.SaveChangesAsync();
				return returnData;
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while creating the return.", ex);
			}
		}
	}
}
