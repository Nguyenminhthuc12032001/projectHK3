import React, { useState, useEffect } from "react";
import { motion } from "framer-motion";
import { ShieldCheck, HeartPulse, BriefcaseMedical, ArrowUp } from "lucide-react";

export default function Home() {
  const [showTopButton, setShowTopButton] = useState(false);

  // Hiển thị nút khi scroll xuống > 300px
  useEffect(() => {
    const handleScroll = () => setShowTopButton(window.scrollY > 300);
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  const scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  return (
    <section className="bg-white text-gray-800 relative">
      {/* Hero Section */}
      <div className="relative bg-gradient-to-r from-blue-700 to-blue-500 text-white py-24">
        <div className="container mx-auto text-center px-6">
          <motion.h1 initial={{ opacity: 0, y: -30 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 1 }} className="text-4xl md:text-6xl font-bold mb-4">
            Welcome to Medical Care Insurance
          </motion.h1>
          <motion.p initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: 0.3, duration: 1 }} className="text-lg md:text-xl mb-8">
            Your trusted partner in protecting health and happiness for every family.
          </motion.p>
        </div>
      </div>

      {/* Info Section */}
      <div className="container mx-auto px-6 py-20 grid md:grid-cols-3 gap-12">
        <motion.div whileHover={{ scale: 1.05 }} className="bg-blue-50 rounded-2xl shadow-md p-8 text-center">
          <ShieldCheck className="mx-auto text-blue-600 w-12 h-12 mb-4" />
          <h3 className="text-xl font-semibold mb-2">Reliable Protection</h3>
          <p>We provide comprehensive health insurance plans ensuring your peace of mind in every situation.</p>
        </motion.div>

        <motion.div whileHover={{ scale: 1.05 }} className="bg-blue-50 rounded-2xl shadow-md p-8 text-center">
          <HeartPulse className="mx-auto text-blue-600 w-12 h-12 mb-4" />
          <h3 className="text-xl font-semibold mb-2">Health First</h3>
          <p>Our mission is to deliver the best healthcare support with a network of trusted medical partners.</p>
        </motion.div>

        <motion.div whileHover={{ scale: 1.05 }} className="bg-blue-50 rounded-2xl shadow-md p-8 text-center">
          <BriefcaseMedical className="mx-auto text-blue-600 w-12 h-12 mb-4" />
          <h3 className="text-xl font-semibold mb-2">Corporate Solutions</h3>
          <p>We offer flexible insurance programs designed specifically for businesses and organizations.</p>
        </motion.div>
      </div>

      {/* Company Info Section */}
      <section className="bg-blue-50 py-20">
        <div className="container mx-auto flex flex-col md:flex-row items-center gap-10 px-6 lg:px-16">
          <motion.div initial={{ opacity: 0, x: -50 }} whileInView={{ opacity: 1, x: 0 }} transition={{ duration: 1 }} className="md:w-1/2">
            <h2 className="text-3xl md:text-4xl font-bold text-blue-800 mb-4">Caring for Life, Securing Your Future</h2>
            <p className="text-gray-700 text-lg mb-6 leading-relaxed">
              With over 10 years of experience in health insurance, Medical Care has been committed to delivering trusted protection and peace of mind for thousands of families and companies across the country.
            </p>
          </motion.div>
          <motion.img src="src/assets/img/home.jpg" alt="Medical Care Office" initial={{ opacity: 0, x: 50 }} whileInView={{ opacity: 1, x: 0 }} transition={{ duration: 1 }} className="rounded-2xl shadow-lg md:w-1/2 object-cover h-[400px]" />
        </div>
      </section>

      {/* Back to Top Button */}
      {showTopButton && (
        <motion.button
          onClick={scrollToTop}
          initial={{ opacity: 0, y: 50 }}
          animate={{ opacity: 1, y: 0 }}
          whileHover={{ scale: 1.1 }}
          whileTap={{ scale: 0.95 }}
          className="fixed bottom-8 right-8 bg-blue-700 text-white p-4 rounded-full shadow-lg z-50"
        >
          <ArrowUp className="w-6 h-6" />
        </motion.button>
      )}
    </section>
  );
}
