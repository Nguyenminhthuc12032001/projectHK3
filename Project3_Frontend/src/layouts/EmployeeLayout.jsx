import { Outlet} from "react-router-dom";

export default function EmployeeLayout() {

  return (
    <div className="flex">
      <aside className="w-60 bg-gray-800 text-white p-4">Employee Sidebar</aside>
      <main className="flex-1 p-6 bg-gray-100">
        <Outlet />
      </main>
    </div>
  );
}