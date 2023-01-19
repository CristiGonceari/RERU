using CODWER.RERU.Personal.Application.Services;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.ImportEmployeeFunctions
{
    public class ImportEmployeeFunctionsCommandHandler : IRequestHandler<ImportEmployeeFunctionsCommand, FileDataDto>
    {
        private readonly IImportEmployeeFunctionsService _importEmployeeFunctionsService;

        public ImportEmployeeFunctionsCommandHandler(IImportEmployeeFunctionsService importEmployeeFunctionsService)
        {
            _importEmployeeFunctionsService = importEmployeeFunctionsService;
        }

        public async Task<FileDataDto> Handle(ImportEmployeeFunctionsCommand request, CancellationToken cancellationToken)
        {
            return await _importEmployeeFunctionsService.Import(request.Data.File);
        }
    }
}
