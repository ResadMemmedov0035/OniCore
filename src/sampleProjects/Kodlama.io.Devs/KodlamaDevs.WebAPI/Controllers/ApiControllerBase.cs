using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KodlamaDevs.WebAPI.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        protected string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? string.Empty;
        }
    }
}
