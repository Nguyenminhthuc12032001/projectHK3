import { Outlet, Link } from "react-router-dom";

export default function PublicLayout() {
  return (
    <div>
      <header style={{ padding: "1rem", background: "#0077cc", color: "white" }}>
        <h1>My Website</h1>
        <nav>
          <Link to="/" style={{ marginRight: "1rem", color: "white" }}>Home</Link>
          <Link to="/login" style={{ color: "white" }}>Login</Link>
        </nav>
      </header>

      <main style={{ padding: "2rem" }}>
        <Outlet />
      </main>

      <footer style={{ padding: "1rem", background: "#eee", marginTop: "2rem" }}>
        © 2025 My Website
      </footer>
    </div>
  );
}
