using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.MODELS__Data_
{
    //Rasmus
    public class Label
    {
        public int LabelId { get; set; }
        public int IncidentId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Label() { }

        public Label(int labelId, int incidentId, string name)
        {
            LabelId = labelId;
            IncidentId = incidentId;
            Name = name;
        }
    }
}
