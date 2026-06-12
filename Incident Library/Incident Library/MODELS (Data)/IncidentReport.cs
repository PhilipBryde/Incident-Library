using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Incident_Library.MODELS__Data_
{
    /// <summary>
    /// Datamodel der repræsenterer en incident i systemet
    /// Bruges af IncidentRepository til database og ViewModels til sortering og visning
    /// Sidney
    /// </summary>
    public class IncidentReport
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HowDiscovered { get; set; }
        public string WhatIsIncident { get; set; }
        public string HowResolved { get; set; }
        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }
        // Liste af labels tilknyttet dette incident
        public List<Label> Labels { get; set; } = new List<Label>();

    }



}
