import { Outlet, Link } from "react-router-dom";
import Navbar from "../components/Navbar.jsx";
import Footer from "../components/Footer.jsx";
import { NavbarPublic } from "../components/Navbar-public.jsx";
import { AuthProvider } from "../context/AuthContext.jsx";

export default function PublicLayout() {
  return (
    <div>
      <NavbarPublic></NavbarPublic>
      <main className="">
        <Outlet />
      </main>
      <div className="">
        <Footer></Footer>
      </div>
    </div>
  );
}
