using KodlamaDevs.Application.Features.Developers.Commands;
using KodlamaDevs.Application.Features.Developers.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodlamaDevs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDeveloperCommand registerCommand)
        {
            AuthorizedDeveloperDTO authorizedDev = await Mediator.Send(registerCommand);
            return Created("", authorizedDev);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand loginCommand)
        {
            AuthorizedDeveloperDTO authorizedDev = await Mediator.Send(loginCommand);
            return Ok(authorizedDev);
        }
    }
}
