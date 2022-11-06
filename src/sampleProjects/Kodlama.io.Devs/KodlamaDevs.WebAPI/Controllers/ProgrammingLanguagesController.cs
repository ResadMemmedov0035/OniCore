using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Queries;
using Microsoft.AspNetCore.Mvc;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProgrammingLanguageCommand createCommand)
        {
            CreatedProgrammingLanguageDTO created = await Mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateCommand)
        {
            UpdatedProgrammingLanguageDTO updated = await Mediator.Send(updateCommand);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedProgrammingLanguageDTO deleted = await Mediator.Send(new DeleteProgrammingLanguageCommand(id));
            return Ok(deleted);
        }        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetProgrammingLanguageByIdDTO language = await Mediator.Send(new GetProgrammingLanguageByIdQuery(id));
            return Ok(language);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageParams pageParams)
        {
            GetProgrammingLanguageListQuery getListQuery = new() { PageParams = pageParams };
            GetProgrammingLanguageListDTO list = await Mediator.Send(getListQuery);
            return Ok(list);
        }
    }
}
