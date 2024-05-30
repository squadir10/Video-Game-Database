import React, { useState, useEffect } from "react";
import "./Styles.css"; // Assuming your stylesheet is named 'styles.css'
import { Container } from "react-bootstrap"; // Make sure react-bootstrap is installed
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import GameModify from "./GameModify";

// ... rest of your code remains the same

const GameDetail = () => {
  const navigate = useNavigate();
  const { gameId } = useParams();
  const [game, setGame] = useState(null);
  const [isEditMode, setIsEditMode] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Fetch game details including developer, publisher, and reviews
        const response = await axios.get(`http://localhost:5021/api/Games/${gameId}`);
        if (response.data) {
          setGame(response.data);
        }
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, [gameId]);

  const handleBackClick = () => {
    navigate("/"); // Assuming your home route is '/'
  };

  const handleModify = () => {
    setIsEditMode(true);
  };

  const handleSave = async (updatedGame) => {
    try {
      console.log("handleSave received:", updatedGame);
      if (!updatedGame || !updatedGame.gameID) {
        throw new Error("The game must have an ID.");
      }

      const gameForSave = {
        ...updatedGame,
        developer: updatedGame.developer || game.developer,
        publisher: updatedGame.publisher || game.publisher
      };

      const response = await axios.put(
        `http://localhost:5021/api/Games/${updatedGame.gameID}`,
        gameForSave
      );

      setGame(response.data);
      setIsEditMode(false);
    } catch (error) {
      if (error.response) {
        // Handle HTTP errors here
        console.error(
          "Error saving the updated game details:",
          error.response.data
        );
      } else {
        // Handle generic errors here
        console.error("Error:", error.message);
      }
    }
  };

  const handleCancel = () => {
    setIsEditMode(false);
  };

  return (
    <Container>
      <div
        style={{
          backgroundColor: "#FFA500",
          padding: "10px",
          textAlign: "center",
        }}
      >
        <h1>Welcome To The VG Database!</h1>
      </div>

      {isEditMode ? (
        <GameModify game={game} onSave={handleSave} onCancel={handleCancel} />
      ) : (
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            padding: "10px",
          }}
        >
          <button
            className="button-style"
            onClick={handleBackClick}
            style={{ backgroundColor: "red", color: "white" }}
          >
            Back
          </button>
          <button
            className="button-style"
            onClick={handleModify}
            style={{ backgroundColor: "blue", color: "white" }}
          >
            Modify
          </button>
        </div>
      )}

      {!isEditMode && game && (
        <div>
          <div className="section">
            <h1>{game.title}</h1>
            <p>
              <b>Release Date: </b>
              {new Date(game.releaseDate).toLocaleDateString()}
            </p>
            <p>
              <b>Genre: </b>
              {game.genre}
            </p>
            <p>
              <b>Platform: </b>
              {game.platform}
            </p>
          </div>

          <div className="section">
            <h2>Developer:</h2>
            <p>
              <b>Name: </b>
              {game.developer.name}
            </p>
            <p>
              <b>Location: </b>
              {game.developer.location}
            </p>
            <p>
              <b>Founding Date: </b>
              {new Date(game.developer.foundingDate).toLocaleDateString()}
            </p>
          </div>

          <div className="section">
            <h2>Publisher:</h2>
            <p>
              <b>Name: </b>
              {game.publisher.name}
            </p>
            <p>
              <b>Headquarters: </b>
              {game.publisher.headquarters}
            </p>
            <p>
              <b>Founding Date: </b>
              {new Date(game.publisher.foundingDate).toLocaleDateString()}
            </p>
          </div>

          {game.gameReviews && game.gameReviews.length > 0 && (
            <div className="section">
              <h2>Review:</h2>
              {game.gameReviews.map((review) => (
                <div key={review.gameReviewID}>
                  <p>
                    <b>Score: </b>
                    {review.score}
                  </p>
                  <p>
                    <b>Review Text: </b>
                    {review.reviewText}
                  </p>
                  <p>
                    <b>Review Date: </b>
                    {new Date(review.reviewDate).toLocaleDateString()}
                  </p>
                  <div className="section">
                    <h2>Reviewer:</h2>
                    <p>
                      <b>Name: </b>
                      {review.reviewer.name}
                    </p>
                    <p>
                      <b>Affiliation: </b>
                      {review.reviewer.affiliation}
                    </p>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      )}

      <footer
        className="text-center"
        style={{ backgroundColor: "#FFD580", padding: "10px" }}
      >
        <p>Sabrina Quadir</p>
      </footer>
    </Container>
  );
};

export default GameDetail;
