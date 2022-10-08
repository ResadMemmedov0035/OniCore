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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDeveloperCommand updateCommand)
        {
            updateCommand.Id = id;
            UpdatedDeveloperDTO updated = await Mediator.Send(updateCommand);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedDeveloperDTO deleted = await Mediator.Send(new DeleteDeveloperCommand { Id = id });
            return Ok(deleted);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageParams pageParams)
        {
            GetDeveloperListDTO list = await Mediator.Send(new GetDeveloperListQuery { PageParams = pageParams });
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetDeveloperByIdDTO developer = await Mediator.Send(new GetDeveloperByIdQuery { Id = id });
            return Ok(developer);
        }

        [HttpGet("{id}/claims")]
        public async Task<IActionResult> GetClaims([FromRoute] int id, [FromQuery] PageParams pageParams)
        {
            GetDeveloperClaimListDTO claimList = await Mediator.Send(new GetDeveloperClaimListQuery { Id = id, PageParams = pageParams });
            return Ok(claimList);
        }

        [HttpPost("{id}/claims/{claimId}")]
        public async Task<IActionResult> AddClaim([FromRoute] int id, [FromRoute] int claimId)
        {
            AddedDeveloperClaimDTO claimDTO = await Mediator.Send(new AddDeveloperClaimCommand { Id = id, ClaimId = claimId });
            return Ok(claimDTO);
        }

        [HttpDelete("{id}/claims/{claimId}")]
        public async Task<IActionResult> RemoveClaim([FromRoute] int id, [FromRoute] int claimId)
        {
            RemovedDeveloperClaimDTO claimDTO = await Mediator.Send(new RemoveDeveloperClaimCommand { Id = id, ClaimId = claimId });
            return Ok(claimDTO);
        }

        private void WriteRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
