﻿using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class Location : SoftDeleteBaseEntity
    {
        public Location()
        {
            LocationResponsiblePersons = new HashSet<LocationResponsiblePerson>();
            LocationClients = new HashSet<LocationClient>();
            EventLocations = new HashSet<EventLocation>();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public TestingLocationType Type { get; set; }
        public int Places { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LocationResponsiblePerson> LocationResponsiblePersons { get; set; }
        public virtual ICollection<LocationClient> LocationClients { get; set; }
        public virtual ICollection<EventLocation> EventLocations { get; set; }
    }
}