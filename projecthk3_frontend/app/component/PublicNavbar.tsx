"use client";
import Link from "next/link";
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

export default function PublicNavbar() {
  return (
    <Navbar className="bg-blue-900 dark:bg-blue-900"  >
      <NavbarBrand as ={Link} href="/">
        <img src="/logo-1.png" className="mr-4 h-8 sm:h-12" alt="Flowbite React Logo" />
        <span className="self-center whitespace-nowrap text-xl font-semibold dark:text-white">Medical Care</span>
      </NavbarBrand>
      <div className="flex md:order-2">
        <Dropdown
          arrowIcon={false}
          inline
          label={
            <Avatar alt="User settings" img="https://flowbite.com/docs/images/people/profile-picture-5.jpg" rounded />
          }
        >
          <DropdownHeader>
            <span className="block text-sm">Bonnie Green</span>
            <span className="block truncate text-sm font-medium">name@flowbite.com</span>
          </DropdownHeader>
          <DropdownItem>Dashboard</DropdownItem>
          <DropdownItem>Settings</DropdownItem>
          <DropdownItem>Earnings</DropdownItem>
          <DropdownDivider />
          <DropdownItem>Sign out</DropdownItem>
        </Dropdown>
        <NavbarToggle />
      </div>
      <NavbarCollapse>
        <NavbarLink as={Link} href="#" active>
          Home
        </NavbarLink>
        <NavbarLink as={Link} href="/about">About</NavbarLink>
        <NavbarLink as={Link} href="/contact">Contact</NavbarLink>
        <NavbarLink as={Link} href="/feedback">Feedback</NavbarLink>
        <NavbarLink  as={Link} href="/login">Login/Register</NavbarLink>
      </NavbarCollapse>
    </Navbar>
  );
}
