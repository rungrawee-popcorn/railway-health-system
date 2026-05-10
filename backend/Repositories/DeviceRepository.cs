using Dapper;
using Npgsql;
using backend.Models;

namespace backend.Repositories
{
    public class DeviceRepository
    {
        private readonly string _connectionString;

        public DeviceRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Device>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM devices";
            return await connection.QueryAsync<Device>(sql);
        }

        public async Task Create(Device device)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"INSERT INTO devices (name, location, status)
                        VALUES (@Name, @Location, @Status)";
            await connection.ExecuteAsync(sql, device);
        }

        public async Task Update(Device device)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"UPDATE devices 
                        SET name = @Name,
                            location = @Location,
                            status = @Status
                        WHERE id = @Id";

            await connection.ExecuteAsync(sql, device);
        }

        public async Task Delete(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM devices WHERE id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}