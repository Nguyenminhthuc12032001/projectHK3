import { Link } from "react-router-dom";

export default function Home() {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gradient-to-b from-slate-50 to-slate-200 text-gray-800">
      <h1 className="text-4xl font-bold mb-4">🌉 Welcome to the Portal</h1>
      <p className="text-lg text-gray-600 mb-8 text-center max-w-md">
        This is a simple public homepage.  
        You can log in as <strong>Admin</strong> or <strong>Employee</strong> to access protected sections.
      </p>

      <div className="flex gap-4">
        <Link
          to="/login"
          className="px-5 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
        >
          Login
        </Link>
        <Link
          to="/register"
          className="px-5 py-2 bg-gray-300 text-gray-700 rounded-lg hover:bg-gray-400 transition"
        >
          Register
        </Link>
      </div>

      <footer className="absolute bottom-4 text-sm text-gray-500">
        © 2025 Your Company | Public Access
      </footer>
    </div>
  );
}
