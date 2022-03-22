using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.UserGuide.GetUserGuide
{
    public class GetUserGuidePdfQueryHandler : IRequestHandler<GetUserGuidePdfQuery, FileDataDto>
    {
        public async Task<FileDataDto> Handle(GetUserGuidePdfQuery request, CancellationToken cancellationToken)
        {
            var path = new FileInfo("UserGuide/GhidPersonal.pdf").FullName;
            var bytes = await File.ReadAllBytesAsync(path);

            return new FileDataDto
            {
                Content = bytes,
                ContentType = "application/pdf",
                Name = "Ghidul Utilizatorului.pdf"
            };
        }
    }
}
