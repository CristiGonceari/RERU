using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IExportPositionDiagramService
    {
        public Task<FileDataDto> ExportPositionDiagram(int positionId);
    }
}
