// src/layouts/AdminLayout.jsx

import React, { useState, useEffect } from "react";
import { Outlet, useLocation } from "react-router-dom"; 
import { motion } from "framer-motion";
import { ArrowUp, Menu as MenuIcon, X as CloseIcon } from "lucide-react";

const AdminLayout = () => {
    const location = useLocation(); 
    const [isMenuOpen, setIsMenuOpen] = useState(false); 
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

    // Các mục điều hướng Admin (phải khớp với đường dẫn trong AppRoutes.jsx)
    const navItems = [
        { label: 'Home', link: '/' }, 
        { label: 'Dashboard', link: '/admin' }, 
        { label: 'Add Resource', link: '/admin/resource' },
        { label: 'Employee Support', link: '/admin/suport' },
        { label: 'Search', link: '/admin/search' },
        { label: 'Request Status', link: '/admin/status' },
        { label: 'Log out', link: '/logout' }, 
    ];

    return (
        <div className="bg-white text-gray-800 min-h-screen relative">
            {/* HEADER & NAVIGATION (Phong cách hiện đại) */}
            <header className="bg-gradient-to-r from-blue-700 to-blue-500 text-white py-4 md:py-6 shadow-lg">
                <div className="container mx-auto px-6 flex justify-between items-center">
                    {/* Logo/Tên hệ thống */}
                    <h1 className="text-2xl md:text-3xl font-bold font-serif italic">Medical Care</h1>

                    {/* Nút Hamburger cho Mobile */}
                    <div className="md:hidden">
                        <button onClick={() => setIsMenuOpen(!isMenuOpen)} className="text-white focus:outline-none">
                            {isMenuOpen ? <CloseIcon className="w-8 h-8" /> : <MenuIcon className="w-8 h-8" />}
                        </button>
                    </div>

                    {/* Menu Desktop */}
                    <nav className="hidden md:flex space-x-6 text-lg font-medium">
                        {navItems.map((item) => (
                            <a 
                                key={item.label} 
                                href={item.link} 
                                className={`hover:text-blue-200 transition-colors duration-200 ${location.pathname === item.link ? 'text-blue-200 underline' : ''}`}
                            >
                                {item.label}
                            </a>
                        ))}
                    </nav>
                </div>
                
                {/* Menu Mobile Dropdown (ĐÃ HOÀN THIỆN) */}
                {isMenuOpen && (
                    <motion.nav 
                        initial={{ opacity: 0, y: -20 }} 
                        animate={{ opacity: 1, y: 0 }} 
                        transition={{ duration: 0.3 }}
                        className="md:hidden bg-blue-600 px-6 py-4 mt-2 space-y-3 text-lg"
                    >
                        {navItems.map((item) => (
                            <a 
                                key={item.label} 
                                href={item.link} 
                                className="block text-white hover:text-blue-200 transition-colors duration-200"
                                onClick={() => setIsMenuOpen(false)} 
                            >
                                {item.label}
                            </a>
                        ))}
                    </motion.nav>
                )}
            </header>

            {/* MAIN CONTENT AREA */}
            <main className="py-10">
                <div className="container mx-auto px-6">
                    
                    {/* ĐÂY LÀ NƠI CÁC TRANG ADMIN CON ĐƯỢC RENDER */}
                    <Outlet /> 
                </div>
            </main>

            {/* FOOTER */}
            <footer className="bg-blue-800 text-white py-8 mt-10">
                <div className="container mx-auto px-6 text-center text-sm">
                    <p>&copy; {new Date().getFullYear()} Medical Care. All rights reserved.</p>
                </div>
            </footer>

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
        </div>
    );
};

export default AdminLayout;