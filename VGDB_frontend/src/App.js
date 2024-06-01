import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import HomePage from "./HomePage"; 
import AddGame from "./AddGame"; 
import GameDetail from "./GameDetail"; 

// layout of the website and its pages
const App = () => (
  <Router>
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/add-game" element={<AddGame />} />
      <Route path="/game/:gameId" element={<GameDetail />} />
    </Routes>
  </Router>
);

export default App;
