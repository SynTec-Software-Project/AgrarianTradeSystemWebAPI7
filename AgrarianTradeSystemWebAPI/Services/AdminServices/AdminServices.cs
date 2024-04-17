using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Models.AdminModels;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using AgrarianTradeSystemWebAPI.Services.EmailService;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AgrarianTradeSystemWebAPI.Services.AdminServices
{
    public class AdminServices : IAdminServices
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        public AdminServices(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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
                    GNCertificate = farmer.GSLetterImg,
                    NICFront = farmer.NICFrontImg,
                    NICBack = farmer.NICBackImg,
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
                    GNCertificate = farmer.GSLetterImg,
                    NICFront = farmer.NICFrontImg,
                    NICBack = farmer.NICBackImg,
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
            _emailService.approveUserMail(request, courier.FirstName, courier.LastName);
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
            _emailService.approveUserMail(request, farmer.FirstName, farmer.LastName);
            farmer.Approved = true;
            await _context.SaveChangesAsync();
            return ("Approved successfully");
        }

        public async Task<string> DenyFarmer(UserDenyDto request)
        {
            var farmer = await _context.Farmers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (farmer == null)
            {
                throw new AdminErrorException("Invalid Email");
            }
            _emailService.rejectUserMail(request.Email, farmer.FirstName, farmer.LastName, request.Reason);
            _context.Farmers.Remove(farmer);
            await _context.SaveChangesAsync();
            return ("Farmer denied");
        }

        public async Task<string> DenyCourier(UserDenyDto request)
        {
            var courier = await _context.Couriers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (courier == null)
            {
                throw new AdminErrorException("Invalid Email");
            }
            _emailService.rejectUserMail(request.Email, courier.FirstName, courier.LastName, request.Reason);
            _context.Couriers.Remove(courier);
            await _context.SaveChangesAsync();
            return ("Courier denied");
        }
    }
}
