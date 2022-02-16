using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Personal.Application.TemplateParsers.Implementations
{
    public class TemplateConvertor : ITemplateConvertor
    {
        private readonly IGeneratePdf _generatePdf;

        public TemplateConvertor(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }

        public async Task<FileDataDto> GetPdfFromHtmlString(HtmlContentDto content)
        {
            var res = Parse(content.FileContent);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = "Converted.pdf"
            };
        }

        public async Task<FileDataDto> GetPdfFromFile(IFormFile file)
        {
            var html = await ReadAsStringAsync(file);
            var res = Parse(html);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = file.FileName.Replace(".html", ".pdf")
            };
        }

        public async Task<string> ReadAsStringAsync(IFormFile file)
        {
            var result = new StringBuilder();
            using var reader = new StreamReader(file.OpenReadStream());
            while (reader.Peek() >= 0)
                result.AppendLine(await reader.ReadLineAsync());
            return result.ToString();
        }

        public async Task<FileDataDto> GetPdfFromHtml(Dictionary<string, string> dictionary, string fileName)
        {
            var path = new FileInfo(fileName).FullName;
            var source = await File.ReadAllTextAsync(path);
            fileName = fileName.Split('/').Last().Replace(".html", ".pdf");

            foreach (var item in dictionary)
            {
                source = source.Replace(item.Key, item.Value);
            }

            var res = Parse(source);

            return new FileDataDto
            {
                Content = res,
                ContentType = "application/pdf",
                Name = fileName
            };
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
