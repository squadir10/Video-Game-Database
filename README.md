# 🎮 VG Database

Welcome to the **VG Database**! This web application allows users to manage a comprehensive database of video games, including details about the game, its developers, publishers, and reviews. 

## Table of Contents

- [About the Project](#about-the-project)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgements](#acknowledgements)

## About the Project

The **VG Database** is a full-stack web application built using React for the frontend and ASP.NET Core for the backend. It allows users to add, modify, view, and delete video game entries, along with other details.

## Features

- Add new video game entries with details about the game, developer, publisher, and reviews.
- Modify existing game entries.
- View detailed information about each game.
- Delete game entries.
- Filter and search games based on various attributes like platform, genre, developer location, etc.

## Getting Started

To run this project follow these simple steps: 

### Prerequisites

- Node.js
- npm (Node Package Manager)
- .NET Core SDK
- SQL Server (or any preferred database)
- c# and JS

### Installation

1. Clone the repo
  
2. Navigate to the project directory on your computer

#### Backend Setup

3. Navigate to the backend directory

   cd VGDB_backend

4. Restore the .NET dependencies

   dotnet restore

5. Update the database connection string in `appsettings.json`
   
6. Run the backend server

   dotnet run

#### Frontend Setup

8. Navigate to the frontend directory

   cd ../VGDB_frontend

9. Install npm packages

   npm install

10. Run the React app

    npm start

## Usage

Once the application is set up, you can start managing video game entries through the web interface.

1. **Add Game**: Navigate to the "Add Game" section and fill in the details to add a new game.
2. **Modify Game**: Click on any game entry and choose "Modify" to update its details.
3. **View Game**: Click "View More" to see detailed information about a game.
4. **Delete Game**: Click the trash icon to remove a game entry.

## License

Distributed under the MIT License.
