import { Outlet, Link } from "react-router-dom";

export default function PublicLayout() {
  return (
    <div className="min-h-screen  grid grid-cols-12 grid-rows-3">
      <div className="row-span-1 col-span-12 bg-yellow-500">
              navbar 
            </div>
      <main className="row-span-1 col-span-12 bg-blue-700">
        <Outlet />
      </main>
      <div className="row-span-1 col-span-12 bg-green-700">
              footer
      </div>
    </div>
  );
}
