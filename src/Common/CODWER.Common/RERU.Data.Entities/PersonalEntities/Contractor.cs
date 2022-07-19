using System;
using System.Collections.Generic;
using System.Linq;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Entities.PersonalEntities.Files;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;
using RERU.Data.Entities.PersonalEntities.TimeSheetTables;

namespace RERU.Data.Entities.PersonalEntities
{
    public class Contractor : SoftDeleteBaseEntity
    {
        public Contractor()
        {
            ContractorDepartments = new HashSet<ContractorDepartment>();

            Attestations = new HashSet<Attestation>();
            Badges = new HashSet<Badge>();
            Bonuses = new HashSet<Bonus>();
            Penalizations = new HashSet<Penalization>();
            Positions = new HashSet<Position>();
            Ranks = new HashSet<Rank>();
            Vacations = new HashSet<Vacation>();

            //ByteArrayFiles = new HashSet<ByteArrayFile>();

            //Studies = new HashSet<Studies.Study>();

            Instructions = new HashSet<Instruction>();
            Contracts = new HashSet<IndividualContract>();

            TimeSheetTables = new HashSet<TimeSheetTable>();
        }

        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime BirthDate { get; set; }
        public SexTypeEnum Sex { get; set; }
        public ContractorAvatar Avatar { get; set; }

        public int BloodTypeId { get; set; }                // nomenclature 
        public NomenclatureRecord BloodType { get; set; }   // nomenclature 

        public int? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        //public Bulletin Bulletin { get; set; }
        public ContractorLocalPermission Permission { get; set; }

        public virtual ICollection<ContractorDepartment> ContractorDepartments { get; set; }
        public virtual ICollection<TimeSheetTable> TimeSheetTables { get; set; }



        public virtual ICollection<Attestation> Attestations { get; set; }
        public virtual ICollection<Badge> Badges { get; set; }
        public virtual ICollection<Bonus> Bonuses { get; set; }
        public virtual ICollection<Penalization> Penalizations { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Rank> Ranks { get; set; }
        public virtual ICollection<Vacation> Vacations { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }

        //public virtual ICollection<ByteArrayFile> ByteArrayFiles { get; set; }

        //public virtual ICollection<Studies.Study> Studies { get; set; }

        public virtual ICollection<Instruction> Instructions { get; set; }

        public virtual ICollection<IndividualContract> Contracts { get; set; }
        public virtual ICollection<IndividualContract> Contractors { get; set; }

        public virtual ICollection<ContractorFile> ContractorFiles { get; set; }

        public Position GetLastPosition() => Positions.OrderByDescending(x => x.FromDate).FirstOrDefault();

        public Position GetCurrentPositionOnData(DateTime data) => Positions.FirstOrDefault(p =>
            (p.FromDate == null && p.ToDate == null)
            || (p.ToDate == null && p.FromDate != null && p.FromDate < data)
            || (p.FromDate == null && p.ToDate != null && p.ToDate > data)
            || (p.FromDate != null && p.ToDate != null && p.FromDate < data && p.ToDate > data));
    }

}
