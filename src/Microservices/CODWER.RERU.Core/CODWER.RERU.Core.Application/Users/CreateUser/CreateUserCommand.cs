using System;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommand : IRequest<int> 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? DepartmentColaboratorId { get; set; }
        public int? RoleColaboratorId { get; set; }
        public bool EmailNotification { get; set; }
        public AccessModeEnum AccessModeEnum { get; set; }
    }
}