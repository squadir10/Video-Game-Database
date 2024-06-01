/* 
Name: Sabrina Quadir 
Description: 
The HomePage.js file is the main page of the web application, displaying a list of video games in a grid view. 
This component fetches and renders a collection of game entries from the backend API, 
displaying key details such as:
-title
-platform
-review scores. 

It includes functionalities for searching, filtering, adding, viewing, and removing games. 
The homepage serves as the central hub for navigating the application's various features, 
providing users with an overview of the available video games and easy access to 
detailed information and modification options.
 */

import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import "./Styles.css";
import {Container,Row,Col,Button,InputGroup,Input,Dropdown,DropdownToggle,DropdownMenu,DropdownItem,} from "reactstrap";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const HomePage = () => {
  const navigate = useNavigate();
  const [games, setGames] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [selectedFilter, setSelectedFilter] = useState("title");

  const toggleDropdown = () => setDropdownOpen(!dropdownOpen);

  useEffect(() => {
    axios
      .get("http://localhost:5021/api/Games")
      .then((response) => setGames(response.data))
      .catch((error) => console.error("Error fetching games:", error));
  }, []);

  const deleteGame = async (gameId) => {
    try {
      await axios.delete(`http://localhost:5021/api/Games/${gameId}`);
      setGames(games.filter((game) => game.gameID !== gameId));
    } catch (error) {
      console.error("Error deleting the game:", error);
    }
  };

  const handleViewMoreClick = (gameId) => {
    navigate(`/game/${gameId}`);
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleFilterSelect = (filter) => {
    setSelectedFilter(filter);
  };

  const chunk = (array, size) => {
    const chunkedArr = [];
    for (let i = 0; i < array.length; i += size) {
      chunkedArr.push(array.slice(i, i + size));
    }
    return chunkedArr;
  };

  const filterDisplayNames = {
    title: "Title",
    platform: "Platform",
    genre: "Genre",
    developer: "Developer",
    developerLocation: "Developer Location",
    publisher: "Publisher",
    publisherLocation: "Publisher Location",
    score: "Score",
    reviewerName: "Reviewer Name",
    reviewerAffiliation: "Reviewer Affiliation",
  };

  const filterGames = (games) => {
    return games.filter((game) => {
      switch (selectedFilter) {
        case "platform":
          return game.platform.toLowerCase().includes(searchTerm.toLowerCase());
        case "genre":
          return game.genre.toLowerCase().includes(searchTerm.toLowerCase());
        case "developer":
          return game.developer.name
            .toLowerCase()
            .includes(searchTerm.toLowerCase());
        case "developerLocation":
          return game.developer.location
            .toLowerCase()
            .includes(searchTerm.toLowerCase());
        case "publisher":
          return game.publisher.name
            .toLowerCase()
            .includes(searchTerm.toLowerCase());
        case "publisherLocation":
          return game.publisher.headquarters
            .toLowerCase()
            .includes(searchTerm.toLowerCase());
        case "score":
          return game.gameReviews.some((review) =>
            review.score.toString().includes(searchTerm)
          );
        case "reviewerName":
          return game.gameReviews.some((review) =>
            review.reviewer.name
              .toLowerCase()
              .includes(searchTerm.toLowerCase())
          );
        case "reviewerAffiliation":
          return game.gameReviews.some((review) =>
            review.reviewer.affiliation
              .toLowerCase()
              .includes(searchTerm.toLowerCase())
          );
        default:
          return game.title.toLowerCase().includes(searchTerm.toLowerCase());
      }
    });
  };

  const filteredGames = filterGames(games);
  const gameRows = chunk(filteredGames, 5);

  return (
    <Container>
      <div style={{ backgroundColor: "#FFA500", padding: "10px" }}>
        <h1 className="text-center my-4">Welcome To The VG Database!</h1>
      </div>

      <div className="d-flex justify-content-between align-items-center mb-4">
        <InputGroup>
          <Input
            placeholder={`Search by ${filterDisplayNames[selectedFilter]}...`}
            value={searchTerm}
            onChange={handleSearchChange}
          />
        </InputGroup>
        <Dropdown isOpen={dropdownOpen} toggle={toggleDropdown}>
          <DropdownToggle caret>
            <i className="bi bi-funnel-fill" style={{ marginRight: "8px" }}></i>
            Filter by: {filterDisplayNames[selectedFilter]}
          </DropdownToggle>
          <DropdownMenu>
            {Object.keys(filterDisplayNames).map((filter) => (
              <DropdownItem
                key={filter}
                onClick={() => handleFilterSelect(filter)}
              >
                {filterDisplayNames[filter]}
              </DropdownItem>
            ))}
          </DropdownMenu>
        </Dropdown>
        <Button
          color="info"
          onClick={() => navigate("/add-game")}
          style={{ marginLeft: "8px" }}
        >
          <i class="bi bi-pencil-square" style={{ marginRight: "8px"}}></i>
          Add A New Game
        </Button>
      </div>
      {gameRows.map((gameRow, rowIndex) => (
        <Row key={rowIndex} className="five-cols mb-4">
          {gameRow.map((game, index) => (
            <Col key={index} className="mb-4">
              <div className="p-3 border game-tile">
                <h3>{game.title}</h3>
                <p className="text-muted" style={{ fontSize: "smaller" }}>
                  {game.platform} | Score:{" "}
                  {game.gameReviews[0]?.score ?? "No score"}
                </p>
                <Button
                  color="danger"
                  className="mr-2"
                  onClick={() => deleteGame(game.gameID)}
                >
                  <i className="bi bi-trash-fill"></i>
                </Button>
                <Button
                  color="success"
                  style={{ marginLeft: "3px" }}
                  onClick={() => handleViewMoreClick(game.gameID)}
                >
                  <i className="bi bi-eye" style={{ marginRight: "8px" }}></i>
                  View More
                </Button>
              </div>
            </Col>
          ))}
        </Row>
      ))}

      <footer
        className="text-center"
        style={{ backgroundColor: "#FFD580", padding: "10px" }}
      >
        <p>Sabrina Quadir</p>
      </footer>
    </Container>
  );
};

export default HomePage;
