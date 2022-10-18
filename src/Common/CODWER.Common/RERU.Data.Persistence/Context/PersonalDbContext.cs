using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Configurations;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Entities.PersonalEntities.Documents;
using RERU.Data.Entities.PersonalEntities.Files;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;
using RERU.Data.Entities.PersonalEntities.TimeSheetTables;
using Address = RERU.Data.Entities.Address;

namespace RERU.Data.Persistence.Context
{
    public partial class AppDbContext
    {
        #region FisaPersonala
        public virtual DbSet<CandidateNationality> CandidateNationalities { get; set; }
        public virtual DbSet<CandidateCitizenship> CandidateCitizens { get; set; }
        public virtual DbSet<Bulletin> Bulletins { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Study> Studies { get; set; }
        public virtual DbSet<StudyType> StudyTypes { get; set; }
        public virtual DbSet<ModernLanguageLevel> ModernLanguageLevels { get; set; }
        public virtual DbSet<ModernLanguage> ModernLanguages { get; set; }
        public virtual DbSet<RecommendationForStudy> RecommendationForStudies { get; set; }
        public virtual DbSet<MaterialStatus> MaterialStatuses { get; set; }
        public virtual DbSet<MaterialStatusType> MaterialStatusTypes { get; set; }
        public virtual DbSet<KinshipRelationWithUserProfile> KinshipRelationWithUserProfiles { get; set; }
        public virtual DbSet<KinshipRelation> KinshipRelations { get; set; }
        public virtual DbSet<KinshipRelationCriminalData> KinshipRelationCriminalDatas { get; set; }
        public virtual DbSet<MilitaryObligation> MilitaryObligations { get; set; }
        public virtual DbSet<Autobiography> Autobiographies { get; set; }
        #endregion

        #region HR Tables


        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<ContractorAvatar> Avatars { get; set; }
        public virtual DbSet<ContractorLocalPermission> ContractorPermissions { get; set; }

        //public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ContractorDepartment> ContractorDepartments { get; set; }

        public virtual DbSet<Attestation> Attestations { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<Bonus> Bonuses { get; set; }
        public virtual DbSet<Penalization> Penalizations { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<FamilyMember> FamilyMembers { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<DismissalRequest> DismissalRequests { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<TimeSheetTable> TimeSheetTables { get; set; }
        public virtual DbSet<HrDocumentTemplate> HrDocumentTemplates { get; set; }
        public virtual DbSet<HrDocumentTemplateCategory> HrDocumentTemplateCategories { get; set; }
        public virtual DbSet<HrDocumentTemplateKey> HrDocumentTemplateKeys { get; set; }

        public virtual DbSet<OrganizationalChart> OrganizationalCharts { get; set; }
        public virtual DbSet<ContractorFile> ContractorFiles { get; set; }


        #region Organization-role relations

        public virtual DbSet<DepartmentRoleRelation> DepartmentRoleRelations { get; set; }
        public virtual DbSet<ParentDepartmentChildDepartment> ParentDepartmentChildDepartments { get; set; }
        public virtual DbSet<ParentDepartmentChildRole> ParentDepartmentChildRoles { get; set; }
        public virtual DbSet<ParentRoleChildRole> ParentRoleChildRoles { get; set; }
        public virtual DbSet<ParentRoleChildDepartment> ParentRoleChildDepartments { get; set; }

        #endregion

        //public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<DepartmentRoleContent> DepartmentRoleContents { get; set; }


        #region config
        public virtual DbSet<VacationConfiguration> VacationConfigurations { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }

        #endregion

        #region NomenclatureTypes
        public virtual DbSet<NomenclatureType> NomenclatureTypes { get; set; }

        public virtual DbSet<NomenclatureColumn> NomenclatureColumns { get; set; }

        public virtual DbSet<NomenclatureRecord> NomenclatureRecords { get; set; }

        public virtual DbSet<RecordValue> RecordValues { get; set; }

        public virtual DbSet<RecordValueBoolean> RecordValueBooleans { get; set; }
        public virtual DbSet<RecordValueChar> RecordValueChars { get; set; }
        public virtual DbSet<RecordValueDate> RecordValueDates { get; set; }
        public virtual DbSet<RecordValueDateTime> RecordValuesDateTimes { get; set; }
        public virtual DbSet<RecordValueDouble> RecordValueDoubles { get; set; }
        public virtual DbSet<RecordValueEmail> RecordValueEmails { get; set; }
        public virtual DbSet<RecordValueInteger> RecordValueIntegers { get; set; }
        public virtual DbSet<RecordValueText> RecordValueTexts { get; set; }
        #endregion


        //public virtual DbSet<Study> Studies { get; set; }
        //public virtual DbSet<Bulletin> Bulletins { get; set; }

        public virtual DbSet<IndividualContract> Contracts { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticlePersonalModuleRole> ArticlePersonalModuleRoles { get; set; }

        #endregion
    }
}
