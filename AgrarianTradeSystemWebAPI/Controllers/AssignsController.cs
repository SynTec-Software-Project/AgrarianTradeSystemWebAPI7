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
    [Route("api/assigns")]
    [ApiController]
    public class AssignsController : ControllerBase
    {
        private readonly DataContext _context;

        public AssignsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Assigns - default
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assigns>>> GetAssigns()
        {
          if (_context.Assigns == null)
          {
              return NotFound("No Data Found");
          }
            return await _context.Assigns.ToListAsync();
        }

        // GET: api/Assigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assigns>> GetAssigns(int id)
        {
          if (_context.Assigns == null)
          {
              return NotFound("No Data Found");
          }
            var assigns = await _context.Assigns.FindAsync(id);

            if (assigns == null)
            {
                return NotFound("No Data Found");
            }

            return assigns;
        }
        /*
        // PUT: api/Assigns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssigns(int id, Assigns assigns)
        {
            if (id != assigns.OrderId)
            {
                return BadRequest("You are trying to modify the wrong assigns");
            }

           //  _context.Entry(assigns).State = EntityState.Modified;

            try
            {
                Assigns entry_ = await _context.Assigns.FindAsync(e => e.OrderId == order.OrderId);
                if (entry_.PickupDate != assigns.PickupDate)
                {
                    entry_.PickupDate = assigns.PickupDate;
                }

                if (entry_.DeliveryDate != assigns.DeliveryDate)
                {
                    entry_.DeliveryDate = assigns.DeliveryDate;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignsExists(id))
                {
                    return NotFound("The assign with the OrderId"+" "+"does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Assign updated successfully!");
        }
        */

        // POST: api/Assigns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assigns>> PostAssigns(Assigns assigns)
        {
          if (_context.Assigns == null)
          {
              return Problem("Entity set 'Assigns'  is null.");
          }
            
            try
            {
                _context.Assigns.Add(assigns);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssignsExists(assigns.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAssigns", new { id = assigns.OrderId }, assigns);
        }

        /*
        // DELETE: api/Assigns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssigns(int id)
        {
            if (_context.Assigns == null)
            {
                return NotFound();
            }
            var assigns = await _context.Assigns.FindAsync(id);
            if (assigns == null)
            {
                return NotFound();
            }

            _context.Assigns.Remove(assigns);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool AssignsExists(int id)
        {
            return (_context.Assigns?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
