import React, { useState, useCallback } from 'react';

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

const GameModify = ({ game, onSave, onCancel }) => {
  const [editGame, setEditGame] = useState(() => ({
    ...game,
    releaseDate: game.releaseDate?.split("T")[0] || "",
    developerFoundingDate: game.developer?.foundingDate?.split("T")[0] || "",
    publisherFoundingDate: game.publisher?.foundingDate?.split("T")[0] || "",
    reviewDate: game.review?.date?.split("T")[0] || "",
    reviewer: { ...game.reviewer }, // Ensure this is a nested object
    review: { ...game.review } // Ensure this is a nested object
  }));

  const handleChange = useCallback((e) => {
    const { name, value } = e.target;
    setEditGame(currentState => {
      const newState = { ...currentState };
      setValue(newState, name, value);
      return newState;
    });
  }, []);


  const handleSubmit = useCallback((e) => {
    e.preventDefault();
    onSave(editGame);
  }, [editGame, onSave]);

  return (
    <form onSubmit={handleSubmit} className="form-container">
      <span style={{ marginTop: "25px" }}>
        <FormField label="Game Title" name="title" value={editGame.title} onChange={handleChange} />
        <FormField label="Release Date" type="date" name="releaseDate" value={editGame.releaseDate} onChange={handleChange} />
        <FormField label="Genre" name="genre" value={editGame.genre} onChange={handleChange} />
        <FormField label="Platform" name="platform" value={editGame.platform} onChange={handleChange} />
        <FormField label="Developer Name" name="developer.Name" value={editGame.developer?.Name} onChange={handleChange} />
        <FormField label="Developer Location" name="developer.location" value={editGame.developer?.location} onChange={handleChange} />
        <FormField label="Developer Founding Date" type="date" name="developer.foundingDate" value={editGame.developer?.foundingDate} onChange={handleChange} />
        <FormField label="Publisher Name" name="publisher.name" value={editGame.publisher?.name} onChange={handleChange} />
        <FormField label="Publisher Headquarters" name="publisher.headquarters" value={editGame.publisher?.headquarters} onChange={handleChange} />
        <FormField label="Publisher Founding Date" type="date" name="publisher.foundingDate" value={editGame.publisher?.foundingDate} onChange={handleChange} />
        <FormField label="Review Score" type="number" name="review.score" value={editGame.review?.score} onChange={handleChange} />
        <FormField label="Review Text" type="textarea" name="review.text" value={editGame.review?.text} onChange={handleChange} />
        <FormField label="Review Date" type="date" name="review.date" value={editGame.review?.date} onChange={handleChange} />
        <FormField label="Reviewer Name" name="reviewer.name" value={editGame.reviewer?.name} onChange={handleChange} />
        <FormField label="Reviewer Affiliation" name="reviewer.affiliation" value={editGame.reviewer?.affiliation} onChange={handleChange} />
      </span>

      <span style={{ marginBottom: "25px" }}>
        <div className="form-actions">
          <button type="submit" style={{ backgroundColor: "green", color: "white" }}>Save</button>
          <button type="button" onClick={onCancel} style={{ backgroundColor: "gray", color: "white" }}>Cancel</button>
        </div>
      </span>
    </form>
  );
};

export default GameModify;
