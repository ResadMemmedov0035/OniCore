using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;
using OniCore.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using System.Security.Claims;

namespace OniCore.CrossCuttingConcerns.Exceptions.Handlers
{
    public class HttpExceptionHandler : ExceptionHandler
    {
        private HttpResponse? _response;
        private readonly ILogger<HttpExceptionHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _logMessageTemplate = "Exception: {ExceptionMessage} | User: {@User}";

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
            _logger.LogInformation(businessException, _logMessageTemplate, businessException.Message, GetUser());

            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = new BusinessProblemDetails(businessException.Message).AsJson();
            return Response.WriteAsync(details);
        }

        protected override Task HandleException(ValidationException validationException)
        {
            _logger.LogInformation(validationException, _logMessageTemplate, validationException.Message, GetUser());

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
            _logger.LogInformation(notFoundException, _logMessageTemplate, notFoundException.Message, GetUser());

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
                Name = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?",
                Email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "?"
            };
        }
    }
}
