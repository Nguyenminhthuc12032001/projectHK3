export default function Footer() {
  return (
    <footer className="bg-blue-900 text-white mt-10 py-8">
      <div className="container mx-auto px-6 grid grid-cols-1 md:grid-cols-3 gap-6">
        {/* Cột 1 */}
        <div>
          <h2 className="text-lg font-semibold mb-3">About Us</h2>
          <p className="text-sm text-gray-200">
            InsuranceCo cung cấp các gói bảo hiểm sức khỏe, nhân thọ và tai nạn với cam kết bảo vệ bạn và gia đình.
          </p>
        </div>

        {/* Cột 2 */}
        <div>
          <h2 className="text-lg font-semibold mb-3">Quick Links</h2>
          <ul className="space-y-2 text-sm">
            <li><a href="/" className="hover:text-yellow-300">Home</a></li>
            <li><a href="/about" className="hover:text-yellow-300">About</a></li>
            <li><a href="/policies" className="hover:text-yellow-300">Policies</a></li>
            <li><a href="/claims" className="hover:text-yellow-300">Claims</a></li>
          </ul>
        </div>

        {/* Cột 3 */}
        <div>
          <h2 className="text-lg font-semibold mb-3">Contact Us</h2>
          <ul className="text-sm text-gray-200 space-y-2">
            <li>📍 123 Đường Lê Lợi, TP.HCM</li>
            <li>📞 (028) 1234 5678</li>
            <li>📧 support@insuranceco.vn</li>
          </ul>
        </div>
      </div>

      <div className="border-t border-gray-700 mt-6 pt-4 text-center text-sm text-gray-300">
        © {new Date().getFullYear()} InsuranceCo. All rights reserved.
      </div>
    </footer>
  );
}
