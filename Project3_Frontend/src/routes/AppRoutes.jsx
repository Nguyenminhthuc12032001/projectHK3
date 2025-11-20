
import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";

// Layouts
import AdminLayout from "../layouts/AdminLayout";
import EmployeeLayout from "../layouts/EmployeeLayout";
import PublicLayout from "../layouts/PublicLayout";

// Admin Pages
import AdDashboard from "../pages/admin/AdDashboard";
import AddResource from "../pages/admin/AddResource";
import EmployeeSupport from "../pages/admin/EmployeeSupport"; // Đổi tên file chuẩn
import RequestStatus from "../pages/admin/RequestStatus";
import SearchA from "../pages/admin/Search";

// Employee Pages
import EmDashboard from "../pages/employee/EmDashboard";
import EmployeeDetails from "../pages/employee/EmployeeDetails";
import Search from "../pages/employee/Search";
import OrderForInsurance from "../pages/employee/OrderForIssurence";

// Public Pages
import Home from "../pages/public/Home";
import Forbidden from "../pages/public/Forbidden";
import Login from "../pages/public/Login";
import AboutUs from "../pages/public/AboutUs";
import ContactUs from "../pages/public/ContactUs";
import Register from "../pages/public/Register";
import Feedback from "../pages/public/Feedback";

export default function AppRoutes() {
  return (
    <Routes>
      {/* Public Pages */}
      <Route element={<PublicLayout />}>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/403" element={<Forbidden />} />
        <Route path="/about" element={<AboutUs />} />
        <Route path="/contact" element={<ContactUs />} />
        <Route path="/register" element={<Register />} />
        <Route path="/feedback" element={<Feedback />} />
      </Route>

      {/* Employee Protected Routes */}
      <Route
        path="/employee"
        element={
          <ProtectedRoute requiredRole="User">
            <EmployeeLayout />
          </ProtectedRoute>
        }
      >
        <Route index element={<EmDashboard />} />
        <Route path="details" element={<EmployeeDetails />} />
        <Route path="search" element={<Search />} />
        <Route path="orderforinsurance" element={<OrderForInsurance />} />
      </Route>

      {/* Admin Protected Routes */}
      <Route
        path="/admin"
        element={
          <ProtectedRoute requiredRole="Admin">
            <AdminLayout />
          </ProtectedRoute>
        }
      >
        <Route index element={<AdDashboard />} />          
        <Route path="dashboard" element={<AdDashboard />} />  
        <Route path="resource" element={<AddResource />} />
        <Route path="search" element={<SearchA />} />
        <Route path="support" element={<EmployeeSupport />} /> 
        <Route path="status" element={<RequestStatus />} />
      </Route>

      {/* Catch-all route */}
      <Route path="*" element={<Forbidden />} />
    </Routes>
  );
}
