using Incident_Library.INTERFACES;
using Incident_Library.MODELS__Data_;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Incident_Library.Repository
{
    public class UserRepository : IUserRepository
    {
        // Henter én bruger fra databasen baseret på navn og kodeord
        // Bruges til login - returnerer brugeren hvis den findes, ellers null
        public async Task<User?> GetByNameAndPasswordAsync(string name, string password)
        {
            // Opretter forbindelse til databasen via vores centrale ConnectionString
            using var connection = new SqliteConnection(Database.ConnectionString);
            await connection.OpenAsync();

            // SQL-forespørgsel der søger efter brugeren med det givne navn og kodeord
            // @Name og @Password er parametre der forhindrer SQL injection
            using var command = new SqliteCommand(
                "SELECT * FROM User WHERE Name = @Name AND Password = @Password", connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Password", password);

            // Udfører forespørgslen og læser resultatet
            using var reader = await command.ExecuteReaderAsync();

            // Hvis der er en række i resultatet, bygger vi et User-objekt og returnerer det
            if (await reader.ReadAsync())
            {
                return new User(
                    Convert.ToInt32(reader["UserID"]),
                    reader["Name"]?.ToString() ?? string.Empty,
                    reader["Password"]?.ToString() ?? string.Empty,
                    Convert.ToInt32(reader["Role"])
                );
            }

            // Ingen bruger fundet med det givne navn og kodeord
            return null;
        }

        // Henter alle brugere fra databasen
        // Bruges i Admin Panel så admin kan se og administrere brugere
        public async Task<List<User>> GetAllAsync()
        {
            var users = new List<User>();

            using var connection = new SqliteConnection(Database.ConnectionString);
            await connection.OpenAsync();

            // Henter alle rækker fra User-tabellen
            using var command = new SqliteCommand("SELECT * FROM User", connection);
            using var reader = await command.ExecuteReaderAsync();

            // Læser én række ad gangen og bygger et User-objekt for hver bruger
            while (await reader.ReadAsync())
            {
                users.Add(new User(
                    Convert.ToInt32(reader["UserID"]),
                    reader["Name"]?.ToString() ?? string.Empty,
                    reader["Password"]?.ToString() ?? string.Empty,
                    Convert.ToInt32(reader["Role"])
                ));
            }

            return users;
        }
    }
}
