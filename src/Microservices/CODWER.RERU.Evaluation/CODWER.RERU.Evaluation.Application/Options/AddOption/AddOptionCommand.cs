using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Options.AddOption
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_INTREBARI)]
    public class AddOptionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int QuestionUnitId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public AddFileDto FileDto { get; set; }
    }
}
