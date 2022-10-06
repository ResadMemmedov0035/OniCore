using KodlamaDevs.Application.Features.Developers.Commands;
using KodlamaDevs.Application.Features.Developers.DTOs;
using KodlamaDevs.Application.Features.Developers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OniCore.Persistence.Pagination;

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
            return CreatedAtAction(nameof(GetById), new { authorizedDev.Id }, authorizedDev);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand loginCommand)
        {
            AuthorizedDeveloperDTO authorizedDev = await Mediator.Send(loginCommand);
            return Ok(authorizedDev);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDeveloperCommand updateCommand)
        {
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
    }
}
