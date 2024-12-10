import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Signin from '../components/Signin';  // Ensure this path is correct
import Signup from '../components/Signup';  // Ensure this path is correct
import Investor from '../components/Investor';
import Home from '../components/Home';
import InvestorTable from '../components/InvestorTable';


const AppRoutes = () => (
  <Router>
    <Routes>
      <Route path="/" element={<Home />} />  {/* Default route */}
      <Route path="/signin" element={<Signin />} />
      <Route path="/signup" element={<Signup />} />
      <Route path="/investor" element={<Investor />} />
      <Route path="/investorTable" element={<InvestorTable />} />
    </Routes>
  </Router>
);

export default AppRoutes;
