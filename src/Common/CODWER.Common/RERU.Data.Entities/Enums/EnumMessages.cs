using CVU.ERP.Common.DataTransferObjects.Users;

namespace RERU.Data.Entities.Enums
{
    public static class EnumMessages
    {
        public const string Random = "Întâmplător";
        public const string Strict = "Ordonat";
        public const string Programmed = "Programat";
        public const string AllowedToStart = "Permis să înceapă";
        public const string InProgress = "În proces";
        public const string Terminated = "Terminat";
        public const string Verified = "Verificat";
        public const string Closed = "Închis";
        public const string FreeText = "Formă liberă";
        public const string MultipleAnswers = "Răspunsuri multiple";
        public const string OneAnswer = "Un răspuns";
        public const string HashedAnswer = "Completează textul";
        public const string Draft = "Maculator";
        public const string Active = "Activ";
        public const string Inactive = "Inactiv";
        public const string Canceled = "Anulat";
        public const string NoResult = "Fără rezultat";
        public const string Passed = "Susținut";
        public const string NotPassed = "Nesusținut";

        public const string Employee = "Angajat";
        public const string Candidate = "Candidat";

        public const string Able = "Apt";
        public const string NotAble = "Inapt";
        public const string Accepted = "Admis";
        public const string Rejected = "Respins";

        public const string Recommended = "Se recomandă/Nu se recomanda";
        public const string PassedNotPassed = "Susținut/Nesusținut";
        public const string AbleNotAble = "Apt/Inapt";
        public const string AcceptedRejected = "Admis/Respins";
        public const string NoQualifying = "Nici o calificare";

        public const string Test = "Test";
        public const string Poll = "Sondaj";
        public const string Evaluation = "Evaluare";

        public const string Column1 = "Coloana 1";
        public const string Column2 = "Coloana 2";
        public const string Column3 = "Coloana 3";
        public const string Column4 = "Coloana 4";

        public const string All = "Toți";
        public const string AllDepartments = "Toate departamentele";
        public const string CurrentDepartment = "Departamentul curent";
        public const string OnlyCandidates = "Doar candidați";

        public const string New = "Nou";
        public const string Refused = "Refuzat";
        public const string Approved = "Aprobat";
        public const string Wait = "În aşteptare";

        public const string Class = "Clasă";
        public const string ComputerClass = "Clasă cu calculatoare";

        public const string Secondary = "Medii";
        public const string Higher = "Superioare";
        public const string Master = "Masterat";
        public const string Doctorate = "Doctorat";

        public const string Perfectiosation = "Perfecționare";
        public const string Specialization = "Specializare";

        public const string Employment = "Angajare";
        public const string Transfer = "Transferare";
        public const string Detachment = "Detașare";
        public const string Default = "Neschimbat";

        public const string Dissatisfied = "Nesatisfăcător";
        public const string Satisfied = "Satisfăcător";
        public const string Good = "Bine";
        public const string VeryGood = "Foarte bine";

        public const string Without = "Nu sunt";
        public const string Warning = "Avertisment";
        public const string Reprimand = "Mustrare";
        public const string HarshRebuke = "Mustrare aspră";
        public const string Downgrade = "Retrogradare cu un grad special";
        public const string Demotion = "Retrogradare în funcţie";
        public const string Dismissal = "Concediere din funcţia publică cu statut special";


        public static string Translate(TestStatusEnum testStatus)
        {
            return testStatus switch
            {
                TestStatusEnum.Programmed => Programmed,
                TestStatusEnum.AlowedToStart => AllowedToStart,
                TestStatusEnum.InProgress => InProgress,
                TestStatusEnum.Terminated => Terminated,
                TestStatusEnum.Verified => Verified,
                TestStatusEnum.Closed => Closed,
                _ => "-"
            };
        }

        public static string Translate(QuestionTypeEnum testStatus)
        {
            return testStatus switch
            {
                QuestionTypeEnum.FreeText => FreeText,
                QuestionTypeEnum.OneAnswer => OneAnswer,
                QuestionTypeEnum.MultipleAnswers => MultipleAnswers,
                QuestionTypeEnum.HashedAnswer => HashedAnswer,
                _ => "-"
            };
        }

        public static string Translate(SequenceEnum testStatus)
        {
            return testStatus switch
            {
                SequenceEnum.Random => Random,
                SequenceEnum.Strict => Strict,
                _ => "-"
            };
        }

        public static string Translate(QuestionUnitStatusEnum questionStatus)
        {
            return questionStatus switch
            {
                QuestionUnitStatusEnum.Draft => Draft,
                QuestionUnitStatusEnum.Active => Active,
                QuestionUnitStatusEnum.Inactive => Inactive,
                _ => "-"
            };
        }

        public static string Translate(TestResultStatusEnum testResultStatus)
        {
            return testResultStatus switch
            {
                TestResultStatusEnum.NoResult => NoResult,
                TestResultStatusEnum.Passed => Passed,
                TestResultStatusEnum.NotPassed => NotPassed,
                TestResultStatusEnum.Able => Able,
                TestResultStatusEnum.NotAble => NotAble,
                TestResultStatusEnum.Accepted => Accepted,
                TestResultStatusEnum.Rejected => Rejected,
                TestResultStatusEnum.Recommended => Recommended,
                _ => "-"
            };
        }

