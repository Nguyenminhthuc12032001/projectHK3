import { Outlet, Link } from "react-router-dom";

export default function AdminLayout() {
  return (
    <div style={{ display: "flex" }}>
      {/* Sidebar */}
      <aside style={{ width: "200px", background: "#222", color: "white", minHeight: "100vh", padding: "1rem" }}>
        <h2>Admin Panel</h2>
        <nav>
          <Link to="/admin" style={{ display: "block", color: "white", marginBottom: "1rem" }}>Dashboard</Link>
          <Link to="/admin/users" style={{ display: "block", color: "white" }}>Users</Link>
        </nav>
      </aside>

      {/* Main content */}
      <main style={{ flex: 1, padding: "2rem" }}>
        <Outlet />
      </main>
    </div>
  );
}
