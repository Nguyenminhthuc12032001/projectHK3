import React, { useState } from "react";

// --- Employee Register Form ---
const EmployeeRegisterForm = () => {
  const handleSubmit = (e) => {
    e.preventDefault();
    alert("Employee added successfully!");
  };

  const formFields = [
    { label: "Designation", key: "designation", type: "text", value: "Manager" },
    { label: "Join Date", key: "joinDate", type: "text", value: "1/30/2008" },
    { label: "Salary", key: "salary", type: "text", value: "29000" },
    { label: "Address", key: "address", type: "textarea", value: "Near Rama Temple" },
    { label: "Contact No", key: "contactNo", type: "text", value: "9867456123" },
    { label: "State", key: "state", type: "text", value: "Telangana" },
    { label: "Country", key: "country", type: "text", value: "India" },
    { label: "City", key: "city", type: "text", value: "Vijayawada" },
  ];

  const renderField = (field) => (
    <div
      key={field.key}
      className={`flex ${field.type === "textarea" ? "items-start" : "items-center"} mb-4`}
    >
      <label
        htmlFor={field.key}
        className="w-40 text-right pr-4 text-sm font-semibold text-gray-700"
      >
        {field.label}:
      </label>
      {field.type === "textarea" ? (
        <textarea
          id={field.key}
          defaultValue={field.value}
          rows="3"
          className="flex-1 border p-2 rounded"
        />
      ) : (
        <input
          type={field.type}
          id={field.key}
          defaultValue={field.value}
          className="flex-1 border p-2 rounded"
        />
      )}
    </div>
  );

  return (
    <form
      onSubmit={handleSubmit}
      className="max-w-lg mx-auto bg-white p-6 rounded-lg shadow-inner"
    >
      <h3 className="text-xl font-semibold mb-4 text-blue-700 text-center">
        Employee Register
      </h3>
      {formFields.map(renderField)}
      <div className="flex justify-center space-x-4 pt-4">
        <button
          type="submit"
          className="px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
        >
          Add
        </button>
        <button
          type="button"
          className="px-6 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400"
        >
          Cancel
        </button>
      </div>
    </form>
  );
};

// --- Employee Details Table ---
const EmployeeDetailsTable = () => {
  const employeeData = [
    {
      id: 102,
      name: "Emp",
      phone: "9899018427",
      address: "RTC X Road",
      joinDate: "12/7/2002",
      designation: "Project Leader",
      salary: "27000.00",
    },
    {
      id: 113,
      name: "Anji",
      phone: "0007",
      address: "Vijayawada",
      joinDate: "12/12/1990",
      designation: "Clerk",
      salary: "15000.00",
    },
  ];

  return (
    <div className="overflow-x-auto w-full mx-auto bg-white p-6 rounded-lg shadow-md">
      <h3 className="text-xl font-semibold mb-4 text-blue-700 text-center">
        Employee Details
      </h3>
      <table className="w-full text-sm border-collapse border border-blue-400">
        <thead>
          <tr className="bg-blue-600 text-white font-bold border border-blue-700">
            {[
              "EmpId",
              "Employee Name",
              "Phone No",
              "Address",
              "Join Date",
              "Designation",
              "Salary",
              "Edit/Delete",
            ].map((header) => (
              <th key={header} className="p-2 border border-blue-700">
                {header}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {employeeData.map((emp) => (
            <tr
              key={emp.id}
              className="hover:bg-blue-50 transition duration-150 border border-blue-400"
            >
              <td className="p-2 border border-blue-400 text-center">{emp.id}</td>
              <td className="p-2 border border-blue-400">{emp.name}</td>
              <td className="p-2 border border-blue-400 text-center">{emp.phone}</td>
              <td className="p-2 border border-blue-400">{emp.address}</td>
              <td className="p-2 border border-blue-400 text-xs whitespace-nowrap">
                {emp.joinDate}
              </td>
              <td className="p-2 border border-blue-400">{emp.designation}</td>
              <td className="p-2 border border-blue-400 text-right">{emp.salary}</td>
              <td className="p-2 border border-blue-400 text-center whitespace-nowrap">
                <a
                  href={`/edit-emp/${emp.id}`}
                  className="text-blue-600 hover:text-blue-800 underline mr-2"
                >
                  Edit
                </a>
                <a
                  href={`/delete-emp/${emp.id}`}
                  className="text-red-600 hover:text-red-800 underline"
                >
                  Delete
                </a>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

// --- Employee Support Component ---
export default function EmployeeSupport() {
  const [activeTab, setActiveTab] = useState("details");

  const buttonClass = (tab) =>
    `px-6 py-2 font-semibold transition-colors duration-200 ${
      activeTab === tab
        ? "bg-blue-700 text-white shadow-lg"
        : "bg-gray-200 text-gray-700 hover:bg-gray-300 border border-gray-400"
    }`;

  return (
    <div className="container mx-auto px-6 py-8">
      <h2 className="text-2xl font-semibold text-blue-700 mb-6 text-center">
        Employee Support
      </h2>

      <div className="flex justify-center mb-8 space-x-4">
        <button
          onClick={() => setActiveTab("details")}
          className={buttonClass("details")}
        >
          Employee Details
        </button>
        <button
          onClick={() => setActiveTab("register")}
          className={buttonClass("register")}
        >
          Employee Register
        </button>
      </div>

      {activeTab === "details" && <EmployeeDetailsTable />}
      {activeTab === "register" && <EmployeeRegisterForm />}
    </div>
  );
}
