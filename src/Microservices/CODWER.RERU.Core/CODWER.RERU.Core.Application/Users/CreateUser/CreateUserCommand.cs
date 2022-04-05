using MediatR;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{

    public class CreateUserCommand : IRequest<int> 
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public bool EmailNotification { get; set; }
    }
}