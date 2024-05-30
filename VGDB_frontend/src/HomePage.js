import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Styles.css';
import { Container, Row, Col, Button, InputGroup, Input } from 'reactstrap';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const HomePage = () => {
  const navigate = useNavigate();
  const [games, setGames] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    // Fetch games and scores from the backend when the component mounts
    const fetchGamesAndScores = async () => {
      try {
        const gamesResponse = await axios.get('http://localhost:5021/api/Games');
        const scoresResponse = await axios.get('http://localhost:5021/api/GameReviews/Scores');

        const scoresMap = new Map(scoresResponse.data.map(score => [score.gameID, score.score]));

        const gamesWithScores = gamesResponse.data.map(game => ({
          ...game,
          score: scoresMap.get(game.gameID) || 'No score'
        }));

        setGames(gamesWithScores);
      } catch (error) {
        console.error('Error fetching games and scores:', error);
      }
    };

    fetchGamesAndScores();
  }, []);

  const handleAddGameClick = () => {
    // handle add game logic
  };

  const handleViewMoreClick = (gameId) => {
    navigate(`/game/${gameId}`);
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const filterGamesBySearchTerm = (games, searchTerm) => {
    const lowerCaseSearchTerm = searchTerm.toLowerCase();
    return games.filter(game =>
      game.title.toLowerCase().includes(lowerCaseSearchTerm)
    );
  };

  const deleteGame = async (gameId) => {
    try {
      await axios.delete(`http://localhost:5021/api/Games/${gameId}`);
      setGames(games.filter(game => game.gameID !== gameId));
    } catch (error) {
      console.error('Error deleting the game:', error);
    }
  };

  const chunk = (array, size) => {
    const chunkedArr = [];
    for (let i = 0; i < array.length; i += size) {
      chunkedArr.push(array.slice(i, i + size));
    }
    return chunkedArr;
  };

  const filteredGames = searchTerm ? filterGamesBySearchTerm(games, searchTerm) : games;
  const gameRows = chunk(filteredGames, 5);

  return (
    <Container>
      <div style={{ backgroundColor: '#FFA500', padding: '10px' }}>
        <h1 className="text-center my-4">Welcome To The VG Database!</h1>
      </div>

      <div className="d-flex justify-content-between align-items-center mb-4">
        <InputGroup>
          <Input
            placeholder="Type to search the database..."
            value={searchTerm}
            onChange={handleSearchChange}
          />
        </InputGroup>
        <Button color="info" onClick={handleAddGameClick} style={{ marginLeft: '8px' }}>Add Game</Button>
      </div>

      {gameRows.map((gameRow, rowIndex) => (
        <Row key={rowIndex} className="five-cols mb-4">
          {gameRow.map((game, index) => (
            <Col key={index} className="mb-4">
              <div className="p-3 border game-tile">
                <h3>{game.title}</h3>
                <p className="text-muted" style={{ fontSize: 'smaller' }}>
                  {game.platform} | Score: {game.score}
                </p>
                <Button color="danger" className="mr-2" onClick={() => deleteGame(game.gameID)}>Remove</Button>
                <Button color="success" style={{ marginLeft: '8px' }} onClick={() => handleViewMoreClick(game.gameID)}>View More</Button>
              </div>
            </Col>
          ))}
        </Row>
      ))}

      <footer className="text-center" style={{ backgroundColor: '#FFD580', padding: '10px' }}>
        <p>Sabrina Quadir</p>
      </footer>
    </Container>
  );
};

export default HomePage;
