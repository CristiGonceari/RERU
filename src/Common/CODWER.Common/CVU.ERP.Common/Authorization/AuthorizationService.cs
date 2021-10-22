using CVU.ERP.Common.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace CVU.ERP.Common.Authorization
{
    public abstract class AuthorizationSetup<TRequest>
    {
        internal List<int> Roles = new List<int>();

        protected internal virtual bool CustomAuthorizationConditions(TRequest request, int roleId)
        {
            return true;
        }

        protected void NeedsRole(int roleId)
        {
            Roles.Add(roleId);
        }
    }

    public class AuthorizationService<TRequest>
    {
        public AuthorizationService(TRequest request)
        {
            List<Type> dependentClasses = FindAllDerivedTypes<AuthorizationSetup<TRequest>>(typeof(TRequest).Assembly);

            Roles = new List<int>();

            if (dependentClasses.Count > 1)
            {
                throw new AppException("Invalid implementation of authorization service. You must inherit this class only once per type of TRequest", HttpStatusCode.BadRequest);
            }
            else if (dependentClasses.Count == 1)
            {
                AuthorizationSetup<TRequest> obj;

                obj = Activator.CreateInstance(dependentClasses.First()) as AuthorizationSetup<TRequest>;

                Roles = obj.Roles;

                AllowAnnonymous = false;

                ValidateCustomAuthorizationConditions += obj.CustomAuthorizationConditions;
            }
        }

        public IEnumerable<int> Roles { get; private set; }

        public Func<TRequest, int, bool> ValidateCustomAuthorizationConditions { get; private set; }

        public bool AllowAnnonymous = true;

        private List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            var derivedType = typeof(T);

            var types = assembly.GetTypes()
                .Where(d => d != derivedType && derivedType.IsAssignableFrom(d))
                .ToList();

            return types;

        }

    }
}
