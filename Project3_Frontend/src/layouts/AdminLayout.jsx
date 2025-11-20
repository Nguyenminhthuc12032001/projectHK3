import { Link, Outlet, useLocation } from "react-router-dom";

export default function AdminLayout() {
  const location = useLocation();

  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      {/* HEADER */}
      <header className="bg-blue-700 text-white py-4 shadow-md z-50">
        <div className="container mx-auto flex justify-between items-center px-8">
          <img
            src="src/assets/img/logo.png"
            alt="Logo"
            className="h-20 w-28 object-cover rounded-md shadow"
          />

          <h1 className="text-2xl font-bold">Medical Care</h1>

          {/* NAV MENU giống EmployeeLayout */}
          <nav className="flex flex-1 justify-center gap-8">
            <Link to="/admin" className="hover:text-gray-200">Home</Link>
            <Link to="/admin/resource" className="hover:text-gray-200">Add Resource</Link>
            <Link to="/admin/support" className="hover:text-gray-200">Employee Support</Link>
            <Link to="/admin/search" className="hover:text-gray-200">Search</Link>
            <Link to="/admin/status" className="hover:text-gray-200">Request Status</Link>
            <Link to="/login" className="hover:text-gray-200">Log Out</Link>
          </nav>
        </div>
      </header>

      {/* Main content */}
      <main className="flex-1 container mx-auto px-6 py-8">
        <Outlet />
      </main>

      {/* Footer giống AdminLayout */}
      <footer className="bg-blue-900 text-white py-6 text-center text-sm">
        © {new Date().getFullYear()} Medical Care — Admin Panel
      </footer>
    </div>
  );
}
