using KodlamaDevs.Application.Features.Technologies.Commands;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Features.Technologies.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTechnologyCommand createCommand)
        {
            CreatedTechnologyDTO created = await Mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateCommand)
        {
            UpdatedTechnologyDTO updated = await Mediator.Send(updateCommand);
            return Ok(updated);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteTechnologyCommand deleteCommand)
        {
            DeletedTechnologyDTO deleted = await Mediator.Send(deleteCommand);
            return Ok(deleted);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetTechnologyByIdQuery getByIdQuery)
        {
            GetTechnologyByIdDTO technology = await Mediator.Send(getByIdQuery);
            return Ok(technology);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationParams paginationParams)
        {
            GetTechnologyListQuery getListQuery = new() { PaginationParams = paginationParams };
            GetTechnologyListDTO list = await Mediator.Send(getListQuery);
            return Ok(list);
        }

    }
}
