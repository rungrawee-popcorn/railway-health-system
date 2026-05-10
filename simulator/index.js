const axios = require("axios");

function generateSensorData() {
  return {
    deviceId: 1,
    temperature: Math.floor(Math.random() * 100),
    vibration: Math.floor(Math.random() * 10),
    timestamp: new Date().toISOString(),
  };
}

async function sendSensorData() {
  const data = generateSensorData();

  try {
    const response = await axios.post(
      "http://localhost:5205/api/sensor-data",
      data
    );

    console.log("Sent:", data);
    console.log("Response:", response.data);
  } catch (error) {
    console.error("Error:", error.message);
  }
}

setInterval(sendSensorData, 5000);