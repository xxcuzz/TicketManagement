using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.AdoRepositories
{
    public class SeatRepository : IRepository<Seat>
    {
        private readonly string _connectionString;

        public SeatRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddAsync(Seat item)
        {
            var createCommand = $"INSERT INTO Seat (AreaId, Row, Number) VALUES (@AreaId, @Row, @Number)";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@AreaId", item.AreaId);
            command.Parameters.AddWithValue("@Row", item.Row);
            command.Parameters.AddWithValue("@Number", item.Number);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Seat item)
        {
            var removeCommand = $"DELETE FROM Seat WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@Id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Seat item)
        {
            var removeCommand = $"UPDATE Seat " +
                $"SET AreaId = @areaId, " +
                $"Row = @row, " +
                $"Number = @number " +
                $"WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@areaId", item.AreaId);
            command.Parameters.AddWithValue("@row", item.Row);
            command.Parameters.AddWithValue("@number", item.Number);
            command.Parameters.AddWithValue("@id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public IQueryable<Seat> GetAll()
        {
            var seatList = new List<Seat>();

            var createCommand = $"SELECT Id, AreaId, Row, Number FROM Seat";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand(createCommand, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var seat = new Seat
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    AreaId = Convert.ToInt32(reader["AreaId"]),
                    Row = Convert.ToInt32(reader["Row"]),
                    Number = Convert.ToInt32(reader["Number"]),
                };

                seatList.Add(seat);
            }

            return seatList.AsQueryable();
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            var createCommand = $"SELECT Id, AreaId, Number, Row FROM Seat WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();

            var seat = new Seat
            {
                Id = Convert.ToInt32(reader["Id"]),
                AreaId = Convert.ToInt32(reader["AreaId"]),
                Number = Convert.ToInt32(reader["Number"]),
                Row = Convert.ToInt32(reader["Row"]),
            };

            return seat;
        }
    }
}
