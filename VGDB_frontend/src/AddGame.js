/* 
Name: Sabrina Quadir 
Description: 
The AddGame.js file defines a React component for adding a new video game entry to the database in a web application. 
This component provides a form for the user to input details about a new game, including its 
-title
-release date
-genre
-platform
-developer
-publisher
-reviews. 

The form data is submitted to the backend so that it can create a new game record in the database. 
In order to submit a new game, all entries MUST BE FILLED!

*/

import React, { useState, useCallback } from "react";
import { Container, Button } from "reactstrap";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import './Styles.css';

//Had to use regex in order to process the reviews and reviewers due to it being an array
// also to handle nested keys
const setValue = (obj, path, value) => {
  const keys = path.replace(/\[(\d+)\]/g, '.$1').split('.');
  const lastKey = keys.pop();
  const lastObj = keys.reduce((obj, key) => obj[key] = obj[key] || {}, obj);
  lastObj[lastKey] = value;
};

//handle form fields and taking in input and text
const FormField = ({ label, type = "text", name, value, onChange }) => (
  <div className="form-field">
    <label>
      <span style={{ marginRight: "8px" }}>
        <b>{label}: </b>
      </span>
      {type === "textarea" ? (
        <textarea name={name} value={value || ""} onChange={onChange} />
      ) : (
        <input
          type={type}
          name={name}
          value={value || ""}
          onChange={onChange}
        />
      )}
    </label>
  </div>
);

//form page
const AddGame = () => {
  const navigate = useNavigate();
  const [newGame, setNewGame] = useState({
    title: "",
    releaseDate: "",
    genre: "",
    platform: "",
    developer: {
      name: "",
      location: "",
      foundingDate: "",
    },
    publisher: {
      name: "",
      headquarters: "",
      foundingDate: "",
    },
    gameReviews: [
      {
        score: "",
        reviewText: "",
        reviewDate: "",
        reviewer: {
          name: "",
          affiliation: "",
        },
      },
    ],
  });

  //supposed to handle the changes that are made and save it to the database
  const handleChange = useCallback((e) => {
    const { name, value } = e.target;
    setNewGame((currentState) => {
      const newState = { ...currentState };
      setValue(newState, name, value);
      return newState;
    });
  }, []);

  //should lead back to the games to see a newly made entry
  const handleSubmit = useCallback(
    (e) => {
      e.preventDefault();
      axios
        .post("http://localhost:5021/api/Games", newGame)
        .then((response) => {
          navigate("/");
        })
        .catch((error) => {
          console.error("Error adding new game:", error);
        });
    },
    [newGame, navigate]
  );

  const handleCancel = () => {
    navigate("/");
  };

  //website format and design
  return (
    <Container>
      {
        //header
      }
      <div className="section" style={{ backgroundColor: '#FFA500', padding: '10px' }}>
        <h1 className="text-center my-4">Welcome To The VG Database!</h1>
      </div>

      {
        //disclaimer box
      }
      <div className="banner-box">
        <h2 className="text-center my-4">Add A New Game Entry</h2>
        <p className="text-center">Add a new game entry to the database!</p>
      </div>

      {
        //main form fields
      }

      <form onSubmit={handleSubmit} className="form-container">
        <span className="section" style={{ marginTop: "25px" }}>
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
          {
            //review and reveiwer section
          }
          <FormField label="Review Score" type="number" name="gameReviews[0].score" value={newGame.gameReviews[0].score} onChange={handleChange} />
          <FormField label="Review Text" type="textarea" name="gameReviews[0].reviewText" value={newGame.gameReviews[0].reviewText} onChange={handleChange} />
          <FormField label="Review Date" type="date" name="gameReviews[0].reviewDate" value={newGame.gameReviews[0].reviewDate} onChange={handleChange} />
          <FormField label="Reviewer Name" name="gameReviews[0].reviewer.name" value={newGame.gameReviews[0].reviewer.name} onChange={handleChange} />
          <FormField label="Reviewer Affiliation" name="gameReviews[0].reviewer.affiliation" value={newGame.gameReviews[0].reviewer.affiliation} onChange={handleChange} />
        </span>
          {
            //submit and cancel button
          }
        <span className="form-actions">
          <Button type="submit" className="button-style" style={{ backgroundColor: "green" }}>Save</Button>
          <Button type="button" className="button-style" onClick={handleCancel} style={{ backgroundColor: "gray" }}>Cancel</Button>
        </span>
      </form>
          {
            //footer
          }
      <footer className="section" style={{ backgroundColor: '#FFD580', padding: '10px', marginTop: '20px' }}>
        <p>Sabrina Quadir</p>
      </footer>
    </Container>
  );
};

export default AddGame;
