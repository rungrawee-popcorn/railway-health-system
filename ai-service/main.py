from fastapi import FastAPI

app = FastAPI()

@app.get("/")
def root():
    return {"message": "AI Service is running"}

@app.post("/analyze")
def analyze(data: dict):
    temp = data.get("temperature", 0)

    if temp > 80:
        status = "CRITICAL"
    elif temp > 60:
        status = "WARNING"
    else:
        status = "OK"

    return {
        "temperature": temp,
        "status": status
    }