import { useState } from "react";

export default function ContactUs() {
  const [formData, setFormData] = useState({
    name: "",
    role: "",
    comment: "",
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("New feedback submitted:", formData);
    alert("Thank you for your feedback!");
    setFormData({ name: "", role: "", comment: "" });
  };

  const feedbacks = [
    {
      name: "Jolly",
      comment:
        "The service is excellent! I got my health insurance processed quickly and easily.",
      role: "Customer",
      img: "https://i.pravatar.cc/150?u=1",
    },
    {
      name: "Tom",
      comment:
        "Friendly support team and clear information. Highly recommended for families.",
      role: "Client",
      img: "https://i.pravatar.cc/150?u=2",
    },
    {
      name: "Jeery",
      comment:
        "Professional staff and reliable insurance plans. I feel secure with their service.",
      role: "Partner",
      img: "https://i.pravatar.cc/150?u=3",
    },
  ];

  return (
    <section className="bg-blue-50 py-16 px-6">
      <div className="container mx-auto text-center max-w-3xl">
        <h2 className="text-4xl font-bold text-blue-900 mb-10">Customer Feedback</h2>

        {/* Form để gửi feedback */}
        <form
          onSubmit={handleSubmit}
          className="bg-white p-6 rounded-xl shadow-md mb-12 space-y-4"
        >
          <h3 className="text-2xl font-semibold text-blue-800 mb-4">Leave Your Feedback</h3>
          <input
            type="text"
            name="name"
            placeholder="Your Name"
            value={formData.name}
            onChange={handleChange}
            required
            className="w-full border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            type="text"
            name="role"
            placeholder="Your Role (Customer / Client / Partner)"
            value={formData.role}
            onChange={handleChange}
            required
            className="w-full border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <textarea
            name="comment"
            placeholder="Your Feedback"
            rows="4"
            value={formData.comment}
            onChange={handleChange}
            required
            className="w-full border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          ></textarea>
          <button
            type="submit"
            className="bg-blue-900 text-white px-6 py-3 rounded-lg hover:bg-blue-800 transition w-full"
          >
            Submit Feedback
          </button>
        </form>

        {/* Hiển thị feedback cards */}
        <div className="grid md:grid-cols-3 gap-8">
          {feedbacks.map((fb, index) => (
            <div
              key={index}
              className="bg-white rounded-2xl shadow-md p-6 hover:shadow-lg transition"
            >
              <div className="flex flex-col items-center">
                <img
                  src={fb.img}
                  alt={fb.name}
                  className="w-20 h-20 rounded-full object-cover mb-4"
                />
                <h3 className="text-xl font-semibold text-blue-800">{fb.name}</h3>
                <p className="text-sm text-gray-500">{fb.role}</p>
                <p className="text-gray-700 mt-3 italic">"{fb.comment}"</p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
