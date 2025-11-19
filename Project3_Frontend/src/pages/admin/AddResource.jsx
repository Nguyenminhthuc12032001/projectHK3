import React, { useState } from 'react';
import ModernLayout from './AdDashboard'; // Sử dụng Layout hiện đại

// --- Component Form Con: Thêm Công ty Bảo hiểm ---
const AddCompanyForm = () => {
    // ... (logic form và render fields tương tự AddInsuranceCompanyForm.jsx đã làm)
    const handleSubmit = (e) => { e.preventDefault(); alert('Đã thêm Công ty Bảo hiểm!'); };
    
    // Helper function để render các trường form
    const renderField = (label, id, type = 'text', defaultValue, isTextarea = false) => (
        <div className={`flex ${isTextarea ? 'items-start' : 'items-center'} mb-4`}>
            <label htmlFor={id} className="w-40 text-right pr-4 text-sm font-semibold text-gray-700">
                {label}:
            </label>
            {isTextarea ? (
                <textarea id={id} name={id} rows="3" defaultValue={defaultValue} className="flex-1 border border-gray-300 p-2 bg-white rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 resize-none"></textarea>
            ) : (
                <input type={type} id={id} name={id} defaultValue={defaultValue} className="flex-1 border border-gray-300 p-2 bg-white rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"/>
            )}
        </div>
    );
    
    return (
        <form onSubmit={handleSubmit} className="space-y-2 w-full max-w-lg mx-auto bg-white p-6 rounded-lg shadow-inner">
            <h3 className="text-xl font-semibold mb-4 text-blue-700">Add Insurence Company Details</h3>
            {renderField('Company Name', 'companyName', 'text', 'Marga Darsi')}
            {renderField('Address', 'address', 'text', 'Marga Darsi', true)}
            {renderField('Phone no', 'phoneNo', 'text', '9846098097')}
            {renderField('Company Url', 'companyUrl', 'text', 'www.MargaDarsi.com')}
            <div className="flex justify-center pt-4 space-x-4">
                <button type="submit" className="px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition duration-200 shadow-md">Add</button>
                <button type="button" className="px-6 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400 transition duration-200 shadow-md">Cancel</button>
            </div>
        </form>
    );
};

// --- Component Form Con: Thêm Chính sách Bảo hiểm ---
const AddPolicyForm = () => {
    const handleSubmit = (e) => { e.preventDefault(); alert('Đã thêm Chính sách Bảo hiểm!'); };
    // Danh sách mẫu
    const companies = [{ value: 'Birla', label: 'Birla' }, { value: 'Marga Darsi', label: 'Marga Darsi' }];
    
    const renderField = (label, id, type = 'text', defaultValue, isSelect = false) => (
        <div className={`flex items-center mb-4`}>
            <label htmlFor={id} className="w-40 text-right pr-4 text-sm font-semibold text-gray-700">
                {label}:
            </label>
            {isSelect ? (
                <select id={id} name={id} defaultValue={defaultValue} className="flex-1 border border-gray-300 p-2 bg-white rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500">
                    {companies.map(opt => <option key={opt.value} value={opt.value}>{opt.label}</option>)}
                </select>
            ) : (
                <input type={type} id={id} name={id} defaultValue={defaultValue} className="flex-1 border border-gray-300 p-2 bg-white rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"/>
            )}
        </div>
    );

    return (
        <form onSubmit={handleSubmit} className="space-y-2 w-full max-w-lg mx-auto bg-white p-6 rounded-lg shadow-inner">
            <h3 className="text-xl font-semibold mb-4 text-blue-700">Add Policy Details</h3>
            {renderField('Policy Type Name', 'policyTypeName', 'text', 'abcpolicy')}
            {renderField('Policy Desc', 'policyDesc', 'text', 'abc')}
            {renderField('Policy Amount', 'policyAmount', 'text', '100000')}
            {renderField('Policy EMI', 'policyEmi', 'text', '1000')}
            {renderField('Company Id', 'companyId', 'select', 'Birla', true)}
            {renderField('Medical ID', 'medicalId', 'text', '102145')}

            <div className="flex justify-center pt-4 space-x-4">
                <button type="submit" className="px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition duration-200 shadow-md">ADD</button>
                <button type="button" className="px-6 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400 transition duration-200 shadow-md">Cancel</button>
            </div>
        </form>
    );
};


// === Component Chính: AddResource.jsx ===
export default function AddResource() {
    const [activeTab, setActiveTab] = useState('company');

    const buttonClass = (tabName) => 
        `px-6 py-2 font-semibold transition-colors duration-200 ${
            activeTab === tabName 
            ? 'bg-blue-700 text-white shadow-lg' 
            : 'bg-gray-200 text-gray-700 hover:bg-gray-300 border border-gray-400'
        }`;

    return (
        <ModernLayout title="Add Resource">
            <div className="flex justify-center mb-8 space-x-4">
                <button onClick={() => setActiveTab('company')} className={buttonClass('company')}>
                    Add Insurance Company
                </button>
                <button onClick={() => setActiveTab('policy')} className={buttonClass('policy')}>
                    Add Policy
                </button>
            </div>

            {activeTab === 'company' && <AddCompanyForm />}
            {activeTab === 'policy' && <AddPolicyForm />}

        </ModernLayout>
    );
}