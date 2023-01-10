using RERU.Data.Persistence.Context;
using CVU.ERP.StorageService.Context;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RERU.Data.Entities.Evaluation360;
using System.IO;
using System.Collections.Generic;
using System;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Evaluation360.Application.BLL.Services
{
    public class PdfService : IPdfService
    {
        private readonly AppDbContext _appDbContext;
        private readonly StorageDbContext _storageDbContext;
        private readonly IGeneratePdf _generatePdf;

        public PdfService(AppDbContext appDbContext,
            StorageDbContext storageDbContext,
            IGeneratePdf generatePdf)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _storageDbContext = storageDbContext;
        }

        public async Task<FileDataDto> PrintEvaluationPdf(int evaluationId)
        {
            var evaluation = _appDbContext.Evaluations
                .Include(e => e.EvaluatedUserProfile)
                    .ThenInclude(e => e.Role)
                .Include(e => e.EvaluatorUserProfile)
                    .ThenInclude(e => e.Role)
                .Include(e => e.CounterSignerUserProfile)
                    .ThenInclude(e => e.Role)
                .FirstOrDefault(e => e.Id == evaluationId);

            return await GetPdf(evaluation);
        }

        private async Task<FileDataDto> GetPdf(Evaluation evaluation)
        {
            string path = null;
            string source;
            var myDictionary = new Dictionary<string, string>();
            
            path = new FileInfo("PdfTemplates/Evaluation360.html").FullName;
            myDictionary = GetDictionary(evaluation);

            source = await File.ReadAllTextAsync(path);
            source = ReplaceKeys(source, myDictionary);

            var res = Parse(source);
            
            return FileDataDto.GetPdf("EvaluareaAnualăFinală.pdf", res);
        }

        private Dictionary<string, string> GetDictionary(Evaluation evaluation)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{evaluat_name}", evaluation.EvaluatedUserProfile.LastName + " " + evaluation.EvaluatedUserProfile.FirstName);
            myDictionary.Add("{tr_area_replace}", GetTableContent(evaluation));

            return myDictionary;
        }

        private string GetTableContent(Evaluation evaluation)
        {
            var content = string.Empty;

            content += $@"";

            return content;
        }

        private string ReplaceKeys(string source, Dictionary<string, string> myDictionary)
        {
            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }

            return source;
        }

        private byte[] Parse(string html)
        {
            try
            {
                return _generatePdf.GetPDF(html);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
