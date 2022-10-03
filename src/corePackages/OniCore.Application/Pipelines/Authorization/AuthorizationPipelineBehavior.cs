using MediatR;
using Microsoft.AspNetCore.Http;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;
using OniCore.Security.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Application.Pipelines.Authorization
{
    public class AuthorizationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ISecuredRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationPipelineBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            List<string> userRoles = _httpContextAccessor.HttpContext.User.GetRoles();

            bool isUserAuthorized = userRoles.Any(userRole => request.RequiredRoles.Contains(userRole));

            if (!isUserAuthorized) throw new AuthorizationException("You are not authorized.");

            return await next();
        }
    }
}
