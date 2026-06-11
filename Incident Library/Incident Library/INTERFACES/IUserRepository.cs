using Incident_Library.MODELS__Data_;
using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.INTERFACES
{
    public interface IUserRepository
    {
        // Henter én bruger baseret på navn og kodeord - bruges til login
        Task<User?> GetByNameAndPasswordAsync(string name, string password);

        // Henter alle brugere - bruges i Admin Panel
        Task<List<User>> GetAllAsync();
    }
}
