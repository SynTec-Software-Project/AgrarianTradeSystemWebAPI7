using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Services.ReviewServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReturnController : ControllerBase
	{
        public readonly IReturnServices _returnServices;
        public ReturnController(IReturnServices returnServices)
        {
            _returnServices = returnServices;
        }

		
		[HttpPost]
		public async Task<ActionResult<Returns>> AddReturn(Returns returnData)
		{
			try
			{
				var result = await _returnServices.CreateReturn(returnData);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
