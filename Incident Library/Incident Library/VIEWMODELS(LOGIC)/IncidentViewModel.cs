using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Incident_Library.Repository;
using Incident_Library.MODELS__Data_;

namespace Incident_Library.VIEWMODELS_LOGIC_
{
   class IncidentViewModel
    {
        private readonly IncidentRepository _repo = new IncidentRepository();

        public async Task<List<IncidentReport>> GetByStatusAsync(int statusId)
        {
            var all = await _repo.ReadAsync();
            return all.Where(i => i.Status == statusId).ToList();
        }

        public async Task SaveIncidentAsync(string title, string howDiscovered, string whatIsIncident, string howResolved, int statusId)
        {
            IncidentReport incident = new IncidentReport
            {
                Title = title,
                HowDiscovered = howDiscovered,
                WhatIsIncident = whatIsIncident,
                HowResolved = howResolved,
                Status = statusId
            };

            await _repo.CreateAsync(incident);
        }

        public async Task<List<IncidentReport>> GetAllAsync()
        {
            return await _repo.ReadAsync();
        }
    }
}
