import React from "react";
import '../styles/home.css';  // Import the Home component style

const Home = () => {
    console.log("Home component rendered");
    return (
        <div className="home-container">
            <h2>Welcome To Unit Trust Management System</h2>
            <div>
                <button>
                    <a href="/signin">SIGN IN</a>
                </button>
            </div><br />
            <div>
                <button>
                    <a href="/signup">SIGN UP</a>
                </button>
            </div>
        </div>
    );
};

export default Home;
