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
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation360.Application.BLL.Services
{
    public class PdfService : IPdfService
    {
        private readonly AppDbContext _appDbContext;
        private readonly StorageDbContext _storageDbContext;
        private readonly IGeneratePdf _generatePdf;
        private readonly AppDbContext _dbContext;

        public PdfService(AppDbContext appDbContext,
            StorageDbContext storageDbContext,
            IGeneratePdf generatePdf,
            AppDbContext dbContext)
        {
            _appDbContext = appDbContext;
            _generatePdf = generatePdf;
            _storageDbContext = storageDbContext;
            _dbContext = dbContext;
        }

        public async Task<FileDataDto> PrintEvaluationPdf(int evaluationId)
        {
            var evaluation = _appDbContext.Evaluations
                .Include(e => e.EvaluatedUserProfile)
                    .ThenInclude(e => e.EmployeeFunction)
                .Include(e => e.EvaluatorUserProfile)
                    .ThenInclude(e => e.EmployeeFunction)
                .Include(e => e.CounterSignerUserProfile)
                    .ThenInclude(e => e.EmployeeFunction)
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
            
            return FileDataDto.GetPdf($"{evaluation.EvaluatedUserProfile.FullName}EvaluareaAnuală360.pdf", res);
        }

        private Dictionary<string, string> GetDictionary(Evaluation evaluation)
        {
            List<decimal?> listForM1 = new() {evaluation.Question1, evaluation.Question2, evaluation.Question3, evaluation.Question4, evaluation.Question5};
            decimal? m1 = listForM1.Average();

            List<decimal?> listForM2 = new() {evaluation.Question6, evaluation.Question7, evaluation.Question8};
            decimal? m2 = listForM2.Average();

            List<decimal?> listForM3 = new() {evaluation.Score1, evaluation.Score2, evaluation.Score3, evaluation.Score4, evaluation.Score5};
            decimal? m3 = listForM3.Average();

            List<decimal?> listForPb = new() {evaluation.Question9, evaluation.Question10, evaluation.Question11, evaluation.Question12};
            decimal? pb = listForPb.Average();

            List<decimal?> listForM4 = new() {evaluation.Question13, pb};
            decimal? m4 = listForM4.Average();

            List<decimal?> listForMea = new() {m1, m2, m3, m4};
            decimal? mea = listForMea.Average();
            decimal? mf;

            if (evaluation.PartialEvaluationScore != null)
            {
                List<decimal?> listForMf = new() {mea, evaluation.PartialEvaluationScore};
                mf = listForMf.Average();
            }
            else
            {
                mf = mea;
            }

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{Type}", " a angajatului care exercită funcții de " + evaluation.Type.ToString().ToLower());
            myDictionary.Add("{SubdivisionName}", evaluation.SubdivisionName);
            myDictionary.Add("{DateCompletionGeneralData}", evaluation.DateCompletionGeneralData?.ToString("dd.MM.yyyy"));  
            myDictionary.Add("{NameSurnameEvaluated}", evaluation.EvaluatedUserProfile.FullName);  
            myDictionary.Add("{SubdivisionEvaluated}", evaluation.SubdivisionEvaluated);
            myDictionary.Add("{FunctionSubdivision}", evaluation.EvaluatedUserProfile.EmployeeFunction.Name);
            myDictionary.Add("{SpecialOrMilitaryGrade}", EnumMessages.Translate(evaluation.SpecialOrMilitaryGrade));
            myDictionary.Add("{PeriodEvaluatedFromTo}", evaluation.PeriodEvaluatedFromTo?.ToString("dd.MM.yyyy")); 
            myDictionary.Add("{PeriodEvaluatedUpTo}", evaluation.PeriodEvaluatedUpTo?.ToString("dd.MM.yyyy"));  
            myDictionary.Add("{EducationEnum}", EnumMessages.Translate(evaluation.EducationEnum)); 
            myDictionary.Add("{ProfessionalTrainingActivities}", Concatenate(EnumMessages.Translate(evaluation.ProfessionalTrainingActivities).ToLower(), evaluation.ProfessionalTrainingActivitiesType?.ToString().ToLower()));
            myDictionary.Add("{CourseName}", Valide(evaluation.CourseName));
            myDictionary.Add("{PeriodRunningActivity}", Concatenate(evaluation.PeriodRunningActivityFromTo?.ToString("dd.MM.yyyy"), evaluation.PeriodRunningActivityUpTo?.ToString("dd.MM.yyyy")));  
            myDictionary.Add("{AdministrativeActOfStudies}", Valide(evaluation.AdministrativeActOfStudies)); 
            myDictionary.Add("{ServiceDuringEvaluationCourse}", EnumMessages.Translate(evaluation.ServiceDuringEvaluationCourse).ToLower());
            myDictionary.Add("{FunctionEvaluated}", Valide(evaluation.FunctionEvaluated));  
            myDictionary.Add("{AppointmentDate}", Valide(evaluation.AppointmentDate?.ToString("dd.MM.yyyy")));  
            myDictionary.Add("{AdministrativeActService}", Valide(evaluation.AdministrativeActService));
            myDictionary.Add("{PartialEvaluation}", Concatenate(evaluation.PartialEvaluationPeriodFromTo?.ToString("dd.MM.yyyy"), evaluation.PartialEvaluationPeriodUpTo?.ToString("dd.MM.yyyy")));  
            myDictionary.Add("{PartialEvaluationScore}", Valide(evaluation.PartialEvaluationScore?.ToString()));
            myDictionary.Add("{QualifierPartialEvaluations}", EnumMessages.Translate(evaluation.QualifierPartialEvaluations));  
            myDictionary.Add("{SanctionApplied}", EnumMessages.Translate(evaluation.SanctionApplied));
            myDictionary.Add("{DateSanctionApplication}", Valide(evaluation.DateSanctionApplication?.ToString("dd.MM.yyyy")));  
            myDictionary.Add("{DateLiftingSanction}", Valide(evaluation.DateLiftingSanction?.ToString("dd.MM.yyyy"))); 
            myDictionary.Add("{QualificationEvaluationObtained2YearsPast}", EnumMessages.Translate(evaluation.QualificationEvaluationObtained2YearsPast));
            myDictionary.Add("{QualificationEvaluationObtainedPreviousYear}", EnumMessages.Translate(evaluation.QualificationEvaluationObtainedPreviousYear));
            myDictionary.Add("{QualificationQuarter1}", EnumMessages.Translate(evaluation.QualificationQuarter1));
            myDictionary.Add("{QualificationQuarter2}", EnumMessages.Translate(evaluation.QualificationQuarter2));
            myDictionary.Add("{QualificationQuarter3}", EnumMessages.Translate(evaluation.QualificationQuarter3));
            myDictionary.Add("{QualificationQuarter4}", EnumMessages.Translate(evaluation.QualificationQuarter4));
            myDictionary.Add("{Question1}", evaluation.Question1.ToString());
            myDictionary.Add("{Question2}", evaluation.Question2.ToString());
            myDictionary.Add("{Question3}", evaluation.Question3.ToString());
            myDictionary.Add("{Question4}", evaluation.Question4.ToString());
            myDictionary.Add("{Question5}", evaluation.Question5.ToString());
            myDictionary.Add("{m1}", m1?.ToString("0.00"));
            myDictionary.Add("{Question6}", evaluation.Question6.ToString());
            myDictionary.Add("{Question7}", evaluation.Question7.ToString());
            myDictionary.Add("{Question8}", evaluation.Question8.ToString());
            myDictionary.Add("{m2}", m2?.ToString("0.00"));
            myDictionary.Add("{Question9}", evaluation.Question9.ToString());
            myDictionary.Add("{Question10}", evaluation.Question10.ToString());
            myDictionary.Add("{Question11}", evaluation.Question11.ToString());
            myDictionary.Add("{Question12}", evaluation.Question12.ToString());
            myDictionary.Add("{Question13}", evaluation.Question13.ToString());
            myDictionary.Add("{Goal1}", Valide(evaluation.Goal1));
            myDictionary.Add("{Goal2}", Valide(evaluation.Goal2));
            myDictionary.Add("{Goal3}", Valide(evaluation.Goal3));
            myDictionary.Add("{Goal4}", Valide(evaluation.Goal4));
            myDictionary.Add("{Goal5}", Valide(evaluation.Goal5));
            myDictionary.Add("{KPI1}", Valide(evaluation.KPI1));
            myDictionary.Add("{KPI2}", Valide(evaluation.KPI2));
            myDictionary.Add("{KPI3}", Valide(evaluation.KPI3));
            myDictionary.Add("{KPI4}", Valide(evaluation.KPI4));
            myDictionary.Add("{KPI5}", Valide(evaluation.KPI5));
            myDictionary.Add("{PerformanceTerm1}", Valide(evaluation.PerformanceTerm1));
            myDictionary.Add("{PerformanceTerm2}", Valide(evaluation.PerformanceTerm2));
            myDictionary.Add("{PerformanceTerm3}", Valide(evaluation.PerformanceTerm3));
            myDictionary.Add("{PerformanceTerm4}", Valide(evaluation.PerformanceTerm4));
            myDictionary.Add("{PerformanceTerm5}", Valide(evaluation.PerformanceTerm5));
            myDictionary.Add("{Score1}", Valide(evaluation.Score1?.ToString()));
            myDictionary.Add("{Score2}", Valide(evaluation.Score2?.ToString()));
            myDictionary.Add("{Score3}", Valide(evaluation.Score3?.ToString()));
            myDictionary.Add("{Score4}", Valide(evaluation.Score4?.ToString()));
            myDictionary.Add("{Score5}", Valide(evaluation.Score5?.ToString()));
            myDictionary.Add("{m3}", Valide(m3?.ToString("0.00")));
            myDictionary.Add("{m4}", m4?.ToString("0.00"));
            myDictionary.Add("{pb}", pb?.ToString("0.00"));
            myDictionary.Add("{mea}", mea?.ToString("0.00"));
            myDictionary.Add("{mf}", mf?.ToString("0.00"));
            myDictionary.Add("{FinalEvaluationQualification}", EnumMessages.Translate(evaluation.FinalEvaluationQualification));
            myDictionary.Add("{DateEvaluationInterview}", Valide(evaluation.DateEvaluationInterview?.ToString("dd.MM.yyyy")));
            myDictionary.Add("{DateSettingIindividualGoals}", Valide(evaluation.DateSettingIindividualGoals?.ToString("dd.MM.yyyy")));
            myDictionary.Add("{Need1ProfessionalDevelopmentEvaluated}", Valide(evaluation.Need1ProfessionalDevelopmentEvaluated));
            myDictionary.Add("{Need2ProfessionalDevelopmentEvaluated}", Valide(evaluation.Need2ProfessionalDevelopmentEvaluated));
            myDictionary.Add("{Need3ProfessionalDevelopmentEvaluated}", Valide(evaluation.Need3ProfessionalDevelopmentEvaluated));
            myDictionary.Add("{Need4ProfessionalDevelopmentEvaluated}", Valide(evaluation.Need4ProfessionalDevelopmentEvaluated));
            myDictionary.Add("{Need5ProfessionalDevelopmentEvaluated}", Valide(evaluation.Need5ProfessionalDevelopmentEvaluated));
            myDictionary.Add("{CommentsEvaluator}", Valide(evaluation.CommentsEvaluator));
            myDictionary.Add("{CommentsEvaluated}", evaluation.CommentsEvaluated);
            myDictionary.Add("{DateAccepEvaluated}", evaluation.DateAcceptOrRejectEvaluated?.ToString("dd.MM.yyyy"));
            myDictionary.Add("{NameSurnameEvaluator}", evaluation.EvaluatorUserProfile.FullName);
            myDictionary.Add("{FunctionEvaluator}", evaluation.EvaluatorUserProfile.EmployeeFunction.Name);
            myDictionary.Add("{DateEvaluatedKnow}", evaluation.DateEvaluatedKnow?.ToString("dd.MM.yyyy"));

            if (evaluation.CounterSignerUserProfileId != null)
            {
                myDictionary.Add("{OtherComments}", evaluation.OtherComments);
                myDictionary.Add("{NameSurnameCounterSigner}", evaluation.CounterSignerUserProfile.FullName);
                myDictionary.Add("{FunctionCounterSigner}", evaluation.CounterSignerUserProfile.EmployeeFunction.Name);
                myDictionary.Add("{DateCompletionCounterSigner}", evaluation.DateCompletionCounterSigner?.ToString("dd.MM.yyyy"));
                myDictionary.Add("{BIFAT}", "BIFAT");
                myDictionary.Add("{Semnat}", "Semnat");
            }
            else
            {
                myDictionary.Add("{OtherComments}", "—");
                myDictionary.Add("{NameSurnameCounterSigner}", "—");
                myDictionary.Add("{FunctionCounterSigner}", "—");
                myDictionary.Add("{DateCompletionCounterSigner}", "—");
                myDictionary.Add("{BIFAT}", "—");
                myDictionary.Add("{Semnat}", "—");
            }

            return myDictionary;
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

        private string Valide(string word)
        {
            if (word != null)
            {
                return word;
            }
            else
            {
                return "—";
            }
        }

        private string Concatenate(string word1, string word2)
        {
            if (word1 != null && word2 != null)
            {
                return word1 + " - " + word2;
            }
            else
            {
                return "—";
            }
        }
    }
}