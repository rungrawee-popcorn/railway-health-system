export default function LoginPage() {
  return (
    <div className="flex items-center justify-center h-screen bg-gray-100">
      <div className="bg-white p-8 rounded-xl shadow-md w-96">
        <h1 className="text-2xl font-bold mb-6 text-center">
          Login
        </h1>

        <input
          className="w-full border p-2 mb-3 rounded"
          placeholder="Username"
        />

        <input
          className="w-full border p-2 mb-3 rounded"
          placeholder="Password"
          type="password"
        />

        <button className="w-full bg-blue-500 text-white p-2 rounded">
          Login
        </button>
      </div>
    </div>
  );
}