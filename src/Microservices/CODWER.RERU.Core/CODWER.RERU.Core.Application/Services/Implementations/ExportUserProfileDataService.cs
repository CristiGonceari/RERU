using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Services.Implementations
{
    public class ExportUserProfileDataService : IExportUserProfileData
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;
        private int _row { get; set; }
        private int _userProfileId = new();
        private readonly Color _color = Color.FromArgb(169, 169, 169);

        public ExportUserProfileDataService(AppDbContext appDbContext, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }
        public async Task<FileDataDto> ExportUserProfileDatas(int userProfileId)
        {
            var userProfile = await _appDbContext.UserProfiles
                                            .Include(up => up.ModuleRoles)
                                            .ThenInclude(mr => mr.ModuleRole)
                                            .ThenInclude(mr => mr.Module)
                                            .Include(up => up.Role)
                                            .Include(up => up.Department)
                                            .Include(up => up.EmployeeFunction)
                                            .Include(up => up.SolicitedVacantPositions.OrderByDescending(svp => svp.CreateDate))
                                            .ThenInclude(up => up.CandidatePosition)
                                            .ThenInclude(up => up.RequiredDocumentPositions)
                                            .Include(svp => svp.SolicitedVacantPositionUserFiles)
                                            .FirstOrDefaultAsync(up => up.Id == userProfileId);

            _userProfileId = userProfileId;

            var file = await CreateExcellFile(userProfile);

            return file;
        }
        public async Task<FileDataDto> CreateExcellFile(UserProfile userProfile)
        {
            var memoryStream = new MemoryStream();
            using var package = new ExcelPackage(memoryStream);
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            await SetUserProfileGeneralDatas(userProfile, workSheet);

            await SetUserProfileModuleAccess(userProfile.ModuleRoles, workSheet);

            await SetUserProfileSolicitedPosition(userProfile.SolicitedVacantPositions.ToList(), workSheet); ;

            await SetUserProfileTests(workSheet);

            await SetUserProfileEvaluations(workSheet);

            await SetUserProfilePolls(workSheet);

            var excellFile = GetExcelFile(package, userProfile.FullName);

            return excellFile;
        }

        private async Task<ExcelWorksheet> SetUserProfileGeneralDatas(UserProfile userProfile, ExcelWorksheet workSheet)
        {
            _row = 1;

            if (!string.IsNullOrEmpty(userProfile.MediaFileId))
            {
                var image = await _storageFileService.GetFile(userProfile.MediaFileId);
                if (image != null) 
                {
                    MemoryStream ms = new MemoryStream(image.Content);

                    var poza = workSheet.Drawings.AddPicture(image.Name, Image.FromStream(ms));
                    poza.SetPosition(3, 15, 0, 0);
                    poza.SetSize(220, 230);
                }
            }

            workSheet.Rows.Height = 27;
            workSheet.Columns.Width = 22;
            workSheet.Columns.AutoFit();

            workSheet.Cells[_row, 1, 2, 8].Value = "FIŞA PERSONALĂ";
            workSheet.Cells[_row, 1, 2, 8].Merge = true;
            workSheet.Cells[_row, 1, 2, 8].Style.Font.Size = 18;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, 2, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row = _row + 2;

            workSheet.Cells[_row, 1, 12, 2].Merge = true;
            workSheet.Cells[_row, 1, 12, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[_row, 3, 12, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[_row, 3].Value = "Numele:";
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 4, 3, 8].Value = string.IsNullOrEmpty(userProfile.FirstName) ? " - - - - -" : userProfile.FirstName;
            workSheet.Cells[_row, 4, 3, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Prenumele:";
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 4, 4, 8].Value = string.IsNullOrEmpty(userProfile.LastName) ? " - - - - -" : userProfile.LastName;
            workSheet.Cells[_row, 4, 4, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Patronimicul:";
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 4, 5, 8].Value = string.IsNullOrEmpty(userProfile.FatherName) ? " - - - - -" : userProfile.FatherName;
            workSheet.Cells[_row, 4, 5, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Data nasteri:";
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 4, 6, 8].Value = string.IsNullOrEmpty(userProfile.BirthDate?.ToString("dd-MM-yyyy")) ? " - - - - -" : userProfile.BirthDate?.ToString("dd-MM-yyyy");
            workSheet.Cells[_row, 4, 6, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Telefon:";
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 4, 7, 8].Value = string.IsNullOrEmpty(userProfile.PhoneNumber) ? " - - - - -" : userProfile.PhoneNumber;
            workSheet.Cells[_row, 4, 7, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Mail:";
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 4, 8, 8].Value = userProfile.Email;
            workSheet.Cells[_row, 4, 8, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Rol:";
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 4, 9, 8].Value = string.IsNullOrEmpty(userProfile.Role?.Name) ? " - - - - -" : userProfile.Role?.Name;
            workSheet.Cells[_row, 4, 9, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Funcție:";
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 4, 10, 8].Value = string.IsNullOrEmpty(userProfile.EmployeeFunction?.Name) ? " - - - - -" : userProfile.EmployeeFunction?.Name;
            workSheet.Cells[_row, 4, 10, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Departament:";
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 4, 11, 8].Value = string.IsNullOrEmpty(userProfile.Department?.Name) ? " - - - - -" : userProfile.Department?.Name;
            workSheet.Cells[_row, 4, 11, 8].Merge = true;
            _row++;

            workSheet.Cells[_row, 3].Value = "Mod Acces:";
            workSheet.Cells[_row, 3].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 4, 12, 8].Value = string.IsNullOrEmpty(userProfile.AccessModeEnum.ToString()) ? " - - - - -" : userProfile.AccessModeEnum.ToString();
            workSheet.Cells[_row, 4, 12, 8].Merge = true;
            _row++;

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileModuleAccess(List<UserProfileModuleRole> userProfileModuleRoles, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            workSheet.Cells[_row, 1, 13, 8].Value = "1. Accesul la module";
            workSheet.Cells[_row, 1, 13, 8].Merge = true;
            workSheet.Cells[_row, 1, 13, 8].Style.Font.Size = 18;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, 13, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, 13, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row = _row + 2;

            if (userProfileModuleRoles.Count() != 0)
            {

                workSheet.Cells[_row, 1, _row, 3].Value = "Modul";
                workSheet.Cells[_row, 1, _row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1, _row, 3].Merge = true;
                workSheet.Cells[_row, 1, _row, 3].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1, _row, 3].Style.Font.Bold = true;
                workSheet.Cells[_row, 1, _row, 3].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 4, _row, 6].Value = "Permisiune";
                workSheet.Cells[_row, 4, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 4, _row, 6].Merge = true;
                workSheet.Cells[_row, 4, _row, 6].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 4, _row, 6].Style.Font.Bold = true;
                workSheet.Cells[_row, 4, _row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 7, _row, 8].Value = "Cod Permisiune";
                workSheet.Cells[_row, 7, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 7, _row, 8].Merge = true;
                workSheet.Cells[_row, 7, _row, 8].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 7, _row, 8].Style.Font.Bold = true;
                workSheet.Cells[_row, 7, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                _row++;

                foreach (var role in userProfileModuleRoles)
                {
                    workSheet.Cells[_row, 1, _row, 3].Value = role.ModuleRole?.Module?.Name;
                    workSheet.Cells[_row, 1, _row, 3].Merge = true;
                    workSheet.Cells[_row, 1, _row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 1, _row, 3].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 4, _row, 6].Value = role.ModuleRole?.Name;
                    workSheet.Cells[_row, 4, _row, 6].Merge = true;
                    workSheet.Cells[_row, 4, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 7, _row, 8].Value = role.ModuleRole?.Code;
                    workSheet.Cells[_row, 7, _row, 8].Merge = true;
                    workSheet.Cells[_row, 7, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 7, _row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    _row++;

                }
            }
            else
            {
                workSheet.Cells[_row - 2, 1, 13, 8].Value = "1. Accesul la module: nu există informaţii.";
            }

            workSheet.Cells[initialRow, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileSolicitedPosition(List<SolicitedVacantPosition> solicitedVacantPosition, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            workSheet.Cells[_row, 1, _row + 1, 8].Value = "2.Pozitii solicitate";
            workSheet.Cells[_row, 1, _row + 1, 8].Merge = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row = _row + 2;

            if (solicitedVacantPosition.Count() != 0)
            {

                workSheet.Cells[_row, 1, _row, 2].Value = "POZIȚIE CANDIDATĂ";
                workSheet.Cells[_row, 1, _row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1, _row, 2].Merge = true;
                workSheet.Cells[_row, 1, _row, 2].Style.Font.Bold = true;
                workSheet.Cells[_row, 1, _row, 2].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1, _row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 3, _row, 4].Value = "TIMP SOLICITAT";
                workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 3, _row, 4].Merge = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 3, _row, 4].Style.Font.Bold = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 5, _row, 6].Value = "STATUT";
                workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 5, _row, 6].Merge = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 5, _row, 6].Style.Font.Bold = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 7, _row, 8].Value = "DOCUMENTE ATAȘATE/NECESARE";
                workSheet.Cells[_row, 7, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 7, _row, 8].Merge = true;
                workSheet.Cells[_row, 7, _row, 8].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 7, _row, 8].Style.Font.Bold = true;
                workSheet.Cells[_row, 7, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                _row++;

                foreach (var vacantPosition in solicitedVacantPosition)
                {
                    workSheet.Cells[_row, 1, _row, 2].Value = vacantPosition.CandidatePosition?.Name;
                    workSheet.Cells[_row, 1, _row, 2].Merge = true;
                    workSheet.Cells[_row, 1, _row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 1, _row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 3, _row, 4].Value = vacantPosition.CreateDate.ToString("dd-MM-yyyy, H:mm");
                    workSheet.Cells[_row, 3, _row, 4].Merge = true;
                    workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 5, _row, 6].Value = TranslateSolicitedPositionStatusEnum(vacantPosition.SolicitedPositionStatus);
                    workSheet.Cells[_row, 5, _row, 6].Merge = true;
                    workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 5, _row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 7, _row, 8].Value = vacantPosition.SolicitedVacantPositionUserFiles?.Count() + "/" + vacantPosition?.CandidatePosition?.RequiredDocumentPositions?.Count();
                    workSheet.Cells[_row, 7, _row, 8].Merge = true;
                    workSheet.Cells[_row, 7, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 7, _row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    _row++;
                }
            }
            else
            {
                workSheet.Cells[_row - 2, 1, _row + 1, 8].Value = "2.Pozitii solicitate: nu există informaţii.";

            }

            workSheet.Cells[initialRow, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileTests(ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            workSheet.Cells[_row, 1, _row + 1, 8].Value = "3.Teste";
            workSheet.Cells[_row, 1, _row + 1, 8].Merge = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row = _row + 2;

            await SetCellsForUserProfileTests(workSheet);

            await SetCellsForUserProfileEvaluatedTests(workSheet);

            workSheet.Cells[initialRow, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileEvaluations(ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            workSheet.Cells[_row, 1, _row + 1, 8].Value = "4.Evaluari";
            workSheet.Cells[_row, 1, _row + 1, 8].Merge = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row = _row + 2;

            await SetCellsForUserProfileEvaluations(workSheet);

            await SetCellsForUserProfileEvaluatedEvaluations(workSheet);

            workSheet.Cells[initialRow, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfilePolls(ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            workSheet.Cells[_row, 1, _row + 1, 8].Value = "5.Sondaje";
            workSheet.Cells[_row, 1, _row + 1, 8].Merge = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row = _row + 2;

            await SetCellsForUserProfilePolls(workSheet);

            _row--;
            workSheet.Cells[initialRow, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }

        private async Task<ExcelWorksheet> SetCellsForUserProfileTests(ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, 1, _row, 8].Value = "Teste efectuate";
            workSheet.Cells[_row, 1, _row, 8].Merge = true;
            workSheet.Cells[_row, 1, _row, 8].Style.Font.Size = 16;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row++;

            var getTest = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Test);

            if (getTest.Count() != 0)
            {
                workSheet.Cells[_row, 1].Value = "DATA";
                workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1].Style.Font.Bold = true;
                workSheet.Cells[_row, 1].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 2].Value = "STATUT";
                workSheet.Cells[_row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 2].Style.Font.Bold = true;
                workSheet.Cells[_row, 2].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 3, _row, 4].Value = "TESTUL";
                workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 3, _row, 4].Merge = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Font.Bold = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 3, _row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 5, _row, 6].Value = "EVENIMENT";
                workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 5, _row, 6].Merge = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Font.Bold = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 5, _row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 7, _row, 8].Value = "PUNCTE / SCORUL MIN.";
                workSheet.Cells[_row, 7, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 7, _row, 8].Merge = true;
                workSheet.Cells[_row, 7, _row, 8].Style.Font.Bold = true;
                workSheet.Cells[_row, 7, _row, 8].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 7, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                _row++;

                foreach (var test in getTest)
                {
                    workSheet.Cells[_row, 1].Value = test.ProgrammedTime.ToString("dd-MM-yyyy") + "/" + (string.IsNullOrEmpty(test.EndProgrammedTime?.ToString("dd-MM-yyyy")) ? " - - - - -" : test.EndProgrammedTime?.ToString("dd-MM-yyyy"));
                    workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 2].Value = TranslateTestStatusEnum(test.TestStatus);
                    workSheet.Cells[_row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 3, _row, 4].Value = test.TestTemplate.Name;
                    workSheet.Cells[_row, 3, _row, 4].Merge = true;
                    workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 5, _row, 6].Value = string.IsNullOrEmpty(test.Event?.Name) ? " - - - - -" : test.Event.Name;
                    workSheet.Cells[_row, 5, _row, 6].Merge = true;
                    workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 5, _row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 7, _row, 8].Value = (test.AccumulatedPercentage ?? 0) + "/" + test.TestTemplate.MinPercent;
                    workSheet.Cells[_row, 7, _row, 8].Merge = true;
                    workSheet.Cells[_row, 7, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 7, _row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    _row++;
                }
            }
            else
            {
                workSheet.Cells[_row - 1, 1, _row, 8].Value = "Teste efectuate: nu există informaţii.";
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluatedTests(ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, 1, _row, 8].Value = "Teste evaluate";
            workSheet.Cells[_row, 1, _row, 8].Merge = true;
            workSheet.Cells[_row, 1, _row, 8].Style.Font.Size = 16;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row++;

            var getEvaluatedTests = await GetUserProfileEvaluatedTestsOrEvaluations(TestTemplateModeEnum.Test);

            if (getEvaluatedTests.Count() != 0)
            {

                workSheet.Cells[_row, 1].Value = "DATA";
                workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1].Style.Font.Bold = true;
                workSheet.Cells[_row, 1].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 2].Value = "STATUT";
                workSheet.Cells[_row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 2].Style.Font.Bold = true;
                workSheet.Cells[_row, 2].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 3, _row, 4].Value = "EVALUAT";
                workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 3, _row, 4].Merge = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Font.Bold = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 3, _row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 5, _row, 6].Value = "TESTUL";
                workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 5, _row, 6].Merge = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Font.Bold = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 5, _row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 7].Value = "PUNCTE / SCORUL MIN.";
                workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 7].Style.Font.Bold = true;
                workSheet.Cells[_row, 7].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 8].Value = "REZULTAT";
                workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 8].Style.Font.Bold = true;
                workSheet.Cells[_row, 8].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                _row++;

                foreach (var evaluatedTest in getEvaluatedTests)
                {
                    workSheet.Cells[_row, 1].Value = evaluatedTest.ProgrammedTime.ToString("dd-MM-yyyy") + "/" + (string.IsNullOrEmpty(evaluatedTest.EndProgrammedTime?.ToString("dd-MM-yyyy")) ? " - - - - -" : evaluatedTest.EndProgrammedTime?.ToString("dd-MM-yyyy"));
                    workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 2].Value = TranslateTestStatusEnum(evaluatedTest.TestStatus);
                    workSheet.Cells[_row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 3, _row, 4].Value = evaluatedTest.UserProfile?.FullName ?? " - - - - -";
                    workSheet.Cells[_row, 3, _row, 4].Merge = true;
                    workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 5, _row, 6].Value = evaluatedTest.TestTemplate.Name;
                    workSheet.Cells[_row, 5, _row, 6].Merge = true;
                    workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 7].Value = (evaluatedTest.AccumulatedPercentage ?? 0) + "/" + evaluatedTest.TestTemplate.MinPercent;
                    workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 8].Value = TranslateResultStatusValue(evaluatedTest.ResultStatusValue);
                    workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    _row++;
                }
            }
            else
            {
                workSheet.Cells[_row - 1 , 1, _row, 8].Value = "Teste evaluate: nu există informaţii.";
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluations(ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, 1, _row, 8].Value = "Evaluarii";
            workSheet.Cells[_row, 1, _row, 8].Merge = true;
            workSheet.Cells[_row, 1, _row, 8].Style.Font.Size = 16;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row++;

            var getEvaluations = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Evaluation);

            if (getEvaluations.Count() != 0)
            {

                workSheet.Cells[_row, 1].Value = "NUME TEST";
                workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1].Style.Font.Bold = true;
                workSheet.Cells[_row, 1].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 2, _row, 3].Value = "EVALUATOR";
                workSheet.Cells[_row, 2, _row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 2, _row, 3].Merge = true;
                workSheet.Cells[_row, 2, _row, 3].Style.Font.Bold = true;
                workSheet.Cells[_row, 2, _row, 3].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 2, _row, 3].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 4, _row, 5].Value = "EVENIMENT";
                workSheet.Cells[_row, 4, _row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 4, _row, 5].Merge = true;
                workSheet.Cells[_row, 4, _row, 5].Style.Font.Bold = true;
                workSheet.Cells[_row, 4, _row, 5].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 4, _row, 5].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 6].Value = "LOCAȚIE";
                workSheet.Cells[_row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 6].Style.Font.Bold = true;
                workSheet.Cells[_row, 6].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 7].Value = "STATUT";
                workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 7].Style.Font.Bold = true;
                workSheet.Cells[_row, 7].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 8].Value = "REZULTAT";
                workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 8].Style.Font.Bold = true;
                workSheet.Cells[_row, 8].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                _row++;

                foreach (var evaluation in getEvaluations)
                {
                    workSheet.Cells[_row, 1].Value = evaluation.TestTemplate.Name;
                    workSheet.Cells[_row, 1].Merge = true;
                    workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 2, _row, 3].Value = evaluation.Evaluator?.FullName ?? "- - - - -";
                    workSheet.Cells[_row, 2, _row, 3].Merge = true;
                    workSheet.Cells[_row, 2, _row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 4, _row, 5].Value = string.IsNullOrEmpty(evaluation.Event?.Name) ? " - - - - -" : evaluation.Event.Name;
                    workSheet.Cells[_row, 4, _row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 6].Value = evaluation.Location?.Name ?? "--------";
                    workSheet.Cells[_row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 7].Value = TranslateTestStatusEnum(evaluation.TestStatus);
                    workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 8].Value = TranslateResultStatusValue(evaluation.ResultStatusValue);
                    workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    _row++;
                }
            }
            else
            {
                workSheet.Cells[_row - 1, 1, _row, 8].Value = "Evaluarii: nu există informaţii.";

            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluatedEvaluations(ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, 1, _row, 8].Value = "Evaluari evaluate";
            workSheet.Cells[_row, 1, _row, 8].Merge = true;
            workSheet.Cells[_row, 1, _row, 8].Style.Font.Size = 16;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            _row++;

            var getEvaluatedEvaluations = await GetUserProfileEvaluatedTestsOrEvaluations(TestTemplateModeEnum.Evaluation);

            if (getEvaluatedEvaluations.Count() != 0)
            {

                workSheet.Cells[_row, 1].Value = "DATA Programata";
                workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1].Style.Font.Bold = true;
                workSheet.Cells[_row, 1].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 2].Value = "EVALUARE";
                workSheet.Cells[_row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 2].Style.Font.Bold = true;
                workSheet.Cells[_row, 2].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 3, _row, 4].Value = "EVALUAT";
                workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 3, _row, 4].Merge = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Font.Bold = true;
                workSheet.Cells[_row, 3, _row, 4].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 3, _row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 5, _row, 6].Value = "EVENIMENT";
                workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 5, _row, 6].Merge = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Font.Bold = true;
                workSheet.Cells[_row, 5, _row, 6].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 5, _row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 7].Value = "STATUT";
                workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 7].Merge = true;
                workSheet.Cells[_row, 7].Style.Font.Bold = true;
                workSheet.Cells[_row, 7].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 8].Value = "REZULTAT";
                workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 8].Merge = true;
                workSheet.Cells[_row, 8].Style.Font.Bold = true;
                workSheet.Cells[_row, 8].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                _row++;

                foreach (var evaluation in getEvaluatedEvaluations)
                {

                    workSheet.Cells[_row, 1].Value = evaluation.ProgrammedTime.ToString("dd-MM-yyyy");
                    workSheet.Cells[_row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 2].Value = evaluation.TestTemplate.Name;
                    workSheet.Cells[_row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 3, _row, 4].Value = evaluation.UserProfile?.FullName ?? " - - - - -";
                    workSheet.Cells[_row, 3, _row, 4].Merge = true;
                    workSheet.Cells[_row, 3, _row, 4].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    workSheet.Cells[_row, 3, _row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 5, _row, 6].Value = string.IsNullOrEmpty(evaluation.Event?.Name) ? " - - - - -" : evaluation.Event.Name;
                    workSheet.Cells[_row, 5, _row, 6].Merge = true;
                    workSheet.Cells[_row, 5, _row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 7].Value = TranslateTestStatusEnum(evaluation.TestStatus);
                    workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 8].Value = TranslateResultStatusValue(evaluation.ResultStatusValue);
                    workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    _row++;
                }
            }
            else
            {
                workSheet.Cells[_row - 1, 1, _row, 8].Value = "Evaluari evaluate: nu există informaţii.";

            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfilePolls(ExcelWorksheet workSheet)
        {
            var getPolls = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Poll);

            if (getPolls.Count() != 0)
            {

                workSheet.Cells[_row, 1].Value = "NUME";
                workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1].Style.Font.Bold = true;
                workSheet.Cells[_row, 1].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 2, _row, 3].Value = "EVENIMENT";
                workSheet.Cells[_row, 2, _row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 2, _row, 3].Merge = true;
                workSheet.Cells[_row, 2, _row, 3].Style.Font.Bold = true;
                workSheet.Cells[_row, 2, _row, 3].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 2, _row, 3].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 4, _row, 5].Value = "STATUT";
                workSheet.Cells[_row, 4, _row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 4, _row, 5].Merge = true;
                workSheet.Cells[_row, 4, _row, 5].Style.Font.Bold = true;
                workSheet.Cells[_row, 4, _row, 5].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 4, _row, 5].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 6].Value = "TIMPUL VOTĂRII";
                workSheet.Cells[_row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 6].Style.Font.Bold = true;
                workSheet.Cells[_row, 6].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 7].Value = "DATA DE ÎNCEPUT";
                workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 7].Style.Font.Bold = true;
                workSheet.Cells[_row, 7].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                workSheet.Cells[_row, 8].Value = "DATA DE ÎNCHEIERE";
                workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 8].Style.Font.Bold = true;
                workSheet.Cells[_row, 8].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                _row++;

                foreach (var poll in getPolls)
                {
                    workSheet.Cells[_row, 1].Value = poll.TestTemplate.Name;
                    workSheet.Cells[_row, 1].Merge = true;
                    workSheet.Cells[_row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 2, _row, 3].Value = string.IsNullOrEmpty(poll.Event?.Name) ? " - - - - -" : poll.Event.Name;
                    workSheet.Cells[_row, 2, _row, 3].Merge = true;
                    workSheet.Cells[_row, 2, _row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 4, _row, 5].Value = TranslateTestStatusEnum(poll.TestStatus);
                    workSheet.Cells[_row, 4, _row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[_row, 6].Value = string.IsNullOrEmpty(poll.ProgrammedTime.ToString("dd-MM-yyyy")) ? " - - - - -" : poll.ProgrammedTime.ToString("dd-MM-yyyy");
                    workSheet.Cells[_row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 7].Value = string.IsNullOrEmpty(poll.Event?.FromDate.ToString("dd-MM-yyyy")) ? " - - - - -" : poll.Event?.FromDate.ToString("dd-MM-yyyy");
                    workSheet.Cells[_row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[_row, 8].Value = string.IsNullOrEmpty(poll.Event?.TillDate.ToString("dd-MM-yyyy")) ? " - - - - -" : poll.Event?.TillDate.ToString("dd-MM-yyyy");
                    workSheet.Cells[_row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[_row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    _row++;
                }
            }
            else
            {
                workSheet.Cells[_row - 2, 1, _row -2, 8].Value = "5.Sondaje: nu există informaţii.";

            }

            return workSheet;
        }

        private async Task<List<Test>> GetUserProfileEvaluatedTestsOrEvaluations(TestTemplateModeEnum mode)
        {
            var getEvaluatedTests = _appDbContext.Tests
                    .Include(t => t.TestTemplate)
                    .Include(t => t.Event)
                    .Include(t => t.Evaluator)
                    .Include(t => t.Location)
                    .Where(upt => (upt.EvaluatorId == _userProfileId ||
                        _appDbContext.EventEvaluators.Any(ee => ee.EventId == upt.EventId && ee.EvaluatorId == _userProfileId)) &&
                        upt.TestTemplate.Mode == mode)
                    .OrderByDescending(t => t.CreateDate)
                    .ToList();

            return getEvaluatedTests;
        }
        private async Task<List<Test>> GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum mode)
        {
            var getUserProfileTests = _appDbContext.Tests
                    .Include(t => t.TestTemplate)
                    .Include(t => t.Event)
                    .Include(t => t.Evaluator)
                    .Include(t => t.Location)
                    .Where(t => t.TestTemplate.Mode == mode && t.UserProfileId == _userProfileId)
                    .OrderByDescending(t => t.CreateDate)
                    .ToList();

            return getUserProfileTests;
        }
        private FileDataDto GetExcelFile(ExcelPackage package, string name)
        {
            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetExcel("Dosarul_Utilizatorului" + $"({name})", streamBytesArray);
        }
        private string TranslateTestStatusEnum(TestStatusEnum status) 
        {
            string translatedStatus = "";

            switch (status) 
            {
                case TestStatusEnum.Programmed:
                    translatedStatus = "Programat";
                    break;

                case TestStatusEnum.AlowedToStart:
                    translatedStatus = "Permis să înceapă";
                    break;

                case TestStatusEnum.InProgress:
                    translatedStatus = "În progres";
                    break;

                case TestStatusEnum.Terminated:
                    translatedStatus = "Finisat";
                    break;

                case TestStatusEnum.Verified:
                    translatedStatus = "Verificat";
                    break;

                case TestStatusEnum.Closed:
                    translatedStatus = "Închis";
                    break;
            }

            return translatedStatus;
        }
        private string TranslateResultStatusValue(string result)
        {
            string translatedResult = "";

            if (result.Contains("Recommended"))
            {
                translatedResult = result.Replace("Recommended", "Se recomandă");
            }
            else if (result.Contains("NoResult"))
            {
                translatedResult = result.Replace("NoResult", "Fără rezultat");
            }
            else if (result.Contains("Passed"))
            {
                translatedResult = result.Replace("Passed", "Susținut");
            }
            else if (result.Contains("Rejected"))
            {
                translatedResult = result.Replace("Rejected", "Respins");
            }
            else if (result.Contains("Accepted"))
            {
                translatedResult = result.Replace("Accepted", "Admis");
            }
            else if (result.Contains("NotAble"))
            {
                translatedResult = result.Replace("Not able", "Inapt");
            }
            else if (result.Contains("Able"))
            {
                translatedResult = result.Replace("Able", "Apt");
            }
            else
            {
                return result;
            }

            return translatedResult;
        }
        private string TranslateSolicitedPositionStatusEnum(SolicitedPositionStatusEnum positionStatus)
        {
            string translatedPositionStatus = "";

            switch (positionStatus)
            {
                case SolicitedPositionStatusEnum.New:
                    translatedPositionStatus = "Nou";
                    break;

                case SolicitedPositionStatusEnum.Refused:
                    translatedPositionStatus = "Refuzat";
                    break;

                case SolicitedPositionStatusEnum.Approved:
                    translatedPositionStatus = "Aprobat";
                    break;

                case SolicitedPositionStatusEnum.Wait:
                    translatedPositionStatus = "În aşteptare";
                    break;
            }

            return translatedPositionStatus;
        }
    }
}
