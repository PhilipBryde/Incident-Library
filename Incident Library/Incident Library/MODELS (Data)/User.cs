using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.MODELS__Data_
{
    public class User
    { //Rasmus
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Role { get; set; } 

        public User(int userId, string name, string password, int role)
        {
            UserId = userId;
            Name = name;
            Password = password;
            Role = role;
        }
        // den tomme Konstruktør er til når vi henter objekter fra databasen 
        public User() { }

        // Oversætter rolle-tallet til et læsbart navn
        public string RoleName
        {
            get
            {
                if (Role == 1) return "Admin";
                if (Role == 2) return "User";
                return "Unknown";
            }
        }
    }
}
