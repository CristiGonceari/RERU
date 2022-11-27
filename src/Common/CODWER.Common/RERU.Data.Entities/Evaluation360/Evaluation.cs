using System.ComponentModel.DataAnnotations.Schema;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities.Evaluation360
{
    public class Evaluation : SoftDeleteBaseEntity
    {
        public int EvaluatedUserProfileId { set; get; }
        public int EvaluatorUserProfileId { set; get; }
        public int CounterSingerUserProfileId { set; get; }

        [ForeignKey(nameof(EvaluatedUserProfileId))]
        public UserProfile EvaluatedUserProfile { set; get; }

        [ForeignKey(nameof(EvaluatorUserProfileId))]
        public UserProfile EvaluatorUserProfile { set; get; }

        [ForeignKey(nameof(CounterSingerUserProfileId))]
        public UserProfile CounterSignerUserProfile { set; get; }
        public EvaluationTypeEnum Type { set; get; }
    
        public decimal Points { set; get; }

        public EvaluationStatusEnum Status {set;get;}


        //  public int Id { set; get; }
        //  public string? SubdivisionName { set; get; }
        //  public string? DateCompletionGeneralData { set; get; }
        //  public string? NameSurnameEvaluatedEmployee { set; get; }
        //  public string? FunctionSubdivision { set; get; }
        //  public string? SpecialOrMilitaryGrade { set; get; }
        //  public string? PeriodEvaluatedFromTo { set; get; }
        //  public string? PeriodEvaluatedUpTo { set; get; }
        //  public string? Education { set; get; }
        //  public string? ProfessionalTrainingActivities { set; get; }
        //  public string? CourseName { set; get; }
        //  public string? PeriodRunningActivity { set; get; }
        //  public string? AdministrativeActOfStudies { set; get; }
        //  public string? ModificationServiceReportDuringEvaluationCourse { set; get; }
        //  public string? Function { set; get; }
        //  public string? AppointmentDate { set; get; }
        //  public string? AdministrativeActService { set; get; }
        //  public string? PartialEvaluationPeriod { set; get; }
        //  public string? FinalScorePartialEvaluations { set; get; }
        //  public string? QualifierPartialEvaluations { set; get; }
        //  public string? SanctionAppliedEvaluationCourse { set; get; }
        //  public string? DateSanctionApplication { set; get; }
        //  public string? DateLiftingSanction { set; get; }
        //  public string? QualificationEvaluationObtained2YearsPast { set; get; }
        //  public string? QualificationEvaluationObtainedPreviousYear { set; get; }
        //  public string? QualificationQuarter1 { set; get; }
        //  public string? QualificationQuarter2 { set; get; }
        //  public string? QualificationQuarter3 { set; get; }
        //  public string? QualificationQuarter4 { set; get; }
        //  public string? Question1 { set; get; }
        //  public string? Question2 { set; get; }
        //  public string? Question3 { set; get; }
        //  public string? Question4 { set; get; }
        //  public string? Question5 { set; get; }
        //  public string? Question6 { set; get; }
        //  public string? Question7 { set; get; }
        //  public string? Question8 { set; get; }
        //  public string? Question9 { set; get; }
        //  public string? Question10 { set; get; }
        //  public string? Question11 { set; get; }
        //  public string? Question12 { set; get; }
        //  public string? Question13 { set; get; }
        //  public string? FinalEvaluationQualification { set; get; }
        //  public string? DateEvaluatiorInterview { set; get; }
        //  public string? DateSettingIindividualGoals { set; get; }
        //  public string? Need1ProfessionalDevelopmentEvaluatedEmployee { set; get; }
        //  public string? Need2ProfessionalDevelopmentEvaluatedEmployee { set; get; }
        //  public string? EvaluatorComments { set; get; }
        //  public string? CommentsEvaluatedEmployee { set; get; }
        //  public string? DateAgreementDisagreementAwardedQualifier { set; get; }
        //  public string? NameSurnameFunctionEvaluator { set; get; }
        //  public string? DateAppreciationLevelMnifestationEvaluatedCriteria { set; get; }
        //  public string? CheckComment1 { set; get; }
        //  public string? CheckComment2 { set; get; }
        //  public string? CheckComment3 { set; get; }
        //  public string? CheckComment4 { set; get; }
        //  public string? OtherComments { set; get; }
        //  public string? DecisionCountersignatory { set; get; }
        //  public string? NameSurnameFunctionCountersignatory { set; get; }
        //  public string? DateCompletionCountersignatory { set; get; }
        //  public string? DateSignatureEvaluatedEmployee { set; get; }
    }
}