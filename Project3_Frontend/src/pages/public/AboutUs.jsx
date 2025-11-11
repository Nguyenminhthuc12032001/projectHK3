export default function AboutUs() {
  return (
    <section className="bg-white py-16 px-6">
      <div className="container mx-auto grid md:grid-cols-2 gap-10 items-center">
        {/* Text Section */}
        <div>
          <h2 className="text-4xl font-bold text-blue-900 mb-4">About Our Company</h2>
          <p className="text-gray-700 mb-4">
            At <span className="font-semibold">Medicare Insurance</span>, we have been
            providing trusted health and life insurance services for over 10 years.
            Our mission is to deliver protection and peace of mind for individuals,
            families, and businesses.
          </p>
          <p className="text-gray-700 mb-6">
            We pride ourselves on transparency, reliability, and customer satisfaction.
            With a strong team of professionals and partners, we ensure your health and
            financial safety at every stage of life.
          </p>
          <button className="bg-blue-900 text-white px-6 py-3 rounded-lg hover:bg-blue-800 transition">
            Learn More
          </button>
        </div>

        {/* Image Section */}
        <div className="flex justify-center">
          <img
            src="/aboutus.jpg"
            alt="Insurance Team"
            className="rounded-2xl shadow-lg w-full max-w-md"
          />
        </div>
      </div>
    </section>
  );
}
