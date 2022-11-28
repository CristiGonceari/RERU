using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations
{
    public class EditEvaluationDto
    {
        public int Id { set; get; }
        public string EvaluatedName { set; get; }
        public string SubdivisionName { set; get; }
        public DateTime DateCompletionGeneralData { set; get; }
       // public EvaluationStatusEnum Status { set; get; }
        public string NameSurnameEvaluatedEmployee { set; get; }
        public string FunctionSubdivision { set; get; }
        public string SpecialOrMilitaryGrade { set; get; }
        public string PeriodEvaluatedFromTo { set; get; }
        public string PeriodEvaluatedUpTo { set; get; }
        public string Education { set; get; }
        public string ProfessionalTrainingActivities { set; get; }
        public string CourseName { set; get; }
        public string PeriodRunningActivity { set; get; }
        public string AdministrativeActOfStudies { set; get; }
        public string ModificationServiceReportDuringEvaluationCourse { set; get; }
        public string Function { set; get; }
        public string AppointmentDate { set; get; }
        public string AdministrativeActService { set; get; }
        public string PartialEvaluationPeriod { set; get; }
        public string FinalScorePartialEvaluations { set; get; }
        public string QualifierPartialEvaluations { set; get; }
        public string SanctionAppliedEvaluationCourse { set; get; }
        public string DateSanctionApplication { set; get; }
        public string DateLiftingSanction { set; get; }
        public string QualificationEvaluationObtained2YearsPast { set; get; }
        public string QualificationEvaluationObtainedPreviousYear { set; get; }
        public string QualificationQuarter1 { set; get; }
        public string QualificationQuarter2 { set; get; }
        public string QualificationQuarter3 { set; get; }
        public string QualificationQuarter4 { set; get; }
        public string Question1 { set; get; }
        public string Question2 { set; get; }
        public string Question3 { set; get; }
        public string Question4 { set; get; }
        public string Question5 { set; get; }
        public string Question6 { set; get; }
        public string Question7 { set; get; }
        public string Question8 { set; get; }
        public string Question9 { set; get; }
        public string Question10 { set; get; }
        public string Question11 { set; get; }
        public string Question12 { set; get; }
        public string Question13 { set; get; }
        public string Goal1 { set; get; }
        public string Goal2{ set; get; }
        public string Goal3{ set; get; }
        public string Goal4{ set; get; }
        public string Goal5{ set; get; }
        public string KPI1 { set; get; }
        public string KPI2 { set; get; }
        public string KPI3 { set; get; }
        public string KPI4 { set; get; }
        public string KPI5 { set; get; }
        public string PerformanceTerm1 { set; get; }
        public string PerformanceTerm2 { set; get; }
        public string PerformanceTerm3 { set; get; }
        public string PerformanceTerm4 { set; get; }
        public string PerformanceTerm5 { set; get; }
        public string Score1 { set; get; }
        public string Score2 { set; get; }
        public string Score3 { set; get; }
        public string Score4 { set; get; }
        public string Score5 { set; get; }
        public string FinalEvaluationQualification { set; get; }
        public string DateEvaluatiorInterview { set; get; }
        public string DateSettingIindividualGoals { set; get; }
        public string Need1ProfessionalDevelopmentEvaluatedEmployee { set; get; }
        public string Need2ProfessionalDevelopmentEvaluatedEmployee { set; get; }
        public string EvaluatorComments { set; get; }
    }
}