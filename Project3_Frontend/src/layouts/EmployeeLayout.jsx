import { Link, Outlet, useLocation } from "react-router-dom";

export default function EmployeeLayout() {
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

          {/* NAV MENU */}
          <nav className="flex flex-1 justify-center gap-8">
            <Link to="/employee" className="hover:text-gray-200">Home</Link>
            <Link to="/employee/details" className="hover:text-gray-200">Employee Details</Link>
            <Link to="/employee/search" className="hover:text-gray-200">Search</Link>
            <Link to="/employee/orderforinsurance" className="hover:text-gray-200">Order For Insurance</Link>
            <Link to="/login" className="hover:text-gray-200">Log Out</Link>
          </nav>
        </div>
      </header>

      {/* Main content */}
      <main className="grow flex flex-col items-center text-center px-4 py-10">
        {/* Chỉ hiển thị welcome message khi ở trang /employee */}
        {!location.pathname.includes("employee/details") &&
         !location.pathname.includes("employee/search") &&
         !location.pathname.includes("employee/orderforinsurance") && (
          <h2 className="text-2xl font-semibold text-blue-900 mb-6">
            Welcome to The Employee
          </h2>
        )}

        {/* Trang con */}
        <Outlet />
      </main>

      {/* Footer giống AdminLayout */}
      <footer className="bg-blue-900 text-white py-6 text-center text-sm">
        © {new Date().getFullYear()} Medical Care — Admin Panel
      </footer>
    </div>
  );
}
