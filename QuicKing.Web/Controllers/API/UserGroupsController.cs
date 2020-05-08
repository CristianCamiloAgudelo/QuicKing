using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicKing.Common.Enums;
using QuicKing.Common.Models;
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
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;


        public UserGroupsController(
            DataContext context,
            IConverterHelper converterHelper,
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _mailHelper = mailHelper;

        }

        [HttpPost]
        public async Task<IActionResult> PostUserGroup([FromBody] AddUserGroupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            UserEntity proposalUser = await _userHelper.GetUserAsync(request.UserId);
            if (proposalUser == null)
            {
                return BadRequest("Error, Usuario no existe");
            }

            UserEntity requiredUser = await _userHelper.GetUserAsync(request.Email);
            if (requiredUser == null)
            {
                return BadRequest("Error, Usuario no existe");
            }

            UserGroupEntity userGroup = await _context.UserGroups
                .Include(ug => ug.Users)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(ug => ug.User.Id == request.UserId.ToString());
            if (userGroup != null)
            {
                UserGroupDetailEntity user = userGroup.Users.FirstOrDefault(u => u.User.Email == request.Email);
                if (user != null)
                {
                    return BadRequest("El ususario ya esta registrado al grupo");
                }
            }

            UserGroupRequestEntity userGroupRequest = new UserGroupRequestEntity
            {
                ProposalUser = proposalUser,
                RequiredUser = requiredUser,
                Status = UserGroupStatus.Pending,
                Token = Guid.NewGuid()
            };

            try
            {
                _context.UserGroupRequests.Add(userGroupRequest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string linkConfirm = Url.Action("ConfirmUserGroup", "Account", new
            {
                requestId = userGroupRequest.Id,
                token = userGroupRequest.Token
            }, protocol: HttpContext.Request.Scheme);

            string linkReject = Url.Action("RejectUserGroup", "Account", new
            {
                requestId = userGroupRequest.Id,
                token = userGroupRequest.Token
            }, protocol: HttpContext.Request.Scheme);

            Response response = _mailHelper.SendMail(request.Email, "Asunto: Solicitar unirse al grupo", $"<h1> Asunto: Solicitar unirse al grupo </h1>" +
                $"El Usuario: {proposalUser.FullName} ({proposalUser.Email}), Solicita que te unas al grupo" +
                $"</hr></br></br> Deseo confirmar <a href = \"{linkConfirm}\">Confirma aqui </a>" +
                $"</hr></br></br> Si deseas rechazar <a href = \"{linkReject}\"> Rechaza aqui </a>");

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok("Solicitud para unirse al grupo ha sido enviada al correo electrónico.");
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
