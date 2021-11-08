using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CODWER.RERU.Evaluation.Application.Options.AddOption;
using CODWER.RERU.Evaluation.Application.Options.DeleteOption;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class QuestionUnitService : IQuestionUnitService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        private const string _openingHashTag = "[answer]";
        private const string _closingHashTag = "[/answer]";
        private const string _openingInputHtmlTag = "<app-hash-option-input optionId='";
        private const string _closingInputHtmlTag = "'></app-hash-option-input>";
        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        private List<int> _rowsToDelete = new List<int>();

        public QuestionUnitService(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }
        public async Task<QuestionUnit> GetHashedQuestionUnit(int questionUnitId)
        {
            var question = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == questionUnitId);
            var options = await _appDbContext.Options
                .Where(x =>x.QuestionUnitId == questionUnitId)
                .ToListAsync();

            foreach(var option in options)
            {
                question.Question = question.Question.Replace($"[{option.Id}]", $"{_openingInputHtmlTag}{option.Id}{_closingInputHtmlTag}");
            }
            return question;
        }

        public async Task<QuestionUnit> GetUnHashedQuestionUnit(int questionUnitId)
        {
            var question = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == questionUnitId);
            var options = await _appDbContext.Options
                .Where(x => x.QuestionUnitId == questionUnitId)
                .ToListAsync();

            foreach (var option in options)
            {
                question.Question = question.Question.Replace($"[{option.Id}]", $"{_openingHashTag}{option.Answer}{_closingHashTag}");
            }
            return question;
        }

        public async Task<string> GetUnHashedQuestionWithoutTags(int questionUnitId)
        {
            var question = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == questionUnitId);
            var options = await _appDbContext.Options
                .Where(x => x.QuestionUnitId == questionUnitId)
                .ToListAsync();

            foreach (var option in options)
            {
                question.Question = question.Question.Replace($"{_openingInputHtmlTag}{option.Id}{_closingInputHtmlTag}", option.Answer);
            }
            return question.Question;
        }

        public async Task HashQuestionUnit(int questionId)
        {
            var question = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == questionId);

            var hashedQuestion = await CreateHashStringAndSave(question.Id, question.Question, 0);

            question.Question = hashedQuestion;
            await _appDbContext.SaveChangesAsync();
        }

        private async Task<string> CreateHashStringAndSave(int questionId, string input, int counter)
        {
            counter++;
            var answer = input;

            var key = GetKeyFromString(answer);

            if (!string.IsNullOrWhiteSpace(key))
            {
                var existingOption = _appDbContext.Options.FirstOrDefault(x => x.QuestionUnitId == questionId && x.InternalId == counter);

                if (existingOption == null) 
                {
                    var savedOptionId = await SaveOption(key, questionId, counter);
                    answer = answer.Replace($"{_openingHashTag}{key}{_closingHashTag}", $"[{savedOptionId}]");
                }
                else
                {
                    if (key != existingOption.Answer) 
                    {
                        await EditOption(key, existingOption.Id);                        
                    }
                    answer = answer.Replace($"{_openingHashTag}{key}{_closingHashTag}", $"[{existingOption.Id}]");
                }

                if (answer.Contains(_openingHashTag))
                {
                    answer = await CreateHashStringAndSave(questionId, answer, counter);
                }
                else
                {
                    await RemoveUnusedOptions(counter, questionId);
                }
            }         

            return answer;
        }

        private string GetKeyFromString(string input)
        {
            var tempKey = input.Substring(input.IndexOf(_openingHashTag));
            var keyWithTags = tempKey.Substring(0, tempKey.IndexOf(_closingHashTag) + _closingHashTag.Length);
            return keyWithTags.Replace(_openingHashTag, "").Replace(_closingHashTag, "").Trim();
        }

        private async Task<int> SaveOption (string key, int questionUnitId, int counter)
        {
            var answerToSave = new Option
            {
                Answer = key,
                InternalId = counter,
                IsCorrect = true,
                QuestionUnitId = questionUnitId
            };
            await _appDbContext.Options.AddAsync(answerToSave);
            await _appDbContext.SaveChangesAsync();

            return answerToSave.Id;
        }

        private async Task EditOption(string key, int optionId)
        {
            var optionToEdit = await _appDbContext.Options.FirstOrDefaultAsync(x => x.Id == optionId);
            optionToEdit.Answer = key;
            await _appDbContext.SaveChangesAsync();
        }

        private async Task RemoveUnusedOptions(int counter, int questionId)
        {
            var optionsToDelete = await _appDbContext.Options
                .Where(x => x.QuestionUnitId == questionId && x.InternalId > counter)
                .IgnoreQueryFilters()
                .ToListAsync();

            _appDbContext.Options.RemoveRange(optionsToDelete);
        }

        public async Task<byte[]> GenerateExcelTemplate(QuestionTypeEnum questionType)
        {
            if (questionType == QuestionTypeEnum.FreeText)
            {
                return await GenerateFreeTextTemplate($"{questionType.ToString()}Template");
            }
            else if (questionType == QuestionTypeEnum.MultiplyAnswers || questionType == QuestionTypeEnum.OneAnswer)
            {
                return await GenerateStandartAnswerTemplate(questionType);
            }
            else if (questionType == QuestionTypeEnum.HashedAnswer)
            {
                return await GenerateHashedTemplate($"{questionType.ToString()}Template");
            }
            else
            {
                return null;
            }
        }

        private async Task<byte[]> GenerateHashedTemplate(string workSheetName)
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add(workSheetName);
                ws.Cells["A1"].Value = $"QuestionType={(int)QuestionTypeEnum.HashedAnswer}";
                ws.Cells["B1"].Value = "Category Name";
                ws.Cells["C1"].Value = "Question with answer";
                ws.Cells["D1"].Value = "Points (greater than 0)";
                ws.Cells["E1"].Value = "Tags (comma separated)";
                ws.Cells["F1"].Value = "Note: Please use right answer between tags [answer][/answer], whith no spaces at the beginning and at the end";

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).Width = 100;
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).Width = 50;

                ws.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["F1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);                
                ws.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                return await p.GetAsByteArrayAsync();
            }
        }

        private async Task<byte[]> GenerateFreeTextTemplate(string workSheetName)
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add(workSheetName);
                ws.Cells["A1"].Value = $"QuestionType={(int)QuestionTypeEnum.FreeText}";
                ws.Cells["B1"].Value = "Category Name";
                ws.Cells["C1"].Value = "Question";
                ws.Cells["D1"].Value = "Points (greater than 0)";
                ws.Cells["E1"].Value = "Tags (comma separated)";

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).Width = 75;
                ws.Column(4).AutoFit();
                ws.Column(5).Width = 50;
                ws.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                return await p.GetAsByteArrayAsync();
            }
        }

        private async Task<byte[]> GenerateStandartAnswerTemplate(QuestionTypeEnum questionType)
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add($"{questionType.ToString()}Template");
                ws.Cells["A1"].Value = $"QuestionType={(int)questionType}";
                ws.Cells["B1"].Value = "Category Name";
                ws.Cells["C1"].Value = "Question";
                ws.Cells["D1"].Value = "Answer";
                ws.Cells["E1"].Value = "Is Right? (Yes-1, No-0)";
                ws.Cells["F1"].Value = "Points (greater than 0)";
                ws.Cells["G1"].Value = "Tags (comma separated)";
                ws.Cells["J1"].Value = "Note: One answer per row. New question Must be in one line with first answer. In Column D Please use only digits 0 1";

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).Width = 50;
                ws.Column(4).Width = 50;
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();
                ws.Column(7).AutoFit();
                ws.Column(8).Width = 60;
                ws.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells["J1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                ws.Cells["J1"].Style.WrapText = true;

                ws.Cells["A1:J1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                return await p.GetAsByteArrayAsync();
            }
        }

        public async Task<byte[]> BulkQuestionsUpload(IFormFile input)
        {
            using (var stream = new MemoryStream())
            {
                await input.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    var questionType = (QuestionTypeEnum)Int32.Parse(ws.Cells["A1"].Value.ToString().Replace("QuestionType=", ""));
                    
                    switch (questionType)
                    {
                        case QuestionTypeEnum.FreeText:
                            await UploadHashedOrFreeTextAnswer(ws, QuestionTypeEnum.FreeText);
                            break;

                        case QuestionTypeEnum.OneAnswer:
                            await UploadOneOrMultiplyAnswer(ws, QuestionTypeEnum.OneAnswer);
                            break;

                        case QuestionTypeEnum.MultiplyAnswers:
                            await UploadOneOrMultiplyAnswer(ws, QuestionTypeEnum.MultiplyAnswers);
                            break;

                        case QuestionTypeEnum.HashedAnswer:
                            await UploadHashedOrFreeTextAnswer(ws, QuestionTypeEnum.HashedAnswer);
                            break;

                        default:
                            break;
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

            var backCounter = 0;
            foreach (var rowToDelete in _rowsToDelete.Distinct().OrderBy(x => x)) 
            {
                ws.DeleteRow(rowToDelete - backCounter);
                backCounter++;
            }
            p.Save();
            return await p.GetAsByteArrayAsync();
        }

        private List<string> ParseTags(string input)
        {
            var answer = new List<string>();

            while (true)
            {
                if (input.Contains(','))
                {
                    var tagToAdd = input.Substring(0, input.IndexOf(','));
                    answer.Add(tagToAdd.Trim());
                    input = input.Replace($"{tagToAdd},", "");
                }
                else if (!string.IsNullOrWhiteSpace(input))
                {
                    answer.Add(input.Trim());
                    break;
                }
                else
                {
                    break;
                }
            }
            return answer;
        }

        private async Task UploadOneOrMultiplyAnswer(ExcelWorksheet ws, QuestionTypeEnum questionType)
        {
            var rowCount = ws.Dimension.Rows;
            AddQuestionUnitExcelDto questionToAdd = null; 

            for (int row = 2; row <= rowCount; row++)
            {
                var questionText = (ws.Cells[row, 3].Value ?? string.Empty).ToString();
                var answerText = (ws.Cells[row, 4].Value ?? string.Empty).ToString();

                if (!string.IsNullOrWhiteSpace(questionText))
                {
                    if (questionToAdd != null && questionToAdd.AddOptions != null && questionToAdd.AddOptions.Count > 0)  
                    {
                        //here is new question. Save what we have and reset all
                        await SaveQuestionAndOptions(questionToAdd);
                        questionToAdd = null;
                    }

                    questionToAdd = ParseQuestion(ws, questionType, row);

                    if (questionToAdd.Error) 
                    {
                        continue;
                    }

                    var option = ParseOption(ws, row, questionToAdd);

                    if (option != null)
                    {
                        questionToAdd.AddOptions.Add(option);
                    }

                }
                else 
                {
                    if (!questionToAdd.Error)
                    {
                        var option = ParseOption(ws, row, questionToAdd);

                        if (option != null)
                        {
                            questionToAdd.AddOptions.Add(option);
                        }
                    }                    
                }
            }

            if (questionToAdd?.AddOptions?.Count > 0) 
            {
                await SaveQuestionAndOptions(questionToAdd);
            }

        }        

        private async Task UploadHashedOrFreeTextAnswer(ExcelWorksheet ws, QuestionTypeEnum questionType)
        {
            var rowCount = ws.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                var question = ParseQuestion(ws, questionType, row);

                if (question != null)
                {
                    await SaveQuestion(question);
                }                
            }
        }

        private AddQuestionUnitExcelDto ParseQuestion(ExcelWorksheet ws, QuestionTypeEnum questionType, int row)
        {
            var categoryText = (ws.Cells[row, 2].Value ?? string.Empty).ToString();
            var column = GetColumnFromType(questionType);

            if (string.IsNullOrWhiteSpace(categoryText))
            {
                _errors.Add($"{column}{row}", "Category is empty");

                return new AddQuestionUnitExcelDto() { Error = true };
            }

            var category = _appDbContext.QuestionCategories.FirstOrDefault(x => x.Name.ToLower() == categoryText.Trim().ToLower());

            if (category == null)
            {
                _errors.Add($"{column}{row}", $"Cant' find Category '{ws.Cells[row, 2].Value}' in Database");
                return new AddQuestionUnitExcelDto() { Error = true };
            }

            var questionText = ws.Cells[row, 3].Value.ToString().Trim();
            if (String.IsNullOrWhiteSpace(questionText))
            {
                _errors.Add($"{column}{row}", "Question is empty");
                return new AddQuestionUnitExcelDto() { Error = true };
            }

            string pointsColumn = GetPointsColumn(questionType);
            bool isPointsValueNumeric = int.TryParse((ws.Cells[$"{pointsColumn}{row}"].Value ?? string.Empty).ToString(), out int points);
            int questionPoints = 0;
            if (isPointsValueNumeric)
            {
                questionPoints = int.Parse((ws.Cells[$"{pointsColumn}{row}"].Value).ToString());
                if (questionPoints < 1)
                {
                    _errors.Add($"{column}{row}", "Points must be greater than 0");
                    return new AddQuestionUnitExcelDto() { Error = true };
                }
            }
            else
            {
                _errors.Add($"{column}{row}", "Enter numeric value for points");
                return new AddQuestionUnitExcelDto() { Error = true };
            }

            string tagColumn = GetTagColumn(questionType);
            var tagsString = (ws.Cells[$"{tagColumn}{row}"].Value ?? string.Empty).ToString();
            List<string> tagsToAdd = !string.IsNullOrWhiteSpace(tagsString) ? ParseTags(tagsString) : null;

            return new AddQuestionUnitExcelDto
            {
                Error = false,
                Row = row,
                QuestionType = questionType,
                QuestionUnitDto = new AddEditQuestionUnitDto()
                {
                    QuestionCategoryId = category.Id,
                    QuestionType = questionType,
                    Question = questionText,
                    Tags = tagsToAdd,
                    Status = QuestionUnitStatusEnum.Active,
                    QuestionPoints = questionPoints
                },
                AddOptions = new List<AddOptionExcelDto>()
            };
        }

        private AddOptionExcelDto ParseOption(ExcelWorksheet ws, int row, AddQuestionUnitExcelDto questionToAdd)
        {
            var answer = (ws.Cells[row, 4].Value ?? string.Empty).ToString();
            var column = GetColumnFromType(questionToAdd.QuestionType);

            if (string.IsNullOrWhiteSpace(answer))
            {
                _errors.Add($"{column}{row}", $"Wrong or empty answer");
                return new AddOptionExcelDto { Error = true };
            }

            bool isCorrect;
            try
            {
                var isCorrectText = (ws.Cells[row, 5].Value ?? string.Empty).ToString();
                if (isCorrectText.Equals("1"))
                {
                    isCorrect = true;
                }
                else if(isCorrectText.Equals("0"))
                {
                    isCorrect = false;
                }
                else
                {
                    _errors.Add($"{column}{row}", "Can't parse is Answer right or wrong. Only '1' and '0' are allowed!");
                    return new AddOptionExcelDto { Error = true };
                }
                
            }
            catch
            {
                _errors.Add($"{column}{row}", "Can't parse is Answer right or wrong. Only '1' and '0' are allowed!");
                return new AddOptionExcelDto { Error = true };
            }

            return new AddOptionExcelDto()
            {
                Error = false,
                Row = row,
                OptionDto = new AddEditOptionDto()
                {
                    Answer = answer.Trim(),
                    IsCorrect = isCorrect
                }
            };
        }

        private string GetTagColumn(QuestionTypeEnum questionType)
        {
            if (questionType == QuestionTypeEnum.FreeText || questionType == QuestionTypeEnum.HashedAnswer)
            {
                return "F";
            }
            else
            {
                return "G";
            }
        }

        private string GetPointsColumn(QuestionTypeEnum questionType)
        {
            if (questionType == QuestionTypeEnum.FreeText || questionType == QuestionTypeEnum.HashedAnswer)
            {
                return "D";
            }
            else
            {
                return "F";
            }
        }

        private string GetColumnFromType(QuestionTypeEnum questionType)
        {
            if(questionType==QuestionTypeEnum.FreeText|| questionType == QuestionTypeEnum.HashedAnswer)
            {
                return "F";
            }
            else
            {
                return "J";
            }  
        }

        private async Task SaveQuestionAndOptions(AddQuestionUnitExcelDto questionToAdd)
        {
            if (questionToAdd != null && questionToAdd.AddOptions.Count > 0 && !questionToAdd.Error && questionToAdd.AddOptions.All(x => x.Error == false)) 
            {
                var questionId = await SaveQuestion(questionToAdd);

                if (questionId.HasValue)
                {
                    var newOptions = await SaveOptions(questionToAdd.AddOptions, questionId.Value, questionToAdd.QuestionType);

                    if (newOptions.Any(x => x.Error == true))
                    {
                        if (newOptions.Any(x => x.Id > 0))
                        {
                            foreach(var savedOption in newOptions.Where(x => x.Id > 0))
                            {
                                await DeleteOption(savedOption.Id);
                            }
                        }

                        foreach(var option in newOptions)
                        {
                            _rowsToDelete.Remove(option.Row);
                        }
                    }
                }
                else
                {
                    _rowsToDelete.Remove(questionToAdd.Row);
                }
            }
        }

        private async Task<int?> SaveQuestion(AddQuestionUnitExcelDto questionToAdd)
        {
            if (questionToAdd != null && !questionToAdd.Error)
            {
                var column = GetColumnFromType(questionToAdd.QuestionType);

                if (questionToAdd.QuestionType == QuestionTypeEnum.OneAnswer && questionToAdd.AddOptions.Where(x => x.OptionDto.IsCorrect == true).Count() != 1)
                {
                    _errors.Add($"{column}{questionToAdd.Row}", $"Question Type 'One Answer' allows 1 right answer!");
                    return null;
                }

                try
                {
                    var questionId = await _mediator.Send(new AddQuestionUnitCommand { Data = questionToAdd.QuestionUnitDto });
                    _rowsToDelete.Add(questionToAdd.Row);
                    return questionId;
                }
                catch (Exception ex)
                {
                    _errors.Add($"{column}{questionToAdd.Row}", $"Error with adding question: {ex.Message}");
                    return null;
                }
            }
            return null;
        }

        private async Task<List<AddOptionExcelDto>> SaveOptions(List<AddOptionExcelDto> options, int questionId, QuestionTypeEnum questionType)
        {
            var column = GetColumnFromType(questionType);

            foreach (var option in options)
            {
                try
                {
                    option.OptionDto.QuestionUnitId = questionId;
                    option.Id = await _mediator.Send(new AddOptionCommand { Input = option.OptionDto });
                    _rowsToDelete.Add(option.Row);                    
                }
                catch (Exception ex)
                {
                    option.Error = true;
                    _errors.Add($"{column}{option.Row}", $"Error with adding option: {ex.Message}");
                }
            }
            return options;
        }

        private async Task DeleteOption(int id)
        {
            await _mediator.Send(new DeleteOptionCommand { Id = id });
        }
    }
}
