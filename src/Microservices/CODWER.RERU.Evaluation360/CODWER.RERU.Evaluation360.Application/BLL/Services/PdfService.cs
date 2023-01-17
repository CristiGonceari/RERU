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
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;

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
            
            return FileDataDto.GetPdf($"{evaluation.EvaluatedUserProfile.FullName}EvaluareaAnuală360.pdf", res);
        }

        private Dictionary<string, string> GetDictionary(Evaluation evaluation)
        {
            List<decimal?> listForM1 = new List<decimal?> {evaluation.Question1, evaluation.Question2, evaluation.Question3, evaluation.Question4, evaluation.Question5};
            decimal? m1 = listForM1.Average();

            List<decimal?> listForM2 = new List<decimal?> {evaluation.Question6, evaluation.Question7, evaluation.Question8};
            decimal? m2 = listForM2.Average();

            List<decimal?> listForM3 = new List<decimal?> {evaluation.Score1, evaluation.Score2, evaluation.Score3, evaluation.Score4, evaluation.Score5};
            decimal? m3 = listForM3.Average();

            List<decimal?> listForPb = new List<decimal?> {evaluation.Question9, evaluation.Question10, evaluation.Question11, evaluation.Question12};
            decimal? pb = listForPb.Average();

            List<decimal?> listForM4 = new List<decimal?> {evaluation.Question13, pb};
            decimal? m4 = listForM4.Average();

            List<decimal?> listForMea = new List<decimal?> {m1, m2, m3, m4};
            decimal? mea = listForMea.Sum();
            decimal? mf;

            if (evaluation.PartialEvaluationScore != null)
            {
                List<decimal?> listForMf = new List<decimal?> {mea, evaluation.PartialEvaluationScore};
                mf = listForMf.Sum();
            }
            else
            {
                mf = mea;
            }

            if (mf >= 1 && mf <= 1.5m) evaluation.FinalEvaluationQualification = QualifiersEnum.Dissatisfied;
            else if (mf >= 1.51m && mf <= 2.5m) evaluation.FinalEvaluationQualification = QualifiersEnum.Satisfied;
            else if (mf >= 2.51m && mf <= 3.5m) evaluation.FinalEvaluationQualification = QualifiersEnum.Good;
            else if (mf >= 3.51m && mf <= 4m) evaluation.FinalEvaluationQualification = QualifiersEnum.VeryGood;

            var myDictionary = new Dictionary<string, string>();

            myDictionary.Add("{Type}", " a angajatului care exercită funcții de " + evaluation.Type.ToString()); //TODO: de schimbat in viitorul apropiat
            myDictionary.Add("{SubdivisionName}", evaluation.SubdivisionName);
            myDictionary.Add("{DateCompletionGeneralData}", evaluation.DateCompletionGeneralData?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{NameSurnameEvaluated}", evaluation.EvaluatedUserProfile.FullName);  
            myDictionary.Add("{SubdivisionEvaluated}", evaluation.SubdivisionEvaluated);
            myDictionary.Add("{FunctionSubdivision}", evaluation.EvaluatedUserProfile.Role.Name);
            myDictionary.Add("{SpecialOrMilitaryGrade}", evaluation.SpecialOrMilitaryGrade.ToString());
            myDictionary.Add("{SpecialOrMilitaryGradeText}", evaluation.SpecialOrMilitaryGradeText);
            myDictionary.Add("{PeriodEvaluatedFromTo}", evaluation.PeriodEvaluatedFromTo?.ToString("dd/MM/yyyy")); 
            myDictionary.Add("{PeriodEvaluatedUpTo}", evaluation.PeriodEvaluatedUpTo?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{EducationEnum}", evaluation.EducationEnum.ToString());  
            myDictionary.Add("{ProfessionalTrainingActivitiesEnum}", evaluation.ProfessionalTrainingActivities.ToString());  
            myDictionary.Add("{ProfessionalTrainingActivitiesType}", evaluation.ProfessionalTrainingActivitiesType.ToString());  
            myDictionary.Add("{CourseName}", evaluation.CourseName);  
            myDictionary.Add("{PeriodRunningActivityFromTo}", evaluation.PeriodRunningActivityFromTo?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{PeriodRunningActivityUpTo}", evaluation.PeriodRunningActivityUpTo?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{AdministrativeActOfStudies}", evaluation.AdministrativeActOfStudies);  
            myDictionary.Add("{ServiceDuringEvaluationCourse}", evaluation.ServiceDuringEvaluationCourse.ToString());  
            myDictionary.Add("{FunctionEvaluated}", evaluation.FunctionEvaluated);  
            myDictionary.Add("{AppointmentDate}", evaluation.AppointmentDate?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{AdministrativeActService}", evaluation.AdministrativeActService);  
            myDictionary.Add("{PartialEvaluationPeriodFromTo}", evaluation.PartialEvaluationPeriodFromTo?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{PartialEvaluationPeriodUpTo}", evaluation.PartialEvaluationPeriodUpTo?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{PartialEvaluationScore}", evaluation.PartialEvaluationScore.ToString());  
            myDictionary.Add("{QualifierPartialEvaluations}", evaluation.QualifierPartialEvaluations.ToString());  
            myDictionary.Add("{SanctionAppliedEvaluationCourse}", evaluation.SanctionAppliedEvaluationCourse);  
            myDictionary.Add("{DateSanctionApplication}", evaluation.DateSanctionApplication?.ToString("dd/MM/yyyy"));  
            myDictionary.Add("{DateLiftingSanction}", evaluation.DateLiftingSanction?.ToString("dd/MM/yyyy"));
            myDictionary.Add("{QualificationEvaluationObtained2YearsPast}", evaluation.QualificationEvaluationObtained2YearsPast.ToString());
            myDictionary.Add("{QualificationEvaluationObtainedPreviousYear}", evaluation.QualificationEvaluationObtainedPreviousYear.ToString());
            myDictionary.Add("{QualificationQuarter1}", evaluation.QualificationQuarter1.ToString());
            myDictionary.Add("{QualificationQuarter2}", evaluation.QualificationQuarter2.ToString());
            myDictionary.Add("{QualificationQuarter3}", evaluation.QualificationQuarter3.ToString());
            myDictionary.Add("{QualificationQuarter4}", evaluation.QualificationQuarter4.ToString());
            myDictionary.Add("{Question1}", evaluation.Question1.ToString());
            myDictionary.Add("{Question2}", evaluation.Question2.ToString());
            myDictionary.Add("{Question3}", evaluation.Question3.ToString());
            myDictionary.Add("{Question4}", evaluation.Question4.ToString());
            myDictionary.Add("{Question5}", evaluation.Question5.ToString());
            myDictionary.Add("{m1}", m1.ToString());
            myDictionary.Add("{Question6}", evaluation.Question6.ToString());
            myDictionary.Add("{Question7}", evaluation.Question7.ToString());
            myDictionary.Add("{Question8}", evaluation.Question8.ToString());
            myDictionary.Add("{m2}", m2.ToString());
            myDictionary.Add("{Question9}", evaluation.Question9.ToString());
            myDictionary.Add("{Question10}", evaluation.Question10.ToString());
            myDictionary.Add("{Question11}", evaluation.Question11.ToString());
            myDictionary.Add("{Question12}", evaluation.Question12.ToString());
            myDictionary.Add("{Question13}", evaluation.Question13.ToString());
            myDictionary.Add("{Goal1}", evaluation.Goal1);
            myDictionary.Add("{Goal2}", evaluation.Goal2);
            myDictionary.Add("{Goal3}", evaluation.Goal3);
            myDictionary.Add("{Goal4}", evaluation.Goal4);
            myDictionary.Add("{Goal5}", evaluation.Goal5);
            myDictionary.Add("{KPI1}", evaluation.KPI1);
            myDictionary.Add("{KPI2}", evaluation.KPI2);
            myDictionary.Add("{KPI3}", evaluation.KPI3);
            myDictionary.Add("{KPI4}", evaluation.KPI4);
            myDictionary.Add("{KPI5}", evaluation.KPI5);
            myDictionary.Add("{PerformanceTerm1}", evaluation.PerformanceTerm1);
            myDictionary.Add("{PerformanceTerm2}", evaluation.PerformanceTerm2);
            myDictionary.Add("{PerformanceTerm3}", evaluation.PerformanceTerm3);
            myDictionary.Add("{PerformanceTerm4}", evaluation.PerformanceTerm4);
            myDictionary.Add("{PerformanceTerm5}", evaluation.PerformanceTerm5);
            myDictionary.Add("{Score1}", evaluation.Score1.ToString());
            myDictionary.Add("{Score2}", evaluation.Score2.ToString());
            myDictionary.Add("{Score3}", evaluation.Score3.ToString());
            myDictionary.Add("{Score4}", evaluation.Score4.ToString());
            myDictionary.Add("{Score5}", evaluation.Score5.ToString());
            myDictionary.Add("{m3}", m3.ToString());
            myDictionary.Add("{m4}", m4.ToString());
            myDictionary.Add("{pb}", pb.ToString());
            myDictionary.Add("{mea}", mea.ToString());
            myDictionary.Add("{mf}", mf.ToString());
            myDictionary.Add("{ScoringRange}", ScoringRange(mf));
            myDictionary.Add("{FinalEvaluationQualification}", evaluation.FinalEvaluationQualification.ToString());
            myDictionary.Add("{DateEvaluationInterview}", evaluation.DateEvaluationInterview?.ToString("dd/MM/yyyy"));
            myDictionary.Add("{DateSettingIindividualGoals}", evaluation.DateSettingIindividualGoals?.ToString("dd/MM/yyyy"));
            myDictionary.Add("{Need1ProfessionalDevelopmentEvaluated}", evaluation.Need1ProfessionalDevelopmentEvaluated);
            myDictionary.Add("{Need2ProfessionalDevelopmentEvaluated}", evaluation.Need2ProfessionalDevelopmentEvaluated);
            myDictionary.Add("{Need3ProfessionalDevelopmentEvaluated}", evaluation.Need3ProfessionalDevelopmentEvaluated);
            myDictionary.Add("{Need4ProfessionalDevelopmentEvaluated}", evaluation.Need4ProfessionalDevelopmentEvaluated);
            myDictionary.Add("{Need5ProfessionalDevelopmentEvaluated}", evaluation.Need5ProfessionalDevelopmentEvaluated);
            myDictionary.Add("{CommentsEvaluator}", evaluation.CommentsEvaluator);
            myDictionary.Add("{CommentsEvaluated}", evaluation.CommentsEvaluated);
            myDictionary.Add("{DateAccepEvaluated}", evaluation.DateAcceptOrRejectEvaluated?.ToString("dd/MM/yyyy"));
            myDictionary.Add("{NameSurnameEvaluator}", evaluation.EvaluatorUserProfile.FullName);
            myDictionary.Add("{FunctionEvaluator}", evaluation.EvaluatorUserProfile.Role.Name);
            myDictionary.Add("{OtherComments}", evaluation.OtherComments);
            myDictionary.Add("{NameSurnameCounterSigner}", evaluation.CounterSignerUserProfile.FullName);
            myDictionary.Add("{FunctionCounterSigner}", evaluation.CounterSignerUserProfile.Role.Name);
            myDictionary.Add("{DateCompletionCounterSigner}", evaluation.DateCompletionCounterSigner?.ToString("dd/MM/yyyy"));
            myDictionary.Add("{DateEvaluatedKnow}", evaluation.DateEvaluatedKnow?.ToString("dd/MM/yyyy"));

            return myDictionary;
        }

        private string ScoringRange(decimal? mf)
        {
            if (mf >= 1 && mf <= 1.5m) return "între 1,00 şi 1,50";
            else if (mf >= 1.51m && mf <= 2.5m) return "între 1,51 şi 2,50";
            else if (mf >= 2.51m && mf <= 3.5m) return "între 2,51 şi 3,50";
            else if (mf >= 3.51m && mf <= 4m) return "între 3,51 şi 4,00";
            else throw new ArgumentOutOfRangeException("");
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
