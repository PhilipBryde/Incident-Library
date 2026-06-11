using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Incident_Library.INTERFACES;
using Incident_Library.MODELS__Data_;
using Incident_Library.Repository;

namespace Incident_Library.VIEWMODELS_LOGIC_
{
    public class AdminViewModel
    {
        private readonly IUserRepository _userRepo = new UserRepository();

        // Henter alle brugere fra databasen - bruges til at vise listen i Admin Panel
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }
    }
}
