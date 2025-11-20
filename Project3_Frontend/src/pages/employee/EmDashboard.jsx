import { Link, Outlet } from "react-router-dom";

export default function EmployeeLayout() {
  return (
    <div className="min-h-screen flex flex-col bg-gray-100">
      
      {/* Logo trên cùng - Giữ nguyên */}
      <div className="flex justify-center py-4 bg-gray-50"></div>

      

      {/* Phần nội dung hiển thị - CHỈ CÒN LẠI <Outlet /> */}
      <main className=" grow flex flex-col items-center justify-center text-center px-4">
    
        <Outlet />
      </main>
    </div>
  );
}