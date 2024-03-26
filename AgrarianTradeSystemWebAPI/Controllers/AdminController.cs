using AgrarianTradeSystemWebAPI.Models.AdminModels;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using AgrarianTradeSystemWebAPI.Services.AdminServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpGet("NewCouriers")]
        public async Task<ActionResult<List<GetCourierModel>>> GetAllNewCouriers()
        {
            return await _adminServices.GetAllNewCouriers();
        }

        [HttpGet("ApprovedCouriers")]
        public async Task<ActionResult<List<GetCourierModel>>> GetAllApprovedCouriers()
        {
            return await _adminServices.GetAllApprovedCouriers();
        }

        [HttpGet("NewFarmers")]
        public async Task<ActionResult<List<GetFarmerModel>>> GetAllNewFarmers()
        {
            return await _adminServices.GetAllNewFarmers();
        }

        [HttpGet("ApprovedFarmers")]
        public async Task<ActionResult<List<GetFarmerModel>>> GetAllApprovedFarmers()
        {
            return await _adminServices.GetAllApprovedFarmers();
        }
    }
}
