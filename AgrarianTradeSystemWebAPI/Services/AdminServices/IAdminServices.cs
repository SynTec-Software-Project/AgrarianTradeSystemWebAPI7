using AgrarianTradeSystemWebAPI.Models.AdminModels;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Services.AdminServices
{
    public interface IAdminServices
    {
        Task<List<GetCourierModel>> GetAllNewCouriers();
        Task<List<GetCourierModel>> GetAllApprovedCouriers();
        Task<List<GetFarmerModel>> GetAllNewFarmers();
        Task<List<GetFarmerModel>> GetAllApprovedFarmers();
        Task<string> ApproveFarmer(string request);
        Task<string> ApproveCourier(string request);
    }
}
