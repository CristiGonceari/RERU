using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class TestType : SoftDeleteBaseEntity
    {
        public TestType()
        {
            Tests = new HashSet<Test>();
            TestTypeQuestionCategories = new HashSet<TestTypeQuestionCategory>();
            EventTestTypes = new HashSet<EventTestType>();
        }

        public string Name { get; set; }
        public string Rules { get; set; }
        public int QuestionCount { get; set; }
        public int MinPercent { get; set; }
        public int Duration { get; set; }
        public TestTypeStatusEnum Status { get; set; }
        public TestTypeModeEnum Mode { get; set; }
        public SequenceEnum CategoriesSequence { get; set; }
        public TestTypeSettings Settings { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<TestTypeQuestionCategory> TestTypeQuestionCategories { get; set; }
        public virtual ICollection<EventTestType> EventTestTypes { get; set; }
    }
}
