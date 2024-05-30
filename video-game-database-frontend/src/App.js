import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './HomePage';
import GameDetail from './GameDetail';

const App = () => {
  return (
    <Router>
      <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/game/:gameId" element={<GameDetail />} />
      </Routes>
    </Router>
  );
};

export default App;
