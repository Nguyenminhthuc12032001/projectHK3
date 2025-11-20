import React from "react";

export default function RequestStatus() {
  const requestData = [
    { 
      id: 29, 
      date: '2/6/2008 12:00:00 AM', 
      empNo: 102, 
      policyId: 2, 
      policyName: 'Retirement Plans', 
      policyAmount: '100000.0000', 
      emi: '1000.0000', 
      companyId: 1, 
      companyName: 'Bajaj Allianz' 
    },
    // Thêm các yêu cầu khác
  ];

  return (
    <div className="container mx-auto p-8">
      <h2 className="text-2xl font-semibold text-blue-700 mb-6 text-center">
        Policy Request Details
      </h2>

      <div className="overflow-x-auto w-full mx-auto bg-white p-6 rounded-lg shadow-md">
        <table className="w-full text-sm border-collapse border border-blue-400">
          {/* Header */}
          <thead>
            <tr className="bg-blue-600 text-white font-bold border border-blue-700">
              {[
                'RequestId', 'RequestDate', 'EmpNo', 'PolicyId', 'PolicyName', 
                'PolicyAmount', 'EMI', 'CompanyId', 'CompanyName', 'Status'
              ].map(header => (
                <th key={header} className="p-2 border border-blue-700 whitespace-nowrap">
                  {header}
                </th>
              ))}
            </tr>
          </thead>

          {/* Body */}
          <tbody>
            {requestData.map(req => (
              <tr key={req.id} className="hover:bg-blue-50 transition duration-150 border border-blue-400">
                <td className="p-2 border border-blue-400 text-center">{req.id}</td>
                <td className="p-2 border border-blue-400 text-xs whitespace-nowrap">{req.date}</td>
                <td className="p-2 border border-blue-400 text-center">{req.empNo}</td>
                <td className="p-2 border border-blue-400 text-center">{req.policyId}</td>
                <td className="p-2 border border-blue-400">{req.policyName}</td>
                <td className="p-2 border border-blue-400 text-right">{req.policyAmount}</td>
                <td className="p-2 border border-blue-400 text-right">{req.emi}</td>
                <td className="p-2 border border-blue-400 text-center">{req.companyId}</td>
                <td className="p-2 border border-blue-400">{req.companyName}</td>
                <td className="p-2 border border-blue-400 text-center whitespace-nowrap">
                  <a href={`/view-policy/${req.id}`} className="text-blue-600 hover:text-blue-800 underline">
                    CheckForStatus
                  </a>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
