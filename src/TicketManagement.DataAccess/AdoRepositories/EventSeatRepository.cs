using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.AdoRepositories
{
    public class EventSeatRepository : IRepository<EventSeat>
    {
        private readonly string _connectionString;

        public EventSeatRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> UpdateAsync(EventSeat item)
        {
            var updateCommand = $"UPDATE EventSeat " +
                $"SET EventAreaId = @eventAreaId, " +
                $"Row = @row, " +
                $"Number = @number, " +
                $"State = @state, " +
                $"WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(updateCommand, connection);
            command.Parameters.AddWithValue("@eventAreaId", item.EventAreaId);
            command.Parameters.AddWithValue("@row", item.Row);
            command.Parameters.AddWithValue("@number", item.Number);
            command.Parameters.AddWithValue("@state", item.State);
            command.Parameters.AddWithValue("@id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public IQueryable<EventSeat> GetAll()
        {
            var eventSeatList = new List<EventSeat>();

            var createCommand = $"SELECT Id, EventAreaId, Row, Number, State FROM EventSeat";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(createCommand, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var eventSeat = new EventSeat
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    EventAreaId = Convert.ToInt32(reader["EventAreaId"]),
                    Row = Convert.ToInt32(reader["Row"]),
                    Number = Convert.ToInt32(reader["Number"]),
                    State = Convert.ToInt32(reader["State"]),
                };

                eventSeatList.Add(eventSeat);
            }

            return eventSeatList.AsQueryable();
        }

        public async Task<EventSeat> GetByIdAsync(int id)
        {
            var eventSeatResult = new EventSeat();

            var createCommand = $"SELECT Id, EventAreaId, Row, Number, State FROM EventSeat WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var eventSeat = new EventSeat
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    EventAreaId = Convert.ToInt32(reader["EventAreaId"]),
                    Row = Convert.ToInt32(reader["Row"]),
                    Number = Convert.ToInt32(reader["Number"]),
                    State = Convert.ToInt32(reader["State"]),
                };

                eventSeatResult = eventSeat;
            }

            return eventSeatResult;
        }

        public async Task<bool> AddAsync(EventSeat item)
        {
            var createCommand = $"INSERT INTO EventSeat (EventAreaId, Row, Number, State) VALUES (@EventAreaId, @Row, @Number, @State)";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@EventAreaId", item.EventAreaId);
            command.Parameters.AddWithValue("@Row", item.Row);
            command.Parameters.AddWithValue("@Number", item.Number);
            command.Parameters.AddWithValue("@State", item.State);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(EventSeat item)
        {
            var removeCommand = $"DELETE FROM EventSeat WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@Id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}
