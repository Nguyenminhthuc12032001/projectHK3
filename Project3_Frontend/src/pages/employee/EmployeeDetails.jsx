import React, { useState } from "react";

export default function EmployeeDetails() {
  const [employee, setEmployee] = useState({
    firstName: "",
    lastName: "",
    username: "",
    password: "",
    address: "",
    contactNo: "",
    state: "",
    country: "",
    city: ""
  });

  const handleChange = (e) => {
    setEmployee({ ...employee, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(employee);
    alert("Employee Registered Successfully!");
  };

  return (
    <div className="max-w-2xl mx-auto bg-white shadow-lg p-8 rounded-lg">
      <h2 className="text-2xl font-semibold mb-6 text-center text-blue-700">
        Employee Registration
      </h2>

      <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-2 gap-4">
        <input type="text" name="firstName" placeholder="First Name"
          value={employee.firstName} onChange={handleChange}
          className="border p-2 rounded" />
        <input type="text" name="lastName" placeholder="Last Name"
          value={employee.lastName} onChange={handleChange}
          className="border p-2 rounded" />
        <input type="text" name="username" placeholder="Username"
          value={employee.username} onChange={handleChange}
          className="border p-2 rounded" />
        <input type="password" name="password" placeholder="Password"
          value={employee.password} onChange={handleChange}
          className="border p-2 rounded" />
        <input type="text" name="address" placeholder="Address"
          value={employee.address} onChange={handleChange}
          className="border p-2 rounded col-span-2" />
        <input type="text" name="contactNo" placeholder="Contact No"
          value={employee.contactNo} onChange={handleChange}
          className="border p-2 rounded" />
        <input type="text" name="state" placeholder="State"
          value={employee.state} onChange={handleChange}
          className="border p-2 rounded" />
        <input type="text" name="country" placeholder="Country"
          value={employee.country} onChange={handleChange}
          className="border p-2 rounded" />
        <input type="text" name="city" placeholder="City"
          value={employee.city} onChange={handleChange}
          className="border p-2 rounded" />

        <button type="submit" className="col-span-2 bg-blue-700 text-white py-2 rounded hover:bg-blue-800">
          Register
        </button>
      </form>
    </div>
  );
}
