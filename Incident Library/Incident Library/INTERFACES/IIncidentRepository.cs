using Incident_Library.MODELS__Data_;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Incident_Library.INTERFACES
{
    public interface IIncidentRepository
    {
        // Henter alle incidents fra databasen
        Task<List<IncidentReport>> ReadAsync();

        // Opretter et nyt incident i databasen
        Task CreateAsync(IncidentReport i);

        // Opdaterer et eksisterende incident i databasen
        Task UpdateAsync(IncidentReport i);

        // Sletter et incident fra databasen
        Task DeleteAsync(IncidentReport i);
    }
}