        public static string Translate(TestTemplateModeEnum testTypeMode)
        {
            return testTypeMode switch
            {
                TestTemplateModeEnum.Test => Test,
                TestTemplateModeEnum.Poll => Poll,
                TestTemplateModeEnum.Evaluation => Evaluation,
                _ => "-"
            };
        }

        public static string Translate(MedicalColumnEnum column)
        {
            return column switch
            {
                MedicalColumnEnum.Column1 => Column1,
                MedicalColumnEnum.Column2 => Column2,
                MedicalColumnEnum.Column3 => Column3,
                MedicalColumnEnum.Column4 => Column4,
                _ => "-"
            };
        }

        public static string Translate(TestTemplateStatusEnum testTemplateEnum)
        {
            return testTemplateEnum switch
            {
                TestTemplateStatusEnum.Draft => Draft,
                TestTemplateStatusEnum.Active => Active,
                TestTemplateStatusEnum.Canceled => Canceled,
                _ => "-"
            };
        }

        public static string Translate(QualifyingTypeEnum qualifyingType)
        {
            return qualifyingType switch
            {
                QualifyingTypeEnum.PassedNotPassed => PassedNotPassed,
                QualifyingTypeEnum.AbleNotAble => AbleNotAble,
                QualifyingTypeEnum.AcceptedRejected => AcceptedRejected,
                QualifyingTypeEnum.Recommended => Recommended,
                QualifyingTypeEnum.NoQualifying => NoQualifying,
                _ => "-"
            };
        }

        public static string Translate(UserStatusEnum userStatusEnum)
        {
            return userStatusEnum switch
            {
                UserStatusEnum.Employee => Employee,
                UserStatusEnum.Candidate => Candidate,
                _ => "-"
            };
        }

        public static string Translate(AccessModeEnum accessModeEnum)
        {
            return accessModeEnum switch
            {
                AccessModeEnum.All => All,
                AccessModeEnum.AllDepartments => AllDepartments,
                AccessModeEnum.CurrentDepartment => CurrentDepartment,
                AccessModeEnum.OnlyCandidates => OnlyCandidates,
                _ => "-"
            };
        }

        public static string Translate(SolicitedPositionStatusEnum solicitedTestStatus)
        {
            return solicitedTestStatus switch
            {
                SolicitedPositionStatusEnum.New => New,
                SolicitedPositionStatusEnum.Refused => Refused,
                SolicitedPositionStatusEnum.Approved => Approved,
                SolicitedPositionStatusEnum.Wait => Wait,
                _ => "-"
            };
        }

        public static string Translate(TestingLocationType testingLocationType)
        {
            return testingLocationType switch
            {
                TestingLocationType.Class => New,
                TestingLocationType.ComputerClass => Refused,
                _ => "-"
            };
        }

        public static string Translate(EducationEnum? education)
        {
            return education switch
            {
                 EducationEnum.Doctorate => Doctorate,
                 EducationEnum.Higher => Higher,
                 EducationEnum.Master => Master,
                 EducationEnum.Secondary => Secondary,
                _ => "-"
            };
        }

        public static string Translate(ProfessionalTrainingActivitiesEnum? professionalTrainingActivities)
        {
            return professionalTrainingActivities switch
            {
                 ProfessionalTrainingActivitiesEnum.Perfectiosation => Perfectiosation,
                 ProfessionalTrainingActivitiesEnum.Specialization => Specialization,
                _ => "-"
            };
        }

        public static string Translate(ServiceDuringEvaluationCourse? serviceDuringEvaluationCourse)
        {
            return serviceDuringEvaluationCourse switch
            {
                 ServiceDuringEvaluationCourse.Employment => Employment,
                 ServiceDuringEvaluationCourse.Transfer => Transfer,
                 ServiceDuringEvaluationCourse.Detachment => Detachment,
                 ServiceDuringEvaluationCourse.Default => Default,
                 _ => "-"
            };
        }

        public static string Translate(QualifiersEnum? qualifiersEnum)
        {
            return qualifiersEnum switch
            {
                 QualifiersEnum.Dissatisfied => Dissatisfied,
                 QualifiersEnum.Satisfied => Satisfied,
                 QualifiersEnum.Good => Good,
                 QualifiersEnum.VeryGood => VeryGood,
                 _ => "-"
            };
        }

        public static string Translate(SanctionEnum? sanctionEnum)
        {
            return sanctionEnum switch
            {
                 SanctionEnum.Without => Without,
                 SanctionEnum.Warning => Warning,
                 SanctionEnum.Reprimand => Reprimand,
                 SanctionEnum.HarshRebuke => HarshRebuke,
                 SanctionEnum.Downgrade => Downgrade,
                 SanctionEnum.Demotion => Demotion,
                 SanctionEnum.Dismissal => Dismissal,
                 _ => "-"
            };
        }
    }
}
