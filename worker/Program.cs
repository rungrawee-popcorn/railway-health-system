using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Net.Http.Json;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "admin"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// สร้าง queue (เหมือน backend)
channel.QueueDeclare(
    queue: "sensor_queue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

Console.WriteLine("Waiting for messages...");

// สร้าง consumer
var consumer = new EventingBasicConsumer(channel);

// เมื่อมี message เข้ามา
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received: {message}");

    // สร้าง HttpClient
    using var httpClient = new HttpClient();

    // ส่งไป AI (FastAPI)
    var response = await httpClient.PostAsJsonAsync(
        "http://127.0.0.1:8000/analyze",
        new { temperature = 85 } // เดี๋ยวค่อยเปลี่ยนเป็น dynamic
    );

    var result = await response.Content.ReadAsStringAsync();

    Console.WriteLine($"AI Result: {result}");
};

// เริ่มฟัง queue
channel.BasicConsume(
    queue: "sensor_queue",
    autoAck: true,
    consumer: consumer
);

// กันโปรแกรมปิด
Console.ReadLine();