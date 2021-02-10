using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService service)
        {
            _loginService = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDTO user)
        {
            if (!ModelState.IsValid || user == null)
                return BadRequest(ModelState);
            
            try
            {
                var result = await _loginService.FindByLogin(user);

                if (result != null)
                    return result;
                else
                    return NotFound();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        } 
    }
}