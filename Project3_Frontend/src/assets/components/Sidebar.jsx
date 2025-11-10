import { Link, useLocation } from "react-router-dom";

export default function Sidebar() {
  const location = useLocation();

  const menuItems = [
    { name: "Dashboard", path: "/dashboard" },
    { name: "Policies", path: "/policies" },
    { name: "Claims", path: "/claims" },
    { name: "Customers", path: "/customers" },
    { name: "Reports", path: "/reports" },
  ];

  return (
    <aside className="bg-blue-900 text-white w-64 min-h-screen p-6">
      <h1 className="text-2xl font-bold mb-8 text-center">Admin Panel</h1>
      <nav className="space-y-3">
        {menuItems.map((item) => (
          <Link
            key={item.path}
            to={item.path}
            className={`block px-4 py-2 rounded-lg ${
              location.pathname === item.path
                ? "bg-blue-700 font-semibold"
                : "hover:bg-blue-800"
            }`}
          >
            {item.name}
          </Link>
        ))}
      </nav>
    </aside>
  );
}
