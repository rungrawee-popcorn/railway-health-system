using Dapper;
using Npgsql;
using backend.DTOs;

namespace backend.Repositories
{
    public class DashboardRepository
    {
        private readonly string _connectionString;

        public DashboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<DashboardDeviceDto>> GetDashboardData()
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var sql = @"
                SELECT
                    d.id,
                    d.name,
                    d.location,

                    COALESCE(sd.temperature, 0) AS Temperature,
                    COALESCE(sd.vibration, 0) AS Vibration,

                    CASE
                        WHEN sd.temperature >= 80 THEN 'CRITICAL'
                        WHEN sd.temperature >= 60 THEN 'WARNING'
                        ELSE 'OK'
                    END AS Status,

                    sd.timestamp

                FROM devices d

                LEFT JOIN LATERAL
                (
                    SELECT *
                    FROM sensor_data s
                    WHERE s.device_id = d.id
                    ORDER BY s.timestamp DESC
                    LIMIT 1
                ) sd ON true

                ORDER BY d.id;
            ";

            return await connection.QueryAsync<DashboardDeviceDto>(sql);
        }
    }
}