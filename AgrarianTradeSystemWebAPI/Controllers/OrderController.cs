using AgrarianTradeSystemWebAPI.Dtos;
using AgrarianTradeSystemWebAPI.Models;
using AgrarianTradeSystemWebAPI.Services.OrderServices;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        //get data by id
        [HttpGet("courier/{userId}")]
        public async Task<ActionResult<List<CourierOrderDto>>> GetCourierOrders(string userId)
        {
            var orders = await _orderServices.GetCourierOrders(userId);
            if (orders is null || orders.Count == 0)
                return NotFound("No orders found for the user");

            var orderDtos = new List<CourierOrderDto>();
            foreach (var order in orders)
            {
                var orderDto = new CourierOrderDto
                {
                    OrderID = order.OrderID,
                    ProductTitle = order.Product.ProductTitle,
                    ProductImageUrl = order.Product.ProductImageUrl,
                    DeliveryDate = order.DeliveryDate,
                    PickupDate = order.PickupDate,
                    DeliveryFee = order.DeliveryFee,
                    OrderStatus = order.OrderStatus,
                    TotalQuantity = order.TotalQuantity,
                    CustomerFName = order.Buyer.FirstName,
                    CustomerLName = order.Buyer.LastName,
                    CustomerAddL1 = order.Buyer.AddL1,
                    CustomerAddL2 = order.Buyer.AddL2,
                    CustomerAddL3 = order.Buyer.AddL3,
                    CustomerPhoneNumber = order.Buyer.PhoneNumber,
                    FarmerFName = order.Product.Farmer.FirstName,
                    FarmerLName = order.Product.Farmer.LastName,
                    FarmerAddL1 = order.Product.Farmer.AddL1,
                    FarmerAddL2 = order.Product.Farmer.AddL2,
                    FarmerAddL3 = order.Product.Farmer.AddL3,
                    FarmerPhoneNumber = order.Product.Farmer.PhoneNumber
                };

                orderDtos.Add(orderDto);
            }
            return Ok(orderDtos);
        }

        // Get buyer's orders
        [HttpGet("buyer/{userId}")]
        public async Task<ActionResult<List<BuyerOrderDto>>> GetBuyerOrders(string userId)
        {
            var orders = await _orderServices.GetBuyerOrders(userId);

            if (orders is null || orders.Count == 0)
                return NotFound("No orders found for the user");

            var orderDtos = new List<BuyerOrderDto>();
            foreach (var order in orders)
            {
                var orderDto = new BuyerOrderDto
                {
                    OrderID = order.OrderID,
                    ProductTitle = order.Product.ProductTitle,
                    ProductImageUrl = order.Product.ProductImageUrl,
                    DeliveryDate = order.DeliveryDate,
                    OrderedDate = order.OrderedDate,
                    DeliveryFee = order.DeliveryFee,
                    OrderStatus = order.OrderStatus,
                    TotalPrice = order.TotalPrice,
                    TotalQuantity = order.TotalQuantity,
                    FarmerFName = order.Product.Farmer.FirstName,
                    FarmerLName = order.Product.Farmer.LastName,
                    FarmerAddL1 = order.Product.Farmer.AddL1,
                    FarmerAddL2 = order.Product.Farmer.AddL2,
                    FarmerAddL3 = order.Product.Farmer.AddL3,
                    FarmerPhoneNumber = order.Product.Farmer.PhoneNumber,
                    CourierFName = order.Courier.FirstName,
                    CourierLName = order.Courier.LastName,
                    CourierAddL1 = order.Courier.AddL1,
                    CourierAddL2 = order.Courier.AddL2,
                    CourierAddL3 = order.Courier.AddL3,
                    CourierPhoneNumber = order.Courier.PhoneNumber
                };

                orderDtos.Add(orderDto);
            }
            return Ok(orderDtos);
        }

        // Get farmer's orders
        [HttpGet("farmer/{userId}")]
        public async Task<ActionResult<List<FarmerOrderDto>>> GetFarmerOrders(string userId)
        {
            var orders = await _orderServices.GetFarmerOrders(userId);
            if (orders is null || orders.Count == 0)
                return NotFound("No orders found for the farmer");

            var orderDtos = new List<FarmerOrderDto>();
            foreach (var order in orders)
            {
                var orderDto = new FarmerOrderDto
                {
                    OrderID = order.OrderID,
                    ProductTitle = order.Product.ProductTitle,
                    ProductImageUrl = order.Product.ProductImageUrl,
                    OrderedDate = order.OrderedDate,
                    TotalPrice = order.TotalPrice,
                    OrderStatus = order.OrderStatus,
                    TotalQuantity = order.TotalQuantity,
                    CourierFName = order.Courier.FirstName,
                    CourierLName = order.Courier.LastName,
                    CourierAddL1 = order.Courier.AddL1,
                    CourierAddL2 = order.Courier.AddL2,
                    CourierAddL3 = order.Courier.AddL3,
                    CourierPhoneNumber = order.Courier.PhoneNumber,
                    CustomerFName = order.Buyer.FirstName,
                    CustomerLName = order.Buyer.LastName,
                    CustomerAddL1 = order.Buyer.AddL1,
                    CustomerAddL2 = order.Buyer.AddL2,
                    CustomerAddL3 = order.Buyer.AddL3,
                    CustomerPhoneNumber = order.Buyer.PhoneNumber
                };

                orderDtos.Add(orderDto);
            }
            return Ok(orderDtos);
        }



        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string orderStatus)
        {
            try
            {
                await _orderServices.UpdateOrderStatus(orderId,orderStatus);
                return Ok("Order status updated successfully");
            }
            catch (Exception ex) 
            {
                return BadRequest("Failed to update order status: " + ex.Message);
            }
        }
    }
}
