import React from "react";

const SearchBox = ({ onSearch }) => {
  return (
    <div className="search-box">
      <input
        type="text"
        placeholder="Search for products..."
        onChange={(e) => onSearch(e.target.value)}
      />
    </div>
  );
};

export default SearchBox;
