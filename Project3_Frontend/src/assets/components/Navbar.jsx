import { Link } from "react-router-dom";

export default function Header() {
  return (
    <header className="bg-blue-800 text-white shadow-md">
      <div className="container mx-auto px-6 py-4 flex justify-between items-center">
        <Link to="/" className="text-2xl font-bold">🏥 InsuranceCo</Link>

        <nav className="space-x-6">
          <Link to="/" className="hover:text-yellow-300">Home</Link>
          <Link to="/about" className="hover:text-yellow-300">About</Link>
          <Link to="/policies" className="hover:text-yellow-300">Policies</Link>
          <Link to="/claims" className="hover:text-yellow-300">Claims</Link>
        </nav>

        <div>
          <Link
            to="/login"
            className="bg-yellow-400 text-blue-900 px-4 py-2 rounded-lg hover:bg-yellow-300"
          >
            Login
          </Link>
        </div>
      </div>
    </header>
  );
}
