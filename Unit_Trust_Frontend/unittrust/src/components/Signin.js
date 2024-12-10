import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import '../styles/signin.css'; // Import the CSS file

const SignIn = () => {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post("https://localhost:7182/api/user/login", formData);
      alert(response.data.message);
      navigate("/InvestorTable"); // Redirect to the Investor page after login
    } catch (error) {
      alert(error.response.data.message || "Invalid username or password");
    }
  };

  return (
    <form className="login-form" onSubmit={handleSubmit}>
      <h2>SIGN IN</h2>
      <div className="form-field">
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={formData.email}
          onChange={handleChange}
          required
        />
      </div>
      <div className="form-field">
        <input
          type="password"
          name="password"
          placeholder="Password"
          value={formData.password}
          onChange={handleChange}
          required
        />
      </div>
      <button type="submit" className="submit-button">Login</button>
    </form>
  );
};

export default SignIn;
