const axios = require("axios");

async function getDevices() {
  try {
    const response = await axios.get(
      "http://railway-backend/api/devices"
    );

    return response.data;
  } catch (error) {
    console.error("Failed to fetch devices:", error.message);

    return [];
  }
}

function generateSensorData(deviceId) {
  return {
    deviceId,

    temperature: Math.floor(Math.random() * 100),

    vibration: Math.floor(Math.random() * 10),

    timestamp: new Date().toISOString(),
  };
}

async function sendSensorData() {
  const devices = await getDevices();

  if (devices.length === 0) {
    console.log("No devices found");

    return;
  }

  const randomDevice =
    devices[Math.floor(Math.random() * devices.length)];

  const data = generateSensorData(randomDevice.id);

  try {
    const response = await axios.post(
      "http://railway-backend/api/sensor-data",
      data
    );

    console.log("Sent:", data);

    console.log("Response:", response.data);
  } catch (error) {
    console.error("Error:", error.message);
  }
}

console.log("Railway Simulator Started...");

setInterval(sendSensorData, 5000);