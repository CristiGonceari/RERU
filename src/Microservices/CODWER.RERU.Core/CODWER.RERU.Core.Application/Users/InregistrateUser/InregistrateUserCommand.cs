using System;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.InregistrateUser
{
    public class InregistrateUserCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public int? CandidatePositionId { set; get; }
        public bool EmailNotification { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Code { get; set; }
    }
}
