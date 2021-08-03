using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.AdoRepositories
{
    public class LayoutRepository : IRepository<Layout>
    {
        private readonly string _connectionString;

        public LayoutRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddAsync(Layout item)
        {
            var createCommand = $"INSERT INTO Layout (Description, VenueId) VALUES (@Description, @VenueId)";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@VenueId", item.VenueId);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Layout item)
        {
            var removeCommand = $"DELETE FROM Layout WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@Id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Layout item)
        {
            var removeCommand = $"UPDATE Layout " +
                $"SET VenueId = @venueId, " +
                $"Description = @description, " +
                $"WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@venueId", item.VenueId);
            command.Parameters.AddWithValue("@description", item.Description);
            command.Parameters.AddWithValue("@id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public IQueryable<Layout> GetAll()
        {
            var layoutList = new List<Layout>();

            var createCommand = $"SELECT Id, VenueId, Description FROM Layout";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(createCommand, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var layout = new Layout
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    VenueId = Convert.ToInt32(reader["VenueId"]),
                    Description = Convert.ToString(reader["Description"]),
                };

                layoutList.Add(layout);
            }

            return layoutList.AsQueryable();
        }

        public async Task<IQueryable<Layout>> GetLayoutsByVenueIdAsync(int venueId)
        {
            var layoutList = new List<Layout>();

            var createCommand = $"SELECT Id, VenueId, Description FROM Layout WHERE VenueId = @venueId";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@venueId", venueId);
            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var layout = new Layout
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    VenueId = Convert.ToInt32(reader["VenueId"]),
                    Description = Convert.ToString(reader["Description"]),
                };

                layoutList.Add(layout);
            }

            return layoutList.AsQueryable();
        }

        public async Task<Layout> GetByIdAsync(int id)
        {
            var createCommand = $"SELECT Id, VenueId, Description FROM Layout WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();

            var layout = new Layout
            {
                Id = Convert.ToInt32(reader["Id"]),
                VenueId = Convert.ToInt32(reader["VenueId"]),
                Description = Convert.ToString(reader["Description"]),
            };

            return layout;
        }
    }
}
