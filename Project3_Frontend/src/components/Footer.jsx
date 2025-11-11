export default function Footer() {
  return (
    <footer className="bg-blue-900 text-white mt-10">
      <div className="container mx-auto px-6 py-12 grid md:grid-cols-3 gap-8 items-start">

        {/* 1️⃣ Thông tin công ty */}
        <div>
          <h2 className="text-2xl font-bold mb-4">Medicare Insurance</h2>
          <p>123 Nguyen Trai, Thanh Xuan, Hanoi, Vietnam</p>
          <p>Email: support@medicare.com</p>
          <p>Phone: +84 123 456 789</p>
        </div>

        {/* 2️⃣ Mạng xã hội / Quick links */}
        <div>
          <h2 className="text-2xl font-bold mb-4">Quick Links</h2>
          <ul className="space-y-2">
            <li><a href="/" className="hover:text-yellow-300">Home</a></li>
            <li><a href="/about" className="hover:text-yellow-300">About Us</a></li>
            <li><a href="/contact" className="hover:text-yellow-300">Contact</a></li>
            <li><a href="/login" className="hover:text-yellow-300">Login</a></li>
          </ul>

          <h2 className="text-2xl font-bold mt-6 mb-2">Follow Us</h2>
          <div className="flex space-x-4">
            <a href="#" className="hover:text-yellow-300">Facebook</a>
            <a href="#" className="hover:text-yellow-300">Twitter</a>
            <a href="#" className="hover:text-yellow-300">LinkedIn</a>
          </div>
        </div>

        {/* 3️⃣ Google Map */}
        <div className="rounded-lg overflow-hidden shadow-lg">
          <iframe
            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3723.108787854494!2d105.81771981534926!3d21.00284689250749!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135ab1e2e7efdab%3A0x2b2f28e2296a1f9b!2sNguyen%20Trai%2C%20Thanh%20Xuan%2C%20Hanoi!5e0!3m2!1sen!2s!4v1699538389013!5m2!1sen!2s" 
            width="100%" 
            height="250" 
            style={{ border: 0 }} 
            allowFullScreen="" 
            loading="lazy"
            referrerPolicy="no-referrer-when-downgrade"
            title="Medicare Location"
          ></iframe>
        </div>

      </div>

      {/* Bản quyền */}
      <div className="text-center text-gray-300 mt-6 pb-4 border-t border-gray-700">
        &copy; 2025 Medicare Insurance. All rights reserved.
      </div>
    </footer>
  );
}
