import React, { useState } from "react";

// --- Add Company Form ---
const AddCompanyForm = () => {
  const handleSubmit = (e) => { e.preventDefault(); alert("Insurance Company added successfully!"); };

  const renderField = (label, id, isTextarea = false) => (
    <div className={`flex ${isTextarea ? 'items-start' : 'items-center'} mb-4`}>
      <label htmlFor={id} className="w-40 text-right pr-4 text-sm font-semibold text-gray-700">{label}:</label>
      {isTextarea ? (
        <textarea id={id} rows="3" className="flex-1 border p-2 rounded" />
      ) : (
        <input type="text" id={id} className="flex-1 border p-2 rounded" />
      )}
    </div>
  );

  return (
    <form onSubmit={handleSubmit} className="max-w-lg mx-auto bg-white p-6 rounded-lg shadow-inner">
      <h3 className="text-xl font-semibold mb-4 text-blue-700">Add Insurance Company</h3>
      {renderField("Company Name", "companyName")}
      {renderField("Address", "address", true)}
      {renderField("Phone Number", "phoneNo")}
      {renderField("Company URL", "companyUrl")}
      <div className="flex justify-center space-x-4 pt-4">
        <button type="submit" className="px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700">Add</button>
        <button type="button" className="px-6 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400">Cancel</button>
      </div>
    </form>
  );
};

// --- Add Policy Form ---
const AddPolicyForm = () => {
  const handleSubmit = (e) => { e.preventDefault(); alert("Policy added successfully!"); };
  const companies = ["Birla", "Marga Darsi"];

  const renderField = (label, id, isSelect = false) => (
    <div className="flex items-center mb-4">
      <label htmlFor={id} className="w-40 text-right pr-4 text-sm font-semibold text-gray-700">{label}:</label>
      {isSelect ? (
        <select id={id} className="flex-1 border p-2 rounded">
          {companies.map(c => <option key={c}>{c}</option>)}
        </select>
      ) : (
        <input type="text" id={id} className="flex-1 border p-2 rounded" />
      )}
    </div>
  );

  return (
    <form onSubmit={handleSubmit} className="max-w-lg mx-auto bg-white p-6 rounded-lg shadow-inner">
      <h3 className="text-xl font-semibold mb-4 text-blue-700">Add Policy</h3>
      {renderField("Policy Type Name", "policyTypeName")}
      {renderField("Policy Description", "policyDesc")}
      {renderField("Policy Amount", "policyAmount")}
      {renderField("Policy EMI", "policyEmi")}
      {renderField("Company Name", "companyId", true)}
      {renderField("Medical ID", "medicalId")}
      <div className="flex justify-center space-x-4 pt-4">
        <button type="submit" className="px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700">Add</button>
        <button type="button" className="px-6 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400">Cancel</button>
      </div>
    </form>
  );
};

// --- AddResource Component ---
export default function AddResource() {
  const [activeTab, setActiveTab] = useState("company");

  const buttonClass = (tab) =>
    `px-6 py-2 font-semibold transition-colors duration-200 ${
      activeTab === tab ? "bg-blue-700 text-white shadow-lg" : "bg-gray-200 text-gray-700 hover:bg-gray-300 border border-gray-400"
    }`;

  return (
    <div className="container mx-auto px-6 py-8">
      <h2 className="text-2xl font-semibold text-blue-700 mb-6 text-center">Add Resource</h2>

      <div className="flex justify-center mb-8 space-x-4">
        <button onClick={() => setActiveTab("company")} className={buttonClass("company")}>Add Company</button>
        <button onClick={() => setActiveTab("policy")} className={buttonClass("policy")}>Add Policy</button>
      </div>

      {activeTab === "company" && <AddCompanyForm />}
      {activeTab === "policy" && <AddPolicyForm />}
    </div>
  );
}
