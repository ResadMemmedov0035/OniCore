using KodlamaDevs.Application.Features.Developers.Commands;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Queries;
using Microsoft.AspNetCore.Mvc;
using OniCore.Persistence.Pagination;
using OniCore.Security.Entities;

namespace KodlamaDevs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDeveloperCommand registerCommand)
        {
            registerCommand.IpAddress = GetIpAddress();
            AuthorizedDeveloperDTO authorizedDev = await Mediator.Send(registerCommand);

            WriteRefreshTokenToCookie(authorizedDev.RefreshToken);

            return CreatedAtAction(nameof(GetById), new { authorizedDev.Id }, authorizedDev);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand loginCommand)
        {
            AuthorizedDeveloperDTO authorizedDev = await Mediator.Send(loginCommand);
            return Ok(authorizedDev);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute(Name = "Id")]int id, [FromBody] UpdateDeveloperCommand updateCommand)
        {
            updateCommand.Id = id;
            UpdatedDeveloperDTO updated = await Mediator.Send(updateCommand);
            return Ok(updated);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteDeveloperCommand deleteCommand)
        {
            DeletedDeveloperDTO deleted = await Mediator.Send(deleteCommand);
            return Ok(deleted);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageParams pageParams)
        {
            GetDeveloperListDTO list = await Mediator.Send(new GetDeveloperListQuery { PageParams = pageParams });
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetDeveloperByIdQuery getByIdQuery)
        {
            GetDeveloperByIdDTO developer = await Mediator.Send(getByIdQuery);
            return Ok(developer);
        }

        private void WriteRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
