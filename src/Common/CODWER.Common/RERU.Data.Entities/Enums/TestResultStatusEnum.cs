namespace RERU.Data.Entities.Enums
{
    //public enum TestResultStatusEnum
    //{
    //    NoResult = 0,
    //    Passed = 1,
    //    NotPassed = 2
    //}

    public enum TestResultStatusEnum
    {
        NoResult = 0,
        Passed = 1,
        NotPassed = 2,

        Able = 3,
        NotAble = 4,
        Accepted = 5,
        Rejected = 6,
        Recommended = 7,
    }

    public static class TestResultStatusCalculator
    {
        public static TestResultStatusEnum GetPositiveQualify(this QualifyingTypeEnum type)
        {
            return type switch
            {
                QualifyingTypeEnum.PassedNotPassed => TestResultStatusEnum.Passed,
                QualifyingTypeEnum.AbleNotAble => TestResultStatusEnum.Able,
                QualifyingTypeEnum.AcceptedRejected => TestResultStatusEnum.Accepted,
                QualifyingTypeEnum.Recommended => TestResultStatusEnum.Recommended,
                _ => TestResultStatusEnum.NoResult
            };
        }

        public static TestResultStatusEnum GetNegativeQualify(this QualifyingTypeEnum type)
        {
            return type switch
            {
                QualifyingTypeEnum.PassedNotPassed => TestResultStatusEnum.NotPassed,
                QualifyingTypeEnum.AbleNotAble => TestResultStatusEnum.NotAble,
                QualifyingTypeEnum.AcceptedRejected => TestResultStatusEnum.Rejected,
                QualifyingTypeEnum.Recommended => TestResultStatusEnum.Recommended,
                _ => TestResultStatusEnum.NoResult
            };
        }

    }
}
