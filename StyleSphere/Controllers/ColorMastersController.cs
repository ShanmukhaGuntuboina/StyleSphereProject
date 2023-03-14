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
    public class ColorMastersController : ControllerBase
    {
        private readonly StyleSphereDbContext _context;

        public ColorMastersController(StyleSphereDbContext context)
        {
            _context = context;
        }

        // GET: api/ColorMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorMaster>>> GetColorMasters()
        {
            return await _context.ColorMasters.ToListAsync();
        }

        // GET: api/ColorMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorMaster>> GetColorMaster(int id)
        {
            var colorMaster = await _context.ColorMasters.FindAsync(id);

            if (colorMaster == null)
            {
                return NotFound();
            }

            return colorMaster;
        }

        // PUT: api/ColorMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColorMaster(int id, ColorMaster colorMaster)
        {
            if (id != colorMaster.ColorId)
            {
                return BadRequest();
            }

            _context.Entry(colorMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorMasterExists(id))
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

        // POST: api/ColorMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ColorMaster>> PostColorMaster(ColorMaster colorMaster)
        {
            _context.ColorMasters.Add(colorMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColorMaster", new { id = colorMaster.ColorId }, colorMaster);
        }

        // DELETE: api/ColorMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColorMaster(int id)
        {
            var colorMaster = await _context.ColorMasters.FindAsync(id);
            if (colorMaster == null)
            {
                return NotFound();
            }

            _context.ColorMasters.Remove(colorMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorMasterExists(int id)
        {
            return _context.ColorMasters.Any(e => e.ColorId == id);
        }
    }
}
