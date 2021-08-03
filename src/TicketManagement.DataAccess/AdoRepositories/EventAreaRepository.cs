using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.AdoRepositories
{
    public class EventAreaRepository : IRepository<EventArea>
    {
        private readonly string _connectionString;

        public EventAreaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> UpdateAsync(EventArea item)
        {
            var removeCommand = $"UPDATE EventArea " +
                $"SET EventId = @eventId, " +
                $"Description = @description, " +
                $"CoordX = @coordX " +
                $"CoordY = @coordY " +
                $"Price = @price " +
                $"WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@eventId", item.EventId);
            command.Parameters.AddWithValue("@description", item.Description);
            command.Parameters.AddWithValue("@coordX", item.CoordX);
            command.Parameters.AddWithValue("@coordY", item.CoordY);
            command.Parameters.AddWithValue("@price", item.Price);
            command.Parameters.AddWithValue("@id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public IQueryable<EventArea> GetAll()
        {
            var eventAreaList = new List<EventArea>();

            var createCommand = $"SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(createCommand, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var eventArea = new EventArea
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    EventId = Convert.ToInt32(reader["EventId"]),
                    Description = Convert.ToString(reader["Description"]),
                    CoordX = Convert.ToInt32(reader["CoordX"]),
                    CoordY = Convert.ToInt32(reader["CoordY"]),
                    Price = Convert.ToInt32(reader["Price"]),
                };

                eventAreaList.Add(eventArea);
            }

            return eventAreaList.AsQueryable();
        }

        public async Task<EventArea> GetByIdAsync(int id)
        {
            var createCommand = $"SELECT Id, EventId, Description, CoordX, CoordY, Price FROM EventArea WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = await command.ExecuteReaderAsync();

            var eventArea = new EventArea
            {
                Id = Convert.ToInt32(reader["Id"]),
                EventId = Convert.ToInt32(reader["EventId"]),
                Description = Convert.ToString(reader["Description"]),
                CoordX = Convert.ToInt32(reader["CoordX"]),
                CoordY = Convert.ToInt32(reader["CoordY"]),
                Price = Convert.ToInt32(reader["Price"]),
            };

            return eventArea;
        }

        public async Task<bool> AddAsync(EventArea item)
        {
            var createCommand = $"INSERT INTO EventArea (EventId, Description, CoordX, CoordY, Price) VALUES (@EventId, @Description, @CoordX, @CoordY, @Price)";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@EventId", item.EventId);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@CoordX", item.CoordX);
            command.Parameters.AddWithValue("@CoordY", item.CoordY);
            command.Parameters.AddWithValue("@Price", item.Price);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(EventArea item)
        {
            var removeCommand = $"DELETE FROM EventArea WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@Id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}
