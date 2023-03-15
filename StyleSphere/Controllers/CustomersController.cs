using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleSphere.Models;

namespace StyleSphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public CustomersController(StyleSphereDbContext context)
        {
            _context = context;
        }

        [Route("Login")]
        [HttpGet]
        public async Task<ActionResult<Customer>> LoginCustomer(string email, string password)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Email == email);
            if (customer == null || customer.Password != password)
            {
                return BadRequest("Invalid Details");
            }
            return Ok(customer);
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        private bool CustomerExists(string email)
        {
            return _context.Customers.Any(e => e.Email == email);
        }
    }
}
