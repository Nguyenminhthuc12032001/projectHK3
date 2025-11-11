export default function ContactUs() {
  const feedbacks = [
    {
      name: "Nguyen Van A",
      comment:
        "The service is excellent! I got my health insurance processed quickly and easily.",
      role: "Customer",
      img: "/user1.jpg",
    },
    {
      name: "Tran Thi B",
      comment:
        "Friendly support team and clear information. Highly recommended for families.",
      role: "Client",
      img: "/user2.jpg",
    },
    {
      name: "Le Hoang C",
      comment:
        "Professional staff and reliable insurance plans. I feel secure with their service.",
      role: "Partner",
      img: "/user3.jpg",
    },
  ];

  return (
    <section className="bg-blue-50 py-16 px-6">
      <div className="container mx-auto text-center">
        <h2 className="text-4xl font-bold text-blue-900 mb-10">Customer Feedback</h2>

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
