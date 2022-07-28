using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.ServiceProvider;
using CVU.ERP.ServiceProvider.Exceptions;
using MediatR;

namespace CVU.ERP.Module.Application.Infrastructure {
    public class RequestAuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {
        private readonly IEnumerable<ICurrentApplicationUserProvider> _currentUserProviders;
        public RequestAuthorizationBehaviour (IEnumerable<ICurrentApplicationUserProvider> currentUserProviders) {
            _currentUserProviders = currentUserProviders;
        }

        public async Task<TResponse> Handle (TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {

            var moduleOperationAttribute = (ModuleOperationAttribute) Attribute.GetCustomAttribute (request.GetType (), typeof (ModuleOperationAttribute));

            if (moduleOperationAttribute != null) {

                if (moduleOperationAttribute.RequiresAuthentication && !_currentUserProviders.Any (x => x.IsAuthenticated)) {
                    throw new ApplicationUnauthenticatedException ();
                }
                if (moduleOperationAttribute != null) {
                    var requiredPermission = moduleOperationAttribute.Permission;
                    if (!string.IsNullOrEmpty (requiredPermission)) {
                        if (!_currentUserProviders.Any (x => x.IsAuthenticated)) {
                            throw new ApplicationUnauthenticatedException ();
                        }
                        var currentUser = await _currentUserProviders.FirstOrDefault (x => x.IsAuthenticated).Get ();
                        if (!currentUser.Permissions.Contains (requiredPermission)) {
                            throw new ApplicationUnauthorizedException ();
                        }
                    }
                }
            }
            return await next ();
        }
    }
}