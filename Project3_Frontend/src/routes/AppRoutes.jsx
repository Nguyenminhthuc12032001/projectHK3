import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";

// Layouts
import AdminLayout from "../layouts/AdminLayout";
import EmployeeLayout from "../layouts/EmployeeLayout";
import PublicLayout from "../layouts/PublicLayout";

// Pages
//Admin Pages
import AdDashboard from "../pages/admin/AdDashboard";

//Employee Pages
import EmDashboard from "../pages/employee/EmDashboard";


//Public Pages
import Home from "../pages/public/Home";
import Forbidden from "../pages/public/Forbidden";
import Login from "../pages/public/Login";



export default function AppRoutes() {
  return (
    <Routes>
      {/* Public */}
      <Route element={<PublicLayout />}>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/403" element={<Forbidden />} />

      </Route>

      {/* Employee Protected Routes */}
      <Route element={<ProtectedRoute roles={["employee"]} />}>
        <Route element={<EmployeeLayout />}>
          <Route path="/employee" element={<EmDashboard />} />      
        </Route>
      </Route>

      {/* Admin Protected Routes */}
      <Route element={<ProtectedRoute roles={["admin"]} />}>
        <Route element={<AdminLayout />}>
          <Route path="/admin" element={<AdDashboard />} />
        </Route>
      </Route>
    </Routes>
  );
}
