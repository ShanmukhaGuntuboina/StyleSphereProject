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
    public class FavoritesController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public FavoritesController(StyleSphereDbContext context)
        {
            _context = context;
        }
        [HttpGet("{CusId}")]
        public List<Favorite> GetFavoritesForCustomer(int CusId)
        {
            var data = _context.Favorites.Where(a => a.CustomerId == CusId).ToList();

            return data;
        }


        // POST: api/Favorite
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> SaveFavorite(int customerId, int productId)
        {
            //Get the customer and product from the database
            var customer = await _context.Customers.FindAsync(customerId);
            var product = await _context.Products.FindAsync(productId);

            //Returns error if customer or product is not found
            if (customer == null || product == null)
            {
                return NotFound();
            }

            //Add a favorite
            var favorite = new Favorite
            {
                CustomerId = customerId,
                ProductId = productId,
                ActiveStatus = true
            };

            // Add the new favorite to the database and save changes
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
