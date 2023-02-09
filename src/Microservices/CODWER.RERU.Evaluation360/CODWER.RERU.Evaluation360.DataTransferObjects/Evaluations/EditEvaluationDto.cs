using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;

    public class EditEvaluationDto
    {
        public string? SubdivisionName { set; get; }
        public string EvaluatedName { set; get; }
        public string? FunctionSubdivision { set; get; }
        public string? SubdivisionEvaluated { set; get; }
        public GradesEnum? SpecialOrMilitaryGrade { set; get; }
        public DateTime? PeriodEvaluatedFromTo { set; get; }
        public DateTime? PeriodEvaluatedUpTo { set; get; }
        public EducationEnum? EducationEnum { set; get; }
        public ProfessionalTrainingActivitiesEnum? ProfessionalTrainingActivities { set; get; }
        public ProfessionalTrainingActivitiesType? ProfessionalTrainingActivitiesType { set; get; }
        public string? CourseName { set; get; }
        public DateTime? PeriodRunningActivityFromTo { set; get; }
        public DateTime? PeriodRunningActivityUpTo { set; get; }
        public string? AdministrativeActOfStudies { set; get; }
        public ServiceDuringEvaluationCourse? ServiceDuringEvaluationCourse { set; get; }
        public string? FunctionEvaluated { set; get; }
        public DateTime? AppointmentDate { set; get; }
        public string? AdministrativeActService { set; get; }
        public DateTime? PartialEvaluationPeriodFromTo { set; get; }
        public DateTime? PartialEvaluationPeriodUpTo { set; get; }
        public decimal? PartialEvaluationScore { set; get; }
        public QualifiersEnum? QualifierPartialEvaluations { set; get; }
        public SanctionEnum? SanctionApplied { set; get; }
        public DateTime? DateSanctionApplication { set; get; }
        public DateTime? DateLiftingSanction { set; get; }
        public QualifiersEnum? QualificationEvaluationObtained2YearsPast { set; get; }
        public QualifiersEnum? QualificationEvaluationObtainedPreviousYear { set; get; }
        public QualifiersEnum? QualificationQuarter1 { set; get; }
        public QualifiersEnum? QualificationQuarter2 { set; get; }
        public QualifiersEnum? QualificationQuarter3 { set; get; }
        public QualifiersEnum? QualificationQuarter4 { set; get; }
        public decimal? Question1 { set; get; }
        public decimal? Question2 { set; get; }
        public decimal? Question3 { set; get; }
        public decimal? Question4 { set; get; }
        public decimal? Question5 { set; get; }
        public decimal? Question6 { set; get; }
        public decimal? Question7 { set; get; }
        public decimal? Question8 { set; get; }
        public string? Goal1 { set; get; }
        public string? Goal2 { set; get; }
        public string? Goal3 { set; get; }
        public string? Goal4 { set; get; }
        public string? Goal5 { set; get; }
        public string? KPI1 { set; get; }
        public string? KPI2 { set; get; }
        public string? KPI3 { set; get; }
        public string? KPI4 { set; get; }
        public string? KPI5 { set; get; }
        public string? PerformanceTerm1 { set; get; }
        public string? PerformanceTerm2 { set; get; }
        public string? PerformanceTerm3 { set; get; }
        public string? PerformanceTerm4 { set; get; }
        public string? PerformanceTerm5 { set; get; }
        public decimal? Score1 { set; get; }
        public decimal? Score2 { set; get; }
        public decimal? Score3 { set; get; }
        public decimal? Score4 { set; get; }
        public decimal? Score5 { set; get; }
        public decimal? Question9 { set; get; }
        public decimal? Question10 { set; get; }
        public decimal? Question11 { set; get; }
        public decimal? Question12 { set; get; }
        public decimal? Question13 { set; get; }
        public QualifiersEnum? FinalEvaluationQualification { set; get; }
        public DateTime? DateEvaluationInterview { set; get; }
        public DateTime? DateSettingIindividualGoals { set; get; }
        public string? Need1ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need2ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need3ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need4ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need5ProfessionalDevelopmentEvaluated { set; get; }
        public string? CommentsEvaluator { set; get; }
    }