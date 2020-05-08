using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicKing.Web.Data;
using QuicKing.Web.Data.Entities;
using QuicKing.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuicKing.Web.Controllers.API
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserGroupsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public UserGroupsController(
            DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroup([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserGroupEntity userGroup = await _context.UserGroups
                .Include(ug => ug.Users)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(u => u.User.Id == id);

            if (userGroup == null || userGroup?.Users == null)
            {
                return Ok();
            }

            return Ok(_converterHelper.ToUserGroupResponse(userGroup.Users.ToList()));
        }
    }

}
