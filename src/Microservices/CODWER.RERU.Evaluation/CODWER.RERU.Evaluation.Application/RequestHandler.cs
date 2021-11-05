using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ISession = CODWER.RERU.Evaluation.Application.Interfaces.ISession;

namespace CODWER.RERU.Evaluation.Application
{
    public abstract class RequestHandler<TRequest, TResponse> : Session, IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        //protected AppDbContext appDbContext;

        public RequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            //appDbContext = (AppDbContext)serviceProvider.GetService(typeof(AppDbContext));
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public class Session : ISession
    {
        public IHttpContextAccessor httpContextAccessor { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public string HostName { get; set; }
        private IConfiguration configuration { get; set; }

        public Session(IServiceProvider serviceProvider)
        {
            configuration = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));
            httpContextAccessor = (IHttpContextAccessor)serviceProvider.GetService(typeof(IHttpContextAccessor));

            CurrentUser = GetCurrentUser();

            HostName = configuration.GetSection("HostName").Value;
        }

        private CurrentUser GetCurrentUser()
        {
            var principal = httpContextAccessor.HttpContext.User;

            var email = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = principal.FindFirstValue(ClaimTypes.Role);
            var id = principal.FindFirstValue(JwtRegisteredClaimNames.Sid);

            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var user = new CurrentUser()
            {
                Id = int.Parse(id),
                Email = email,
                Role = Enum.Parse<Role>(role)
            };

            return user;
        }
    }

    public class CurrentUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
