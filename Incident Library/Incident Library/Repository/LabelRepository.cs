using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Incident_Library.MODELS__Data_;

namespace Incident_Library.Repository
{ // Rasmus
    public class LabelRepository
    {
        // Henter alle labels tilknyttet et bestemt incident
        public async Task<List<Label>> GetByIncidentIdAsync(int incidentId)
        {
            var labels = new List<Label>();

            using var connection = new SqliteConnection(Database.ConnectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(
                "SELECT * FROM Label WHERE IncidentID = @id", connection);
            command.Parameters.AddWithValue("@id", incidentId);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                labels.Add(new Label(
                    Convert.ToInt32(reader["LabelID"]),
                    Convert.ToInt32(reader["IncidentID"]),
                    reader["Name"]?.ToString() ?? string.Empty
                ));
            }

            return labels;
        }

        // Gemmer en ny label til databasen
        public async Task CreateAsync(Label label)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(
                "INSERT INTO Label (IncidentID, Name) VALUES (@IncidentID, @Name)", connection);
            command.Parameters.AddWithValue("@IncidentID", label.IncidentId);
            command.Parameters.AddWithValue("@Name", label.Name);

            await command.ExecuteNonQueryAsync();
        }

        // Sletter alle labels tilknyttet et bestemt incident
        // Bruges når vi gemmer et incident - vi sletter først alle labels og indsætter dem igen
        public async Task DeleteByIncidentIdAsync(int incidentId)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            await connection.OpenAsync();

            using var command = new SqliteCommand(
                "DELETE FROM Label WHERE IncidentID = @id", connection);
            command.Parameters.AddWithValue("@id", incidentId);

            await command.ExecuteNonQueryAsync();
        }
    }
}
