import { Navigate, Outlet } from "react-router-dom";


export default function ProtectedRoute({ roles }) {
    //Phần này lấy token của
    const token = 123456;
 // const token = localStorage.getItem("token");
  const role = "access All role";

  // Nếu chưa login
  if (!token) {
    return <Navigate to="/login" replace />;
  }

  // Nếu có role nhưng không đúng quyền
  if (roles && roles.includes(role)) {
    return <Navigate to="/403" replace />;
  }

  // Nếu hợp lệ → render component con (Outlet)
  return <Outlet />;
}
