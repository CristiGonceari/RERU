using AutoMapper;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorMappings
{
    public class UserStateConverter : IValueConverter<Contractor, string>
    {
        public string Convert(Contractor sourceMember, ResolutionContext context)
        {
            if (sourceMember.UserProfile.DepartmentColaboratorId is not null || sourceMember.UserProfile.RoleColaboratorId is not null)
            {
                return "Employee";
            }

            return "Candidate";
        }
    }
}
