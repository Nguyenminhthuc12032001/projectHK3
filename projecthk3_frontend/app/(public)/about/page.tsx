import Image from "next/image";

export default function AboutUs() {
  return (
    <section className="bg-gray-50 py-16 px-6 md:px-12 lg:px-24">
      <div className="max-w-6xl mx-auto grid md:grid-cols-2 gap-12 items-center">
        {/* Left: Image */}
        <div>
          <Image
            src="/logo-1.png"
            alt="About Medicare Insurance"
            width={600}   
            height={400}   
            className="rounded-2xl shadow-lg w-full"
          />
        </div>

        {/* Right: Content */}
        <div>
          <h2 className="text-3xl font-bold text-blue-900 mb-4">About Us</h2>
          <p className="text-gray-700 leading-7 mb-6">
            At <span className="font-semibold text-blue-700">Medical Care</span>,
            we are dedicated to providing affordable, reliable, and comprehensive
            health insurance solutions to individuals and families. Our mission is
            to ensure that everyone has access to the best healthcare without
            financial stress.
          </p>
          <p className="text-gray-700 leading-7 mb-6">
            With over 10 years of experience in the insurance industry, we work
            closely with trusted hospitals and healthcare providers across Vietnam
            to deliver quality care and fast claim support.
          </p>
          <p className="text-gray-700 leading-7 mb-8">
            We believe that peace of mind comes from knowing you and your loved
            ones are protected — and that’s exactly what we aim to provide.
          </p>

          <a
            href="/contact"
            className="inline-block bg-blue-900 text-white px-6 py-3 rounded-lg hover:bg-blue-800 transition"
          >
            Contact Us
          </a>
        </div>
      </div>
    </section>
  );
}
