import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './HomePage';  // Ensure the correct path
import AddGame from './AddGame';    // Ensure the correct path
import GameDetail from './GameDetail';  // Ensure the correct path

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
