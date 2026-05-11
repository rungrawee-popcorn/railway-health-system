namespace backend.Models
{
    public class SensorData
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public double Temperature { get; set; }

        public double Vibration { get; set; }

        public string Status { get; set; } = "OK";

        public DateTime Created_At { get; set; }
    }
}