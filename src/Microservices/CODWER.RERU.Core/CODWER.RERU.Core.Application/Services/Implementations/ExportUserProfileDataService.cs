using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.CandidateNationality)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.CandidateCitizenship)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.Bulletin)
                                                    .ThenInclude(b => b.BirthPlace)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.Bulletin)
                                                    .ThenInclude(b => b.ParentsResidenceAddress)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.Bulletin)
                                                    .ThenInclude(b => b.ResidenceAddress)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.MaterialStatus)
                                                    .ThenInclude(ms => ms.MaterialStatusType)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.KinshipRelationCriminalData)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.Studies)
                                                  .ThenInclude(s => s.StudyType)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.ModernLanguageLevels)
                                                    .ThenInclude(mll => mll.ModernLanguage)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.RecommendationForStudies)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.KinshipRelationWithUserProfiles)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.KinshipRelations)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.MilitaryObligations)
                                            .Include(up => up.Contractor)
                                                .ThenInclude(c => c.Autobiography)
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

            await SetUserProfileStudies(userProfile.Contractor?.Studies.ToList(), workSheet);

            await SetUserProfileModernLanguageKnowledgeLevel(userProfile.Contractor?.ModernLanguageLevels.ToList(), workSheet);

            await SetUserProfileMilitaryObligation(userProfile.Contractor?.MilitaryObligations.ToList(), workSheet);

            await SetUserProfileStudyRecommendation(userProfile.Contractor?.RecommendationForStudies.ToList(), workSheet);

            await SetKinshipRelationWithUserProfile(userProfile.Contractor?.KinshipRelationWithUserProfiles.ToList(), workSheet);

            await SetKinshipRelation(userProfile.Contractor?.KinshipRelations.ToList(), workSheet);

            await SetKinshipRelationCriminalData(userProfile.Contractor?.KinshipRelationCriminalData, workSheet, package);

            await SetUserProfileAutobiography(userProfile.Contractor?.Autobiography, workSheet);

            await SetUserProfileModuleAccess(userProfile.ModuleRoles, workSheet);

            await SetUserProfileSolicitedPosition(userProfile.SolicitedVacantPositions.ToList(), workSheet);

            await SetUserProfileTests(workSheet);

            await SetUserProfileEvaluations(workSheet);

            await SetUserProfilePolls(workSheet);

            workSheet.Columns.AutoFit();

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

            DefaultTitle(1, 8, "FIŞA PERSONALĂ", workSheet);

            workSheet.Cells[_row, 1, 13, 2].Merge = true;
            workSheet.Cells[_row, 1, 13, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[_row, 3, 13, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);

            workSheet.Cells[_row, 3, 13, 3].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, 6, 13, 6].Style.Border.BorderAround(ExcelBorderStyle.Medium);


            DefaultSingleHeadColumn(3, "Numele:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.FirstName) ? " - - - - -" : userProfile.FirstName, workSheet);

            DefaultSingleHeadColumn(6, "Prenumele:", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.LastName) ? " - - - - -" : userProfile.LastName, workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Patronimicul:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.FatherName) ? " - - - - -" : userProfile.FatherName, workSheet);

            DefaultSingleHeadColumn(6, "Data nasteri:", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.BirthDate?.ToString("dd-MM-yyyy")) ? " - - - - -" : userProfile.BirthDate?.ToString("dd-MM-yyyy"), workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Naţionalitate:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.Contractor?.CandidateNationality?.NationalityName) ? " - - - - -" : userProfile.Contractor.CandidateNationality.NationalityName, workSheet);

            DefaultSingleHeadColumn(6, "Cetățenie:", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.Contractor?.CandidateCitizenship?.CitizenshipName) ? " - - - - -" : userProfile.Contractor.CandidateCitizenship.CitizenshipName, workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Nivelul de cunoaștere a limbii de stat:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.Contractor?.StateLanguageLevel?.ToString()) 
                ? " - - - - -" 
                : EnumMessages.Translate(userProfile.Contractor?.StateLanguageLevel), workSheet);

            DefaultSingleHeadColumn(6, "Sex:", workSheet);
            DefaultMultipleColumnValue(7, 8, 
                string.IsNullOrEmpty(userProfile.Contractor?.Sex?.ToString()) 
                    ? " - - - - -" 
                    : EnumMessages.Translate(userProfile.Contractor?.Sex)
                , workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Buletinul de identitate (seria, eliberat la data, de catre):", workSheet);
            CustomSingleColumnValue(4, userProfile.Contractor?.Bulletin == null ? " - - - - -" : userProfile.Contractor?.Bulletin.Series + ", " + userProfile.Contractor?.Bulletin.ReleaseDay.ToString("dd-MM-yyyy"), workSheet);
            CustomSingleColumnValue(5, userProfile.Contractor?.Bulletin == null ? "" : userProfile.Contractor?.Bulletin.EmittedBy, workSheet);

            DefaultSingleHeadColumn(6, "IDNP(13 cifre):", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.Idnp) ? " - - - - -" : userProfile.Idnp.ToString(), workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Starea civilă la data completării dosarului personal:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.Contractor?.MaterialStatus?.MaterialStatusType?.Name) ? " - - - - -" : userProfile.Contractor.MaterialStatus.MaterialStatusType.Name, workSheet);

            DefaultSingleHeadColumn(6, "Locul nașterii(țara, orașul, cod poștal):", workSheet);
            CustomSingleColumnValue(7, userProfile.Contractor?.Bulletin?.BirthPlace == null ? " - - - - -" : userProfile.Contractor.Bulletin.BirthPlace.Country + ", " + userProfile.Contractor.Bulletin.BirthPlace.City, workSheet);
            CustomSingleColumnValue(8, userProfile.Contractor?.Bulletin?.BirthPlace == null ? "" : userProfile.Contractor.Bulletin.BirthPlace.PostCode, workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Viza de domiciliu(țara, orașul, cod poștal):", workSheet);
            DefaultMultipleColumnValue(4, 5, userProfile.Contractor?.Bulletin?.ResidenceAddress == null ? " - - - - -" : userProfile.Contractor.Bulletin.ResidenceAddress.Country + ", " + userProfile.Contractor.Bulletin.ResidenceAddress.City + ", " + userProfile.Contractor.Bulletin.ResidenceAddress.PostCode, workSheet);

            DefaultSingleHeadColumn(6, "Viza de domiciliu a părinților(țara, orașul, cod poștal):", workSheet);
            DefaultMultipleColumnValue(7, 8, userProfile.Contractor?.Bulletin?.ParentsResidenceAddress == null ? " - - - - -" : userProfile.Contractor.Bulletin.ParentsResidenceAddress.Country + ", " + userProfile.Contractor.Bulletin.ParentsResidenceAddress.City + ", " + userProfile.Contractor.Bulletin.ParentsResidenceAddress.PostCode, workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Telefon de contact:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.PhoneNumber) ? " - - - - -" : userProfile.PhoneNumber, workSheet);

            DefaultSingleHeadColumn(6, "Telefon fix:", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.Contractor?.HomePhone) ? " - - - - -" : userProfile.Contractor.HomePhone, workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Telefon de serviciu:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.Contractor?.WorkPhone) ? " - - - - -" : userProfile.Contractor.WorkPhone, workSheet);

            DefaultSingleHeadColumn(6, "E-mail:", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.Email) ? " - - - - -" : userProfile.Email, workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Departament:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.Department?.Name) ? " - - - - -" : userProfile.Department?.Name, workSheet);

            DefaultSingleHeadColumn(6, "Mod Acces:", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.AccessModeEnum.ToString()) ? " - - - - -" : EnumMessages.Translate(userProfile.AccessModeEnum.Value), workSheet);
            _row++;

            DefaultSingleHeadColumn(3, "Rol:", workSheet);
            DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(userProfile.Role?.Name) ? " - - - - -" : userProfile.Role?.Name, workSheet);

            DefaultSingleHeadColumn(6, "Funcție:", workSheet);
            DefaultMultipleColumnValue(7, 8, string.IsNullOrEmpty(userProfile.EmployeeFunction?.Name) ? " - - - - -" : userProfile.EmployeeFunction?.Name, workSheet);
            _row++;

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileStudies(List<Study> studies, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "1. Studii", workSheet);

            if (studies?.Count() != 0 && studies?.Count() != null)
            {
                DefaultSingleHeadColumn(1, "Instituția de învățâmânt", workSheet);
                DefaultSingleHeadColumn(2, "Adresa instituţiei", workSheet);
                DefaultSingleHeadColumn(3, "Tip studii", workSheet);
                DefaultSingleHeadColumn(4, "Facultatea", workSheet);
                DefaultSingleHeadColumn(5, "Frecvența", workSheet);
                DefaultSingleHeadColumn(6, "Anul admiterii", workSheet);
                DefaultSingleHeadColumn(7, "Anul absolvirii", workSheet);
                DefaultSingleHeadColumn(8, "Specialitatea", workSheet);
                _row++;

                foreach (var study in studies)
                {
                    DefaultSingleColumnValue(1, study.Institution, workSheet);
                    DefaultSingleColumnValue(2, study.InstitutionAddress, workSheet);
                    DefaultSingleColumnValue(3, study.StudyType.Name, workSheet);
                    DefaultSingleColumnValue(4, study.Faculty, workSheet);
                    DefaultSingleColumnValue(5, TranslateStudyFrequencyEnum(study.StudyFrequency).ToString(), workSheet);
                    DefaultSingleColumnValue(6, study.YearOfAdmission, workSheet);
                    DefaultSingleColumnValue(7, study.GraduationYear, workSheet);
                    DefaultSingleColumnValue(8, study.Specialty, workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "1. Studii: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;

        }
        private async Task<ExcelWorksheet> SetUserProfileModernLanguageKnowledgeLevel(List<ModernLanguageLevel> modernLanguagesLevel, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "2. Nivelul de cunoaștere a limbilor moderne", workSheet);

            if (modernLanguagesLevel?.Count() != 0 && modernLanguagesLevel?.Count() != null)
            {

                workSheet.Cells[_row, 1, _row + 1, 2].Value = "Denumirea limbii";
                workSheet.Cells[_row, 1, _row + 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[_row, 1, _row + 1, 2].Merge = true;
                workSheet.Cells[_row, 1, _row + 1, 2].Style.Fill.SetBackground(_color);
                workSheet.Cells[_row, 1, _row + 1, 2].Style.Font.Bold = true;
                workSheet.Cells[_row, 1, _row + 1, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                DefaultMultipleHeadColumn(3, 8, "Calificator al cunoștințelor", workSheet);
                _row++;

                DefaultMultipleHeadColumn(3, 4, "Cunostinte bază", workSheet);
                DefaultMultipleHeadColumn(5, 6, "Bine", workSheet);
                DefaultMultipleHeadColumn(7, 8, "Foarte bine", workSheet);
                _row++;

                foreach (var modernLanguage in modernLanguagesLevel)
                {
                    DefaultMultipleColumnValue(1, 2, modernLanguage.ModernLanguage.Name, workSheet);
                    DefaultMultipleColumnValue(3, 4, modernLanguage.KnowledgeQuelifiers == KnowledgeQuelifiersEnum.BasicKnowledge ? ((char)0x221A).ToString() : "X", workSheet);
                    DefaultMultipleColumnValue(5, 6, modernLanguage.KnowledgeQuelifiers == KnowledgeQuelifiersEnum.Good ? ((char)0x221A).ToString() : "X", workSheet);
                    DefaultMultipleColumnValue(7, 8, modernLanguage.KnowledgeQuelifiers == KnowledgeQuelifiersEnum.VeryGood ? ((char)0x221A).ToString() : "X", workSheet);
                    _row++;

                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "2. Nivelul de cunoaștere a limbilor moderne: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileMilitaryObligation(List<MilitaryObligation> userProfileMilitaryObligations, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "3. Obligațiunea militară", workSheet);

            if (userProfileMilitaryObligations?.Count() != 0 && userProfileMilitaryObligations?.Count() != null)
            {
                DefaultMultipleHeadColumn(1, 2, "Obligațiunea militară", workSheet);
                DefaultSingleHeadColumn(3, "Anul mobilizării", workSheet);
                DefaultSingleHeadColumn(4, "Anul retragerii", workSheet);
                DefaultSingleHeadColumn(5, "Efectiv", workSheet);
                DefaultSingleHeadColumn(6, "Specialitate militară", workSheet);
                DefaultSingleHeadColumn(7, "Gradul", workSheet);
                DefaultSingleHeadColumn(8, "Livret militar(seria, numărul, data eliberării, autoritatea eminentă)", workSheet);
                _row++;

                foreach (var militaryObligation in userProfileMilitaryObligations)
                {
                    DefaultMultipleColumnValue(1, 2, militaryObligation.MilitaryObligationType.ToString(), workSheet);
                    DefaultSingleColumnValue(3, militaryObligation.MobilizationYear.Value.ToString("dd-MM-yyyy"), workSheet);
                    DefaultSingleColumnValue(4, militaryObligation.WithdrawalYear.Value.ToString("dd-MM-yyyy"), workSheet);
                    DefaultSingleColumnValue(5, militaryObligation.Efectiv, workSheet);
                    DefaultSingleColumnValue(6, militaryObligation.MilitarySpecialty, workSheet);
                    DefaultSingleColumnValue(7, militaryObligation.Degree, workSheet);

                    DefaultSingleColumnValue(
                        8, 
                        militaryObligation.MilitaryBookletSeries + ", " + 
                        militaryObligation.MilitaryBookletNumber + ", " + 
                        militaryObligation.MilitaryBookletReleaseDay.Value.ToString("dd-MM-yyyy") + ", " + 
                        militaryObligation.MilitaryBookletEminentAuthority, workSheet
                    );
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "3. Obligațiunea militară: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileStudyRecommendation(List<RecommendationForStudy> recommendationForStudies, ExcelWorksheet workSheet)
        {
            int initialRow = _row;
            
            DefaultTitle(1, 8, "4. Recomandare la studii", workSheet);

            if (recommendationForStudies?.Count() != 0 && recommendationForStudies?.Count() != null)
            {
                DefaultMultipleHeadColumn(1, 2, "Numele persoanei ce a recomandat", workSheet);
                DefaultMultipleHeadColumn(3, 4, "Prenumele persoanei ce a recomandat", workSheet);
                DefaultMultipleHeadColumn(5, 6, "Funcția (poziția) deținută", workSheet);
                DefaultMultipleHeadColumn(7, 8, "Subdiviziune MAI", workSheet);
                _row++;

                foreach (var recommendationForStudy in recommendationForStudies)
                {
                    DefaultMultipleColumnValue(1, 2, recommendationForStudy.Name, workSheet);
                    DefaultMultipleColumnValue(3, 4, recommendationForStudy.LastName, workSheet);
                    DefaultMultipleColumnValue(5, 6, recommendationForStudy.Function, workSheet);
                    DefaultMultipleColumnValue(7, 8, recommendationForStudy.Subdivision, workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "4. Recomandare la studiii: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetKinshipRelationWithUserProfile(List<KinshipRelationWithUserProfile> kinshipRelationsWithUserProfiles, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "5. Relații de rudenie sau afinitate cu angajații MAI", workSheet);

            if (kinshipRelationsWithUserProfiles?.Count() != 0 && kinshipRelationsWithUserProfiles?.Count() != null)
            {
                DefaultSingleHeadColumn(1, "Gradul de rudenie", workSheet);

                DefaultMultipleHeadColumn(2, 3, "Numele persoanei", workSheet);
                DefaultMultipleHeadColumn(4, 5, "Prenumele persoanei", workSheet);
                DefaultMultipleHeadColumn(6, 7, "Funcția (poziția) deținută", workSheet);

                DefaultSingleHeadColumn(8, "Subdiviziune MAI", workSheet);
                _row++;

                foreach (var kinshipRelation in kinshipRelationsWithUserProfiles)
                {
                    DefaultSingleColumnValue(1, TranslateKinshipDegreeEnum(kinshipRelation.KinshipDegree), workSheet); //aici tre sa modific

                    DefaultMultipleColumnValue(2, 3, kinshipRelation.Name, workSheet);
                    DefaultMultipleColumnValue(4, 5, kinshipRelation.LastName, workSheet);
                    DefaultMultipleColumnValue(6, 7, kinshipRelation.Function, workSheet);

                    DefaultSingleColumnValue(8, kinshipRelation.Subdivision.ToString(), workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "5. Relații de rudenie sau afinitate cu angajații MAI: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetKinshipRelation(List<KinshipRelation> kinshipRelations, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "6. Date biografice ale rudelor apropiate candidatului", workSheet);

            if (kinshipRelations?.Count() != 0 && kinshipRelations?.Count() != null)
            {
                DefaultSingleHeadColumn(1, "Gradul de rudenie", workSheet);
                DefaultSingleHeadColumn(2, "Numele persoanei", workSheet);
                DefaultSingleHeadColumn(3, "Prenumele persoanei", workSheet);
                DefaultSingleHeadColumn(4, "Data nașterii", workSheet);
                DefaultSingleHeadColumn(5, "Locul nașterii", workSheet);
                DefaultSingleHeadColumn(6, "Funcția (poziția) deținută", workSheet);
                DefaultSingleHeadColumn(7, "Locul de muncă", workSheet);
                DefaultSingleHeadColumn(8, "Domiciliul", workSheet);
                _row++;

                foreach (var kinshipRelation in kinshipRelations)
                {
                    DefaultSingleColumnValue(1, TranslateKinshipDegreeEnum(kinshipRelation.KinshipDegree), workSheet);
                    DefaultSingleColumnValue(2, kinshipRelation.Name, workSheet);
                    DefaultSingleColumnValue(3, kinshipRelation.LastName, workSheet);
                    DefaultSingleColumnValue(4, kinshipRelation.BirthDate.ToString("dd-MM-yyyy"), workSheet);
                    DefaultSingleColumnValue(5, kinshipRelation.BirthLocation, workSheet);
                    DefaultSingleColumnValue(6, kinshipRelation.Function, workSheet);
                    DefaultSingleColumnValue(7, kinshipRelation.WorkLocation, workSheet);
                    DefaultSingleColumnValue(8, kinshipRelation.ResidenceAddress, workSheet);

                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "6. Date biografice ale rudelor apropiate candidatului: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetKinshipRelationCriminalData(KinshipRelationCriminalData kinshipRelationCriminalData, ExcelWorksheet workSheet, ExcelPackage package)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "7. Date referitoare la răspunderea penală sau contravențională a rudelor apropiate ale candidatului", workSheet);

            if(kinshipRelationCriminalData != null)
            {
                HTMLToRows(kinshipRelationCriminalData.Text, workSheet);
            }
            else 
            {
                DefaultEmptyTitle(1, 8, "7. Date referitoare la răspunderea penală sau contravențională a rudelor apropiate ale candidatului: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileAutobiography(Autobiography autobiography, ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "8. Autobiografie", workSheet);

            if (autobiography != null)
            {

                DefaultMultipleHeadColumn(1, 8, "Autobiografia se scrie personal de candidat citeț, ordonat, fără corectări și prescurtări, cu indicarea obligatorie a următoarei informații:", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Numele, prenumele, patronimicul, data și locul nașterii, originea socială, limba maternă;", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Instituțiile de învățământ în care a studiat, anul absolvirii sau abandonării lor, media generală, specialitățile, profesiile obținute;", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Starea civilă, data căsătoriei (divorțului), numele, prenumele, patronimicul soției (soțului), prenumele și anul nașterii copiilor;", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Serviciul militar, perioada, centrul militar care a decis încorporarea; în caz că nu a fost supus serviciului militar se indică temeiul;", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Activitatea în organele elective (perioada și organul);", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Posesia gradelor/titlurilor științifice, științifico-didactice, onorifice și sportive, data acordării lor și autoritatea emitentă;", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Existența stagiului de muncă oficial și neoficial (perioada, funcția (postul), instituția, organizația, întreprinderea);", workSheet);
                _row++;

                AutobiographyMultipleColumnHead(1, 8, "– Locul de trai și viza de domiciliu (se indică ambele dacă nu coincid).", workSheet);
                _row++;

                int firstTestRow = _row;

                HTMLToRows(autobiography.Text, workSheet);
                DefaultTableBorder(firstTestRow, workSheet);
            }
            else
            {
                DefaultEmptyTitle(1, 8, "8.Autobiografie: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileModuleAccess(List<UserProfileModuleRole> userProfileModuleRoles, ExcelWorksheet workSheet)
        {
            int initialRow = _row;
          
            DefaultTitle(1, 8, "9. Accesul la module", workSheet);

            if (userProfileModuleRoles.Count() != 0)
            {
                DefaultMultipleHeadColumn(1, 3, "Modul", workSheet);
                DefaultMultipleHeadColumn(4, 6, "Permisiune", workSheet);
                DefaultMultipleHeadColumn(7, 8, "Cod Permisiune", workSheet);
                _row++;

                foreach (var role in userProfileModuleRoles)
                {
                    DefaultMultipleColumnValue(1, 3, role.ModuleRole?.Module?.Name, workSheet);
                    DefaultMultipleColumnValue(4, 6, role.ModuleRole?.Name, workSheet);
                    DefaultMultipleColumnValue(7, 8, role.ModuleRole?.Code, workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "9.Accesul la module: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileSolicitedPosition(List<SolicitedVacantPosition> solicitedVacantPosition, ExcelWorksheet workSheet)
        {
            int initialRow = _row;
          
            DefaultTitle(1, 8, "10. Pozitii solicitate", workSheet);

            if (solicitedVacantPosition.Count() != 0)
            {
                DefaultMultipleHeadColumn(1, 2, "POZIȚIE CANDIDATĂ", workSheet);
                DefaultMultipleHeadColumn(3, 4, "TIMP SOLICITAT", workSheet);
                DefaultMultipleHeadColumn(5, 6, "STATUT", workSheet);
                DefaultMultipleHeadColumn(7, 8, "DOCUMENTE ATAȘATE/NECESARE", workSheet);
                _row++;

                foreach (var vacantPosition in solicitedVacantPosition)
                {
                    DefaultMultipleColumnValue(1, 2, vacantPosition.CandidatePosition?.Name, workSheet);
                    DefaultMultipleColumnValue(3, 4, vacantPosition.CreateDate.ToString("dd-MM-yyyy, H:mm"), workSheet);
                    DefaultMultipleColumnValue(5, 6, TranslateSolicitedPositionStatusEnum(vacantPosition.SolicitedPositionStatus), workSheet);
                    DefaultMultipleColumnValue(7, 8, vacantPosition.SolicitedVacantPositionUserFiles?.Count() + "/" + vacantPosition?.CandidatePosition?.RequiredDocumentPositions?.Count(), workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "10. Pozitii solicitate: nu există informaţii.", workSheet);
            }

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileTests(ExcelWorksheet workSheet)
        {
            int initialRow = _row;

            DefaultTitle(1, 8, "11. Teste", workSheet);

            await SetCellsForUserProfileTests(workSheet);

            await SetCellsForUserProfileEvaluatedTests(workSheet);

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfileEvaluations(ExcelWorksheet workSheet)
        {
            int initialRow = _row;
            
            DefaultTitle(1, 8, "12 .Evaluari", workSheet);

            await SetCellsForUserProfileEvaluations(workSheet);

            await SetCellsForUserProfileEvaluatedEvaluations(workSheet);

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetUserProfilePolls(ExcelWorksheet workSheet)
        {
            int initialRow = _row;
           
            DefaultTitle(1, 8, "13 .Sondaje", workSheet);

            await SetCellsForUserProfilePolls(workSheet);
            _row--;

            DefaultTableBorder(initialRow, workSheet);

            return workSheet;
        }

        private async Task<ExcelWorksheet> SetCellsForUserProfileTests(ExcelWorksheet workSheet)
        {
            EvaluationTitle(1, 8, "Teste efectuate", workSheet);

            var getTest = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Test);

            if (getTest.Count() != 0)
            {
                DefaultSingleHeadColumn(1, "DATA", workSheet);
                DefaultSingleHeadColumn(2, "STATUT", workSheet);

                DefaultMultipleHeadColumn(3, 4, "TESTUL", workSheet);
                DefaultMultipleHeadColumn(5, 6, "EVENIMENT", workSheet);
                DefaultMultipleHeadColumn(7, 8, "PUNCTE / SCORUL MIN.", workSheet);
                _row++;

                foreach (var test in getTest)
                {
                    DefaultSingleColumnValue(1, test.ProgrammedTime.ToString("dd-MM-yyyy") + "/" + (string.IsNullOrEmpty(test.EndProgrammedTime?.ToString("dd-MM-yyyy")) ? " - - - - -" : test.EndProgrammedTime?.ToString("dd-MM-yyyy")), workSheet);
                    DefaultSingleColumnValue(2, TranslateTestStatusEnum(test.TestStatus), workSheet);

                    DefaultMultipleColumnValue(3, 4, test.TestTemplate.Name, workSheet);
                    DefaultMultipleColumnValue(5, 6, string.IsNullOrEmpty(test.Event?.Name) ? " - - - - -" : test.Event.Name, workSheet);
                    DefaultMultipleColumnValue(7, 8, (test.AccumulatedPercentage ?? 0) + "/" + test.TestTemplate.MinPercent, workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "Teste efectuate: nu există informaţii.", workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluatedTests(ExcelWorksheet workSheet)
        {
            EvaluationTitle(1, 8, "Teste evaluate", workSheet);

            var getEvaluatedTests = await GetUserProfileEvaluatedTestsOrEvaluations(TestTemplateModeEnum.Test);

            if (getEvaluatedTests.Count() != 0)
            {
                DefaultSingleHeadColumn(1, "DATA", workSheet);
                DefaultSingleHeadColumn(2, "STATUT", workSheet);

                DefaultMultipleHeadColumn(3, 4, "EVALUAT", workSheet);
                DefaultMultipleHeadColumn(5, 6, "TESTUL", workSheet);

                DefaultSingleHeadColumn(7, "PUNCTE / SCORUL MIN.", workSheet);
                DefaultSingleHeadColumn(8, "REZULTAT", workSheet);
                _row++;

                foreach (var evaluatedTest in getEvaluatedTests)
                {
                    DefaultSingleColumnValue(1, evaluatedTest.ProgrammedTime.ToString("dd-MM-yyyy") + "/" + (string.IsNullOrEmpty(evaluatedTest.EndProgrammedTime?.ToString("dd-MM-yyyy")) ? " - - - - -" : evaluatedTest.EndProgrammedTime?.ToString("dd-MM-yyyy")), workSheet);
                    DefaultSingleColumnValue(2, TranslateTestStatusEnum(evaluatedTest.TestStatus), workSheet);

                    DefaultMultipleColumnValue(3, 4, evaluatedTest.UserProfile?.FullName ?? " - - - - -", workSheet);
                    DefaultMultipleColumnValue(5, 6, evaluatedTest.TestTemplate.Name, workSheet);

                    DefaultSingleColumnValue(7, (evaluatedTest.AccumulatedPercentage ?? 0) + "/" + evaluatedTest.TestTemplate.MinPercent, workSheet);
                    DefaultSingleColumnValue(8, TranslateResultStatusValue(evaluatedTest.ResultStatusValue), workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "Teste evaluate: nu există informaţii.", workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluations(ExcelWorksheet workSheet)
        {
            EvaluationTitle(1, 8, "Evaluarii", workSheet);

            var getEvaluations = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Evaluation);

            if (getEvaluations.Count() != 0)
            {
                DefaultSingleHeadColumn(1, "NUME TEST", workSheet);

                DefaultMultipleHeadColumn(2, 3, "EVALUATOR", workSheet);
                DefaultMultipleHeadColumn(4, 5, "EVENIMENT", workSheet);

                DefaultSingleHeadColumn(6, "LOCAȚIE", workSheet);
                DefaultSingleHeadColumn(7, "STATUT", workSheet);
                DefaultSingleHeadColumn(8, "REZULTAT", workSheet);
                _row++;

                foreach (var evaluation in getEvaluations)
                {
                    DefaultSingleColumnValue(1, evaluation.TestTemplate.Name.ToString(), workSheet);

                    DefaultMultipleColumnValue(2, 3, evaluation.Evaluator?.FullName ?? "- - - - -", workSheet);
                    DefaultMultipleColumnValue(4, 5, string.IsNullOrEmpty(evaluation.Event?.Name) ? " - - - - -" : evaluation.Event.Name, workSheet);

                    DefaultSingleColumnValue(6, evaluation.Location?.Name ?? "--------", workSheet);
                    DefaultSingleColumnValue(7, TranslateTestStatusEnum(evaluation.TestStatus), workSheet);
                    DefaultSingleColumnValue(8, TranslateResultStatusValue(evaluation.ResultStatusValue), workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "Evaluarii: nu există informaţii.", workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfileEvaluatedEvaluations(ExcelWorksheet workSheet)
        {
            EvaluationTitle(1, 8, "Evaluari evaluate", workSheet);

            var getEvaluatedEvaluations = await GetUserProfileEvaluatedTestsOrEvaluations(TestTemplateModeEnum.Evaluation);

            if (getEvaluatedEvaluations.Count() != 0)
            {
                DefaultSingleHeadColumn(1, "DATA Programata", workSheet);
                DefaultSingleHeadColumn(2, "EVALUARE", workSheet);

                DefaultMultipleHeadColumn(3, 4, "EVALUAT", workSheet);
                DefaultMultipleHeadColumn(5, 6, "EVENIMENT", workSheet);

                DefaultSingleHeadColumn(7, "STATUT", workSheet);
                DefaultSingleHeadColumn(8, "REZULTAT", workSheet);
                _row++;

                foreach (var evaluation in getEvaluatedEvaluations)
                {
                    DefaultSingleColumnValue(1, evaluation.ProgrammedTime.ToString("dd-MM-yyyy"), workSheet);
                    DefaultSingleColumnValue(2, evaluation.TestTemplate.Name, workSheet);

                    DefaultMultipleColumnValue(3, 4, evaluation.UserProfile?.FullName ?? " - - - - -", workSheet);
                    DefaultMultipleColumnValue(5, 6, string.IsNullOrEmpty(evaluation.Event?.Name) ? " - - - - -" : evaluation.Event.Name, workSheet);

                    DefaultSingleColumnValue(7, TranslateTestStatusEnum(evaluation.TestStatus), workSheet);
                    DefaultSingleColumnValue(8, TranslateResultStatusValue(evaluation.ResultStatusValue), workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "Evaluari evaluate: nu există informaţii.", workSheet);
            }

            return workSheet;
        }
        private async Task<ExcelWorksheet> SetCellsForUserProfilePolls(ExcelWorksheet workSheet)
        {
            var getPolls = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Poll);

            if (getPolls.Count() != 0)
            {
                DefaultSingleHeadColumn(1, "NUME", workSheet);

                DefaultMultipleHeadColumn(2, 3, "EVENIMENT", workSheet);
                DefaultMultipleHeadColumn(4, 5, "STATUT", workSheet);

                DefaultSingleHeadColumn(6, "TIMPUL VOTĂRII", workSheet);
                DefaultSingleHeadColumn(7, "DATA DE ÎNCEPUT", workSheet);
                DefaultSingleHeadColumn(8, "DATA DE ÎNCHEIERE", workSheet);
                _row++;

                foreach (var poll in getPolls)
                {
                    DefaultSingleColumnValue(1, poll.TestTemplate.Name, workSheet);

                    DefaultMultipleColumnValue(2, 3, string.IsNullOrEmpty(poll.Event?.Name) ? " - - - - -" : poll.Event.Name, workSheet);
                    DefaultMultipleColumnValue(4, 5, TranslateTestStatusEnum(poll.TestStatus), workSheet);

                    DefaultSingleColumnValue(6, string.IsNullOrEmpty(poll.ProgrammedTime.ToString("dd-MM-yyyy")) ? " - - - - -" : poll.ProgrammedTime.ToString("dd-MM-yyyy"), workSheet);
                    DefaultSingleColumnValue(7, string.IsNullOrEmpty(poll.Event?.FromDate.ToString("dd-MM-yyyy")) ? " - - - - -" : poll.Event?.FromDate.ToString("dd-MM-yyyy"), workSheet);
                    DefaultSingleColumnValue(8, string.IsNullOrEmpty(poll.Event?.TillDate.ToString("dd-MM-yyyy")) ? " - - - - -" : poll.Event?.TillDate.ToString("dd-MM-yyyy"), workSheet);
                    _row++;
                }
            }
            else
            {
                DefaultEmptyTitle(1, 8, "12 .Sondaje: nu există informaţii.", workSheet);
            }

            return workSheet;
        }

        private string TranslateStudyFrequencyEnum(StudyFrequencyEnum frequency)
        {
            string translatedFrequency = "";

            switch (frequency)
            {
                case StudyFrequencyEnum.Daily:
                    translatedFrequency = "Zilnic";
                    break;

                case StudyFrequencyEnum.LowFrequency:
                    translatedFrequency = "Frecvență redusă";
                    break;

                case StudyFrequencyEnum.Remote:
                    translatedFrequency = "La distanță";
                    break;
            }

            return translatedFrequency;
        }
        
        private string TranslateKinshipDegreeEnum(KinshipDegreeEnum degree)
        {        
            string translatedKinshipDegreeEnum = "";

            switch (degree)
            {
                case KinshipDegreeEnum.Father:
                    translatedKinshipDegreeEnum = "Tată";
                    break;

                case KinshipDegreeEnum.Mother:
                    translatedKinshipDegreeEnum = "Mamă";
                    break;

                case KinshipDegreeEnum.Brother:
                    translatedKinshipDegreeEnum = "Frate";
                    break;
                case KinshipDegreeEnum.Sister:
                    translatedKinshipDegreeEnum = "Soră";
                    break;

                case KinshipDegreeEnum.Wife:
                    translatedKinshipDegreeEnum = "Soție";
                    break;

                case KinshipDegreeEnum.Husband:
                    translatedKinshipDegreeEnum = "Soț";
                    break;
                case KinshipDegreeEnum.Children:
                    translatedKinshipDegreeEnum = "Copil";
                    break;

                case KinshipDegreeEnum.Parent:
                    translatedKinshipDegreeEnum = "Părinte";
                    break;

                case KinshipDegreeEnum.HusbandsBrothers:
                    translatedKinshipDegreeEnum = "Fratele soțului";
                    break;
                case KinshipDegreeEnum.HusbandsSisters:
                    translatedKinshipDegreeEnum = "Sora soțului";
                    break;

                case KinshipDegreeEnum.WifesSisters:
                    translatedKinshipDegreeEnum = "Sora soției";
                    break;

                case KinshipDegreeEnum.WifesBrothers:
                    translatedKinshipDegreeEnum = "Fratele soției";
                    break;
            }

            return translatedKinshipDegreeEnum;
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

        private ExcelWorksheet DefaultSingleHeadColumn(int fromCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, fromCol].Value = value;
            workSheet.Cells[_row, fromCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, fromCol].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, fromCol].Style.Font.Bold = true;
            workSheet.Cells[_row, fromCol].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[_row, fromCol].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet DefaultMultipleHeadColumn(int fromCol, int toCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, fromCol, _row, toCol].Value = value;
            workSheet.Cells[_row, fromCol, _row, toCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, fromCol, _row, toCol].Merge = true;
            workSheet.Cells[_row, fromCol, _row, toCol].Style.Fill.SetBackground(_color);
            workSheet.Cells[_row, fromCol, _row, toCol].Style.Font.Bold = true;
            workSheet.Cells[_row, fromCol, _row, toCol].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[ _row, fromCol, _row, toCol].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet AutobiographyMultipleColumnHead(int fromCol, int toCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, fromCol, _row, toCol].Value = value;
            workSheet.Cells[_row, fromCol, _row, toCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, fromCol, _row, toCol].Merge = true;
            workSheet.Cells[_row, fromCol, _row, toCol].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet DefaultSingleColumnValue(int fromCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, fromCol].Value = value;
            workSheet.Cells[_row, fromCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, fromCol].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, fromCol].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet CustomSingleColumnValue(int fromCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, fromCol].Value = value;
            workSheet.Cells[_row, fromCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, fromCol].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet DefaultMultipleColumnValue(int fromCol, int toCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, fromCol, _row, toCol].Value = value;
            workSheet.Cells[_row, fromCol, _row, toCol].Merge = true;
            workSheet.Cells[_row, fromCol, _row, toCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, fromCol, _row, toCol].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            workSheet.Cells[_row, fromCol, _row, toCol].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet DefaultTitle(int fromCol, int toCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, fromCol, _row + 1, toCol].Value = value;
            workSheet.Cells[_row, fromCol, _row + 1, toCol].Merge = true;
            workSheet.Cells[_row, fromCol, _row + 1, toCol].Style.Font.Size = 18;
            workSheet.Cells[_row, fromCol].Style.Font.Bold = true;
            workSheet.Cells[_row, fromCol, _row + 1, toCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, fromCol, _row + 1, toCol].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[_row, fromCol, _row + 1, toCol].EntireRow.Height = 27;
            _row = _row + 2;

            return workSheet;
        }
        private ExcelWorksheet EvaluationTitle(int fromCol, int toCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row, 1, _row, 8].Value = value;
            workSheet.Cells[_row, 1, _row, 8].Merge = true;
            workSheet.Cells[_row, 1, _row, 8].Style.Font.Size = 16;
            workSheet.Cells[_row, 1].Style.Font.Bold = true;
            workSheet.Cells[_row, 1, _row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[_row, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[_row, 1, _row, 8].EntireRow.Height = 27;

            _row++;

            return workSheet;
        }
        private ExcelWorksheet DefaultEmptyTitle(int fromCol, int toCol, string value, ExcelWorksheet workSheet)
        {
            workSheet.Cells[_row - 2, fromCol, _row - 2, toCol].Value = value;
            workSheet.Cells[_row, fromCol, _row, toCol].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet DefaultTableBorder(int initialRow, ExcelWorksheet workSheet)
        {
            workSheet.Cells[initialRow, 1, _row, 8].Style.Border.BorderAround(ExcelBorderStyle.Medium);
            workSheet.Cells[_row, 1, _row, 8].EntireRow.Height = 27;

            return workSheet;
        }
        private ExcelWorksheet HTMLToRows(string data, ExcelWorksheet workSheet)
        {
            string[] linesData = HTMLToText(data).Split(
                '.',
                StringSplitOptions.None
                );

            for (int i = 0; i < linesData.Length; i++)
            {
                var sizeOfString = GetSize(linesData[i]);

                workSheet.Cells[_row, 1].Value = linesData[i];
                workSheet.Cells[_row, 1, _row, 8].Merge = true;
                workSheet.Cells[_row, 1, _row, 8].EntireRow.Height = sizeOfString.Height;
                _row++;

            }

            return workSheet;
        }

        public static SizeF GetSize(string line)
        {
            Font f = new Font("Calibri", 11, FontStyle.Regular);
            Bitmap b = new Bitmap(600, 600);
            Graphics g = Graphics.FromImage(b);
            SizeF sizeOfString = new SizeF();

            sizeOfString = g.MeasureString(line, f);

            return sizeOfString;
        }
        public string HTMLToText(string HTMLCode)
        {
            // Remove new lines since they are not visible in HTML  
            HTMLCode = HTMLCode.Replace("\n", " ");
            // Remove tab spaces  
            HTMLCode = HTMLCode.Replace("\t", " ");
            // Remove multiple white spaces from HTML  
            HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");
            // Remove HEAD tag  
            HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
                                , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // Remove any JavaScript  
            HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
              , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // Replace special characters like &, <, >, " etc.  
            StringBuilder sbHTML = new StringBuilder(HTMLCode);
            // Note: There are many more special characters, these are just  
            // most common. You can add new characters in this arrays if needed  
            string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;",
                        "&gt;", "&reg;", "&copy;", "&bull;", "&trade;","&#39;"};
            string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢", "\'" };
            for (int i = 0; i < OldWords.Length; i++)
            {
                sbHTML.Replace(OldWords[i], NewWords[i]);
            }
            // Check if there are line breaks (<br>) or paragraph (<p>)  
            sbHTML.Replace("<br>", "\n<br>");
            sbHTML.Replace("<br ", "\n<br ");
            sbHTML.Replace("<p ", "\n<p ");
            // Finally, remove all HTML tags and return plain text  
            return System.Text.RegularExpressions.Regex.Replace(
              sbHTML.ToString(), "<[^>]*>", "");
        }
    }
}
