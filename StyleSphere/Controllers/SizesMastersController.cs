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
    public class SizesMastersController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public SizesMastersController(StyleSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/SizesMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizesMaster>>> GetSizesMasters()
        {
            return await _context.SizesMasters.ToListAsync();
        }

        // GET: api/SizesMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SizesMaster>> GetSizesMaster(int id)
        {
            var sizesMaster = await _context.SizesMasters.FindAsync(id);

            if (sizesMaster == null)
            {
                return NotFound();
            }

            return sizesMaster;
        }

        // PUT: api/SizesMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSizesMaster(int id, SizesMaster sizesMaster)
        {
            if (id != sizesMaster.SizeId)
            {
                return BadRequest();
            }

            _context.Entry(sizesMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizesMasterExists(id))
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

        // POST: api/SizesMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SizesMaster>> PostSizesMaster(SizesMaster sizesMaster)
        {
            _context.SizesMasters.Add(sizesMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSizesMaster", new { id = sizesMaster.SizeId }, sizesMaster);
        }

        // DELETE: api/SizesMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizesMaster(int id)
        {
            var sizesMaster = await _context.SizesMasters.FindAsync(id);
            if (sizesMaster == null)
            {
                return NotFound();
            }

            _context.SizesMasters.Remove(sizesMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SizesMasterExists(int id)
        {
            return _context.SizesMasters.Any(e => e.SizeId == id);
        }
    }
}
