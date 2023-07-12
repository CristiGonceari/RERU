using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using Wkhtmltopdf.NetCore;

namespace CODWER.RERU.Core.Application.Services.Implementations
{
    public class ExportUserProfileDataPdfService : IExportUserProfileDataPdf
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGeneratePdf _generatePdf;
        private int _userProfileId = new();

        public ExportUserProfileDataPdfService(AppDbContext appDbContext, IGeneratePdf generatePdf)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
        }

        public async Task<FileDataDto> ExportUserProfileDatasPdf(int userProfileId)
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

            return await GetPdf(userProfile);
        }

        public async Task<FileDataDto> GetPdf(UserProfile userProfile)
        {
            string path = new FileInfo("Templates/Dosar.html").FullName;
            var myDictionary = await GetDictionary(userProfile);
            string source = await File.ReadAllTextAsync(path);

            foreach (var (key, value) in myDictionary)
            {
                source = source.Replace(key, value);
            }

            var res = Parse(source);

            return FileDataDto.GetPdf("Dosarul_Utilizatorului" + $"({userProfile.FullName})", res);
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

        private async Task<Dictionary<string, string>> GetDictionary(UserProfile userProfile)
        {
            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{firstName}", userProfile.FirstName);
            myDictionary.Add("{lastName}", userProfile.LastName);
            myDictionary.Add("{fatherName}", string.IsNullOrEmpty(userProfile.FatherName) ? " - " : userProfile.FatherName);
            myDictionary.Add("{birthDate}", string.IsNullOrEmpty(userProfile.BirthDate?.ToString("dd-MM-yyyy")) ? " - " : userProfile.BirthDate?.ToString("dd-MM-yyyy"));
            myDictionary.Add("{nationality}", string.IsNullOrEmpty(userProfile.Contractor?.CandidateNationality?.NationalityName) ? " - " : 
                                                                    userProfile.Contractor.CandidateNationality.NationalityName);
            myDictionary.Add("{citizenship}", string.IsNullOrEmpty(userProfile.Contractor?.CandidateCitizenship?.CitizenshipName) ? " - " : 
                                                                    userProfile.Contractor.CandidateCitizenship.CitizenshipName);
            myDictionary.Add("{languageLevel}", string.IsNullOrEmpty(userProfile.Contractor?.StateLanguageLevel?.ToString()) ? " - " :
                                                                    EnumMessages.Translate(userProfile.Contractor?.StateLanguageLevel));
            myDictionary.Add("{sex}", string.IsNullOrEmpty(userProfile.Contractor?.Sex?.ToString()) ? " - " : EnumMessages.Translate(userProfile.Contractor?.Sex));
            myDictionary.Add("{bulletinSeriesReleaseDay}", userProfile.Contractor?.Bulletin == null ? " - " : userProfile.Contractor?.Bulletin.Series + ", " + 
                                            userProfile.Contractor?.Bulletin.ReleaseDay.ToString("dd-MM-yyyy"));
            myDictionary.Add("{bulletinEmittedBy}", userProfile.Contractor?.Bulletin == null ? "" : userProfile.Contractor?.Bulletin.EmittedBy);
            myDictionary.Add("{idnp}", string.IsNullOrEmpty(userProfile.Idnp) ? " - " : userProfile.Idnp.ToString());
            myDictionary.Add("{materialStatus}", string.IsNullOrEmpty(userProfile.Contractor?.MaterialStatus?.MaterialStatusType?.Name) ? " - " : 
                                                                        userProfile.Contractor.MaterialStatus.MaterialStatusType.Name);
            myDictionary.Add("{birthPlaceCountryCity}", userProfile.Contractor?.Bulletin?.BirthPlace == null ? " - " : 
                                                    userProfile.Contractor.Bulletin.BirthPlace.Country + ", " + userProfile.Contractor.Bulletin.BirthPlace.City);
            myDictionary.Add("{birthPlacePostCode}", userProfile.Contractor?.Bulletin?.BirthPlace == null ? "" : userProfile.Contractor.Bulletin.BirthPlace.PostCode);
            myDictionary.Add("{residenceAddress}", userProfile.Contractor?.Bulletin?.ResidenceAddress == null ? " - " : 
                                                userProfile.Contractor.Bulletin.ResidenceAddress.Country + ", " + userProfile.Contractor.Bulletin.ResidenceAddress.City + ", " + 
                                                userProfile.Contractor.Bulletin.ResidenceAddress.PostCode);
            myDictionary.Add("{parentsResidenceAddress}", userProfile.Contractor?.Bulletin?.ParentsResidenceAddress == null ? " - " : 
                                                            userProfile.Contractor.Bulletin.ParentsResidenceAddress.Country + ", " + 
                                                            userProfile.Contractor.Bulletin.ParentsResidenceAddress.City + ", " + 
                                                            userProfile.Contractor.Bulletin.ParentsResidenceAddress.PostCode);
            myDictionary.Add("{phoneNumber}", string.IsNullOrEmpty(userProfile.PhoneNumber) ? " - " : userProfile.PhoneNumber);
            myDictionary.Add("{homePhone}", string.IsNullOrEmpty(userProfile.Contractor?.HomePhone) ? " - " : userProfile.Contractor.HomePhone);
            myDictionary.Add("{workPhone}", string.IsNullOrEmpty(userProfile.Contractor?.WorkPhone) ? " - " : userProfile.Contractor.WorkPhone);
            myDictionary.Add("{email}", string.IsNullOrEmpty(userProfile.Email) ? " - " : userProfile.Email);
            myDictionary.Add("{department}", string.IsNullOrEmpty(userProfile.Department?.Name) ? " - " : userProfile.Department?.Name);
            myDictionary.Add("{accessMode}", string.IsNullOrEmpty(userProfile.AccessModeEnum.ToString()) ? " - " : EnumMessages.Translate(userProfile.AccessModeEnum.Value));
            myDictionary.Add("{role}", string.IsNullOrEmpty(userProfile.Role?.Name) ? " - " : userProfile.Role?.Name);
            myDictionary.Add("{employeeFunction}", string.IsNullOrEmpty(userProfile.EmployeeFunction?.Name) ? " - " : userProfile.EmployeeFunction?.Name);
            myDictionary.Add("{studies}", getStudies(userProfile.Contractor?.Studies.ToList()));
            myDictionary.Add("{modernLanguagesLevel}", getModernLanguagesLevel(userProfile.Contractor?.ModernLanguageLevels.ToList()));
            myDictionary.Add("{userProfileMilitaryObligations}", getMilitaryObligations(userProfile.Contractor?.MilitaryObligations.ToList()));
            myDictionary.Add("{recommendationForStudies}", getRecommendationForStudies(userProfile.Contractor?.RecommendationForStudies.ToList()));
            myDictionary.Add("{kinshipRelation}", getKinshipRelation(userProfile.Contractor?.KinshipRelationWithUserProfiles.ToList()));
            myDictionary.Add("{kinshipRelationInfo}", getKinshipRelationInfo(userProfile.Contractor?.KinshipRelations.ToList()));
            myDictionary.Add("{kinshipRelationCriminalData}", userProfile.Contractor?.KinshipRelationCriminalData?.Text ?? "nu există informaţii.");
            myDictionary.Add("{autobiography}", userProfile.Contractor?.Autobiography?.Text ?? "nu există informaţii.");
            myDictionary.Add("{moduleRoles}", getModuleRoles(userProfile.ModuleRoles));
            myDictionary.Add("{solicitedVacantPosition}", getSolicitedVacantPosition(userProfile.SolicitedVacantPositions.ToList()));
            myDictionary.Add("{myTests}", await getMyTestsAsync(userProfile));
            myDictionary.Add("{evaluatedTests}", await getEvaluatedTestsAsync(userProfile));
            myDictionary.Add("{myEvaluations}", await getMyEvaluationsAsync(userProfile));
            myDictionary.Add("{evaluatedEvaluations}", await getEvaluatedEvaluationsAsync(userProfile));
            //myDictionary.Add("{pools}", await getPollsAsync(userProfile));

            return myDictionary;
        }

        public string getStudies(List<Study> studies)
        {
            string content = string.Empty;
            
            if (studies?.Count() != 0 && studies?.Count() != null)
            {
                foreach (var study in studies)
                {
                    if (study.StudyType.ValidationId == 1 || study.StudyType.ValidationId == 2 || study.StudyType.ValidationId == 3) {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tip studii: </td><td colspan=""2"">{study.StudyType.Name}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul admiterii: </td><td colspan=""2"">{study.YearOfAdmission}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Instituția de învățământ:</td><td colspan=""2"">{study.Institution}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul absolvirii:</td><td colspan=""2"">{study.GraduationYear}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa instituţiei:</td><td colspan=""2"">{study.InstitutionAddress}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Actul de studii(seria, nr, data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{study.StudyActSeries}, {study.StudyActNumber}, {study.StudyActRelaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Profilul:</td><td colspan=""2"">{EnumMessages.Translate(study.StudyProfile)}</td>
                                    </tr>";
                    } else if (study.StudyType.ValidationId == 4 || study.StudyType.ValidationId == 5) {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tip studii: </td><td colspan=""2"">{study.StudyType.Name}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Specialitatea: </td><td colspan=""2"">{study.Specialty}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Instituția de învățământ:</td><td colspan=""2"">{study.Institution}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Calificarea:</td><td colspan=""2"">{study.Qualification}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa instituţiei:</td><td colspan=""2"">{study.InstitutionAddress}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul admiterii:</td><td colspan=""2"">{study.YearOfAdmission}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Forma de învățământ:</td><td colspan=""2"">{EnumMessages.Translate(study.StudyFrequency)}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Actul de studii(seria, nr, data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{study.StudyActSeries}, {study.StudyActNumber}, {study.StudyActRelaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul absolvirii:</td><td colspan=""2"">{study.GraduationYear}</td>
                                    </tr>";
                    } else if (study.StudyType.ValidationId == 6) {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tip studii: </td><td colspan=""2"">{study.StudyType.Name}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Calificarea: </td><td colspan=""2"">{study.Qualification}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Instituția de învățământ:</td><td colspan=""2"">{study.Institution}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Titlul(master în...):</td><td colspan=""2"">{study.Title}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa instituţiei:</td><td colspan=""2"">{study.InstitutionAddress}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Specialitatea:</td><td colspan=""2"">{study.Specialty}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Forma de învățământ:</td><td colspan=""2"">{EnumMessages.Translate(study.StudyFrequency)}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul admiterii:</td><td colspan=""2"">{study.YearOfAdmission}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Facultatea:</td><td colspan=""2"">{study.Faculty}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Actul de studii(seria, nr, data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{study.StudyActSeries}, {study.StudyActNumber}, {study.StudyActRelaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul absolvirii:</td><td colspan=""2"">{study.GraduationYear}</td>
                                    </tr>";
                    } else if (study.StudyType.ValidationId == 7 || study.StudyType.ValidationId == 8) {
                        if (study.StudyType.ValidationId == 7)
                        {
                            content += $@"<tr>
                                            <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tip studii:</td><td colspan=""2"">{study.StudyType.Name}</td>
                                            <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Titlul (licențiat în...):</td><td colspan=""2"">{study.Title}</td>
                                        </tr>";
                        }
                        else if (study.StudyType.ValidationId == 8)
                        {
                            content += $@"<tr>
                                            <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tip studii:</td><td colspan=""2"">{study.StudyType.Name}</td>
                                            <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Titlul (master în...):</td><td colspan=""2"">{study.Title}</td>
                                        </tr>";
                        }

                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Instituția de învățământ:</td><td colspan=""2"">{study.Institution}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Specialitatea:</td><td colspan=""2"">{study.Specialty}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa instituţiei:</td><td colspan=""2"">{study.InstitutionAddress}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul admiterii:</td><td colspan=""2"">{study.YearOfAdmission}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Forma de învățământ:</td><td colspan=""2"">{EnumMessages.Translate(study.StudyFrequency)}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul absolvirii:</td><td colspan=""2"" rowspan=""2"">{study.GraduationYear}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Facultatea:</td><td colspan=""2"">{study.Faculty}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Actul de studii(seria, nr, data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{study.StudyActSeries}, {study.StudyActNumber}, {study.StudyActRelaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>";
                    } else if (study.StudyType.ValidationId == 9 || study.StudyType.ValidationId == 10) {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tip studii: </td><td colspan=""2"">{study.StudyType.Name}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Specialitatea: </td><td colspan=""2"">{study.Specialty}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Instituția de învățământ:</td><td colspan=""2"">{study.Institution}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Titlul(doctor în...):</td><td colspan=""2"">{study.Title}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa instituţiei:</td><td colspan=""2"">{study.InstitutionAddress}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul admiterii:</td><td colspan=""2"">{study.YearOfAdmission}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Forma de învățământ:</td><td colspan=""2"">{EnumMessages.Translate(study.StudyFrequency)}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Diploma de doctor(seria, nr, data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{study.StudyActSeries}, {study.StudyActNumber}, {study.StudyActRelaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul absolvirii:</td><td colspan=""2"">{study.GraduationYear}</td>
                                    </tr>";
                    } else if (study.StudyType.ValidationId == 11) {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tip studii: </td><td colspan=""2"">{study.StudyType.Name}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Calificarea: </td><td colspan=""2"">{study.Qualification}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Tipuri de cursuri:</td><td colspan=""2"">{EnumMessages.Translate(study.StudyCourse)}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Nr. de ore/credite:</td><td colspan=""2"">{study.CreditCount}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Instituția de învățământ:</td><td colspan=""2"">{study.Institution}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Certificatul(seria, nr, data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{study.StudyActSeries}, {study.StudyActNumber}, {study.StudyActRelaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa instituţiei:</td><td colspan=""2"">{study.InstitutionAddress}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Perioada de studii(început, sfârșit):</td>
                                        <td colspan=""2"" rowspan=""2"">{study.StartStudyPeriod?.ToString("dd-MM-yyyy")} - {study.EndStudyPeriod?.ToString("dd-MM-yyyy")}</td>
                                    </tr>";
                    }

                    return content;
                }
            }

            return "nu există informaţii.";
        }

        public string getModernLanguagesLevel(List<ModernLanguageLevel> modernLanguagesLevel)
        {
            string content = string.Empty;
            if (modernLanguagesLevel?.Count() != 0 && modernLanguagesLevel?.Count() != null)
            {
                int counter = 1;
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                                    <td colspan=""2"">Numărul</td>
                                    <td colspan=""2"">Denumirea limbii</td>
                                    <td colspan=""2"">Calificator al cunoștințelor</td>
                                </tr>";

                foreach (var modernLanguage in modernLanguagesLevel)
                {
                    content += $@"<tr>
                                    <td colspan=""2"">{counter}</td>
                                    <td colspan=""2"">{modernLanguage.ModernLanguage.Name}</td>
                                    <td colspan=""2"">{EnumMessages.Translate(modernLanguage.KnowledgeQuelifiers)}</td>
                                </tr>";
                    counter++;
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public string getMilitaryObligations(List<MilitaryObligation> userProfileMilitaryObligations)
        {
            string content = string.Empty;
            if (userProfileMilitaryObligations?.Count() != 0 && userProfileMilitaryObligations?.Count() != null)
            {
                foreach (var militaryObligation in userProfileMilitaryObligations)
                {
                    if (militaryObligation.MilitaryObligationType == MilitaryObligationTypeEnum.Disobedient)
                    {
                        content += $@"<tr>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Obligație militară:</td><td colspan=""2"">{EnumMessages.Translate(militaryObligation.MilitaryObligationType)}</td>
                                </tr>";
                    }
                    else if (militaryObligation.MilitaryObligationType == MilitaryObligationTypeEnum.AlternativeService)
                    {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Obligație militară:</td><td colspan=""2"">{EnumMessages.Translate(militaryObligation.MilitaryObligationType)}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Denumirea (instituției/organizației/întreprinderii):</td><td colspan=""2"">{militaryObligation.InstitutionName}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa (instituției/organizației/întreprinderii):</td><td colspan=""2"">{militaryObligation.InstitutionAdress}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Livret militar(seria, nr., data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{militaryObligation.MilitaryBookletSeries}, {militaryObligation.MilitaryBookletNumber}, {militaryObligation.MilitaryBookletReleaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Perioada:</td><td colspan=""2"">{militaryObligation.StartObligationPeriod?.ToString("dd-MM-yyyy")} - {militaryObligation.EndObligationPeriod?.ToString("dd-MM-yyyy")}</td>
                                    </tr>";
                    }
                    else if (militaryObligation.MilitaryObligationType == MilitaryObligationTypeEnum.MilitaryChair)
                    {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Obligație militară:</td><td colspan=""2"">{EnumMessages.Translate(militaryObligation.MilitaryObligationType)}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Denumirea (instituției/organizației/întreprinderii):</td><td colspan=""2"">{militaryObligation.InstitutionName}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adresa (instituției/organizației/întreprinderii):</td><td colspan=""2"">{militaryObligation.InstitutionAdress}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Perioada:</td><td colspan=""2"">{militaryObligation.StartObligationPeriod?.ToString("dd-MM-yyyy")} - {militaryObligation.EndObligationPeriod?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Efectiv:</td><td colspan=""2"">{militaryObligation.Efectiv}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Specialitate militară:</td><td colspan=""2"">{militaryObligation.MilitarySpecialty}</td>
                                    
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Gradul:</td><td colspan=""2"">{militaryObligation.Degree}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Livret militar(seria, nr., data eliberării):</td><td colspan=""2"">{militaryObligation.MilitaryBookletSeries}, {militaryObligation.MilitaryBookletNumber}, {militaryObligation.MilitaryBookletReleaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>";
                    }
                    else if (militaryObligation.MilitaryObligationType == MilitaryObligationTypeEnum.PerformedMilitaryService)
                    {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Obligație militară:</td><td colspan=""2"">{EnumMessages.Translate(militaryObligation.MilitaryObligationType)}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul mobilizării:</td><td colspan=""2"">{militaryObligation.MobilizationYear?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Anul retragerii:</td><td colspan=""2"">{militaryObligation.WithdrawalYear?.ToString("dd-MM-yyyy")}</td>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Efectiv:</td><td colspan=""2"">{militaryObligation.Efectiv}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Specialitate militară:</td><td colspan=""2"">{militaryObligation.MilitarySpecialty}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Livret militar(seria, nr., data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{militaryObligation.MilitaryBookletSeries}, {militaryObligation.MilitaryBookletNumber}, {militaryObligation.MilitaryBookletReleaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>
                                    <tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Gradul:</td><td colspan=""2"">{militaryObligation.Degree}</td>
                                    </tr>";
                    }
                    else if (militaryObligation.MilitaryObligationType == MilitaryObligationTypeEnum.Recruit)
                    {
                        content += $@"<tr>
                                        <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Obligație militară:</td><td colspan=""2"">{EnumMessages.Translate(militaryObligation.MilitaryObligationType)}</td>
                                        <td rowspan=""2""style=""font-size: 15px; background-color: #8e95a1; color: black;"">Adeverinţă de recrutare(seria, nr., data eliberării):</td>
                                        <td colspan=""2"" rowspan=""2"">{militaryObligation.MilitaryBookletSeries}, {militaryObligation.MilitaryBookletNumber}, {militaryObligation.MilitaryBookletReleaseDay?.ToString("dd-MM-yyyy")}</td>
                                    </tr>";
                    }
                }

                return content;
            }
            
            return "nu există informaţii.";
        }

        public string getRecommendationForStudies(List<RecommendationForStudy> recommendationForStudies)
        {
            string content = string.Empty;
            if (recommendationForStudies?.Count() != 0 && recommendationForStudies?.Count() != null)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                                <th colspan=""2"">Numele persoanei ce a recomandat</th>
                                <th colspan=""2"">Prenumele persoanei ce a recomandat</th>
                                <th>Funcție (poziție) deținută</th>
                                <th>Subdiviziune MAI</th>
                            </tr>";

                foreach (var recommendationForStudy in recommendationForStudies)
                {
                    content += $@"<tr>
                                    <td colspan=""2"">{recommendationForStudy.Name}</td>
                                    <td colspan=""2"">{recommendationForStudy.LastName}</td>
                                    <td>{recommendationForStudy.Function}</td>
                                    <td>{recommendationForStudy.Subdivision}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public string getKinshipRelation(List<KinshipRelationWithUserProfile> kinshipRelationsWithUserProfiles)
        {
            string content = string.Empty;
            if (kinshipRelationsWithUserProfiles?.Count() != 0 && kinshipRelationsWithUserProfiles?.Count() != null)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                                <th>Gradul de rudenie</th>
                                <th colspan=""2"">Numele persoanei</th>
                                <th >Prenumele persoanei</th>
                                <th>Funcție (poziție) deținută</th>
                                <th>Subdiviziune MAI</th>
                            </tr>";

                foreach (var kinshipRelation in kinshipRelationsWithUserProfiles)
                {
                    content += $@"<tr>
                                    <td>{EnumMessages.Translate(kinshipRelation.KinshipDegree)}</td>
                                    <td colspan=""2"">{kinshipRelation.Name}</td>
                                    <td>{kinshipRelation.LastName}</td>
                                    <td>{kinshipRelation.Function}</td>
                                    <td>{kinshipRelation.Subdivision}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public string getKinshipRelationInfo(List<KinshipRelation> kinshipRelations)
        {
            string content = string.Empty;
            if (kinshipRelations?.Count() != 0 && kinshipRelations?.Count() != null)
            {
                foreach (var kinshipRelation in kinshipRelations)
                {
                    content += $@"<tr>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Gradul de rudenie:</td><td colspan=""2"">{EnumMessages.Translate(kinshipRelation.KinshipDegree)}</td>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Numele persoanei:</td><td colspan=""2"">{kinshipRelation.Name}</td>
                                </tr>
                                <tr>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Prenumele persoanei:</td><td colspan=""2"">{kinshipRelation.LastName}</td>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Data nașterii:</td><td colspan=""2"">{kinshipRelation.BirthDate.ToString("dd-MM-yyyy")}</td>
                                </tr>
                                <tr>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Locul nașterii:</td><td colspan=""2"">{kinshipRelation.BirthLocation}</td>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Funcție (poziție) deținută:</td><td colspan=""2"">{kinshipRelation.Function}</td>
                                </tr>
                                <tr>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Locul de muncă:</td><td colspan=""2"">{kinshipRelation.WorkLocation}</td>
                                    <td style=""font-size: 15px; background-color: #8e95a1; color: black;"">Domiciliu:</td><td colspan=""2"">{kinshipRelation.ResidenceAddress}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public string getModuleRoles(List<UserProfileModuleRole> userProfileModuleRoles)
        {
            string content = string.Empty;
            if (userProfileModuleRoles.Count() != 0)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                            <th colspan=""2"">Modul</th>
                            <th colspan=""2"">Permisiune</th>
                            <th colspan=""2"">Cod Permisiune</th>
                        </tr>";

                foreach (var role in userProfileModuleRoles)
                {
                    content += $@"<tr>
                                    <td colspan=""2"">{role.ModuleRole?.Module?.Name}</td>
                                    <td colspan=""2"">{role.ModuleRole?.Name}</td>
                                    <td colspan=""2"">{role.ModuleRole?.Code}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public string getSolicitedVacantPosition(List<SolicitedVacantPosition> solicitedVacantPosition)
        {
            string content = string.Empty;
            if (solicitedVacantPosition.Count() != 0)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                            <th colspan=""2"">Poziție candidată</th>
                            <th colspan=""2"">Timp solicitat</th>
                            <th>Statut</th>
                            <th>Documente atașate/necesare</th>
                        </tr>";

                foreach (var vacantPosition in solicitedVacantPosition)
                {
                    content += $@"<tr>
                                    <td colspan=""2"">{vacantPosition.CandidatePosition?.Name}</td>
                                    <td colspan=""2"">{vacantPosition.CreateDate.ToString("dd-MM-yyyy, H:mm")}</td>
                                    <td>{EnumMessages.Translate(vacantPosition.SolicitedPositionStatus)}</td>
                                    <td>{vacantPosition.SolicitedVacantPositionUserFiles?.Count() + "/" + vacantPosition?.CandidatePosition?.RequiredDocumentPositions?.Count()}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public async Task<string> getMyTestsAsync(UserProfile userProfile)
        {
            var getTest = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Test);
            string content = string.Empty;
            
            if (getTest.Count() != 0)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                                <th>Data</th>
                                <th>Statut</th>
                                <th colspan=""2"">Testul</th>
                                <th>Eveniment</th>
                                <th>Puncte / scorul min.</th>
                            </tr>";

                foreach (var test in getTest)
                {
                    content += $@"<tr>
                                    <td>{test.ProgrammedTime.ToString("dd-MM-yyyy") + "/" + (string.IsNullOrEmpty(test.EndProgrammedTime?.ToString("dd-MM-yyyy")) ? " - - - - -" : test.EndProgrammedTime?.ToString("dd-MM-yyyy"))}</td>
                                    <td>{EnumMessages.Translate(test.TestStatus)}</td>
                                    <td colspan=""2"">{test.TestTemplate.Name}</td>
                                    <td>{test?.Event?.Name ?? " - - - - -"}</td>
                                    <td>{(test.AccumulatedPercentage ?? 0) + "/" + test.TestTemplate.MinPercent}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public async Task<string> getEvaluatedTestsAsync(UserProfile userProfile)
        {
            var getEvaluatedTests = await GetUserProfileEvaluatedTestsOrEvaluations(TestTemplateModeEnum.Test);
            string content = string.Empty;

            if (getEvaluatedTests.Count() != 0)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                                <th>Data</th>
                                <th>Statut</th>
                                <th>Evaluat</th>
                                <th>Testul</th>
                                <th>Puncte / scorul min.</th>
                                <th>Rezultat</th>
                            </tr>";

                foreach (var evaluatedTest in getEvaluatedTests)
                {
                    content += $@"<tr>
                                    <td>{evaluatedTest.ProgrammedTime.ToString("dd-MM-yyyy") + "/" + (string.IsNullOrEmpty(evaluatedTest.EndProgrammedTime?.ToString("dd-MM-yyyy")) ? " - - - - -" : evaluatedTest.EndProgrammedTime?.ToString("dd-MM-yyyy"))}</td>
                                    <td>{EnumMessages.Translate(evaluatedTest.TestStatus)}</td>
                                    <td>{evaluatedTest.UserProfile?.FullName ?? " - - - - -"}</td>
                                    <td>{evaluatedTest.TestTemplate.Name}</td>
                                    <td>{(evaluatedTest.AccumulatedPercentage ?? 0) + "/" + evaluatedTest.TestTemplate.MinPercent}</td>
                                    <td>{EnumMessages.Translate(evaluatedTest.ResultStatus)}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public async Task<string> getMyEvaluationsAsync(UserProfile userProfile)
        {
            var getEvaluations = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Evaluation);
            string content = string.Empty;

            if (getEvaluations.Count() != 0)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                                <th>Data programată</th>
                                <th>Evaluare</th>
                                <th>Evaluat</th>
                                <th>Eveniment</th>
                                <th>Statut</th>
                                <th>Rezultat</th>
                            </tr>";

                foreach (var evaluation in getEvaluations)
                {
                    content += $@"<tr>
                                    <td>{evaluation.TestTemplate.Name.ToString()}</td>
                                    <td>{evaluation.Evaluator?.FullName ?? " - - - - -"}</td>
                                    <td>{evaluation.Event?.Name ?? " - - - - -"}</td>
                                    <td>{evaluation.Location?.Name ?? " - - - - -"}</td>
                                    <td>{EnumMessages.Translate(evaluation.TestStatus)}</td>
                                    <td>{EnumMessages.Translate(evaluation.ResultStatus)}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        public async Task<string> getEvaluatedEvaluationsAsync(UserProfile userProfile)
        {
            var getEvaluatedEvaluations = await GetUserProfileEvaluatedTestsOrEvaluations(TestTemplateModeEnum.Evaluation);
            string content = string.Empty;

            if (getEvaluatedEvaluations.Count() != 0)
            {
                content += $@"<tr style=""text-align: center; background-color: #8e95a1; color: black; height: 30px;"">
                                <th>Data programată</th>
                                <th>Evaluare</th>
                                <th>Evaluat</th>
                                <th>Eveniment</th>
                                <th>Statut</th>
                                <th>Rezultat</th>
                            </tr>";

                foreach (var evaluation in getEvaluatedEvaluations)
                {
                    content += $@"<tr>
                                    <td>{evaluation.ProgrammedTime.ToString("dd-MM-yyyy")}</td>
                                    <td>{evaluation.TestTemplate.Name}</td>
                                    <td>{evaluation.UserProfile?.FullName ?? " - - - - -"}</td>
                                    <td>{evaluation.Event?.Name ?? " - - - - -"}</td>
                                    <td>{EnumMessages.Translate(evaluation.TestStatus)}</td>
                                    <td>{EnumMessages.Translate(evaluation.ResultStatus)}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }

        /*public async Task<string> getPollsAsync(UserProfile userProfile)
        {
            var getPolls = await GetUserProfileTestsEvaluationsPolls(TestTemplateModeEnum.Poll);

            if (getPolls.Count() != 0)
            {
                foreach (var poll in getPolls)
                {
                    content += $@"<tr>
                                    <td colspan=""2"">{poll.TestTemplate.Name}</td>
                                    <td colspan=""2"">{poll.Event?.Name ?? " - - - - -"}</td>
                                    <td colspan=""2"">{EnumMessages.Translate(poll.TestStatus)}</td>
                                    <td colspan=""2"">{poll.ProgrammedTime.ToString("dd-MM-yyyy") ?? " - - - - -"}</td>
                                    <td colspan=""2"">{poll.Event?.FromDate.ToString("dd-MM-yyyy") ?? " - - - - -"}</td>
                                    <td colspan=""2"">{poll.Event?.TillDate.ToString("dd-MM-yyyy") ?? " - - - - -"}</td>
                                </tr>";
                }

                return content;
            }

            return "nu există informaţii.";
        }*/

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
    }
}
