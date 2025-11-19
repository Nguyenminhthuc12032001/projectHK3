import {
  Avatar,
  Dropdown,
  DropdownDivider,
  DropdownHeader,
  DropdownItem,
  Navbar,
  NavbarBrand,
  NavbarCollapse,
  NavbarLink,
  NavbarToggle,
} from "flowbite-react";
import { useAuth } from "../context/AuthContext";

import { useNavigate } from "react-router-dom";

export function NavbarPublic() {
  const { isAuthenticated, hasRole, roles, logout } = useAuth();
  const navigate = useNavigate();
  const styleTextNav =
    "text-white! hover:text-yellow-400! font-semibold! text-xl";

  const handleLogout = async () => {
    console.log("Logout clicked!");
    await logout();
    console.log("Logout success!");
    navigate("/login");
  };
  return (
    <Navbar fluid rounded className="bg-blue-900! text-white! shadow-md">
      <NavbarBrand href="/">
        <img
          src="/src/assets/img/logo.png"
          className="mr-3 h-10 sm:h-12"
          alt="Medicare React Logo"
        />
        <span className="self-center whitespace-nowrap text-2xl font-bold dark:text-white">
          Medicare
        </span>
      </NavbarBrand>

      <div className="flex md:order-2">
        {isAuthenticated ? (
          <>
            <Dropdown
              arrowIcon={false}
              inline
              label={
                <Avatar
                  alt="User settings"
                  img="https://flowbite.com/docs/images/people/profile-picture-5.jpg"
                  rounded
                />
              }
            >
              <DropdownHeader>
                <span className="block text-sm">Bonnie Green</span>
                <span className="block truncate text-sm font-medium">
                  name@flowbite.com
                </span>
              </DropdownHeader>
              <DropdownItem>Dashboard</DropdownItem>
              <DropdownItem>Settings</DropdownItem>
              <DropdownItem>Earnings</DropdownItem>
              <DropdownDivider />
              <DropdownItem onClick={handleLogout}>Sign out</DropdownItem>
            </Dropdown>
            <NavbarToggle />
          </>
        ) : (
          <>
            {/* Nếu chưa login */}
            <NavbarCollapse></NavbarCollapse>
            <NavbarToggle />
          </>
        )}
      </div>
      <NavbarCollapse className="text-center!">
        <NavbarLink href="/" className={styleTextNav}>
          Home
        </NavbarLink>
        <NavbarLink href="/about" className={styleTextNav}>
          About
        </NavbarLink>
        <NavbarLink href="/contact" className={styleTextNav}>
          Contact
        </NavbarLink>
        <NavbarLink href="/feedback" className={styleTextNav}>
          Feedback
        </NavbarLink>
        {isAuthenticated ? (
          <></>
        ) : (
          <>
            {" "}
            <NavbarLink href="/login" className={styleTextNav}>
              Login
            </NavbarLink>
            <NavbarLink href="/register" className={styleTextNav}>
              Register
            </NavbarLink>
          </>
        )}
      </NavbarCollapse>
    </Navbar>
  );
}

export default NavbarPublic;
