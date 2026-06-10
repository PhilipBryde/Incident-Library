using Incident_Library.INTERFACES;
using Incident_Library.MODELS__Data_;
using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.SORTING
{
    public class SortByDateOldest : ISortStrategy
    {
        public List<IncidentReport> Sort(List<IncidentReport> incidents)
        {
            for (int i = 0; i < incidents.Count - 1; i++)
            {
                for (int j = 0; j < incidents.Count - i; j++)
                {
                    if (incidents[j].CreatedDate > incidents[j + 1].CreatedDate)
                    {
                        IncidentReport temp = incidents[j];
                        incidents[j] = incidents[j + 1];
                        incidents[j+1] = temp;
                    }
                }
            }
            return incidents;
        }
    }
}
