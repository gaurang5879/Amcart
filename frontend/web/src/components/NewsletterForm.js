import React, { useState } from "react";

const NewsletterForm = () => {
  const [email, setEmail] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    alert(`Thank you for subscribing, ${email}!`);
  };

  return (
    <form onSubmit={handleSubmit} className="newsletter-form">
      <input
        type="email"
        placeholder="Enter your email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />
      <button type="submit" className="btn btn-primary">
        Subscribe
      </button>
    </form>
  );
};

export default NewsletterForm;
