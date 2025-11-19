import React, { useState } from "react";

// Dữ liệu Chính sách (giữ nguyên từ lần trước)
const policyData = [
  // ... (Dữ liệu policyData giữ nguyên như câu trả lời trước)
  { PolicyId: 1, PolicyName: "welfare policy", PolicyDesc: "dsftlsdklfjl;asfjl;sdk;djk", Amount: "100000.00", EMI: "1000.00", CompanyName: "Bajaj Allianj", CompanyDetails: "CompanyDetails" },
  { PolicyId: 2, PolicyName: "retainment Plans", PolicyDesc: "dsftlsdklfjl;asfjl;sdk;djk", Amount: "100000.00", EMI: "1000.00", CompanyName: "Bajaj Allianj", CompanyDetails: "CompanyDetails" },
  { PolicyId: 4, PolicyName: "Health Care", PolicyDesc: "health care for employee", Amount: "150000.00", EMI: "1500.00", CompanyName: "Reliance", CompanyDetails: "CompanyDetails" },
  { PolicyId: 5, PolicyName: "Children Education Plan", PolicyDesc: "Children Education Plan", Amount: "200000.00", EMI: "2000.00", CompanyName: "Reliance", CompanyDetails: "CompanyDetails" },
  { PolicyId: 6, PolicyName: "Accident Relief", PolicyDesc: "accidentrelief", Amount: "500000.00", EMI: "3000.00", CompanyName: "Birla", CompanyDetails: "CompanyDetails" },
  { PolicyId: 7, PolicyName: "welfare policy", PolicyDesc: "welfare policy", Amount: "100000.00", EMI: "1000.00", CompanyName: "Anp Sanmar", CompanyDetails: "CompanyDetails" },
  { PolicyId: 15, PolicyName: "lic", PolicyDesc: "nothing", Amount: "12000.00", EMI: "1000.00", CompanyName: "Reliance", CompanyDetails: "CompanyDetails" },
  { PolicyId: 16, PolicyName: "hospital", PolicyDesc: "Nothing", Amount: "2000000.00", EMI: "213.00", CompanyName: "Anp Sanmar", CompanyDetails: "CompanyDetails" },
  { PolicyId: 17, PolicyName: "hospital", PolicyDesc: "Nothing", Amount: "2000000.00", EMI: "213.00", CompanyName: "Anp Sanmar", CompanyDetails: "CompanyDetails" }
];

// Dữ liệu Chi tiết Công ty Mẫu
const companyDetailsData = {
  "Bajaj Allianj": {
    CompanyId: 1,
    CompanyName: "Bajaj Allianj",
    Address: "Mumbai",
    Phone: "9396488260",
    CompanyUrl: "www.BajajAllianj.com"
  },
  "Reliance": {
    CompanyId: 2,
    CompanyName: "Reliance Insurance",
    Address: "New Delhi",
    Phone: "91xxxxxxxx",
    CompanyUrl: "www.Reliance.com"
  },
  "Birla": {
    CompanyId: 3,
    CompanyName: "Birla Life",
    Address: "Chennai",
    Phone: "92xxxxxxxx",
    CompanyUrl: "www.Birla.com"
  },
  "Anp Sanmar": {
    CompanyId: 4,
    CompanyName: "Anp Sanmar Solutions",
    Address: "Bangalore",
    Phone: "94xxxxxxxx",
    CompanyUrl: "www.AnpSanmar.com"
  }
};

const headers = [
  "PolicyId",
  "PolicyName",
  "PolicyDesc",
  "Amount",
  "EMI",
  "CompanyName",
  "CompanyDetails"
];

