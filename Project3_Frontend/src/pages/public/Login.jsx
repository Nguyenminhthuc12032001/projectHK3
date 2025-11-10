import { useNavigate } from "react-router-dom";

export default function Login() {
  const navigate = useNavigate();

  function handleLogin(role) {
    // Giả lập token và role
    localStorage.setItem("token", "fake-jwt-token");
    localStorage.setItem("role", role);

    // Điều hướng theo role
    if (role === "admin") navigate("/admin");
    else if (role === "employee") navigate("/employee");
    else navigate("/");
  }

  return (
    <div style={{ padding: "40px", textAlign: "center" }}>
      <h1>🔐 Login Mock</h1>
      <button onClick={() => handleLogin("admin")}>Login as Admin</button>
      <button onClick={() => handleLogin("employee")}>Login as Employee</button>
    </div>
  );
}
