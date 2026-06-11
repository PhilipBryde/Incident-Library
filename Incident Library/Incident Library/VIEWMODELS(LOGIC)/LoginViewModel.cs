using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Incident_Library.MODELS__Data_;
using Incident_Library.Repository;

namespace Incident_Library.VIEWMODELS_LOGIC_
{
    public class LoginViewModel
    {
        private readonly UserRepository _repo = new UserRepository();

        // Den bruger der er logget ind - bruges til at vise navn i topbaren
        public User? LoggedInUser { get; private set; }

        // Forsøger at logge ind med det givne brugernavn og kodeord
        // Returnerer true hvis login lykkedes, false hvis ikke
        public async Task<bool> LoginAsync(string name, string password)
        {
            // Spørger databasen om der findes en bruger med det givne navn og kodeord
            User? user = await _repo.GetByNameAndPasswordAsync(name, password);

            if (user != null)
            {
                // Login lykkedes - gem brugeren så vi kan bruge den senere
                LoggedInUser = user;
                return true;
            }

            // Ingen bruger fundet - forkert navn eller kodeord
            return false;
        }
    }
}
