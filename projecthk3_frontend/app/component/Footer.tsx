export default function Footer() {
  return (
    <footer className="bg-blue-900 text-white py-10 mt-10">
      <div className="container mx-auto px-6 grid md:grid-cols-3 gap-8">
        {/* Column 1: thong tin cty */}
        <div>
          <h2 className="text-xl font-semibold mb-4">Medicare Insurance</h2>
          <p className="text-sm leading-6">
            We provide comprehensive health insurance plans to help you and your
            family stay protected and secure in every situation.
          </p>
        </div>

        {/* Column 2: lien ket nhanh */}
        <div>
          <h3 className="text-lg font-semibold mb-4">Quick Links</h3>
          <ul className="space-y-2 text-sm">
            <li><a href="/about" className="hover:text-blue-300">About Us</a></li>
            <li><a href="/services" className="hover:text-blue-300">Insurance Plans</a></li>
            <li><a href="/claims" className="hover:text-blue-300">Claims</a></li>
            <li><a href="/contact" className="hover:text-blue-300">Contact</a></li>
          </ul>
        </div>

        {/* Column 3: lien he */}
        <div>
          <h3 className="text-lg font-semibold mb-4">Get in Touch</h3>
          <p className="text-sm">📍 123 Peace Street, District 1, Ho Chi Minh City</p>
          <p className="text-sm">📞 +84 1900 123 456</p>
          <p className="text-sm">📧 support@medicare.vn</p>
          <div className="mt-4 flex space-x-3">
            <a href="#" className="hover:text-blue-300">Facebook</a>
            <a href="#" className="hover:text-blue-300">Zalo</a>
            <a href="#" className="hover:text-blue-300">LinkedIn</a>
          </div>
        </div>
      </div>

      {/* Copyright */}
      <div className="border-t border-blue-700 mt-8 pt-4 text-center text-sm text-blue-200">
        © 2025 Medicare Insurance. All rights reserved.
      </div>
    </footer>
  );
}
