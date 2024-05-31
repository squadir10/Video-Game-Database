import React, { useState, useCallback } from 'react';
import { Container, Button } from 'reactstrap';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

// Utility to help with nested updates
const setValue = (obj, path, value) => {
  const keys = path.split('.');
  const lastKey = keys.pop();
  const lastObj = keys.reduce((obj, key) => obj[key] = obj[key] || {}, obj); 
  lastObj[lastKey] = value;
};

// Abstracted form field component to reduce redundancy
const FormField = ({ label, type = "text", name, value, onChange }) => (
  <div className="form-field">
    <label>
      <span style={{ marginRight: "8px" }}><b>{label}: </b></span>
      {type === "textarea" ? (
        <textarea name={name} value={value || ""} onChange={onChange} />
      ) : (
        <input type={type} name={name} value={value || ""} onChange={onChange} />
      )}
    </label>
  </div>
);

const AddGame = () => {
  const navigate = useNavigate();
  const [newGame, setNewGame] = useState({
    title: '',
    releaseDate: '',
    genre: '',
    platform: '',
    developer: {
      name: '',
      location: '',
      foundingDate: ''
    },
    publisher: {
      name: '',
      headquarters: '',
      foundingDate: ''
    },
    gameReviews: [{
      score: '',
      reviewText: '',
      reviewDate: '',
      reviewer: {
        name: '',
        affiliation: ''
      }
    }]
  });

  const handleChange = useCallback((e) => {
    const { name, value } = e.target;
    setNewGame(currentState => {
      const newState = { ...currentState };
      setValue(newState, name, value);
      return newState;
    });
  }, []);

  const handleSubmit = useCallback((e) => {
    e.preventDefault();
    axios.post('http://localhost:5021/api/Games', newGame)
      .then(response => {
        navigate('/');
      })
      .catch(error => {
        console.error('Error adding new game:', error);
      });
  }, [newGame, navigate]);

  const handleCancel = () => {
    navigate('/');
  };

  return (
    <Container>
      <div style={{ backgroundColor: '#FFA500', padding: '10px' }}>
        <h1 className="text-center my-4">Welcome To The VG Database!</h1>
      </div>

      <form onSubmit={handleSubmit} className="form-container">
        <span style={{ marginTop: "25px" }}>
          <FormField label="Game Title" name="title" value={newGame.title} onChange={handleChange} />
          <FormField label="Release Date" type="date" name="releaseDate" value={newGame.releaseDate} onChange={handleChange} />
          <FormField label="Genre" name="genre" value={newGame.genre} onChange={handleChange} />
          <FormField label="Platform" name="platform" value={newGame.platform} onChange={handleChange} />
          <FormField label="Developer Name" name="developer.name" value={newGame.developer.name} onChange={handleChange} />
          <FormField label="Developer Location" name="developer.location" value={newGame.developer.location} onChange={handleChange} />
          <FormField label="Developer Founding Date" type="date" name="developer.foundingDate" value={newGame.developer.foundingDate} onChange={handleChange} />
          <FormField label="Publisher Name" name="publisher.name" value={newGame.publisher.name} onChange={handleChange} />
          <FormField label="Publisher Headquarters" name="publisher.headquarters" value={newGame.publisher.headquarters} onChange={handleChange} />
          <FormField label="Publisher Founding Date" type="date" name="publisher.foundingDate" value={newGame.publisher.foundingDate} onChange={handleChange} />
          <FormField label="Review Score" type="number" name="gameReviews.0.score" value={newGame.gameReviews[0].score} onChange={handleChange} />
          <FormField label="Review Text" type="textarea" name="gameReviews.0.reviewText" value={newGame.gameReviews[0].reviewText} onChange={handleChange} />
          <FormField label="Review Date" type="date" name="gameReviews.0.reviewDate" value={newGame.gameReviews[0].reviewDate} onChange={handleChange} />
          <FormField label="Reviewer Name" name="gameReviews.0.reviewer.name" value={newGame.gameReviews[0].reviewer.name} onChange={handleChange} />
          <FormField label="Reviewer Affiliation" name="gameReviews.0.reviewer.affiliation" value={newGame.gameReviews[0].reviewer.affiliation} onChange={handleChange} />
        </span>

        <span style={{ marginBottom: "25px" }}>
          <div className="form-actions">
            <button type="submit" style={{ backgroundColor: "green", color: "white" }}>Save</button>
            <button type="button" onClick={handleCancel} style={{ backgroundColor: "gray", color: "white" }}>Cancel</button>
          </div>
        </span>
      </form>

      <footer className="text-center" style={{ backgroundColor: '#FFD580', padding: '10px' }}>
          <p>Sabrina Quadir</p>
      </footer>
    </Container>
  );
};

export default AddGame;
