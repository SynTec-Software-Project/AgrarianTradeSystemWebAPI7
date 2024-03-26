using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Models.AdminModels;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace AgrarianTradeSystemWebAPI.Services.AdminServices
{
    public class AdminServices : IAdminServices
    {
        private readonly DataContext _context;
        public AdminServices(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GetCourierModel>> GetAllNewCouriers()
        {
            var newCourierModels = new List<GetCourierModel>();
            var NewCouriers = await _context.Couriers.Where(courier => !courier.Approved).ToListAsync();
            foreach (var courier in NewCouriers)
            {
                GetCourierModel getCourierModel = new GetCourierModel
                {
                    UserName = courier.Username,
                    FirstName = courier.FirstName,
                    LastName = courier.LastName,
                    Email = courier.Email,
                    PhoneNumber = courier.PhoneNumber,
                    NIC = courier.NIC,
                    AddL1 = courier.AddL1,
                    AddL2 = courier.AddL2,
                    AddL3 = courier.AddL3,
                    ProfileImg = courier.ProfileImg,
                    VehicleNo = courier.VehicleNo,
                    VehicleImg = courier.VehicleImg,
                    LicenseImg = courier.LicenseImg,
                };
                newCourierModels.Add(getCourierModel);
            }
            return newCourierModels;
        }

        public async Task<List<GetCourierModel>> GetAllApprovedCouriers()
        {
            var newCourierModels = new List<GetCourierModel>();
            var NewCouriers = await _context.Couriers.Where(courier => courier.Approved).ToListAsync();
            foreach (var courier in NewCouriers)
            {
                GetCourierModel getCourierModel = new GetCourierModel
                {
                    UserName = courier.Username,
                    FirstName = courier.FirstName,
                    LastName = courier.LastName,
                    Email = courier.Email,
                    PhoneNumber = courier.PhoneNumber,
                    NIC = courier.NIC,
                    AddL1 = courier.AddL1,
                    AddL2 = courier.AddL2,
                    AddL3 = courier.AddL3,
                    ProfileImg = courier.ProfileImg,
                    VehicleNo = courier.VehicleNo,
                    VehicleImg = courier.VehicleImg,
                    LicenseImg = courier.LicenseImg,
                };
                newCourierModels.Add(getCourierModel);
            }
            return newCourierModels;
        }

        public async Task<List<GetFarmerModel>> GetAllNewFarmers()
        {
            var newFarmerModels = new List<GetFarmerModel>();
            var newFarmers = await _context.Farmers.Where(farmer => !farmer.Approved).ToListAsync();
            foreach(var farmer in newFarmers)
            {
                GetFarmerModel getFarmerModel = new GetFarmerModel
                {
                    UserName = farmer.Username,
                    FirstName = farmer.FirstName,
                    LastName = farmer.LastName,
                    Email = farmer.Email,
                    PhoneNumber = farmer.PhoneNumber,
                    NIC = farmer.NIC,
                    AddL1 = farmer.AddL1,
                    AddL2 = farmer.AddL2,
                    AddL3 = farmer.AddL3,
                    ProfileImg = farmer.ProfileImg,
                    CropTypes = farmer.CropDetails,
                    GNCertificate = farmer.GSLetterImg
                };
                newFarmerModels.Add(getFarmerModel);

            }
            return newFarmerModels;
        }

        public async Task<List<GetFarmerModel>> GetAllApprovedFarmers()
        {
            var newFarmerModels = new List<GetFarmerModel>();
            var newFarmers = await _context.Farmers.Where(farmer => farmer.Approved).ToListAsync();
            foreach (var farmer in newFarmers)
            {
                GetFarmerModel getFarmerModel = new GetFarmerModel
                {
                    UserName = farmer.Username,
                    FirstName = farmer.FirstName,
                    LastName = farmer.LastName,
                    Email = farmer.Email,
                    PhoneNumber = farmer.PhoneNumber,
                    NIC = farmer.NIC,
                    AddL1 = farmer.AddL1,
                    AddL2 = farmer.AddL2,
                    AddL3 = farmer.AddL3,
                    ProfileImg = farmer.ProfileImg,
                    CropTypes = farmer.CropDetails,
                    GNCertificate = farmer.GSLetterImg
                };
                newFarmerModels.Add(getFarmerModel);

            }
            return newFarmerModels;
        }

        public async Task<string> ApproveCourier(string request)
        {
            var courier = await _context.Couriers.FirstOrDefaultAsync(u => u.Email == request);
            if (courier == null)
            {
                throw new AdminErrorException("Invalid Email");
            }
            courier.Approved = true;
            await _context.SaveChangesAsync();
            return ("Approved successfully");
        }
        public async Task<string> ApproveFarmer(string request)
        {
            var farmer = await _context.Farmers.FirstOrDefaultAsync(u => u.Email == request);
            if (farmer == null)
            {
                throw new AdminErrorException("Invalid Email");
            }
            farmer.Approved = true;
            await _context.SaveChangesAsync();
            return ("Approved successfully");
        }
    }
}
