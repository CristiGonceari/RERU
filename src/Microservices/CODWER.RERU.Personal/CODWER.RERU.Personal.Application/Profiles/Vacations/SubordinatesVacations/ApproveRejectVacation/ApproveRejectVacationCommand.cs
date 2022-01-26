using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.ApproveRejectVacation
{
    public class ApproveRejectVacationCommand : IRequest<Unit>
    {
        public ApproveRejectVacationCommand(ApproveRejectVacationDto data)
        {
            Data = data;
        }
        
        public ApproveRejectVacationDto Data { get; set; }
    }
}
