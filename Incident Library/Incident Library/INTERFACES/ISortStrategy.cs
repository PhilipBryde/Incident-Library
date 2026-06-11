using Incident_Library.MODELS__Data_;
using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.INTERFACES
{// Rasmus
    public interface ISortStrategy
    {
        List<IncidentReport> Sort(List<IncidentReport> incidents);
    }
}
