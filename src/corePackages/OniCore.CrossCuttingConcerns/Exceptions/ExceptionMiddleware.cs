using Microsoft.AspNetCore.Http;
using OniCore.CrossCuttingConcerns.Exceptions.Handlers;
using System.Net.Mime;

namespace OniCore.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _handler;

        public ExceptionMiddleware(RequestDelegate next, HttpExceptionHandler handler)
        {
            _next = next;
            _handler = handler;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context.Response, exception);
            }
        }

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = MediaTypeNames.Application.Json;
            _handler.Response = response;
            return _handler.HandleExceptionAsync(exception);
        }
    }
}
