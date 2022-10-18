﻿using RERU.Data.Entities.Enums;

namespace CVU.ERP.Module.Application.EnumMessages
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
        public const string NoResult = "Fără rezultat";
        public const string Passed = "Susținut";
        public const string NotPassed = "Nesusținut";
        public const string IsRecommended = "Se recomandă";
        public const string NotRecommended = "Nu se recomandă";
        public const string Test = "Test";
        public const string Poll = "Sondaj";
        public const string Evaluation = "Evaluare";
        public const string Column1 = "Coloana 1";
        public const string Column2 = "Coloana 2";
        public const string Column3 = "Coloana 3";
        public const string Column4 = "Coloana 4";

        public static string GetTestStatus(TestStatusEnum testStatus)
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

        public static string GetQuestionType(QuestionTypeEnum testStatus)
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

        public static string GetQuestionSequence(SequenceEnum testStatus)
        {
            return testStatus switch
            {
                SequenceEnum.Random => Random,
                SequenceEnum.Strict => Strict,
                _ => "-"
            };
        }

        public static string GetQuestionStatus(QuestionUnitStatusEnum questionStatus)
        {
            return questionStatus switch
            {
                QuestionUnitStatusEnum.Draft => Draft,
                QuestionUnitStatusEnum.Active => Active,
                QuestionUnitStatusEnum.Inactive => Inactive,
                _ => "-"
            };
        }

        public static string GetTestResultStatus(TestResultStatusEnum testResultStatus)
        {
            return testResultStatus switch
            {
                TestResultStatusEnum.NoResult => NoResult,
                TestResultStatusEnum.Passed => Passed,
                TestResultStatusEnum.NotPassed => NotPassed,
                _ => "-"
            };
        }

        public static string GetEvaluationResultStatus(TestResultStatusEnum testResultStatus)
        {
            return testResultStatus switch
            {
                TestResultStatusEnum.NoResult => NoResult,
                TestResultStatusEnum.Passed => IsRecommended,
                TestResultStatusEnum.NotPassed => NotRecommended,
                _ => "-"
            };
        }

        public static string GetTestTemplateTypeEnum(TestTemplateModeEnum testTypeMode)
        {
            return testTypeMode switch
            {
                TestTemplateModeEnum.Test => Test,
                TestTemplateModeEnum.Poll => Poll,
                TestTemplateModeEnum.Evaluation => Evaluation,
                _ => "-"
            };
        }

        public static string GetMedicalColumnEnum(MedicalColumnEnum column)
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
    }
}
