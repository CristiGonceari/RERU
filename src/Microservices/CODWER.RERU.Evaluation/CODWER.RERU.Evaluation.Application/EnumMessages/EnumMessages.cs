﻿using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.EnumMessages
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
    }
}