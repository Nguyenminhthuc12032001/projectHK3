import React, { useState } from 'react';
import ModernLayout from './AdDashboard'; 
import { Search as SearchIcon } from 'lucide-react';

export default function Search() {
    const [searchTerm, setSearchTerm] = useState('');
    const [searchType, setSearchType] = useState('employee');

    const handleSubmit = (e) => {
        e.preventDefault();
        alert(`Đang tìm kiếm "${searchTerm}" trong danh mục ${searchType}...`);
        // Logic gọi API tìm kiếm sẽ nằm ở đây
    };

    return (
        <ModernLayout title="Search"> 
            <div className="flex justify-center flex-col items-center">
                <div className="w-full max-w-xl bg-white p-8 rounded-lg shadow-md">
                    <h3 className="text-xl font-semibold mb-4 text-blue-700 text-center">Global Search</h3>
                    
                    {/* Chọn Loại Tìm kiếm */}
                    <div className="flex justify-center space-x-4 mb-6">
                        <label className="flex items-center space-x-2">
                            <input type="radio" value="employee" checked={searchType === 'employee'} onChange={(e) => setSearchType(e.target.value)} className="text-blue-600 focus:ring-blue-500" />
                            <span className="text-gray-700">Employee</span>
                        </label>
                        <label className="flex items-center space-x-2">
                            <input type="radio" value="policy" checked={searchType === 'policy'} onChange={(e) => setSearchType(e.target.value)} className="text-blue-600 focus:ring-blue-500" />
                            <span className="text-gray-700">Policy</span>
                        </label>
                        <label className="flex items-center space-x-2">
                            <input type="radio" value="company" checked={searchType === 'company'} onChange={(e) => setSearchType(e.target.value)} className="text-blue-600 focus:ring-blue-500" />
                            <span className="text-gray-700">Company</span>
                        </label>
                    </div>

                    {/* Form Tìm kiếm */}
                    <form onSubmit={handleSubmit} className="flex space-x-3">
                        <input
                            type="text"
                            placeholder={`Search for ${searchType}...`}
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                            className="flex-1 border border-gray-300 p-3 bg-white rounded-l-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                            required
                        />
                        <button 
                            type="submit" 
                            className="px-6 py-3 bg-blue-600 text-white rounded-r-md hover:bg-blue-700 transition duration-200 shadow-md flex items-center"
                        >
                            <SearchIcon className="w-5 h-5 mr-2" /> Search
                        </button>
                    </form>
                    
                    {/* Khu vực hiển thị kết quả tìm kiếm (có thể thêm component khác ở đây) */}
                    {/* Ví dụ: {searchResults.length > 0 && <SearchResultsTable data={searchResults} />} */}

                </div>
            </div>
        </ModernLayout>
    );
}