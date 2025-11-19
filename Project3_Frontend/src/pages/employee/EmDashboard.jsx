import { Link, Outlet } from "react-router-dom";

export default function EmployeeLayout() {
  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      {/* Logo trên cùng */}
      <div className="flex justify-center py-4 bg-gray-50"></div>

      {/* Navbar riêng của employee */}
      <header className="bg-blue-700 text-white py-4 shadow-md z-50">
        <div className="container mx-auto flex justify-between items-center px-8">
          <img
            src="src/assets/img/logo.png"
            alt="Logo"
            className="h-20 w-28 object-cover rounded-md shadow"
          />
          <h1 className="text-2xl font-bold">Medical Care</h1>

          <nav className="flex flex-1 justify-center gap-8">
            <Link to="/employee" className="hover:text-gray-200">
              Home
            </Link>
            <Link to="/employee/details" className="hover:text-gray-200">
              Employee Details
            </Link>
            <Link to="/employee/search" className="hover:text-gray-200">
              Search
            </Link>
            <Link
              to="/employee/orderforinsurance"
              className="hover:text-gray-200"
            >
              Order For Insurance
            </Link>
            <Link to="/login" className="hover:text-gray-200">
              Log Out
            </Link>
          </nav>
        </div>
      </header>

      {/* Phần nội dung hiển thị */}
      <main className=" grow flex flex-col items-center justify-center text-center px-4">
        {/* Nội dung mặc định khi chưa có route con */}
        <div className="mb-8">
          <h2 className="text-2xl font-semibold text-blue-900 mb-6">
            Welcome to The Employee
          </h2>
        </div>

        {/* Outlet để hiển thị các trang con */}
        <Outlet />
      </main>
    </div>
  );
}
