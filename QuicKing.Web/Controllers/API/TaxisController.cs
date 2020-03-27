using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicKing.Web.Data;
using QuicKing.Web.Data.Entities;

namespace QuicKing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxisController : ControllerBase
    {
        private readonly DataContext _context;

        public TaxisController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Taxis
        [HttpGet]
        public IEnumerable<TaxiEntity> GetTaxis()
        {
            return _context.Taxis;
        }

        // GET: api/Taxis/5
        [HttpGet("{plaque}")]
        public async Task<IActionResult> GetTaxiEntity([FromRoute] string plaque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            plaque = plaque.ToUpper();
            TaxiEntity taxiEntity = await _context.Taxis
                .Include(t => t.Trips)
                .FirstOrDefaultAsync(t => t.Plaque == plaque);

            if (taxiEntity == null)
            {
                _context.Taxis.Add(new TaxiEntity { Plaque = plaque });
                await _context.SaveChangesAsync();
                taxiEntity = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == plaque);
            }

            return Ok(taxiEntity);
        }



        // POST: api/Taxis
        [HttpPost]
        public async Task<IActionResult> PostTaxiEntity([FromBody] TaxiEntity taxiEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Taxis.Add(taxiEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaxiEntity", new { id = taxiEntity.Id }, taxiEntity);
        }

        
    }
}