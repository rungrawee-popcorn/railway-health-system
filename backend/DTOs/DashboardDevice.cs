namespace backend.DTOs
{
    public class DashboardDeviceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int Temperature { get; set; }

        public int Vibration { get; set; }

        public string Status { get; set; } = "OK";

        public DateTime Timestamp { get; set; }
    }
}