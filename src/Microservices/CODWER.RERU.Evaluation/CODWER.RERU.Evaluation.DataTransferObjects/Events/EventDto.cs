﻿using System;

namespace CVU.ERP.Evaluation.DataTransferObjects.Events
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }        
    }
}
