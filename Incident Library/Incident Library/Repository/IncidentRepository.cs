using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Data.Sqlite;
using Incident_Library.MODELS__Data_;

namespace Incident_Library.Repository
{
    public class IncidentRepository
    {

        //private static string GetConnectionString()
        //{
        //    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        //    string dbPath = Path.Combine(baseDir, "IncidentLibrary.db");
        //    return $"Data Source={dbPath};";
        //}
        public async Task<List<IncidentReport>> ReadAsync() //Læser fra databasen og sætter den ind i en liste
        {
            var incidentList = new List<IncidentReport>();
            using var conncetion = new SqliteConnection("Data Source = IncidentLibrary.db;");
            await conncetion.OpenAsync();
            using var command = new SqliteCommand("SELECT * FROM Incident", conncetion);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                IncidentReport i = new IncidentReport();
                i.Id = Convert.ToInt32(reader["IncidentID"]);
                i.Title = reader["Title"] as string;
                i.HowDiscovered = reader["HowDiscovered"] as string;
                i.WhatIsIncident = reader["WhatIsIncident"] as string;
                i.HowResolved = reader["HowResolved"] as string;
                i.Status = Convert.ToInt32(reader["StatusID"]);

                incidentList.Add(i);
            }
            return incidentList;
        }

        public async Task CreateAsync (IncidentReport i) //Opretter nyt incident til databasen; bemærk at vi siger Close her fordi vi ikke bruger 'using var' (tilføjede bare for at vise at vi kunne)
        {
            SqliteConnection connection = new SqliteConnection("Data Source=IncidentLibrary.db;");
            await connection.OpenAsync();

            SqliteCommand command = new SqliteCommand("INSERT INTO Incident (Title, HowDiscovered, WhatIsIncident, HowResolved, StatusID) VALUES (@Title, @HowDiscovered, @WhatIsIncident, @HowResolved, @StatusID)", connection);
            command.Parameters.AddWithValue("@Title", i.Title);
            command.Parameters.AddWithValue("@HowDiscovered", i.HowDiscovered);
            command.Parameters.AddWithValue("@WhatIsIncident", i.WhatIsIncident);
            command.Parameters.AddWithValue("@HowResolved", i.HowResolved);
            command.Parameters.AddWithValue("@StatusID", i.Status);

           await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task DeleteAsync(IncidentReport i) //Sletter incident fra databasen
        {
            using var connection = new SqliteConnection("Data Source=IncidentLibrary.db;");
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Incident WHERE IncidentID = @id";
            command.Parameters.AddWithValue("@id", i.Id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(IncidentReport i) //Opdaterer databasen
        {
            using var connection = new SqliteConnection("Data Source=IncidentLibrary.db;");
            await connection.OpenAsync();
            using var command = new SqliteCommand("UPDATE Incident SET Title = @Title, HowDiscovered = @HowDiscovered, WhatIsIncident = @WhatIsIncident, HowResolved = @HowResolved, StatusID = @StatusID WHERE IncidentID = @id", connection);
            command.Parameters.AddWithValue("@Title", i.Title);
            command.Parameters.AddWithValue("@HowDiscovered", i.HowDiscovered);
            command.Parameters.AddWithValue("@WhatIsIncident", i.WhatIsIncident);
            command.Parameters.AddWithValue("@HowResolved", i.HowResolved);
            command.Parameters.AddWithValue("@StatusID", i.Status);
            command.Parameters.AddWithValue("@id", i.Id);
            
            await command.ExecuteNonQueryAsync();
        }
    }
}
