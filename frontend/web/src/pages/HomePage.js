import React from "react";
import ProductCard from "../components/ProductCard";
import SearchBox from "../components/SearchBox";
import CategoryMenu from "../components/CategoryMenu";

function HomePage() {
    const categories = ["Men", "Women", "Kids"];
    const products = [
        { id: 1, title: "ShirtNw", price: 25, image: "/images/shirt.jpg" },
        { id: 2, title: "Pants", price: 40, image: "/images/pants.jpg" },
    ];
    return (
        <div className="container">
            <SearchBox onSearch={(query) => console.log(query)} />
            <CategoryMenu categories={categories} />
            <div className="row">
                {products.map((product) => (
                    <ProductCard key={product.id} {...product} />
                ))}
            </div>
        </div>
    );
}

export default HomePage;
