/* 
Name: Sabrina Quadir 
Description: 
The GameModify.js file defines a React component for modifying the details of an existing video game entry.
This component provides a form pre-filled with the current details of the selected game, allowing the user to update information such as:
-title of the game 
-release date
-genre
-platform
-developer
-publisher
-reviews
-reviewer 

Upon submission, the updated data is sent to the backend to update the existing game record in the database. 
Thiss component ensures data integrity and provides an easy way to make edits to the game entries.

 */

import React, { useState, useCallback, useEffect } from "react";
import { Container } from "react-bootstrap";

// Utility to help with nested updates
const setValue = (obj, path, value) => {
  const keys = path.split(".");
  const lastKey = keys.pop();
  const lastObj = keys.reduce((obj, key) => (obj[key] = obj[key] || {}), obj);
  lastObj[lastKey] = value;
};

// Abstracted form field component to reduce redundancy
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

const GameModify = ({ game, onSave, onCancel }) => {
  const [editGame, setEditGame] = useState(() => ({
    ...game,
    releaseDate: game.releaseDate?.split("T")[0] || "",
    developer: {
      ...game.developer,
      foundingDate: game.developer?.foundingDate?.split("T")[0] || "",
      name: game.developer?.name || "",
      location: game.developer?.location || "",
    },
    publisher: {
      ...game.publisher,
      foundingDate: game.publisher?.foundingDate?.split("T")[0] || "",
      name: game.publisher?.name || "",
      headquarters: game.publisher?.headquarters || "",
    },
    gameReviews: game.gameReviews?.length
      ? game.gameReviews.map((review) => ({
          ...review,
          reviewDate: review.reviewDate?.split("T")[0] || "",
          score: review.score || "",
          reviewText: review.reviewText || "",
          reviewer: {
            ...review.reviewer,
            name: review.reviewer?.name || "",
            affiliation: review.reviewer?.affiliation || "",
          },
        }))
      : [
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
  }));

  const handleChange = useCallback((e) => {
    const { name, value } = e.target;
    setEditGame((currentState) => {
      const newState = { ...currentState };
      setValue(newState, name, value);
      return newState;
    });
  }, []);

  const handleSubmit = useCallback(
    (e) => {
      e.preventDefault();
      onSave(editGame);
    },
    [editGame, onSave]
  );

  useEffect(() => {
    if (game.gameReviews && game.gameReviews.length > 0) {
      setEditGame((currentState) => ({
        ...currentState,
        gameReviews: game.gameReviews.map((review) => ({
          ...review,
          reviewDate: review.reviewDate?.split("T")[0] || "",
          score: review.score || "",
          reviewText: review.reviewText || "",
          reviewer: {
            ...review.reviewer,
            name: review.reviewer?.name || "",
            affiliation: review.reviewer?.affiliation || "",
          },
        })),
      }));
    }
  }, [game]);

  return (
    <Container>
      <div
        style={{
          backgroundColor: "#ffdab9",
          padding: "20px",
          margin: "20px auto",
          width: "50%",
        }}
      >
        <h2 className="text-center my-2">Modify Game Entry</h2>
        <p className="text-center">
          Modify the details of an existing game entry
        </p>
      </div>

      <form onSubmit={handleSubmit} className="form-container">
        <span style={{ marginTop: "25px" }}>
          <FormField
            label="Game Title"
            name="title"
            value={editGame.title}
            onChange={handleChange}
          />
          <FormField
            label="Release Date"
            type="date"
            name="releaseDate"
            value={editGame.releaseDate}
            onChange={handleChange}
          />
          <FormField
            label="Genre"
            name="genre"
            value={editGame.genre}
            onChange={handleChange}
          />
          <FormField
            label="Platform"
            name="platform"
            value={editGame.platform}
            onChange={handleChange}
          />
          <FormField
            label="Developer Name"
            name="developer.name"
            value={editGame.developer.name}
            onChange={handleChange}
          />
          <FormField
            label="Developer Location"
            name="developer.location"
            value={editGame.developer.location}
            onChange={handleChange}
          />
          <FormField
            label="Developer Founding Date"
            type="date"
            name="developer.foundingDate"
            value={editGame.developer.foundingDate}
            onChange={handleChange}
          />
          <FormField
            label="Publisher Name"
            name="publisher.name"
            value={editGame.publisher.name}
            onChange={handleChange}
          />
          <FormField
            label="Publisher Headquarters"
            name="publisher.headquarters"
            value={editGame.publisher.headquarters}
            onChange={handleChange}
          />
          <FormField
            label="Publisher Founding Date"
            type="date"
            name="publisher.foundingDate"
            value={editGame.publisher.foundingDate}
            onChange={handleChange}
          />
          {editGame.gameReviews.map((review, index) => (
            <div key={index}>
              <FormField
                label="Review Score"
                type="number"
                name={`gameReviews.${index}.score`}
                value={review.score}
                onChange={handleChange}
              />
              <FormField
                label="Review Text"
                type="textarea"
                name={`gameReviews.${index}.reviewText`}
                value={review.reviewText}
                onChange={handleChange}
              />
              <FormField
                label="Review Date"
                type="date"
                name={`gameReviews.${index}.reviewDate`}
                value={review.reviewDate}
                onChange={handleChange}
              />
              <FormField
                label="Reviewer Name"
                name={`gameReviews.${index}.reviewer.name`}
                value={review.reviewer.name}
                onChange={handleChange}
              />
              <FormField
                label="Reviewer Affiliation"
                name={`gameReviews.${index}.reviewer.affiliation`}
                value={review.reviewer.affiliation}
                onChange={handleChange}
              />
            </div>
          ))}
        </span>

        <span style={{ marginBottom: "25px" }}>
          <div className="form-actions">
            <button
              type="submit"
              style={{ backgroundColor: "green", color: "white" }}
            >
              Save and Submit
            </button>
            <button
              type="button"
              onClick={onCancel}
              style={{ backgroundColor: "gray", color: "white" }}
            >
              Cancel
            </button>
          </div>
        </span>
      </form>
    </Container>
  );
};

export default GameModify;
