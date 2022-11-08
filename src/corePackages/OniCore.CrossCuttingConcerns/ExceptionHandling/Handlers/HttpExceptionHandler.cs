using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OniCore.CrossCuttingConcerns.ExceptionHandling.Exceptions;
using OniCore.CrossCuttingConcerns.ExceptionHandling.HttpProblemDetails;
using System.Security.Claims;

namespace OniCore.CrossCuttingConcerns.ExceptionHandling.Handlers
{
    public class HttpExceptionHandler : ExceptionHandler
    {
        private HttpResponse? _response;
        private readonly ILogger<HttpExceptionHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _logMessageTemplate = "Message: {Message} | User: {@User}";

        public HttpResponse Response
        {
            get => _response ?? throw new ArgumentNullException(nameof(_response));
            set => _response = value;
        }

        public HttpExceptionHandler(ILogger<HttpExceptionHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleException(BusinessException businessException)
        {
            _logger.LogInformation(_logMessageTemplate, businessException.Message, GetUser());

            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new BusinessProblemDetails(businessException.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(ValidationException validationException)
        {
            _logger.LogInformation(_logMessageTemplate, validationException.Message, GetUser());

            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new ValidationProblemDetails(validationException.Errors).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(AuthorizationException authorizationException)
        {
            _logger.LogWarning(authorizationException, _logMessageTemplate, authorizationException.Message, GetUser());

            Response.StatusCode = StatusCodes.Status401Unauthorized;
            string details = new AuthorizationProblemDetails(authorizationException.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(NotFoundException notFoundException)
        {
            _logger.LogInformation(_logMessageTemplate, notFoundException.Message, GetUser());

            Response.StatusCode = StatusCodes.Status404NotFound;
            string details = new NotFoundProblemDetails(notFoundException.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(Exception exception)
        {
            _logger.LogError(exception, _logMessageTemplate, exception.Message, GetUser());

            Response.StatusCode = StatusCodes.Status500InternalServerError;
            string details = new InternalServerErrorProblemDetails(exception.Message).AsJson();
            return Response.WriteAsync(details);
        }

        private object GetUser()
        {
            return new
            {
                Name = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "Unknown",
                Email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown"
            };
        }
    }
}
