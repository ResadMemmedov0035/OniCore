using KodlamaDevs.Application.Features.Technologies.Commands;
using KodlamaDevs.Application.Features.Technologies.DTOs;
using KodlamaDevs.Application.Features.Technologies.Queries;
using Microsoft.AspNetCore.Mvc;
using OniCore.Persistence.Dynamic;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : ApiControllerBase
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedTechnologyDTO deleted = await Mediator.Send(new DeleteTechnologyCommand { Id = id });
            return Ok(deleted);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetTechnologyByIdDTO technology = await Mediator.Send(new GetTechnologyByIdQuery { Id = id });
            return Ok(technology);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageParams pageParams)
        {
            GetTechnologyListQuery getListQuery = new() { PageParams = pageParams };
            GetTechnologyListDTO list = await Mediator.Send(getListQuery);
            return Ok(list);
        }

        [HttpPost("dynamic/filter")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageParams pageParams, [FromBody] DynamicParams dynamicParams)
        {
            GetTechnologyListByDynamicQuery getListQuery = new() { PageParams = pageParams, DynamicParams = dynamicParams };
            GetTechnologyListDTO list = await Mediator.Send(getListQuery);
            return Ok(list);
        }
    }
}