export default function PolicyDetails() {
  // State để lưu thông tin chi tiết công ty được chọn (hoặc null nếu chưa chọn)
  const [selectedCompany, setSelectedCompany] = useState(null);

  // Hàm xử lý khi click vào liên kết "CompanyDetails"
  const handleDetailsClick = (companyName) => {
    const details = companyDetailsData[companyName];
    // Nếu chi tiết công ty đã hiển thị, click lần nữa sẽ ẩn đi (hoặc hiển thị chi tiết mới)
    if (selectedCompany && selectedCompany.CompanyName === companyName) {
        setSelectedCompany(null);
    } else {
        setSelectedCompany(details);
    }
  };

  return (
    <div className="min-h-screen bg-blue-100/50 p-4 sm:p-6 lg:p-8">
      {/* Tiêu đề */}
      <div className="max-w-7xl mx-auto mb-6 text-center">
        <h2 className="text-3xl font-bold text-blue-700 p-2 border-b-2 border-blue-700 inline-block">
          Policy Details
        </h2>
      </div>

      {/* Vùng chứa bảng */}
      <div className="max-w-7xl mx-auto bg-white border border-gray-300 shadow-md overflow-x-auto">
        <table className="min-w-full divide-y divide-gray-200">
          
          {/* Tiêu đề bảng */}
          <thead className="bg-blue-100">
            <tr>
              {headers.map((header) => (
                <th
                  key={header}
                  className="px-6 py-3 text-left text-xs font-bold text-blue-800 uppercase tracking-wider border-b border-gray-300"
                >
                  {header}
                </th>
              ))}
            </tr>
          </thead>

          {/* Nội dung bảng */}
          <tbody className="bg-white divide-y divide-gray-200">
            {policyData.map((policy) => (
              <tr key={policy.PolicyId} className="hover:bg-blue-50">
                <td className="px-6 py-3 whitespace-nowrap text-sm font-medium text-gray-900 border-r border-gray-200">
                  {policy.PolicyId}
                </td>
                <td className="px-6 py-3 whitespace-nowrap text-sm text-gray-700 border-r border-gray-200">
                  {policy.PolicyName}
                </td>
                <td className="px-6 py-3 text-sm text-gray-700 border-r border-gray-200">
                  {policy.PolicyDesc}
                </td>
                <td className="px-6 py-3 whitespace-nowrap text-sm text-gray-700 text-right border-r border-gray-200">
                  {policy.Amount}
                </td>
                <td className="px-6 py-3 whitespace-nowrap text-sm text-gray-700 text-right border-r border-gray-200">
                  {policy.EMI}
                </td>
                <td className="px-6 py-3 whitespace-nowrap text-sm text-gray-700 border-r border-gray-200">
                  {policy.CompanyName}
                </td>
                <td className="px-6 py-3 whitespace-nowrap text-sm text-center">
                  <a
                    href="#"
                    className="text-blue-600 hover:text-blue-800 font-medium underline"
                    // Gán hàm xử lý click, truyền CompanyName vào
                    onClick={(e) => {
                      e.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>
                      handleDetailsClick(policy.CompanyName);
                    }}
                  >
                    {policy.CompanyDetails}
                  </a>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      
      {/* Vùng hiển thị Chi tiết Công ty (chỉ hiển thị khi có selectedCompany) */}
      {selectedCompany && (
        <div className="max-w-xl mx-auto mt-6 p-4 bg-gray-100 border border-gray-400 shadow-xl rounded-lg text-center">
          <h3 className="text-xl font-bold text-blue-700 mb-3 border-b border-blue-400 inline-block p-1">
            Company Details
          </h3>
          <div className="text-left font-mono space-y-1">
            <p><strong>CompanyId:</strong> {selectedCompany.CompanyId}</p>
            <p><strong>CompanyName:</strong> {selectedCompany.CompanyName}</p>
            <p><strong>Address:</strong> {selectedCompany.Address}</p>
            <p><strong>Phone:</strong> {selectedCompany.Phone}</p>
            <p><strong>CompanyUrl:</strong> <a href={`http://${selectedCompany.CompanyUrl}`} target="_blank" rel="noopener noreferrer" className="text-blue-600 hover:underline">{selectedCompany.CompanyUrl}</a></p>
          </div>
        </div>
      )}

      {/* Nút "Apply" mô phỏng */}
      <div className="max-w-7xl mx-auto mt-6 text-center">
        <a href="#" className="text-blue-700 text-lg hover:underline font-semibold">
          Apply
        </a>
      </div>
      
      {/* Footer mô phỏng */}
      <div className="max-w-7xl mx-auto mt-8 bg-gray-700 text-white text-center p-2 text-sm">
        © All Rights Reserved.
      </div>
    </div>
  );
}