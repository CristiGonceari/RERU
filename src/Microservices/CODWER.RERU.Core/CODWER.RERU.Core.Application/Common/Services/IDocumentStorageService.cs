
using MediatR;
using System;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Common.Services
{
    public interface IDocumentStorageService
    {
        Task<string> UploadDocument(byte[] content);
        Task<byte[]> GetDocument(Guid id);
        Task<Unit> Remove(Guid id);

    }
}
