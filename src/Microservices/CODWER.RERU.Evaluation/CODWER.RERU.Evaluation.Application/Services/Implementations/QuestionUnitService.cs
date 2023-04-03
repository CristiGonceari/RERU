using CODWER.RERU.Evaluation.Application.Options.AddOption;
using CODWER.RERU.Evaluation.Application.Options.DeleteOption;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
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
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

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
            var question = await _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .FirstOrDefaultAsync(x => x.Id == questionUnitId);

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
            string hashedQuestion = "";

            try
            {
                hashedQuestion = await CreateHashStringAndSave(question.Id, question.Question, 0);
            }
            catch (Exception e)
            {
                _appDbContext.QuestionUnits.Remove(question);
                await _appDbContext.SaveChangesAsync();
                throw;
            }

            question.Question = hashedQuestion;
            await _appDbContext.SaveChangesAsync();
        }

        private async Task<string> CreateHashStringAndSave(int questionId, string input, int counter)
        {
            counter++;
            var answer = input;

            var key = GetKeyFromString(answer);

            var tempKey = answer.Substring(answer.IndexOf(_openingHashTag));
            var keyWithTags = tempKey.Substring(0, tempKey.IndexOf(_closingHashTag) + _closingHashTag.Length);

            if (!string.IsNullOrWhiteSpace(key))
            {
                var existingOption = _appDbContext.Options.FirstOrDefault(x => x.QuestionUnitId == questionId && x.InternalId == counter);

                if (existingOption == null) 
                {
                    var savedOptionId = await SaveOption(key, questionId, counter);

                    answer = answer.Replace($"{keyWithTags}", $"[{savedOptionId}]");
                }
                else
                { 
                    if (key != existingOption.Answer) 
                    {
                        await EditOption(key, existingOption.Id);                        
                    }
                    answer = answer.Replace($"{keyWithTags}", $"[{existingOption.Id}]");
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
            else
            {
                throw new Exception(ValidationCodes.TAGS_WRITTEN_WITH_MISTAKE_OR_MISSING_ANSWER_OPTION);
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
            switch (questionType)
            {
                case QuestionTypeEnum.FreeText:
                    return await GenerateFreeTextTemplate($"{TranslateAndFormatQuestion(QuestionTypeEnum.FreeText)}Sablon");
                case QuestionTypeEnum.MultipleAnswers:
                case QuestionTypeEnum.OneAnswer:
                    return await GenerateStandardAnswerTemplate(questionType);
                case QuestionTypeEnum.HashedAnswer:
                    return await GenerateHashedTemplate($"{TranslateAndFormatQuestion(QuestionTypeEnum.FreeText)}Sablon");
                case QuestionTypeEnum.FileAnswer:
                    return await GenerateFreeTextTemplate($"{TranslateAndFormatQuestion(QuestionTypeEnum.FreeText)}Sablon");
                default:
                    return null;
            }
        }

        private async Task<byte[]> GenerateHashedTemplate(string workSheetName)
        {
            using var p = new ExcelPackage();
            var ws = p.Workbook.Worksheets.Add(workSheetName);
            ws.Cells["A1"].Value = $"TipÎntrebare={(int)QuestionTypeEnum.HashedAnswer}";
            ws.Cells["B1"].Value = "Categorie";
            ws.Cells["C1"].Value = "Întrebare cu răspuns";
            ws.Cells["D1"].Value = "Puncte (mai multe de 0)";
            ws.Cells["E1"].Value = "Taguri (separate prin virgulă)";
            ws.Cells["F1"].Value = "Notă: Vă rugăm să amplasați răspunsul corect între etichetele [answer][/answer], fără spații la început și la sfârșit";

            ws.Column(1).AutoFit();
            ws.Column(2).AutoFit();
            ws.Column(3).Width = 100;
            ws.Column(4).AutoFit();
            ws.Column(5).AutoFit();
            ws.Column(6).Width = 40;

            ws.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["F1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);   
            ws.Cells["F1"].Style.WrapText = true;

            ws.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            return await p.GetAsByteArrayAsync();
        }

        private async Task<byte[]> GenerateFreeTextTemplate(string workSheetName)
        {
            using var p = new ExcelPackage();
            var ws = p.Workbook.Worksheets.Add(workSheetName);
            ws.Cells["A1"].Value = $"TipÎntrebare={(int)QuestionTypeEnum.FreeText}";
            ws.Cells["B1"].Value = "Categorie";
            ws.Cells["C1"].Value = "Întrebare";
            ws.Cells["D1"].Value = "Puncte (mai multe de 0)";
            ws.Cells["E1"].Value = "Taguri (separate prin virgulă)";

            ws.Column(1).AutoFit();
            ws.Column(2).AutoFit();
            ws.Column(3).Width = 75;
            ws.Column(4).AutoFit();
            ws.Column(5).Width = 50;
            ws.Cells["A1:E1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            return await p.GetAsByteArrayAsync();
        }

        private async Task<byte[]> GenerateStandardAnswerTemplate(QuestionTypeEnum questionType)
        {
            using var p = new ExcelPackage();
            var ws = p.Workbook.Worksheets.Add(TranslateAndFormatQuestion(questionType));
            ws.Cells["A1"].Value = $"TipÎntrebare={(int)questionType}";
            ws.Cells["B1"].Value = "Categorie";
            ws.Cells["C1"].Value = "Întrebare";
            ws.Cells["D1"].Value = "Răspuns";
            ws.Cells["E1"].Value = "Este corect? (Da-1, Nu-0)";
            ws.Cells["F1"].Value = "Puncte (mai multe de 0)";
            ws.Cells["G1"].Value = "Taguri (separate prin virgulă)";
            ws.Cells["H1"].Value = "Notă: Un răspuns per rand. Întrebarea nouă trebuie să fie într-o linie cu primul răspuns. În coloana D utilizați doar 0 sau 1";

            ws.Column(1).AutoFit();
            ws.Column(2).AutoFit();
            ws.Column(3).Width = 50;
            ws.Column(4).Width = 50;
            ws.Column(5).AutoFit();
            ws.Column(6).AutoFit();
            ws.Column(7).AutoFit();
            ws.Column(8).Width = 40;
            ws.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["H1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
            ws.Cells["H1"].Style.WrapText = true;

            ws.Cells["A1:H1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            return await p.GetAsByteArrayAsync();
        }

        public async Task<byte[]> BulkQuestionsUpload(IFormFile input)
        {
            using (var stream = new MemoryStream())
            {
                await input.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    var questionType = (QuestionTypeEnum)Int32.Parse(ws.Cells["A1"].Value.ToString()?.Replace("TipÎntrebare=", ""));
                    
                    switch (questionType)
                    {
                        case QuestionTypeEnum.FreeText:
                            await UploadHashedOrFreeTextAnswer(ws, QuestionTypeEnum.FreeText);
                            break;

                        case QuestionTypeEnum.OneAnswer:
                            await UploadOneOrMultiplyAnswer(ws, QuestionTypeEnum.OneAnswer);
                            break;

                        case QuestionTypeEnum.MultipleAnswers:
                            await UploadOneOrMultiplyAnswer(ws, QuestionTypeEnum.MultipleAnswers);
                            break;

                        case QuestionTypeEnum.HashedAnswer:
                            await UploadHashedOrFreeTextAnswer(ws, QuestionTypeEnum.HashedAnswer);
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
            await p.SaveAsync();

            var backCounter = 0;
            foreach (var rowToDelete in _rowsToDelete.Distinct().OrderBy(x => x)) 
            {
                ws.DeleteRow(rowToDelete - backCounter);
                backCounter++;
            }
            await p.SaveAsync();
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

                var categoryText = (ws.Cells[row, 2].Value ?? string.Empty).ToString();

                var isRowEmpty = IsEmptyRow(ws, row, 1);

                if (isRowEmpty) continue;

                if (!string.IsNullOrWhiteSpace(categoryText) || !string.IsNullOrWhiteSpace(questionText))
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
            var errors = "";
            var categoryText = (ws.Cells[row, 2].Value ?? string.Empty).ToString();
            var column = GetColumnFromType(questionType);

            if (string.IsNullOrWhiteSpace(categoryText))
            {
                errors = "Categoria lipsește ";
            }

            var category = _appDbContext.QuestionCategories.FirstOrDefault(x => x.Name.ToLower() == categoryText.Trim().ToLower());

            if (category == null)
            {
                if (string.IsNullOrWhiteSpace(errors))
                   errors = $"Categoria: '{ws.Cells[row, 2].Value}' nu a fost găsită în baza de date ";
                else
                   errors += $"\nCategoria: '{ws.Cells[row, 2].Value}' nu a fost găsită în baza de date ";
            }

            var questionText = (ws.Cells[row, 3].Value ?? string.Empty).ToString();;
            if (string.IsNullOrWhiteSpace(questionText))
            {
                if (string.IsNullOrWhiteSpace(errors))
                    errors = "Întrebarea lipsește ";
                else
                    errors += "\nÎntrebarea lipsește ";
            }

            string pointsColumn = GetPointsColumn(questionType);
            bool isPointsValueNumeric = int.TryParse((ws.Cells[$"{pointsColumn}{row}"].Value ?? string.Empty).ToString(), out int points);
            int questionPoints = 0;
            if (isPointsValueNumeric)
            {
                questionPoints = int.Parse((ws.Cells[$"{pointsColumn}{row}"].Value).ToString());
                if (questionPoints < 1)
                {
                    if (string.IsNullOrWhiteSpace(errors))
                        errors = "Punctele trebuie să fie mai mari decât 0 ";
                    else
                        errors += "\nPunctele trebuie să fie mai mari decât 0 ";
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(errors))
                    errors = "Introduceți o valoarea numerică pentru puncte ";
                else
                    errors += "\nIntroduceți o valoarea numerică pentru puncte ";
            }

            if (!string.IsNullOrWhiteSpace(errors))
            {
                _errors.Add($"{column}{row}", errors);
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
                _errors.Add($"{column}{row}", "Răspuns greșit sau gol ");
                return new AddOptionExcelDto { Error = true };
            }

            bool isCorrect;
            try
            {
                var isCorrectText = (ws.Cells[row, 5].Value ?? string.Empty).ToString();
                switch (isCorrectText)
                {
                    case "1":
                        isCorrect = true;
                        break;
                    case "0":
                        isCorrect = false;
                        break;
                    default:
                        _errors.Add($"{column}{row}", "Nu se poate analiza dacă Răspunsul este corect sau greșit. Este permis doar '1' și '0'! ");
                        return new AddOptionExcelDto { Error = true };
                }
            }
            catch
            {
                _errors.Add($"{column}{row}", "Nu se poate analiza dacă Răspunsul este corect sau greșit. Este permis doar '1' și '0'! ");
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

        private string GetTagColumn(QuestionTypeEnum questionType) => questionType is QuestionTypeEnum.FreeText or QuestionTypeEnum.HashedAnswer ? "E" : "G";

        private string GetPointsColumn(QuestionTypeEnum questionType) => questionType is QuestionTypeEnum.FreeText or QuestionTypeEnum.HashedAnswer ? "D" : "F";

        private string GetColumnFromType(QuestionTypeEnum questionType) => questionType is QuestionTypeEnum.FreeText or QuestionTypeEnum.HashedAnswer ? "F" : "J";

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

                if (questionToAdd.QuestionType == QuestionTypeEnum.OneAnswer && questionToAdd.AddOptions.Count(x => x.OptionDto.IsCorrect) != 1)
                {
                    _errors.Add($"{column}{questionToAdd.Row}", "Întrebarea de tipul 'UnRăspuns' permit doar un singur răspuns! ");
                    return null;
                }

                if (questionToAdd.QuestionType == QuestionTypeEnum.MultipleAnswers && questionToAdd.AddOptions.Count(x => x.OptionDto.IsCorrect) < 1)
                {
                    _errors.Add($"{column}{questionToAdd.Row}", "Întrebarea de tipul 'RăspunsuriMultiple' ar trebui să aibă mai mult de un răspuns corect! ");
                    return null;
                }

                try
                {
                    var questionId = await _mediator.Send(new AddQuestionUnitCommand { 
                        Question = questionToAdd.QuestionUnitDto.Question,
                        QuestionCategoryId = questionToAdd.QuestionUnitDto.QuestionCategoryId,
                        Tags = questionToAdd.QuestionUnitDto.Tags,
                        Id = questionToAdd.QuestionUnitDto.Id,
                        QuestionType = questionToAdd.QuestionUnitDto.QuestionType,
                        Status = questionToAdd.QuestionUnitDto.Status,
                        QuestionPoints = questionToAdd.QuestionUnitDto.QuestionPoints,

                    });
                    _rowsToDelete.Add(questionToAdd.Row);
                    return questionId;
                }
                catch (Exception ex)
                {
                    _errors.Add($"{column}{questionToAdd.Row}", $"Eroare la adăugarea întrebării: {ex.Message} ");
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
                    option.Id = await _mediator.Send(new AddOptionCommand { 
                        Id = option.OptionDto.Id,
                        QuestionUnitId = option.OptionDto.QuestionUnitId,
                        Answer = option.OptionDto.Answer,
                        IsCorrect = option.OptionDto.IsCorrect,
                    });
                    _rowsToDelete.Add(option.Row);                    
                }
                catch (Exception ex)
                {
                    option.Error = true;
                    _errors.Add($"{column}{option.Row}", $"Eroare la adăugarea opțiunii: {ex.Message} ");
                }
            }
            return options;
        }

        private async Task DeleteOption(int id)
        {
            await _mediator.Send(new DeleteOptionCommand { Id = id });
        }

        private  bool IsEmptyRow (ExcelWorksheet ws, int row, int startColumn) => ws.Cells[row, startColumn, row, ws.Cells.End.Column].All(c => c.Value == null);

        public static string TranslateAndFormatQuestion(QuestionTypeEnum type)
        {
            switch (type)
            {
                case QuestionTypeEnum.FreeText:
                    return "FormaLibera";
                case QuestionTypeEnum.MultipleAnswers:
                    return "RaspunsuriMultiple";
                case QuestionTypeEnum.OneAnswer:
                    return "UnRaspuns";
                case QuestionTypeEnum.HashedAnswer:
                    return "CompleteazaTextul";
                case QuestionTypeEnum.FileAnswer:
                    return "IncarcaFisier";
                default:
                    return "Intrebare";
            }
        }
    }
}
