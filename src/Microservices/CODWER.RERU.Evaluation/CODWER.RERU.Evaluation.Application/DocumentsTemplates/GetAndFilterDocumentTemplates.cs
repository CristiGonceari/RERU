using RERU.Data.Entities.Documents;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using CVU.ERP.StorageService.Entities;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates
{
    public class GetAndFilterDocumentTemplates
    {
        public static IQueryable<DocumentTemplate> Filter(AppDbContext appDbContext, string name, FileTypeEnum fileType)
        {
            var documentTemplates = appDbContext.DocumentTemplates
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                documentTemplates = documentTemplates.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            if (Enum.IsDefined(typeof(FileTypeEnum), fileType))
            {
                documentTemplates = documentTemplates.Where(x => x.FileType == fileType);
            }

            return documentTemplates;
        }
    }
}
