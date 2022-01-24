using AutoMapper;
using CODWER.RERU.Evaluation.Application.Options.AddOption;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class OptionService : IOptionService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private bool isCorrect { get; set; }

        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        private List<int> res { get; set; }

        public OptionService(AppDbContext appDbContext, IMediator mediator, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<byte[]> GenerateExcelTemplate(QuestionTypeEnum questionType)
        {
            switch (questionType)
            {
                case QuestionTypeEnum.OneAnswer:
                    return await GenerateTemplateForOneAnswerOption(questionType);

                default:
                    return null;
            }
        }

        private async Task<byte[]> GenerateTemplateForOneAnswerOption(QuestionTypeEnum questionType)
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add($"{questionType.ToString()}OptionTemplate");
                ws.Cells["A1"].Value = $"Option";
                ws.Cells["B1"].Value = "IsCorrect (Yes-1, No-0)";
                ws.Cells["C1"].Value = "Note: One option per row. Minimum two options and one of them in the column B to be true. New option must be in one line with first option. In Column B Please use only digits 0 1";


                ws.Column(1).Width = 100;
                ws.Column(2).AutoFit();
                ws.Column(3).Width = 100;

                ws.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["C1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                ws.Cells["C1"].Style.WrapText = true;

                ws.Cells["A1:C1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A1:C1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                return await p.GetAsByteArrayAsync();
            }
        }

        public async Task<byte[]> BulkOptionsUpload(IFormFile input, int questionUnitId)
        {
            using (var stream = new MemoryStream())
            {
                await input.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];

                    var optionType = (ws.ToString().Replace("OptionTemplate", ""));

                   
                    if (QuestionTypeEnum.OneAnswer.ToString() == optionType) 
                    {
                        await UploadOneAnswer(ws, questionUnitId);
                    }

                    if (_errors.Count > 0)
                    {
                        return await GenerateErrorsReport(package);
                    }

                    return null;
                }
            }
        }

        private async Task<byte[]> GenerateErrorsReport(ExcelPackage p)
        {
            var ws = p.Workbook.Worksheets.First();

            foreach (var error in _errors)
            {
                ws.Cells[error.Key].Value = error.Value;
                ws.Cells[error.Key].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[error.Key].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
            }
            p.Save();

            return await p.GetAsByteArrayAsync();
        }

        private async Task UploadOneAnswer(ExcelWorksheet ws, int questionUnitId)
        {
            var rowCount = ws.Dimension.Rows;

            var list = new List<AddEditOptionDto>();

            if (rowCount >= 3)
            {
                for (int row = 2; row <= rowCount; row++)
                {
                    var optionIsCorrect = (ws.Cells[row, 2].Value ?? string.Empty).ToString();

                    var option = ParseOption(ws, row, questionUnitId);

                    if (option != null)
                    {
                        list.Add(option);
                        
                    }
                }

                var correctOption = list.Where(x => x.IsCorrect == true).Count();

                if (correctOption == 1)
                {
                    SaveOptions(list);
                }
                else 
                {
                    _errors.Clear();
                    _errors.Add($"{"C"}{2}", "One of option in the column B need to be true(1)");
                }
            }
            else
            {
                _errors.Add($"{"C"}{2}", "Minimum of two options must be entered");
            }
        }

        private async Task<List<AddEditOptionDto>> SaveOptions(List<AddEditOptionDto> options)
        {
            foreach (var option in options)
            {

                var newOption = _mapper.Map<Option>(option);

                await _appDbContext.Options.AddAsync(newOption);

            }

            await _appDbContext.SaveChangesAsync();

            return options;
        }

        private AddEditOptionDto ParseOption(ExcelWorksheet ws, int row, int questionUnitId)
        {
            var answer = (ws.Cells[row, 1].Value ?? string.Empty).ToString();

            if (string.IsNullOrWhiteSpace(answer))
            {
                _errors.Add($"{"C"}{row}", $"Wrong or empty option");
            }

            try
            {
                var isCorrectText = (ws.Cells[row, 2].Value ?? string.Empty).ToString();

                if (isCorrectText.Equals("1"))
                {
                    isCorrect = true;
                }
                else if (isCorrectText.Equals("0"))
                {
                    isCorrect = false;
                }
                else
                {
                    _errors.Add($"{"C"}{row}", "Can't parse is Answer right or wrong. Only '1' and '0' are allowed!");
                }

            }
            catch
            {
                _errors.Add($"{"C"}{row}", "Can't parse is Answer right or wrong. Only '1' and '0' are allowed!");
            }

            return new AddEditOptionDto() {
                Answer = answer.Trim(),
                IsCorrect = isCorrect,
                QuestionUnitId = questionUnitId
                };
        }
    }
}
