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
        private int row { get; set; }
        private int UserProfileId { get; set; }

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
                                            .Include(up => up.SolicitedVacantPositions)
                                            .ThenInclude(up => up.CandidatePosition)
                                            .ThenInclude(up => up.RequiredDocumentPositions)
                                            .Include(svp => svp.SolicitedVacantPositionUserFiles)
                                            .Include(up => up.Tests)
                                            .ThenInclude(up => up.Event)
                                            .Include(up => up.Tests)
                                            .ThenInclude(t => t.TestTemplate)
                                            .Include(up => up.Tests)
                                            .ThenInclude(t => t.Evaluator)
                                            .Include(up => up.Tests)
                                            .ThenInclude(up => up.Location)
                                            .FirstOrDefaultAsync(up => up.Id == userProfileId);

            this.UserProfileId = userProfile.Id;

            var file = await CreateExcellFile(userProfile);

            return file;
        }

        public async Task<FileDataDto> CreateExcellFile(UserProfile userProfile) 
        {
            //var path = new FileInfo("PersonalFile/Fisa-Personala.xlsx");
            var memoryStream = new MemoryStream();
            using var package = new ExcelPackage(memoryStream);
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            await SetUserProfileGeneralDatas(userProfile, workSheet);

            await SetUserProfileModuleAccess(userProfile.ModuleRoles, workSheet);

            await SetUserProfileSolicitedPosition(userProfile.SolicitedVacantPositions.ToList(), workSheet);

            await SetUserProfileTests(userProfile.Tests.ToList(), workSheet);

            await SetUserProfileEvaluations(userProfile.Tests.ToList(), workSheet);

            await SetUserProfilePolls(userProfile.Tests.ToList(), workSheet);

            var excellFile = GetExcelFile(package);

            return excellFile;
        }

        private async Task<ExcelWorksheet> SetUserProfileGeneralDatas(UserProfile userProfile, ExcelWorksheet workSheet)
        {
            int i = 1;

            if (!string.IsNullOrEmpty(userProfile.MediaFileId)) 
            {
                var image = await _storageFileService.GetFile(userProfile.MediaFileId);
                
                MemoryStream ms = new MemoryStream(image.Content);
              
                var poza = workSheet.Drawings.AddPicture(image.Name, Image.FromStream(ms));
                poza.SetPosition(3, 15, 0, 0);
                poza.SetSize(220, 230);
            }

            workSheet.Rows.Height = 27;
            workSheet.Columns.Width= 22;

            workSheet.Cells[i, 1, 2, 8].Value = "FIŞA PERSONALĂ";
            workSheet.Cells[i, 1, 2, 8].Merge = true;
            workSheet.Cells[i, 1, 2, 8].Style.Font.Size = 18;
            workSheet.Cells[i, 1].Style.Font.Bold = true;
            workSheet.Cells[i, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[i, 1, 2, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            i = i+ 2;

            workSheet.Cells[i, 1, 11, 2].Merge = true;
            workSheet.Cells[i, 1, 11, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[i, 3, 11, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[i, 3].Value = "Numele:";
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 4, 3, 8].Value = string.IsNullOrEmpty(userProfile.FirstName) ? " - - - - -" : userProfile.FirstName;
            workSheet.Cells[i, 4, 3, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Prenumele:";
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 4 , 4, 8].Value = string.IsNullOrEmpty(userProfile.LastName) ? " - - - - -" : userProfile.LastName;
            workSheet.Cells[i, 4, 4, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Patronimicul:";
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 4, 5, 8].Value = string.IsNullOrEmpty(userProfile.FatherName) ? " - - - - -" : userProfile.FatherName;
            workSheet.Cells[i, 4, 5, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Data nasteri:";
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 4, 6, 8].Value = string.IsNullOrEmpty(userProfile.BirthDate?.ToString("dd/MM/yyyy")) ? " - - - - -" : userProfile.BirthDate?.ToString("dd/MM/yyyy");
            workSheet.Cells[i, 4, 6, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Telefon:";
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 4, 7, 8].Value = string.IsNullOrEmpty(userProfile.PhoneNumber) ? " - - - - -" : userProfile.PhoneNumber;
            workSheet.Cells[i, 4, 7, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Mail:";
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 4, 8, 8].Value = userProfile.Email;
            workSheet.Cells[i, 4, 8, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Rol:";
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 4, 9, 8].Value = string.IsNullOrEmpty(userProfile.Role?.Name) ? " - - - - -" : userProfile.Role?.Name;
            workSheet.Cells[i, 4, 9, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Departament:";
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 4, 10, 8].Value = string.IsNullOrEmpty(userProfile.Department?.Name) ? " - - - - -" : userProfile.Department?.Name;
            workSheet.Cells[i, 4, 10, 8].Merge = true;
            i++;

            workSheet.Cells[i, 3].Value = "Mod Acces:";
            workSheet.Cells[i, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[i, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[i, 4, 11, 8].Value = string.IsNullOrEmpty(userProfile.AccessModeEnum.ToString()) ? " - - - - -" : userProfile.AccessModeEnum.ToString();
            workSheet.Cells[i, 4, 11, 8].Merge = true;
            i++;

            this.row = i;

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileModuleAccess(List<UserProfileModuleRole> userProfileModuleRoles, ExcelWorksheet workSheet)
        {
            int initialRow = this.row;

            workSheet.Cells[this.row, 1, 13, 8].Value = "1. Accesul la module";
            workSheet.Cells[this.row, 1, 13, 8].Merge = true;
            workSheet.Cells[this.row, 1, 13, 8].Style.Font.Size = 18;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, 13, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, 13, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row = this.row + 2;

            workSheet.Cells[this.row, 1, this.row, 3].Value = "Modul";
            workSheet.Cells[this.row, 1, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 3].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 1, this.row, 3].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row, 3].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 4, this.row, 6].Value = "Permisiune";
            workSheet.Cells[this.row, 4, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 4, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 4, this.row, 6].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 4, this.row, 6].Style.Font.Bold = true;
            workSheet.Cells[this.row, 4, this.row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 7, this.row, 8].Value = "Cod Permisiune";
            workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 7, this.row, 8].Style.Font.Bold = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            if (userProfileModuleRoles.Count() != 0)
            {
                foreach (var role in userProfileModuleRoles)
                {
                    workSheet.Cells[this.row, 1, this.row, 3].Value = role.ModuleRole?.Module?.Name;
                    workSheet.Cells[this.row, 1, this.row, 3].Merge = true;
                    workSheet.Cells[this.row, 1, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 1, this.row, 3].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 4, this.row, 6].Value = role.ModuleRole?.Name;
                    workSheet.Cells[this.row, 4, this.row, 6].Merge = true;
                    workSheet.Cells[this.row, 4, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 7, this.row, 8].Value = role.ModuleRole?.Code;
                    workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
                    workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 7, this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    this.row++;

                }
            }
            else
            {
                await EmptyCellsForUserProfileModuleAcces(workSheet);
            }

            workSheet.Cells[initialRow, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileSolicitedPosition(List<SolicitedVacantPosition> solicitedVacantPosition, ExcelWorksheet workSheet)
        {
            int initialRow = this.row;

            workSheet.Cells[this.row, 1, this.row + 1, 8].Value = "2.Pozitii solicitate";
            workSheet.Cells[this.row, 1, this.row + 1, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row = this.row + 2;

            workSheet.Cells[this.row, 1, this.row, 2].Value = "POZIȚIE CANDIDATĂ";
            workSheet.Cells[this.row, 1, this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 2].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 2].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row, 2].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 1, this.row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 3, this.row, 4].Value = "TIMP SOLICITAT";
            workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 3, this.row, 4].Style.Font.Bold = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 5, this.row, 6].Value = "STATUT";
            workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 5, this.row, 6].Style.Font.Bold = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 7, this.row, 8].Value = "DOCUMENTE ATAȘATE/NECESARE";
            workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 7, this.row, 8].Style.Font.Bold = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            if (solicitedVacantPosition.Count() != 0)
            {
                foreach (var vacantPosition in solicitedVacantPosition)
                {
                    workSheet.Cells[this.row, 1, this.row, 2].Value = vacantPosition.CandidatePosition?.Name;
                    workSheet.Cells[this.row, 1, this.row, 2].Merge = true;
                    workSheet.Cells[this.row, 1, this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 1, this.row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 3, this.row, 4].Value = vacantPosition.CreateDate.ToString("dd/MM/yyyy, H:mm");
                    workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
                    workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 5, this.row, 6].Value = vacantPosition.SolicitedPositionStatus.ToString();
                    workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
                    workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 5, this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 7, this.row, 8].Value = vacantPosition.SolicitedVacantPositionUserFiles?.Count() + "/" + vacantPosition?.CandidatePosition?.RequiredDocumentPositions?.Count();
                    workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
                    workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 7, this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    this.row++;
                }
            }
            else 
            {
                await EmptyCellsForUserProfileVacantPositions(workSheet);
            }

            workSheet.Cells[initialRow, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileTests(List<Test> userProfileTests, ExcelWorksheet workSheet)
        {
            int initialRow = this.row;

            workSheet.Cells[this.row, 1, this.row + 1, 8].Value = "3.Teste";
            workSheet.Cells[this.row, 1, this.row + 1, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row = this.row + 2;

            await SetCellsForUserPorfileTests(userProfileTests, workSheet);

            await SetCellsForUserProfileEvaluatedTests(userProfileTests, workSheet);

            workSheet.Cells[initialRow, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileEvaluations(List<Test> userProfileTests, ExcelWorksheet workSheet)
        {
            int initialRow = this.row;

            workSheet.Cells[this.row, 1, this.row + 1, 8].Value = "4.Evaluari";
            workSheet.Cells[this.row, 1, this.row + 1, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row = this.row + 2;

            await SetCellsForUserProfileEvaluations(userProfileTests, workSheet);

            await SetCellsForUserProfileEvaluatedEvaluations(userProfileTests, workSheet);

            workSheet.Cells[initialRow, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfilePolls(List<Test> userProfileTests, ExcelWorksheet workSheet)
        {
            int initialRow = this.row;

            workSheet.Cells[this.row, 1, this.row + 1, 8].Value = "5.Sondaje";
            workSheet.Cells[this.row, 1, this.row + 1, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Font.Size = 18;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row + 1, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row = this.row + 2;

            await SetCellsForUserProfilePolls(userProfileTests, workSheet);

            this.row--;
            workSheet.Cells[initialRow, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            return workSheet;
        }

        private async Task<ExcelWorksheet> SetCellsForUserPorfileTests(List<Test> tests, ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1, this.row, 8].Value = "Teste efectuate";
            workSheet.Cells[this.row, 1, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Font.Size = 16;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            workSheet.Cells[this.row, 1].Value = "DATA";
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 2].Value = "STATUT";
            workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2].Style.Font.Bold = true;
            workSheet.Cells[this.row, 2].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 3, this.row, 4].Value = "TESTUL";
            workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Font.Bold = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 3, this.row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 5, this.row, 6].Value = "EVENIMENT";
            workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Font.Bold = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 5, this.row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 7, this.row, 8].Value = "PUNCTE / SCORUL MIN.";
            workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Font.Bold = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 7, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            var getTest = await GetUserProfileTests(tests);

            if (getTest.Count() != 0)
            {
                foreach (var test in getTest)
                {
                    workSheet.Cells[this.row, 1].Value = test.ProgrammedTime.ToString("dd/MM/yyyy") + "-" + (string.IsNullOrEmpty(test.EndProgrammedTime?.ToString("dd/MM/yyyy")) ? " - - - - -" : test.EndProgrammedTime?.ToString("dd/MM/yyyy"));
                    workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 2].Value = test.TestStatus.ToString();
                    workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 3, this.row, 4].Value = test.TestTemplate.Name;
                    workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
                    workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 5, this.row, 6].Value = string.IsNullOrEmpty(test.Event?.Name) ? " - - - - -" : test.Event.Name;
                    workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
                    workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 5, this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 7, this.row, 8].Value = test.AccumulatedPercentage + "/" + test.TestTemplate.MinPercent;
                    workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
                    workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 7, this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    this.row++;
                }
            }
            else
            {
                await EmptyCellsForUserProfileTests(workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluatedTests(List<Test> tests, ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1, this.row, 8].Value = "Teste evaluate";
            workSheet.Cells[this.row, 1, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Font.Size = 16;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            workSheet.Cells[this.row, 1].Value = "DATA";
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 2].Value = "STATUT";
            workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2].Style.Font.Bold = true;
            workSheet.Cells[this.row, 2].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 3, this.row, 4].Value = "EVALUAT";
            workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Font.Bold = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 3, this.row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 5, this.row, 6].Value = "TESTUL";
            workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Font.Bold = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 5, this.row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 7].Value = "PUNCTE / SCORUL MIN.";
            workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7].Style.Font.Bold = true;
            workSheet.Cells[this.row, 7].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 8].Value = "REZULTAT";
            workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 8].Style.Font.Bold = true;
            workSheet.Cells[this.row, 8].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            var getEvaluatedTests = await GetUserProfileEvaluatedTests(tests);

            if (getEvaluatedTests.Count() != 0)
            {
                foreach (var evaluatedTest in getEvaluatedTests)
                {
                    workSheet.Cells[this.row, 1].Value = evaluatedTest.ProgrammedTime.ToString("dd/MM/yyyy") + "-" + (string.IsNullOrEmpty(evaluatedTest.EndProgrammedTime?.ToString("dd/MM/yyyy")) ? " - - - - -" : evaluatedTest.EndProgrammedTime?.ToString("dd/MM/yyyy"));
                    workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 2].Value = evaluatedTest.TestStatus.ToString();
                    workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 3, this.row, 4].Value = evaluatedTest.Evaluator.FullName;
                    workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
                    workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 5, this.row, 6].Value = evaluatedTest.TestTemplate;
                    workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
                    workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 7].Value = evaluatedTest.AccumulatedPercentage + "/" + evaluatedTest.TestTemplate.MinPercent;
                    workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 8].Value = evaluatedTest.ResultStatusValue;
                    workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    this.row++;
                }
            }
            else
            {
                await EmptyCellsForUserProfileEvaluatedTests(workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluations(List<Test> tests, ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1, this.row, 8].Value = "Evaluarii";
            workSheet.Cells[this.row, 1, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Font.Size = 16;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            workSheet.Cells[this.row, 1].Value = "NUME TEST";
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 2, this.row, 3].Value = "EVALUATOR";
            workSheet.Cells[this.row, 2, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2, this.row, 3].Merge = true;
            workSheet.Cells[this.row, 2, this.row, 3].Style.Font.Bold = true;
            workSheet.Cells[this.row, 2, this.row, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 2, this.row, 3].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 4, this.row, 5].Value = "EVENIMENT";
            workSheet.Cells[this.row, 4, this.row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 4, this.row, 5].Merge = true;
            workSheet.Cells[this.row, 4, this.row, 5].Style.Font.Bold = true;
            workSheet.Cells[this.row, 4, this.row, 5].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 4, this.row, 5].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 6].Value = "LOCAȚIE";
            workSheet.Cells[this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 6].Style.Font.Bold = true;
            workSheet.Cells[this.row, 6].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 7].Value = "STATUT";
            workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7].Style.Font.Bold = true;
            workSheet.Cells[this.row, 7].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 8].Value = "REZULTAT";
            workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 8].Style.Font.Bold = true;
            workSheet.Cells[this.row, 8].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            var getEvaluations = await GetUserProfileEvaluations(tests);

            if (getEvaluations.Count() != 0)
            {
                foreach (var evaluation in getEvaluations)
                {
                    workSheet.Cells[this.row, 1].Value = evaluation.TestTemplate.Name;
                    workSheet.Cells[this.row, 1].Merge = true;
                    workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 2, this.row, 3].Value = evaluation.Evaluator.FullName;
                    workSheet.Cells[this.row, 2, this.row, 3].Merge = true;
                    workSheet.Cells[this.row, 2, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 4, this.row, 5].Value = string.IsNullOrEmpty(evaluation.Event?.Name) ? " - - - - -" : evaluation.Event.Name;
                    workSheet.Cells[this.row, 4, this.row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 6].Value = evaluation.Location.Name;
                    workSheet.Cells[this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 7].Value = evaluation.TestStatus.ToString();
                    workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 8].Value = evaluation.ResultStatusValue;
                    workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    this.row++;
                }
            }
            else
            {
                await EmptyCellsForUserProfileEvaluationsAndPolls(workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluatedEvaluations(List<Test> tests, ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1, this.row, 8].Value = "Evaluari evaluate";
            workSheet.Cells[this.row, 1, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Font.Size = 16;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            workSheet.Cells[this.row, 1].Value = "EVALUARE";
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 2].Value = "DATA";
            workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2].Style.Font.Bold = true;
            workSheet.Cells[this.row, 2].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 3, this.row, 4].Value = "EVALUAT";
            workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Font.Bold = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 3, this.row, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 5, this.row, 6].Value = "EVENIMENT";
            workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Font.Bold = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 5, this.row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 7].Value = "STATUT";
            workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7].Merge = true;
            workSheet.Cells[this.row, 7].Style.Font.Bold = true;
            workSheet.Cells[this.row, 7].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 8].Value = "REZULTAT";
            workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 8].Merge = true;
            workSheet.Cells[this.row, 8].Style.Font.Bold = true;
            workSheet.Cells[this.row, 8].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            var getEvaluatedEvaluations = await GetUserProfileEvaluatedEvaluations(tests);

            if (getEvaluatedEvaluations.Count() != 0)
            {
                foreach (var evaluation in getEvaluatedEvaluations)
                {
                    workSheet.Cells[this.row, 1].Value = evaluation.TestTemplate.Name;
                    workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 2].Value = evaluation.ProgrammedTime.ToString("dd/MM/yyyy");
                    workSheet.Cells[this.row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 3, this.row, 4].Value = evaluation.Evaluator.FullName;
                    workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
                    workSheet.Cells[this.row, 3, this.row, 4].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 5, this.row, 6].Value = string.IsNullOrEmpty(evaluation.Event?.Name) ? " - - - - -" : evaluation.Event.Name;
                    workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
                    workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 7].Value = evaluation.TestStatus.ToString();
                    workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 8].Value = evaluation.ResultStatusValue;
                    workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    this.row++;
                }
            }
            else
            {
                await EmptyCellsForUserProfileEvaluatedTests(workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfilePolls(List<Test> tests, ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1].Value = "NUME";
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Font.Bold = true;
            workSheet.Cells[this.row, 1].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 2, this.row, 3].Value = "EVENIMENT";
            workSheet.Cells[this.row, 2, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2, this.row, 3].Merge = true;
            workSheet.Cells[this.row, 2, this.row, 3].Style.Font.Bold = true;
            workSheet.Cells[this.row, 2, this.row, 3].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 2, this.row, 3].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 4, this.row, 5].Value = "STATUT";
            workSheet.Cells[this.row, 4, this.row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 4, this.row, 5].Merge = true;
            workSheet.Cells[this.row, 4, this.row, 5].Style.Font.Bold = true;
            workSheet.Cells[this.row, 4, this.row, 5].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 4, this.row, 5].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 6].Value = "TIMPUL VOTĂRII";
            workSheet.Cells[this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 6].Style.Font.Bold = true;
            workSheet.Cells[this.row, 6].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 7].Value = "DATA DE ÎNCEPUT";
            workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7].Style.Font.Bold = true;
            workSheet.Cells[this.row, 7].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 7].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[this.row, 8].Value = "DATA DE ÎNCHEIERE";
            workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 8].Style.Font.Bold = true;
            workSheet.Cells[this.row, 8].Style.Fill.SetBackground(Color.Gray);
            workSheet.Cells[this.row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            this.row++;

            var getPolls = await GetUserProfilePolls(tests);

            if (getPolls.Count() != 0)
            {
                foreach (var poll in getPolls)
                {
                    workSheet.Cells[this.row, 1].Value = poll.TestTemplate.Name;
                    workSheet.Cells[this.row, 1].Merge = true;
                    workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 2, this.row, 3].Value = string.IsNullOrEmpty(poll.Event?.Name) ? " - - - - -" : poll.Event.Name;
                    workSheet.Cells[this.row, 2, this.row, 3].Merge = true;
                    workSheet.Cells[this.row, 2, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 4, this.row, 5].Value = poll.TestStatus.ToString();
                    workSheet.Cells[this.row, 4, this.row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    workSheet.Cells[this.row, 6].Value = string.IsNullOrEmpty(poll.ProgrammedTime.ToString("dd/MM/yyyy")) ? " - - - - -" : poll.ProgrammedTime.ToString("dd/MM/yyyy");
                    workSheet.Cells[this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 7].Value = string.IsNullOrEmpty(poll.Event?.FromDate.ToString("dd/MM/yyyy")) ? " - - - - -" : poll.Event?.FromDate.ToString("dd/MM/yyyy");
                    workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    workSheet.Cells[this.row, 8].Value = string.IsNullOrEmpty(poll.Event?.TillDate.ToString("dd/MM/yyyy")) ? " - - - - -" : poll.Event?.TillDate.ToString("dd/MM/yyyy");
                    workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                    this.row++;
                }
            }
            else
            {
                await EmptyCellsForUserProfileEvaluationsAndPolls(workSheet);
            }

            return workSheet;
        }

        private async Task<ExcelWorksheet> EmptyCellsForUserProfileModuleAcces(ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1, this.row, 3].Value = " - - - - -";
            workSheet.Cells[this.row, 1, this.row, 3].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 3].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 4, this.row, 6].Value = " - - - - -";
            workSheet.Cells[this.row, 4, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 4, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells[this.row, 7, this.row, 8].Value = " - - - - -";
            workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            this.row++;

            return workSheet;
        }
        private async Task<ExcelWorksheet> EmptyCellsForUserProfileVacantPositions(ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1, this.row, 2].Value = " - - - - -";
            workSheet.Cells[this.row, 1, this.row, 2].Merge = true;
            workSheet.Cells[this.row, 1, this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1, this.row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 3, this.row, 4].Value = " - - - - -";
            workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells[this.row, 5, this.row, 6].Value = " - - - - -";
            workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 7, this.row, 8].Value = " - - - - -";
            workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            this.row++;

            return workSheet;
        }
        private async Task<ExcelWorksheet> EmptyCellsForUserProfileTests(ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1].Value = " - - - - -";
            workSheet.Cells[this.row, 1].Merge = true;
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 2].Value = " - - - - -";
            workSheet.Cells[this.row, 2].Merge = true;
            workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 3, this.row, 4].Value = " - - - - -";
            workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells[this.row, 5, this.row, 6].Value = " - - - - -";
            workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 7, this.row, 8].Value = " - - - - -";
            workSheet.Cells[this.row, 7, this.row, 8].Merge = true;
            workSheet.Cells[this.row, 7, this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7, this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            this.row++;

            return workSheet;
        }
        private async Task<ExcelWorksheet> EmptyCellsForUserProfileEvaluatedTests(ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1].Value = " - - - - -";
            workSheet.Cells[this.row, 1].Merge = true;
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 2].Value = " - - - - -";
            workSheet.Cells[this.row, 2].Merge = true;
            workSheet.Cells[this.row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 3, this.row, 4].Value = " - - - - -";
            workSheet.Cells[this.row, 3, this.row, 4].Merge = true;
            workSheet.Cells[this.row, 3, this.row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells[this.row, 5, this.row, 6].Value = " - - - - -";
            workSheet.Cells[this.row, 5, this.row, 6].Merge = true;
            workSheet.Cells[this.row, 5, this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 5, this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 7].Value = " - - - - -";
            workSheet.Cells[this.row, 7].Merge = true;
            workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 8].Value = " - - - - -";
            workSheet.Cells[this.row, 8].Merge = true;
            workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            this.row++;

            return workSheet;
        }
        private async Task<ExcelWorksheet> EmptyCellsForUserProfileEvaluationsAndPolls(ExcelWorksheet workSheet)
        {
            workSheet.Cells[this.row, 1].Value = " - - - - -";
            workSheet.Cells[this.row, 1].Merge = true;
            workSheet.Cells[this.row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 2, this.row, 3].Value = " - - - - -";
            workSheet.Cells[this.row, 2, this.row, 3].Merge = true;
            workSheet.Cells[this.row, 2, this.row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 2, this.row, 3].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 4, this.row, 5].Value = " - - - - -";
            workSheet.Cells[this.row, 4, this.row, 5].Merge = true;
            workSheet.Cells[this.row, 4, this.row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells[this.row, 6].Value = " - - - - -";
            workSheet.Cells[this.row, 6].Merge = true;
            workSheet.Cells[this.row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 7].Value = " - - - - -";
            workSheet.Cells[this.row, 7].Merge = true;
            workSheet.Cells[this.row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;

            workSheet.Cells[this.row, 8].Value = " - - - - -";
            workSheet.Cells[this.row, 8].Merge = true;
            workSheet.Cells[this.row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[this.row, 8].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            this.row++;

            return workSheet;
        }

        private async Task<List<Test>> GetUserProfileEvaluatedTests(List<Test> userProfileTests)
        {
            var getEvaluatedTests = userProfileTests.Where(upt => (upt.EvaluatorId == this.UserProfileId ||
                   _appDbContext.EventEvaluators.Any(ee => ee.EventId == upt.EventId && ee.EvaluatorId == this.UserProfileId)) &&
                   upt.TestTemplate.Mode == TestTemplateModeEnum.Test).ToList();

            return getEvaluatedTests;
        }
        private async Task<List<Test>> GetUserProfileTests(List<Test> userProfileTests)
        {
            var getUserProfileTests = userProfileTests.Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Test && t.UserProfileId == this.UserProfileId).ToList();

            return getUserProfileTests;
        }
        private async Task<List<Test>> GetUserProfileEvaluations(List<Test> userProfileTests)
        {
            var getUserProfileTests = userProfileTests.Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation && t.UserProfileId == this.UserProfileId).ToList();

            return getUserProfileTests;
        }
        private async Task<List<Test>> GetUserProfileEvaluatedEvaluations(List<Test> userProfileTests)
        {
            var getEvaluatedTests = userProfileTests.Where(upt => (upt.EvaluatorId == this.UserProfileId ||
                   _appDbContext.EventEvaluators.Any(ee => ee.EventId == upt.EventId && ee.EvaluatorId == this.UserProfileId)) &&
                   upt.TestTemplate.Mode == TestTemplateModeEnum.Evaluation).ToList();

            return getEvaluatedTests;
        }
        private async Task<List<Test>> GetUserProfilePolls(List<Test> userProfileTests)
        {
            var getPolls = userProfileTests.Where(t => t.TestTemplate.Mode == TestTemplateModeEnum.Poll && t.UserProfileId == this.UserProfileId).ToList();

            return getPolls;
        }
        private FileDataDto GetExcelFile(ExcelPackage package)
        {
            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetExcel("Fisa_Personala", streamBytesArray);
        }
    }
}
