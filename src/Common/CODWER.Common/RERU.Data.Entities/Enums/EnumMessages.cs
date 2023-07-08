using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.StorageService.Entities;
using RERU.Data.Entities.PersonalEntities.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace RERU.Data.Entities.Enums
{
    public static class EnumMessages
    {
        public const string question = "Întrebări";
        public const string testtemplate = "Șabloane de test";
        public const string test = "Test";

        public const string Random = "Întâmplător";
        public const string Strict = "Ordonat";
        public const string Programmed = "Programat";
        public const string AllowedToStart = "Permis să înceapă";
        public const string InProgress = "În progres";
        public const string Terminated = "Finisat";
        public const string Verified = "Verificat";
        public const string Closed = "Închis";
        public const string FreeText = "Formă liberă";
        public const string MultipleAnswers = "Răspunsuri multiple";
        public const string OneAnswer = "Un răspuns";
        public const string HashedAnswer = "Completează textul";
        public const string FileAnswer = "Încărcare fișier";
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

        public const string Recommended = "Se recomandă/Nu se recomandă";
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

        public const string Dynamic = "Dinamic";
        public const string Static = "Static";

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

        public const string General_colonel = "General colonel";
        public const string General_locotenent = "General locotenent";
        public const string General_maior = "General maior";
        public const string Locotenent_colonel = "Locotenent colonel";
        public const string Capitan = "Căpitan";
        public const string Locotenent_major = "Locotenent major";
        public const string Locotenent_sublocotenent = "Locotenent, sublocotenent"; 
        public const string Plutonier_adjutant = "Plutonier adjutant";
        public const string Plutonier_major = "Plutonier major";
        public const string Sergent_major = "Sergent major";
        public const string Sergent_inferior_caporal_soldat  = "Sergent-inferior, caporal, soldat";
        public const string Agent_superior = "Agent superior";
        public const string Agent_principal = "Agent principal";
        public const string Agent_sef_adjunct = "Agent-șef adjunct";
        public const string Agent_sef = "Agent-șef";
        public const string Agent_sef_principal = "Agent-șef principal";
        public const string Inspector_superior = "Inspector superior";
        public const string Inspector_principal = "Inspector principal";
        public const string Comisar_principal = "Comisar principal";
        public const string Comisar_sef = "Comisar-șef";
        public const string Chestor_sef = "Chestor-șef";
        public const string Chestor_general = "Chestor general";
        public const string Sergent_inferior_al_serviciului_intern  = "Sergent-inferior al serviciului intern";
        public const string Sergent_al_serviciului_intern = "Sergent al serviciului intern";
        public const string Sergent_major_al_serviciului_intern = "Sergent-major al serviciului intern"; 
        public const string Plutonier_al_serviciului_intern = "Plutonier al serviciului intern";
        public const string Plutonier_major_al_serviciului_intern = "Plutonier-major al serviciului intern";
        public const string Plutonier_adjutant_al_serviciului_intern = "Plutonier adjutant al serviciului intern";
        public const string Locotenent_al_serviciului_intern = "Locotenent al serviciului intern"; 
        public const string Locotenent_major_al_serviciului_intern = "Locotenent-major al serviciului intern";
        public const string Capitan_al_serviciului_intern = "Căpitan al serviciului intern"; 
        public const string Maior_al_serviciului_intern = "Maior al serviciului intern";
        public const string Locotenent_colonel_al_serviciului_intern = "Locotenent-colone al serviciului intern";
        public const string Colonel_al_serviciului_intern = "Colonel al serviciului intern";
        public const string General_maior_al_serviciului_intern = "General-maior al serviciului intern";
        public const string General_locotenent_al_serviciului_intern = "General-locotenent al serviciului intern";
        public const string General_colonel_al_serviciului_intern = "General-colonel al serviciului intern";

        public const string Medium = "Mediu";
        public const string Basic = "De bază";

        private const string Female = "Femeie";
        private const string Male = "Bărbat";

        public const string ZeroPercent = "Răspuns greșit = 0%";
        public const string OneDivideCountPercent = "Răspuns greșit = 1/n% (n-nr total de opțiuni)";
        public const string MinusCorrectOption = "Răspuns greșit = (-1) * răspuns corect%";

        public const string PregatireGenerala = "Pregătire generală";
        public const string PregatireDeSpecialitate = "Pregatire de specialitate";
        public const string InstructiaTragerii = "Instrucțiunea tragerii";
        public const string InterventiaProfesionala = "Intervenția profesională";

        public const string Daily = "Frecvență la zi";
        public const string LowFrequency = "Frecvență redusă";
        public const string Remote = "La distanță";

        public const string Initial = "Inițiale";
        public const string Retraining = "Recalificare";

        public const string Recruit = "Recrut";
        public const string Disobedient = "Nesupus/ă";
        public const string PerformedMilitaryService = "Serviciul militar îndeplinit";
        public const string AlternativeService = "Serviciu de alternativă";
        public const string MilitaryChair = "Catedra militară";

        public const string Father = "Tată";
        public const string Mother = "Mama";
        public const string Brother = "Frate";
        public const string Sister = "Soră";
        public const string Wife = "Soție";
        public const string Husband = "Soțul";
        public const string Children = "Copii";
        public const string Parent = "Părinte";
        public const string HusbandsBrothers = "Frații soțului";
        public const string HusbandsSisters = "Surorile soțului";
        public const string WifesSisters = "Surorile soției";
        public const string WifesBrothers = "Frații soției";

        public const string Real = "Real";
        public const string Humanist = "Umanist";
        public const string Arts = "Arte";
        public const string Sport = "Sportiv";
        public const string Etc = "Etc";

        public static string Translate(FileTypeEnum fileType)
        {
            return fileType switch
            {
                FileTypeEnum.question => question,
                FileTypeEnum.testtemplate => testtemplate,
                FileTypeEnum.test => test,
                _ => "-"
            };
        }

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
                QuestionTypeEnum.FileAnswer => FileAnswer,
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

        public static string Translate(QualifyingTypeEnum? qualifyingType)
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
                TestingLocationType.Class => Class,
                TestingLocationType.ComputerClass => ComputerClass,
                _ => "-"
            };
        }

        public static string Translate(ModuleTypeEnum moduleTypeEnum)
        {
            return moduleTypeEnum switch
            {
                ModuleTypeEnum.Dynamic => Dynamic,
                ModuleTypeEnum.Default => Static,
                _ => "—"
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
                _ => "—"
            };
        }

        public static string Translate(ProfessionalTrainingActivitiesEnum? professionalTrainingActivities)
        {
            return professionalTrainingActivities switch
            {
                ProfessionalTrainingActivitiesEnum.Perfectiosation => Perfectiosation,
                ProfessionalTrainingActivitiesEnum.Specialization => Specialization,
                _ => "—"
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
                _ => "—"
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
                _ => "—"
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
                _ => "—"
            };
        }

        public static string Translate(GradesEnum? gradesEnum)
        {
            return gradesEnum switch
            {
                GradesEnum.General_colonel => General_colonel,
                GradesEnum.General_locotenent => General_locotenent,
                GradesEnum.General_maior => General_maior,
                GradesEnum.Locotenent_colonel => Locotenent_colonel,
                GradesEnum.Capitan => Capitan,
                GradesEnum.Locotenent_major => Locotenent_major,
                GradesEnum.Locotenent_sublocotenent => Locotenent_sublocotenent,
                GradesEnum.Plutonier_adjutant => Plutonier_adjutant,
                GradesEnum.Plutonier_major => Plutonier_major,
                GradesEnum.Sergent_major => Sergent_major,
                GradesEnum.Sergent_inferior_caporal_soldat  => Sergent_inferior_caporal_soldat,
                GradesEnum.Agent_superior => Agent_superior,
                GradesEnum.Agent_principal => Agent_principal,
                GradesEnum.Agent_sef_adjunct => Agent_sef_adjunct,
                GradesEnum.Agent_sef => Agent_sef,
                GradesEnum.Agent_sef_principal => Agent_sef_principal,
                GradesEnum.Inspector_superior => Inspector_superior,
                GradesEnum.Inspector_principal => Inspector_principal,
                GradesEnum.Comisar_principal => Comisar_principal,
                GradesEnum.Comisar_sef => Comisar_sef,
                GradesEnum.Chestor_sef => Chestor_sef,
                GradesEnum.Chestor_general => Chestor_general,
                GradesEnum.Sergent_inferior_al_serviciului_intern  => Sergent_inferior_al_serviciului_intern,
                GradesEnum.Sergent_al_serviciului_intern => Sergent_al_serviciului_intern,
                GradesEnum.Sergent_major_al_serviciului_intern => Sergent_major_al_serviciului_intern,
                GradesEnum.Plutonier_al_serviciului_intern => Plutonier_al_serviciului_intern,
                GradesEnum.Plutonier_major_al_serviciului_intern => Plutonier_major_al_serviciului_intern,
                GradesEnum.Plutonier_adjutant_al_serviciului_intern => Plutonier_adjutant_al_serviciului_intern,
                GradesEnum.Locotenent_al_serviciului_intern => Locotenent_al_serviciului_intern,
                GradesEnum.Locotenent_major_al_serviciului_intern => Locotenent_major_al_serviciului_intern,
                GradesEnum.Capitan_al_serviciului_intern => Capitan_al_serviciului_intern,
                GradesEnum.Maior_al_serviciului_intern => Maior_al_serviciului_intern,
                GradesEnum.Locotenent_colonel_al_serviciului_intern => Locotenent_colonel_al_serviciului_intern,
                GradesEnum.Colonel_al_serviciului_intern => Colonel_al_serviciului_intern,
                GradesEnum.General_maior_al_serviciului_intern => General_maior_al_serviciului_intern,
                GradesEnum.General_locotenent_al_serviciului_intern => General_locotenent_al_serviciului_intern,
                GradesEnum.General_colonel_al_serviciului_intern => General_colonel_al_serviciului_intern,
                _ => "—"
            };
        }

        public static string Translate(StateLanguageLevel? stateLanguageLevel)
        {
            return stateLanguageLevel switch
            {
                StateLanguageLevel.Basic => Basic,
                StateLanguageLevel.Medium => Medium,
                StateLanguageLevel.Good => Good,
                StateLanguageLevel.VeryGood => VeryGood,
                _ => "—"
            };
        }

        public static string Translate(KnowledgeQuelifiersEnum knowledgeQuelifiers)
        {
            return knowledgeQuelifiers switch
            {
                KnowledgeQuelifiersEnum.BasicKnowledge => Basic,
                KnowledgeQuelifiersEnum.Good => Good,
                KnowledgeQuelifiersEnum.VeryGood => VeryGood,
                _ => "—"
            };
        }


        public static string Translate(SexTypeEnum? sexTypeEnum)
        {
            return sexTypeEnum switch
            {
                SexTypeEnum.Male => Male,
                SexTypeEnum.Female => Female,
                _ => "—"
            };
        }

        public static string Translate(RoleTypeEnum moduleTypeEnum)
        {
            return moduleTypeEnum switch
            {
                RoleTypeEnum.Dynamic => Dynamic,
                RoleTypeEnum.Default => Static,
                _ => "-"
            };
        }

        public static string Translate(ScoreFormulaEnum? scoreFormula)
        {
            return scoreFormula switch
            {
                ScoreFormulaEnum.ZeroPercent => ZeroPercent,
                ScoreFormulaEnum.OneDivideCountPercent => OneDivideCountPercent,
                ScoreFormulaEnum.MinusCorrectOption => MinusCorrectOption,
                _ => "-"
            };
        }

        public static string Translate(BasicTestTemplateEnum? basicTestTemplate)
        {
            return basicTestTemplate switch
            {
                BasicTestTemplateEnum.PregatireGenerala => PregatireGenerala,
                BasicTestTemplateEnum.PregatireDeSpecialitate => PregatireDeSpecialitate,
                BasicTestTemplateEnum.InstructiaTragerii => InstructiaTragerii,
                BasicTestTemplateEnum.InterventiaProfesionala => InterventiaProfesionala,
                _ => "-"
            };
        }

        public static string Translate(StudyFrequencyEnum? studyFrequency)
        {
            return studyFrequency switch
            {
                StudyFrequencyEnum.Daily => Daily,
                StudyFrequencyEnum.LowFrequency => LowFrequency,
                StudyFrequencyEnum.Remote => Remote,
                _ => "-"
            };
        }

        public static string Translate(StudyCourseType? studyCourseType)
        {
            return studyCourseType switch
            {
                StudyCourseType.Initial => Initial,
                StudyCourseType.Perfecting => Perfectiosation,
                StudyCourseType.Retraining => Retraining,
                StudyCourseType.Specialization => Specialization,
                _ => "-"
            };
        }

        public static string Translate(MilitaryObligationTypeEnum? militaryObligationType)
        {
            return militaryObligationType switch
            {
                MilitaryObligationTypeEnum.AlternativeService => AlternativeService,
                MilitaryObligationTypeEnum.Disobedient => Disobedient,
                MilitaryObligationTypeEnum.MilitaryChair => MilitaryChair,
                MilitaryObligationTypeEnum.PerformedMilitaryService => PerformedMilitaryService,
                MilitaryObligationTypeEnum.Recruit => Recruit,
                _ => "-"
            };
        }

        public static string Translate(KinshipDegreeEnum kinshipDegree)
        {
            return kinshipDegree switch
            {
                KinshipDegreeEnum.Brother => Brother,
                KinshipDegreeEnum.Children => Children,
                KinshipDegreeEnum.Father => Father,
                KinshipDegreeEnum.Husband => Husband,
                KinshipDegreeEnum.HusbandsBrothers => HusbandsBrothers,
                KinshipDegreeEnum.HusbandsSisters => HusbandsSisters,
                KinshipDegreeEnum.Mother => Mother,
                KinshipDegreeEnum.Parent => Parent,
                KinshipDegreeEnum.Sister => Sister,
                KinshipDegreeEnum.Wife => Wife,
                KinshipDegreeEnum.WifesBrothers => WifesBrothers,
                KinshipDegreeEnum.WifesSisters => WifesSisters,
                _ => "-"
            };
        }

        public static string Translate(StudyProfileEnum? studyProfile)
        {
            return studyProfile switch
            {
                StudyProfileEnum.Arts => Arts,
                StudyProfileEnum.Etc => Etc,
                StudyProfileEnum.Humanist => Humanist,
                StudyProfileEnum.Real => Real,
                StudyProfileEnum.Sport => Sport,
                _ => "-"
            };
        }
    }
}
