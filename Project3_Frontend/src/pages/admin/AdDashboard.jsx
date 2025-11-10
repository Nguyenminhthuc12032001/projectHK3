export default function Dashboard() {
  const user = localStorage.getItem("role") || "Guest";

  return (
    <div className="bg-white rounded-xl shadow-md p-6">
      <h2 className="text-2xl font-bold text-gray-800 mb-4">
        📊 Admin Dashboard
      </h2>

      <p className="text-gray-600 mb-6">
        Welcome back, <span className="font-semibold text-blue-700">{user}</span>!
      </p>

      <div className="grid grid-cols-3 gap-4">
        <div className="p-4 bg-blue-100 rounded-lg text-center">
          <h3 className="text-xl font-semibold text-blue-700">12</h3>
          <p className="text-gray-700">Total Employees</p>
        </div>

        <div className="p-4 bg-green-100 rounded-lg text-center">
          <h3 className="text-xl font-semibold text-green-700">5</h3>
          <p className="text-gray-700">Pending Tasks</p>
        </div>

        <div className="p-4 bg-yellow-100 rounded-lg text-center">
          <h3 className="text-xl font-semibold text-yellow-700">3</h3>
          <p className="text-gray-700">New Reports</p>
        </div>
      </div>

      <div className="mt-8 text-gray-600">
        <h3 className="text-lg font-semibold mb-2">Quick Tips:</h3>
        <ul className="list-disc pl-6 space-y-1">
          <li>Manage users and roles efficiently.</li>
          <li>Monitor employee progress daily.</li>
          <li>Check system logs for recent activity.</li>
        </ul>
      </div>
    </div>
  );
}
