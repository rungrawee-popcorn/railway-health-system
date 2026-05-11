"use client";

import { useDashboard } from "@/hooks/useDashboard";

type Device = {
  id: number;
  name: string;
  location: string;
  temperature: number;
  vibration: number;
  status: string;
  timestamp: string;
};

export default function DashboardPage() {
  const { data = [], isLoading, isError, error } = useDashboard();

  const total = data.length;
  const ok = data.filter((d: Device) => d.status === "OK").length;
  const critical = data.filter((d: Device) => d.status !== "OK").length;

  if (isLoading) {
    return (
      <div className="h-screen flex items-center justify-center bg-gradient-to-br from-slate-900 via-slate-800 to-indigo-900">
        <div className="text-center">
          <div className="w-10 h-10 border-4 border-white/30 border-t-white rounded-full animate-spin mx-auto mb-4" />
          <p className="text-white/80 text-lg animate-pulse">
            Loading devices...
          </p>
        </div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="h-screen flex items-center justify-center bg-red-50">
        <div className="text-center bg-white p-6 rounded-2xl shadow-lg border border-red-100">
          <p className="text-red-600 font-semibold text-lg">
            ⚠️ System Error
          </p>
          <p className="text-sm text-red-500 mt-2">
            {String(error?.message)}
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-indigo-50 to-slate-100 py-10 px-6">

      <div className="max-w-7xl mx-auto space-y-12">

        {/* HEADER */}
        <div className="space-y-2">
          <h1 className="text-4xl font-extrabold tracking-tight text-slate-800">
            🚆 <span className="text-indigo-600">Railway</span> Health Dashboard
          </h1>
          <p className="text-slate-500">
            Real-time device monitoring system
          </p>
        </div>

        {/* STATS */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">

          <div className="rounded-2xl bg-white/80 backdrop-blur-xl shadow-md border border-white/50 p-6 hover:shadow-xl transition">
            <p className="text-slate-500 text-sm">Total Devices</p>
            <p className="text-4xl font-bold text-slate-800 mt-2">{total}</p>
          </div>

          <div className="rounded-2xl bg-green-50/70 backdrop-blur-xl shadow-md border border-green-200/60 p-6 hover:shadow-xl transition">
            <p className="text-slate-500 text-sm">Healthy Devices</p>
            <p className="text-4xl font-bold text-green-600 mt-2">{ok}</p>
          </div>

          <div className="rounded-2xl bg-red-50/70 backdrop-blur-xl shadow-md border border-red-200/60 p-6 hover:shadow-xl transition">
            <p className="text-slate-500 text-sm">Critical Devices</p>
            <p className="text-4xl font-bold text-red-600 mt-2">{critical}</p>
          </div>

        </div>

        {/* GRID */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">

          {data.map((d: Device) => {
            const isOk = d.status === "OK";

            return (
              <div
                key={d.id}
                className="group relative rounded-2xl p-6 bg-white/80 backdrop-blur-xl
                border border-slate-100 shadow-md hover:shadow-2xl
                transition-all duration-300 hover:-translate-y-1 overflow-hidden"
              >

                {/* glow background (FIXED ไม่บัง content แล้ว) */}
                <div
                  className={`absolute inset-0 opacity-0 group-hover:opacity-100 transition rounded-2xl
                  ${isOk ? "bg-green-200/10" : "bg-red-200/10"}`}
                />

                {/* CONTENT */}
                <div className="relative z-10">

                  {/* TITLE */}
                  <h2 className="text-lg font-semibold text-slate-800 group-hover:text-indigo-600 transition">
                    {d.name}
                  </h2>

                  {/* LOCATION */}
                  <p className="text-slate-500 mt-1 text-sm">
                    📍 {d.location}
                  </p>

                  {/* ID */}
                  <p className="text-xs text-slate-400 mt-3">
                    Device ID: <span className="font-medium">#{d.id}</span>
                  </p>

                  {/* SENSOR DATA */}
                  <div className="mt-4 space-y-2">

                    <div className="flex items-center justify-between text-sm">
                      <span className="text-slate-500">🌡 Temperature</span>

                      <span className="font-semibold text-slate-700">
                        {d.temperature}°C
                      </span>
                    </div>

                    <div className="flex items-center justify-between text-sm">
                      <span className="text-slate-500">📈 Vibration</span>

                      <span className="font-semibold text-slate-700">
                        {d.vibration}
                      </span>
                    </div>

                  </div>

                  {/* STATUS */}
                  <div className="mt-6 flex items-center justify-between">

                    <div className="flex items-center gap-2">
                      <span
                        className={`h-2.5 w-2.5 rounded-full animate-pulse ${
                          isOk
                            ? "bg-green-500 shadow-[0_0_10px_rgba(34,197,94,0.8)]"
                            : "bg-red-500 shadow-[0_0_10px_rgba(239,68,68,0.8)]"
                        }`}
                      />

                      <span className="text-sm text-slate-600">
                        {isOk ? "Healthy" : "Critical"}
                      </span>
                    </div>

                    <span
                      className={`px-3 py-1 text-xs rounded-full font-semibold border shadow-sm ${
                        isOk
                          ? "bg-green-500/10 text-green-700 border-green-400/40"
                          : "bg-red-500/10 text-red-700 border-red-400/40"
                      }`}
                    >
                      {d.status}
                    </span>

                  </div>

                </div>

              </div>
            );
          })}

        </div>

      </div>
    </div>
  );
}