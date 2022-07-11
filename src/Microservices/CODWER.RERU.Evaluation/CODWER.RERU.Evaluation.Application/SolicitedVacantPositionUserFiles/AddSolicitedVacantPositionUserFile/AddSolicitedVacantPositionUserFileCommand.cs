using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.AddSolicitedVacantPositionUserFile
{
    public class AddSolicitedVacantPositionUserFileCommand : IRequest<string>
    {
        public int UserProfileId { get; set; }
        public int SolicitedVacantPositionId { get; set; }
        public int RequiredDocumentId { get; set; }
        public AddFileDto File { get; set; }
    }
}
