import requests
import random
import time

url = "http://localhost:8000/analyze"

while True:
    data = {
        "temperature": random.randint(50, 100)
    }

    response = requests.post(url, json=data)

    print(response.json())

    time.sleep(2)