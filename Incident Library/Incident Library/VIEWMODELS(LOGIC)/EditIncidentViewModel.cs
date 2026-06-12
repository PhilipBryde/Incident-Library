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
        private readonly LabelRepository _labelRepo = new LabelRepository();

        public IncidentReport Incident { get; set; }

        public EditIncidentViewModel(IncidentReport i)
        {
            Incident = i;
        }

        // Henter labels fra databasen og sætter dem på incident objektet - Rasmus
        public async Task LoadLabelsAsync()
        {
            if (Incident.Id != 0)
                Incident.Labels = await _labelRepo.GetByIncidentIdAsync(Incident.Id);
        }

        public async Task SaveAsync() //asynkron metode der gemmer incident; sendes videre til Repository
        {
            if (Incident.Id == 0)
            {
                Incident.CreatedDate = DateTime.Now; //Rasmus
                await _repo.CreateAsync(Incident);
            }
            else
            {
                await _repo.UpdateAsync(Incident);
            }

            // Gem labels - slet gamle og indsæt nye - Rasmus
            if (Incident.Id != 0)
            {
                await _labelRepo.DeleteByIncidentIdAsync(Incident.Id);
                foreach (var label in Incident.Labels)
                {
                    label.IncidentId = Incident.Id;
                    await _labelRepo.CreateAsync(label);
                }
            }
        }

        public async Task DeleteAsync() //Asynkron metode der sletter incident; sendes videre til Repository
        {
            // Slet labels først, derefter incident
            await _labelRepo.DeleteByIncidentIdAsync(Incident.Id); //Rasmus
            await _repo.DeleteAsync(Incident);
        }
    }
}
