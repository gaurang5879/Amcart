import React from "react";

const CategoryMenu = ({ categories }) => {
  return (
    <div className="category-menu">
      <h4>Categories</h4>
      <ul>
        {categories.map((category, index) => (
          <li key={index}>
            <a href={`/category/${category}`}>{category}</a>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CategoryMenu;
