using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.AdoRepositories
{
    public class VenueRepository : IRepository<Venue>
    {
        private readonly string _connectionString;

        public VenueRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddAsync(Venue item)
        {
            var createCommand = $"INSERT INTO Venue (Description, Address, Phone) VALUES (@Description, @Address, @Phone)";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@Address", item.Address);
            command.Parameters.AddWithValue("@Phone", item.Phone);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Venue item)
        {
            var removeCommand = $"DELETE FROM Venue WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@Id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Venue item)
        {
            var removeCommand = $"UPDATE Venue " +
                $"SET Description = @description, " +
                $"Address = @address, " +
                $"Phone = @phone " +
                $"WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@description", item.Description);
            command.Parameters.AddWithValue("@address", item.Address);
            command.Parameters.AddWithValue("@phone", item.Phone);
            command.Parameters.AddWithValue("@id", item.Id);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public IQueryable<Venue> GetAll()
        {
            var commandText = $"SELECT Id, Description, Address, Phone FROM Venue";

            var venueList = new List<Venue>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(commandText, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var venue = new Venue
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Description = Convert.ToString(reader["Description"]),
                    Address = Convert.ToString(reader["Address"]),
                    Phone = Convert.ToString(reader["Phone"]),
                };

                venueList.Add(venue);
            }

            return venueList.AsQueryable();
        }

        public async Task<Venue> GetByIdAsync(int id)
        {
            var createCommand = $"SELECT Id, Description, Address, Phone FROM Venue WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();

            var venue = new Venue
            {
                Id = Convert.ToInt32(reader["Id"]),
                Description = Convert.ToString(reader["Description"]),
                Address = Convert.ToString(reader["Address"]),
                Phone = Convert.ToString(reader["Phone"]),
            };

            return venue;
        }
    }
}
