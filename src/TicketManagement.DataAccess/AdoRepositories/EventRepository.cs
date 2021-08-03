using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.AdoRepositories
{
    public class EventRepository : IRepository<Event>
    {
        private readonly string _connectionString;

        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddAsync(Event item)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("AddEvent", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddEvent",
            };

            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@LayoutId", item.LayoutId);
            command.Parameters.AddWithValue("@EventStart", item.EventStart);
            command.Parameters.AddWithValue("@EventEnd", item.EventEnd);
            command.Parameters.AddWithValue("@Image", item.Image);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Event item)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("dbo.RemoveEvent", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "RemoveEvent",
            };
            command.Parameters.AddWithValue("@Id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Event item)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var command = new SqlCommand("dbo.UpdateEvent", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "UpdateEvent",
            };
            command.Parameters.AddWithValue("@Id", item.Id);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@LayoutId", item.LayoutId);
            command.Parameters.AddWithValue("@EventStart", item.EventStart);
            command.Parameters.AddWithValue("@EventEnd", item.EventEnd);
            command.Parameters.AddWithValue("@Image", item.Image);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public IQueryable<Event> GetAll()
        {
            var eventList = new List<Event>();

            var createCommand = $"SELECT Id, LayoutId, Name, Description, EventStart, EventEnd, Image FROM Event";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(createCommand, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var myEvent = new Event
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    LayoutId = Convert.ToInt32(reader["LayoutId"]),
                    Name = Convert.ToString(reader["Name"]),
                    Description = Convert.ToString(reader["Description"]),
                    EventStart = Convert.ToDateTime(reader["EventStart"]),
                    EventEnd = Convert.ToDateTime(reader["EventEnd"]),
                    Image = Convert.ToString(reader["Image"]),
                };

                eventList.Add(myEvent);
            }

            return eventList.AsQueryable();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            var createCommand = $"SELECT Id, Name, Description, LayoutId, EventStart, EventEnd, Image FROM Event WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();

            var event1 = new Event
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                Description = Convert.ToString(reader["Description"]),
                LayoutId = Convert.ToInt32(reader["LayoutId"]),
                EventStart = Convert.ToDateTime(reader["EventStart"]),
                EventEnd = Convert.ToDateTime(reader["EventEnd"]),
                Image = Convert.ToString(reader["Image"]),
            };

            return event1;
        }
    }
}
