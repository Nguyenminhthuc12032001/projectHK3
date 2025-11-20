import React, { useState } from "react";

export default function OrderPolicyForm() {
  // 1. State với giá trị ban đầu trống hoặc giá trị mặc định có thể chỉnh sửa
  const [formData, setFormData] = useState({
    policyId: "", 
    policyName: "", 
    policyAmount: "", 
    emi: "", 
    requestDate: "", 
    companyName: "", 
    companyId: "", 
  });

  // 2. Hàm xử lý thay đổi input
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  // 3. Hàm xử lý khi submit form
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Dữ liệu gửi đi:", formData);
    alert("Policy Order Submitted Successfully!");
    // Thêm logic gọi API ở đây
  };

  // 4. Hàm xử lý nút Cancel
  const handleCancel = () => {
    console.log("Hủy bỏ thao tác.");
    // Thêm logic quay lại trang trước hoặc xóa form
  };

  return (
    // Background màu xanh nhẹ giống như trang gốc
    <div className="min-h-screen bg-blue-100/50 pt-10 pb-20">
      
      {/* Container form, căn giữa, chiều rộng cố định */}
      <div className="max-w-xl mx-auto bg-white p-8 shadow-2xl rounded-lg border border-blue-300">
        
        {/* Tiêu đề Form */}
        <h2 className="text-xl font-bold text-blue-800 text-center mb-8 border-b-2 border-blue-400 pb-2">
          Order For Insurence Policy
        </h2>

        {/* Form chính */}
        <form onSubmit={handleSubmit} className="space-y-4">
          
          {/* PolicyId - Giả định vẫn là select box */}
          <div className="flex items-center justify-end">
            <label htmlFor="policyId" className="w-1/3 text-right font-semibold mr-4 text-gray-700">PolicyId:</label>
            <select
              id="policyId"
              name="policyId"
              value={formData.policyId}
              onChange={handleChange}
              className="w-2/3 border border-gray-400 p-2 rounded-md focus:ring-blue-500 focus:border-blue-500"
            >
              <option value="">-- Select ID --</option>
              <option value="2">2</option>
              <option value="4">4</option>
              {/* Thêm các ID chính sách khác */}
            </select>
          </div>
          
          {/* PolicyName */}
          <div className="flex items-center justify-end">
            <label htmlFor="policyName" className="w-1/3 text-right font-semibold mr-4 text-gray-700">PolicyName:</label>
            <input
              type="text"
              id="policyName"
              name="policyName"
              value={formData.policyName}
              onChange={handleChange}
              placeholder="Enter Policy Name" // Thêm placeholder
              className="w-2/3 border border-gray-400 p-2 rounded-md focus:ring-blue-500 focus:border-blue-500"
            />
          </div>

          {/* PolicyAmount */}
          <div className="flex items-center justify-end">
            <label htmlFor="policyAmount" className="w-1/3 text-right font-semibold mr-4 text-gray-700">PolicyAmount:</label>
            <input
              type="text"
              id="policyAmount"
              name="policyAmount"
              value={formData.policyAmount}
              onChange={handleChange}
              placeholder="Enter Amount"
              className="w-2/3 border border-gray-400 p-2 rounded-md focus:ring-blue-500 focus:border-blue-500"
            />
          </div>

          {/* EMI */}
          <div className="flex items-center justify-end">
            <label htmlFor="emi" className="w-1/3 text-right font-semibold mr-4 text-gray-700">EMI:</label>
            <input
              type="text"
              id="emi"
              name="emi"
              value={formData.emi}
              onChange={handleChange}
              placeholder="Enter EMI"
              className="w-2/3 border border-gray-400 p-2 rounded-md focus:ring-blue-500 focus:border-blue-500"
            />
          </div>

          {/* Request Date */}
          <div className="flex items-center justify-end">
            <label htmlFor="requestDate" className="w-1/3 text-right font-semibold mr-4 text-gray-700">Request Date:</label>
            <div className="w-2/3 flex items-center border border-gray-400 rounded-md">
                <input
                    type="date"
                    id="requestDate"
                    name="requestDate"
                    value={formData.requestDate}
                    onChange={handleChange}
                    className="p-2 w-full focus:outline-none focus:ring-1 focus:ring-blue-500"
                />
            </div>
          </div>
          
          {/* CompanyName */}
          <div className="flex items-center justify-end">
            <label htmlFor="companyName" className="w-1/3 text-right font-semibold mr-4 text-gray-700">CompanyName:</label>
            <input
              type="text"
              id="companyName"
              name="companyName"
              value={formData.companyName}
              onChange={handleChange}
              placeholder="Enter Company Name"
              className="w-2/3 border border-gray-400 p-2 rounded-md focus:ring-blue-500 focus:border-blue-500"
            />
          </div>

          {/* CompanyId */}
          <div className="flex items-center justify-end">
            <label htmlFor="companyId" className="w-1/3 text-right font-semibold mr-4 text-gray-700">CompanyId:</label>
            <input
              type="text"
              id="companyId"
              name="companyId"
              value={formData.companyId}
              onChange={handleChange}
              placeholder="Enter Company ID"
              className="w-2/3 border border-gray-400 p-2 rounded-md focus:ring-blue-500 focus:border-blue-500"
            />
          </div>

          {/* Nút Submit và Cancel */}
          <div className="flex justify-center pt-6 space-x-4">
            <button
              type="submit"
              className="bg-blue-600 text-white font-semibold py-2 px-6 rounded-md hover:bg-blue-700 transition duration-150"
            >
              ADD
            </button>
            <button
              type="button"
              onClick={handleCancel}
              className="bg-gray-400 text-white font-semibold py-2 px-6 rounded-md hover:bg-gray-500 transition duration-150"
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}