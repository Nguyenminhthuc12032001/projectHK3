import { Outlet, Link } from "react-router-dom";
import Navbar from "../components/Navbar.jsx"
import Footer from "../components/Footer.jsx"



export default function PublicLayout() {
  return (
    <div>
      <Navbar></Navbar>
      <main className="">
        <Outlet />
      </main>
      <div className="">
        <Footer></Footer>
      </div>
    </div>
  );
}
