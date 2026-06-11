using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Incident_Library.MODELS__Data_
{
    public class IncidentReport
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HowDiscovered { get; set; }
        public string WhatIsIncident { get; set; }
        public string HowResolved { get; set; }
        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }



}
