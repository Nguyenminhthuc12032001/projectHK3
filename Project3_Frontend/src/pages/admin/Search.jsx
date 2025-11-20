import React, { useState } from "react";
import { Search as SearchIcon } from "lucide-react";

export default function SearchPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [searchType, setSearchType] = useState("employee");

  const handleSubmit = (e) => {
    e.preventDefault();
    alert(`Searching "${searchTerm}" in ${searchType}`);
  };

  const buttonClass = (type) =>
    `px-4 py-2 rounded-md font-semibold ${
      searchType === type ? "bg-blue-700 text-white" : "bg-gray-200 text-gray-700 hover:bg-gray-300"
    }`;

  return (
    <div className="container mx-auto p-8">
      <h2 className="text-2xl font-semibold text-blue-700 mb-6 text-center">Global Search</h2>

      {/* Search Type Buttons */}
      <div className="flex justify-center space-x-4 mb-6">
        {["employee", "policy", "company"].map((type) => (
          <button key={type} className={buttonClass(type)} onClick={() => setSearchType(type)}>
            {type.charAt(0).toUpperCase() + type.slice(1)}
          </button>
        ))}
      </div>

      {/* Search Form */}
      <form onSubmit={handleSubmit} className="flex max-w-xl mx-auto">
        <input
          type="text"
          placeholder={`Search for ${searchType}...`}
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="flex-1 border border-gray-300 p-3 rounded-l-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          required
        />
        <button
          type="submit"
          className="px-6 py-3 bg-blue-600 text-white rounded-r-md hover:bg-blue-700 flex items-center"
        >
          <SearchIcon className="w-5 h-5 mr-2" /> Search
        </button>
      </form>

      {/* Search Results Placeholder */}
      <div className="mt-6 max-w-xl mx-auto">
        <p className="text-gray-500 text-center">Search results will appear here.</p>
      </div>
    </div>
  );
}
