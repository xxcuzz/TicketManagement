using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.AdoRepositories
{
    public class AreaRepository : IRepository<Area>
    {
        private readonly string _connectionString;

        public AreaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddAsync(Area item)
        {
            var createCommand = $"INSERT INTO Area (LayoutId, Description, CoordX, CoordY) VALUES (@LayoutId, @Description, @CoordX, @CoordY)";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@LayoutId", item.LayoutId);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@CoordX", item.CoordX);
            command.Parameters.AddWithValue("@CoordY", item.CoordY);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Area item)
        {
            var removeCommand = $"DELETE FROM Area WHERE Id = @Id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@Id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Area item)
        {
            var removeCommand = $"UPDATE Area " +
                $"SET LayoutId = @layoutId, " +
                $"Description = @description, " +
                $"CoordX = @coordX " +
                $"CoordY = @coordY " +
                $"WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand(removeCommand, connection);
            command.Parameters.AddWithValue("@layoutId", item.LayoutId);
            command.Parameters.AddWithValue("@description", item.Description);
            command.Parameters.AddWithValue("@coordX", item.CoordX);
            command.Parameters.AddWithValue("@coordY", item.CoordY);
            command.Parameters.AddWithValue("@id", item.Id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public IQueryable<Area> GetAll()
        {
            var areaList = new List<Area>();

            var createCommand = $"SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(createCommand, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var area = new Area
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    LayoutId = Convert.ToInt32(reader["LayoutId"]),
                    Description = Convert.ToString(reader["Description"]),
                    CoordX = Convert.ToInt32(reader["CoordX"]),
                    CoordY = Convert.ToInt32(reader["CoordY"]),
                };

                areaList.Add(area);
            }

            return areaList.AsQueryable();
        }

        public async Task<Area> GetByIdAsync(int id)
        {
            var createCommand = $"SELECT Id, LayoutId, Description, CoordX, CoordY FROM Area WHERE Id = @id";
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@id", id);

            var reader = await command.ExecuteReaderAsync();

            var area = new Area
            {
                Id = Convert.ToInt32(reader["Id"]),
                LayoutId = Convert.ToInt32(reader["LayoutId"]),
                Description = Convert.ToString(reader["Description"]),
                CoordX = Convert.ToInt32(reader["CoordX"]),
                CoordY = Convert.ToInt32(reader["CoordY"]),
            };

            return area;
        }
    }
}
