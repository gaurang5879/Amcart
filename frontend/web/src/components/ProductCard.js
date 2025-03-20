import React from "react";

const ProductCard = ({ title, price, image }) => {
  return (
    <div className="card">
      <img src={image} className="card-img-top" alt={title} />
      <div className="card-body">
        <h5 className="card-title">{title}</h5>
        <p className="card-text">${price}</p>
        <a href="/cart" className="btn btn-primary">
          Add to Cart
        </a>
      </div>
    </div>
  );
};

export default ProductCard;
