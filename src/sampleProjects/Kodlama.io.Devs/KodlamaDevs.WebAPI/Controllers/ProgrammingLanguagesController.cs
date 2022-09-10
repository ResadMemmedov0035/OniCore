using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands;
using KodlamaDevs.Application.Features.ProgrammingLanguages.DTOs;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OniCore.Persistence.Pagination;

namespace KodlamaDevs.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProgrammingLanguageCommand deleteCommand)
        {
            DeletedProgrammingLanguageDTO deleted = await Mediator.Send(deleteCommand);
            return Ok(deleted);
        }        

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetProgrammingLanguageByIdQuery getByIdQuery)
        {
            GetProgrammingLanguageByIdDTO language = await Mediator.Send(getByIdQuery);
            return Ok(language);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationParams paginationParams)
        {
            GetProgrammingLanguageListQuery getListQuery = new() { PaginationParams = paginationParams };
            GetProgrammingLanguageListDTO list = await Mediator.Send(getListQuery);
            return Ok(list);
        }
    }
}
