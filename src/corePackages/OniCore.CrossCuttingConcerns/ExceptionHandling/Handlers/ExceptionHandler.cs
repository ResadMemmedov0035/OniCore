using FluentValidation;
using OniCore.CrossCuttingConcerns.ExceptionHandling.Exceptions;

namespace OniCore.CrossCuttingConcerns.ExceptionHandling.Handlers
{
    public abstract class ExceptionHandler
    {
        public Task HandleExceptionAsync(Exception exception)
        {
            return exception switch
            {
                BusinessException e => HandleException(e),
                ValidationException e => HandleException(e),
                AuthorizationException e => HandleException(e),
                NotFoundException e => HandleException(e),
                _ => HandleException(exception)
            };
        }

        protected abstract Task HandleException(BusinessException businessException);
        protected abstract Task HandleException(ValidationException validationException);
        protected abstract Task HandleException(AuthorizationException authorizationException);
        protected abstract Task HandleException(NotFoundException notFoundException);
        protected abstract Task HandleException(Exception exception);
    }
}
