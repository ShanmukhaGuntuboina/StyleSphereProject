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
    public class ProductMappingsController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public ProductMappingsController(StyleSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductMappings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMapping>>> GetProductMappings()
        {
            return await _context.ProductMappings.ToListAsync();
        }

        // GET: api/ProductMappings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMapping>> GetProductMapping(int id)
        {
            var productMapping = await _context.ProductMappings.FindAsync(id);

            if (productMapping == null)
            {
                return NotFound();
            }

            return productMapping;
        }

        // PUT: api/ProductMappings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductMapping(int id, ProductMapping productMapping)
        {
            if (id != productMapping.ProductMappingId)
            {
                return BadRequest();
            }

            _context.Entry(productMapping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductMappingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductMappings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductMapping>> PostProductMapping(ProductMapping productMapping)
        {
            _context.ProductMappings.Add(productMapping);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductMapping", new { id = productMapping.ProductMappingId }, productMapping);
        }

        // DELETE: api/ProductMappings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMapping(int id)
        {
            var productMapping = await _context.ProductMappings.FindAsync(id);
            if (productMapping == null)
            {
                return NotFound();
            }

            _context.ProductMappings.Remove(productMapping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductMappingExists(int id)
        {
            return _context.ProductMappings.Any(e => e.ProductMappingId == id);
        }
    }
}
