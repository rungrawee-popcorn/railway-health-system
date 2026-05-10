using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/sensor-data")]
    public class SensorDataController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SensorDataController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveSensorData([FromBody] SensorDataDto data)
        {
            try
            {
                using var connection = new NpgsqlConnection(
                    _config.GetConnectionString("DefaultConnection")
                );

                await connection.OpenAsync();

                // เช็ค DB connect ก่อน
                var test = await connection.ExecuteScalarAsync<int>("SELECT 1");
                Console.WriteLine($"DB CONNECTION OK: {test}");

                // debug input ที่เข้ามา
                Console.WriteLine("Sensor Data Received:");
                Console.WriteLine($"DeviceId: {data.DeviceId}, Temp: {data.Temperature}, Vib: {data.Vibration}, Time: {data.Timestamp}");

                var sql = @"
                    INSERT INTO sensor_data (device_id, temperature, vibration, timestamp)
                    VALUES (@DeviceId, @Temperature, @Vibration, @Timestamp)
                ";

                var rows = await connection.ExecuteAsync(sql, data);

                Console.WriteLine($"INSERT SUCCESS ROWS: {rows}");

                return Ok(new
                {
                    message = "Sensor data saved successfully",
                    inserted = rows
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR INSERT SENSOR DATA:");
                Console.WriteLine(ex.Message);

                return StatusCode(500, new
                {
                    message = "Failed to save sensor data",
                    error = ex.Message
                });
            }
        }
    }

    public class SensorDataDto
    {
        public int DeviceId { get; set; }
        public int Temperature { get; set; }
        public int Vibration { get; set; }
        public DateTime Timestamp { get; set; }
    }
}