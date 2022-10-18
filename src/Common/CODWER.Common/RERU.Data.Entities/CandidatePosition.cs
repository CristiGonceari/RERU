﻿using System;
using CVU.ERP.Common.Data.Entities;
using System.Collections.Generic;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class CandidatePosition : SoftDeleteBaseEntity
    {
        public CandidatePosition()
        {
            RequiredDocumentPositions = new HashSet<RequiredDocumentPosition>();
            CandidatePositionNotifications = new HashSet<CandidatePositionNotification>();
            EventUsers = new HashSet<EventUser>();
            EventUserCandidatePositions = new HashSet<EventUserCandidatePosition>();
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public MedicalColumnEnum? MedicalColumn { get; set; }

        public virtual ICollection<EventUserCandidatePosition> EventUserCandidatePositions { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
        public virtual ICollection<RequiredDocumentPosition> RequiredDocumentPositions { get; set; }
        public virtual ICollection<CandidatePositionNotification> CandidatePositionNotifications { get; set; }
    }
}
