using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/sensor-data")]
    public class SensorDataController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceiveSensorData([FromBody] object data)
        {
            Console.WriteLine("Sensor Data Received:");
            Console.WriteLine(data);

            return Ok(new
            {
                message = "Sensor data received successfully"
            });
        }
    }
}