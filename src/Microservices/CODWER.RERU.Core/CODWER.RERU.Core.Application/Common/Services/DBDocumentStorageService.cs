using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Context;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Common.Services
{
    public class DBDocumentStorageService : IDocumentStorageService
    {
        private readonly CoreDbContext _coreDbContext;

        public DBDocumentStorageService(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<byte[]> GetDocument(Guid id)
        {
            var document = _coreDbContext.DocumentBodies.FirstOrDefault(d=>d.Id == id);

            return document.Content;
        }

        public async Task<Unit> Remove(Guid id)
        {
            var document = _coreDbContext.DocumentBodies.FirstOrDefault(d => d.Id == id);

            if (document != null)
            { 
                _coreDbContext.DocumentBodies.Remove(document);

                await _coreDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }

        public async Task<string> UploadDocument(byte [] content)
        {
            var documentEntity = new DocumentBody()
            {
                Content = content
            };

            await _coreDbContext.DocumentBodies.AddAsync(documentEntity);

            await _coreDbContext.SaveChangesAsync();

            return documentEntity.Id.ToString();
        }
    }
}
