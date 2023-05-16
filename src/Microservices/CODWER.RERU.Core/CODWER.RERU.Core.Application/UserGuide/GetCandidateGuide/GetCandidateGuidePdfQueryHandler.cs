using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserGuide.GetCandidateGuide
{
    public class GetCandidateGuidePdfQueryHandler : IRequestHandler<GetCandidateGuidePdfQuery, FileDataDto>
    {
        public async Task<FileDataDto> Handle(GetCandidateGuidePdfQuery request, CancellationToken cancellationToken)
        {
            var path = new FileInfo("UserGuide/GhidulCandidatului.pdf").FullName;
            var bytes = await File.ReadAllBytesAsync(path);

            return FileDataDto.GetPdf("Ghidul candidatului.pdf", bytes);
        }
    }
}
