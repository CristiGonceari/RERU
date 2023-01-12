using System;
using System.ComponentModel.DataAnnotations.Schema;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace RERU.Data.Entities.Evaluation360
{
    public class Evaluation : SoftDeleteBaseEntity
    {
        public int EvaluatedUserProfileId { set; get; }
        public int EvaluatorUserProfileId { set; get; }
        public int? CounterSignerUserProfileId { set; get; }

        [ForeignKey(nameof(EvaluatedUserProfileId))]
        public UserProfile EvaluatedUserProfile { set; get; }

        [ForeignKey(nameof(EvaluatorUserProfileId))]
        public UserProfile EvaluatorUserProfile { set; get; }

        [ForeignKey(nameof(CounterSignerUserProfileId))]
        public UserProfile CounterSignerUserProfile { set; get; }

        public EvaluationTypeEnum? Type { set; get; }
        public decimal? Points { set; get; }
        public EvaluationStatusEnum? Status {set;get;}
        
        public string? SubdivisionName { set; get; }
        public DateTime? DateCompletionGeneralData { set; get; }
        public string? NameSurnameEvaluated { set; get; }
        public string? FunctionSubdivision { set; get; }
        public string? SubdivisionEvaluated { set; get; }
        public GradeEnum? SpecialOrMilitaryGrade { set; get; }
        public String? SpecialOrMilitaryGradeText { set; get; }
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
        public string? FinalScorePartialEvaluations { set; get; }
        public QualifierEnum? QualifierPartialEvaluations { set; get; }
        public string? SanctionAppliedEvaluationCourse { set; get; }
        public DateTime? DateSanctionApplication { set; get; }
        public DateTime? DateLiftingSanction { set; get; }
        public QualifierEnum? QualificationEvaluationObtained2YearsPast { set; get; }
        public QualifierEnum? QualificationEvaluationObtainedPreviousYear { set; get; }
        public QualifierEnum? QualificationQuarter1 { set; get; }
        public QualifierEnum? QualificationQuarter2 { set; get; }
        public QualifierEnum? QualificationQuarter3 { set; get; }
        public QualifierEnum? QualificationQuarter4 { set; get; }
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
        public QualifierEnum? FinalEvaluationQualification { set; get; }
        public DateTime? DateEvaluationInterview { set; get; }
        public DateTime? DateSettingIindividualGoals { set; get; }
        public string? Need1ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need2ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need3ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need4ProfessionalDevelopmentEvaluated { set; get; }
        public string? Need5ProfessionalDevelopmentEvaluated { set; get; }
        public string? CommentsEvaluator { set; get; }
        public string? CommentsEvaluated { set; get; }
        public DateTime? DateAcceptOrRejectEvaluated { set; get; }
        public bool? SignatureEvaluated { set; get; }
        public string? NameSurnameEvaluator { set; get; }
        public string? FunctionEvaluator { set; get; }
        public bool? SignatureEvaluator { set; get; }
        public bool? CheckComment1 { set; get; }
        public bool? CheckComment2 { set; get; }
        public bool? CheckComment3 { set; get; }
        public bool? CheckComment4 { set; get; }
        public string? OtherComments { set; get; }
        public string? NameSurnameCounterSigner { set; get; }
        public string? FunctionCounterSigner { set; get; }
        public DateTime? DateCompletionCounterSigner { set; get; }
        public bool? SignatureCounterSigner { set; get; }
        public DateTime? DateEvaluatedKnow { set; get; }
        public bool? SignatureAcknowledgeEvaluated { set; get; }
    }
}