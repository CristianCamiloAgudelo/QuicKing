using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicKing.Web.Data;
using QuicKing.Web.Data.Entities;
using QuicKing.Web.Helpers;
using System.Threading.Tasks;

namespace QuicKing.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TaxisController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TaxisController(DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: Taxis
        [HttpGet("{plaque}")]
        public async Task<IActionResult> GetTaxiEntity([FromRoute] string plaque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            plaque = plaque.ToUpper();
            TaxiEntity taxiEntity = await _context.Taxis
                .Include(t => t.User)// Conductor
                .Include(t => t.Trips) //Vajes
                .ThenInclude(t => t.TripDetails)
                .Include(t => t.Trips)
                .ThenInclude(t => t.User) //Pasajero
                .FirstOrDefaultAsync(t => t.Plaque == plaque);

            if (taxiEntity == null)
            {
                _context.Taxis.Add(new TaxiEntity { Plaque = plaque });
                await _context.SaveChangesAsync();
                taxiEntity = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == plaque);
            }

            return Ok(_converterHelper.ToTaxiResponse(taxiEntity));
        }
    }
}
