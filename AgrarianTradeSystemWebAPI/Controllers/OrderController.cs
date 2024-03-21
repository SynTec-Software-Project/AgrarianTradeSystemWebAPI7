using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Order - defalut  // use default because we need another one for filtering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound("No Data Found");
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound("No Data Found");
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound("No Data Found");
            }

            return order;
        }

        [HttpPost("filters")]
        public async Task<ActionResult <IEnumerable<Order>>> FilteredOrders(Filter filters)
        {
            if (_context.Orders == null)
            {
                return NotFound("No data Found");
            }

            List<Order> allData = await _context.Orders.ToListAsync();
            
            if (filters.All)
            {
                return allData;
            }

            if(filters.Status != null) 
            { 
                allData = allData.Where(e=>e.Status == filters.Status).ToList();
            }

            return allData;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("You are trying to modify the wrong order");
            }

            // _context.Entry(order).State = EntityState.Modified;

            try
            {
                Order entry_ = await _context.Orders.FirstAsync(e=>e.OrderId ==order.OrderId);

                if(entry_.Status != order.Status)
                {
                    entry_.Status = order.Status;
                }

                if (entry_.OrderedDate != order.OrderedDate)
                {
                    entry_.OrderedDate = order.OrderedDate;
                }

                if (entry_.TotalQuantity != order.TotalQuantity)
                {
                    entry_.TotalQuantity = order.TotalQuantity;
                }

                if (entry_.TotalPrice != order.TotalPrice)
                {
                    entry_.TotalPrice = order.TotalPrice;
                }

                if (entry_.DeliveryFee != order.DeliveryFee)
                {
                    entry_.DeliveryFee = order.DeliveryFee;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound("The order with the OrderId"+" "+"does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Order updated successfully");
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'Orders'  is null.");
          }

          try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
        catch (DbUpdateConcurrencyException e) 
            {
                return BadRequest("Could not create the new Order: "+e.Message);
            }
            

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        /*
        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound("No Data Found!");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("No order with the OrderId "+id);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok("Order deleted successfully");
        }
        */

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
