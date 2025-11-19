import React, { useState } from 'react';
import ModernLayout from './AdDashboard'; // Sử dụng Layout hiện đại

// --- Component Con: Form Đăng ký Nhân viên ---
const EmployeeRegisterForm = () => {
    // ... (logic form tương tự EmployeeRegisterForm.jsx đã làm)
    const handleSubmit = (e) => { e.preventDefault(); alert('Đã đăng ký nhân viên mới!'); };
    
    // Helper function để render các trường form
    const formFields = [
        { label: 'Designation', key: 'designation', type: 'text', value: 'manager' },
        { label: 'Join Date', key: 'joinDate', type: 'text', value: '1/30/2008' },
        { label: 'Salary', key: 'salary', type: 'text', value: '29000' },
        // ... (thêm các trường khác)
        { label: 'Address', key: 'address', type: 'textarea', value: 'near rama temple' },
        { label: 'Contact No', key: 'contactNo', type: 'text', value: '9867456123' },
        // ... (các trường State, Country, City)
    ];

    const renderField = (field) => (
        <div key={field.key} className={`flex ${field.type === 'textarea' ? 'items-start' : 'items-center'} mb-4`}>
            <label htmlFor={field.key} className="w-40 text-right pr-4 text-sm font-semibold text-gray-700">
                {field.label}:
            </label>
            {field.type === 'textarea' ? (
                <textarea id={field.key} name={field.key} rows="3" defaultValue={field.value} className="flex-1 border border-gray-300 p-2 bg-white rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 resize-none"></textarea>
            ) : (
                <input type={field.type} id={field.key} name={field.key} defaultValue={field.value} className="flex-1 border border-gray-300 p-2 bg-white rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"/>
            )}
        </div>
    );

    return (
        <form onSubmit={handleSubmit} className="space-y-2 w-full max-w-lg mx-auto bg-white p-6 rounded-lg shadow-inner">
            <h3 className="text-xl font-semibold mb-4 text-blue-700">Employee Register</h3>
            {formFields.map(renderField)}
            
            <div className="flex justify-center pt-4 space-x-4">
                <button type="submit" className="px-6 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition duration-200 shadow-md">Add</button>
                <button type="button" className="px-6 py-2 bg-gray-300 text-gray-800 rounded-md hover:bg-gray-400 transition duration-200 shadow-md">Cancel</button>
            </div>
        </form>
    );
};

// --- Component Con: Bảng Chi tiết Nhân viên ---
const EmployeeDetailsTable = () => {
    // ... (logic bảng tương tự EmployeeDetailsTable.jsx đã làm)
    const employeeData = [
        { id: 102, name: 'emp', phone: '9899018427', address: 'RTC X Road', joinDate: '12/7/2002 12:00:00 AM', designation: 'Project Leader', salary: '27000.0000' },
        { id: 113, name: 'Anji', phone: '0007', address: 'vijayawada', joinDate: '12/12/1990 12:00:00 AM', designation: 'Clerk', salary: '15000.0000' },
        // ...
    ];

    return (
        <div className="overflow-x-auto w-full mx-auto bg-white p-6 rounded-lg shadow-md">
            <h3 className="text-xl font-semibold mb-4 text-blue-700">Employee Details</h3>
            <table className="w-full text-sm border-collapse border border-blue-400">
                <thead>
                    <tr className="bg-blue-600 text-white font-bold border border-blue-700">
                        {['EmpId', 'EmployeeName', 'PhoneNO', 'Address', 'JoinDate', 'Disgnation', 'Salary', 'Edit/Delete'].map(header => (
                            <th key={header} className="p-2 border border-blue-700">{header}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {employeeData.map((emp) => (
                        <tr key={emp.id} className="hover:bg-blue-50 transition duration-150 border border-blue-400">
                            <td className="p-2 border border-blue-400 text-center">{emp.id}</td>
                            <td className="p-2 border border-blue-400">{emp.name}</td>
                            <td className="p-2 border border-blue-400 text-center">{emp.phone}</td>
                            <td className="p-2 border border-blue-400">{emp.address}</td>
                            <td className="p-2 border border-blue-400 text-xs whitespace-nowrap">{emp.joinDate}</td>
                            <td className="p-2 border border-blue-400">{emp.designation}</td>
                            <td className="p-2 border border-blue-400 text-right">{emp.salary}</td>
                            <td className="p-2 border border-blue-400 text-center whitespace-nowrap">
                                <a href={`/edit-emp/${emp.id}`} className="text-blue-600 hover:text-blue-800 underline mr-2">Edit</a>
                                <a href={`/delete-emp/${emp.id}`} className="text-red-600 hover:text-red-800 underline">Delete</a>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};


// === Component Chính: EmployeeSuport.jsx ===
export default function EmployeeSuport() {
    const [activeTab, setActiveTab] = useState('details'); // Mặc định hiển thị bảng chi tiết

    const buttonClass = (tabName) => 
        `px-6 py-2 font-semibold transition-colors duration-200 ${
            activeTab === tabName 
            ? 'bg-blue-700 text-white shadow-lg' 
            : 'bg-gray-200 text-gray-700 hover:bg-gray-300 border border-gray-400'
        }`;

    return (
        <ModernLayout title="Employee Support">
            <div className="flex justify-center mb-8 space-x-4">
                <button onClick={() => setActiveTab('details')} className={buttonClass('details')}>
                    Employee Details
                </button>
                <button onClick={() => setActiveTab('register')} className={buttonClass('register')}>
                    Employee Register
                </button>
            </div>

            {activeTab === 'register' && <EmployeeRegisterForm />}
            {activeTab === 'details' && <EmployeeDetailsTable />}

        </ModernLayout>
    );
}