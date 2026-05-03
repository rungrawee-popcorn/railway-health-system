namespace backend.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime Created_At { get; set; }
    }
}