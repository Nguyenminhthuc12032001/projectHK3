import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export function ProtectedRoute({ Component, requiredRole = null }) {
  const { isAuthenticated, hasRole } = useAuth();
  isAuthenticated = true;
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (requiredRole && !hasRole(requiredRole)) {
    return <Navigate to="/forbidden" replace />;
  }

  return <Component />;
}

export default ProtectedRoute;
