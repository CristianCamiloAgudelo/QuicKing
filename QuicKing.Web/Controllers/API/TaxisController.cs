using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicKing.Web.Data;
using QuicKing.Web.Data.Entities;
using QuicKing.Web.Helpers;

namespace QuicKing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxisController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TaxisController(DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
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
                .Include(t => t.User)//conductor
                .Include(t => t.Trips)//viajes
                .ThenInclude(t => t.TripDetails)//detalles de viaje
                .Include(t => t.Trips)
                .ThenInclude(t => t.User)//pasajero
                .FirstOrDefaultAsync(t => t.Plaque == plaque);

            if (taxiEntity == null)
            {
                //return NotFound();
                
                _context.Taxis.Add(new TaxiEntity { Plaque = plaque });
                await _context.SaveChangesAsync();
                taxiEntity = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == plaque);
                
            }

            return Ok(_converterHelper.ToTaxiResponse(taxiEntity));
        }
    }
}
