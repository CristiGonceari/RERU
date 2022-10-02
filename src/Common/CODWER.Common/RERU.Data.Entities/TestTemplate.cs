using System;
using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class TestTemplate : SoftDeleteBaseEntity
    {
        public TestTemplate()
        {
            Tests = new HashSet<Test>();
            TestTemplateQuestionCategories = new HashSet<TestTemplateQuestionCategory>();
            EventTestTemplates = new HashSet<EventTestTemplate>();
        }

        public string Name { get; set; }
        public string Rules { get; set; }
        public int QuestionCount { get; set; }
        public int MinPercent { get; set; }
        public int Duration { get; set; }
        public TestTemplateSettings Settings { get; set; }

        public TestTemplateStatusEnum Status { get; set; }
        public TestTemplateModeEnum Mode { get; set; }
        public SequenceEnum CategoriesSequence { get; set; }

        public Guid PdfFileId { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<TestTemplateQuestionCategory> TestTemplateQuestionCategories { get; set; }
        public virtual ICollection<EventTestTemplate> EventTestTemplates { get; set; }
    }
}
