import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";

// Layouts
import AdminLayout from "../layouts/AdminLayout";
import EmployeeLayout from "../layouts/EmployeeLayout";
import PublicLayout from "../layouts/PublicLayout";

// Pages
import AdDashboard from "../pages/admin/AdDashboard";
import EmDashboard from "../pages/employee/EmDashboard";
import EmployeeDetails from "../pages/employee/EmployeeDetails";
import Search from "../pages/employee/Search";
import OrderForIssurence from "../pages/employee/OrderForIssurence";
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

      {/* Employee Protected Pages */}
      <Route element={<ProtectedRoute roles={["employee"]} />}>
        <Route element={<EmployeeLayout />}>
          <Route path="/employee" element={<EmDashboard />} />
          <Route path="/employee/details" element={<EmployeeDetails />} />
          <Route path="/employee/search" element={<Search />} />
          <Route path="/employee/orderforinsurance" element={<OrderForIssurence />} />
        </Route>
      </Route>

      {/* Admin Protected Pages */}
      <Route element={<ProtectedRoute roles={["admin"]} />}>
        <Route element={<AdminLayout />}>
          <Route path="/admin" element={<AdDashboard />} />
        </Route>
      </Route>
    </Routes>
  );
}
