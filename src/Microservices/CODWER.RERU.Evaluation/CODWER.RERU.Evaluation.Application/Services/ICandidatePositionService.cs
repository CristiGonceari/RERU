using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface ICandidatePositionService
    {
        string GetResponsiblePersonName(int id);
        UserProfile GetResponsiblePerson(int id);
    }
}
