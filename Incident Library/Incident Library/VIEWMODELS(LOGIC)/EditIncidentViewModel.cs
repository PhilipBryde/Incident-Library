using Incident_Library.INTERFACES;
using Incident_Library.MODELS__Data_;
using Incident_Library.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.VIEWMODELS_LOGIC_
{
    public class EditIncidentViewModel
    {
        private readonly IIncidentRepository _repo = new IncidentRepository();

        public IncidentReport Incident { get; set; }

        public EditIncidentViewModel(IncidentReport i)
        {
            Incident = i;
        }

        public async Task SaveAsync() //asynkron metode der gemmer incident; sendes videre til Repository
        {
            if (Incident.Id == 0)
            {
                Incident.CreatedDate = DateTime.Now;
                await _repo.CreateAsync(Incident);
            }
            else
            {
                await _repo.UpdateAsync(Incident);
            }
        }

        public async Task DeleteAsync() //Asynkron metode der sletter incident; sendes videre til Repository
        {
            await _repo.DeleteAsync(Incident);
        }
    }
}
