using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.MyProfile.Files.PrintFiles
{
    public class PrintFilesCommand : TableParameter, IRequest<FileDataDto>
    {
    }
}
