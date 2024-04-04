DROP DATABASE IF EXISTS `Video_Game_Database`;
CREATE DATABASE `Video_Game_Database`;
USE `Video_Game_Database`;


CREATE TABLE `Developers` (
  `DeveloperID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Location` varchar(255) DEFAULT NULL,
  `FoundingDate` date DEFAULT NULL,
  PRIMARY KEY (`DeveloperID`)
);

CREATE TABLE `Publishers` (
  `PublisherID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Headquarters` varchar(255) DEFAULT NULL,
  `FoundingDate` date DEFAULT NULL,
  PRIMARY KEY (`PublisherID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `Games` (
  `GameID` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) NOT NULL,
  `ReleaseDate` date DEFAULT NULL,
  `Genre` varchar(255) DEFAULT NULL,
  `Platform` varchar(255) DEFAULT NULL,
  `DeveloperID` int DEFAULT NULL,
  `PublisherID` int DEFAULT NULL,
  PRIMARY KEY (`GameID`),
  KEY `DeveloperID` (`DeveloperID`),
  KEY `PublisherID` (`PublisherID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `Reviewers` (
  `ReviewerID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Affiliation` varchar(255) DEFAULT NULL,
  `ExperienceYears` int DEFAULT NULL,
  PRIMARY KEY (`ReviewerID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `GameReviews` (
  `GameReviewID` int NOT NULL AUTO_INCREMENT,
  `GameID` int NOT NULL,
  `ReviewerID` int NOT NULL,
  `Score` int DEFAULT NULL,
  `ReviewText` text,
  `ReviewDate` datetime DEFAULT NULL,
  PRIMARY KEY (`GameReviewID`),
  KEY `GameID` (`GameID`),
  KEY `ReviewerID` (`ReviewerID`),
  CONSTRAINT `FK_Game` FOREIGN KEY (`GameID`) REFERENCES `Games` (`GameID`),
  CONSTRAINT `FK_Reviewer` FOREIGN KEY (`ReviewerID`) REFERENCES `Reviewers` (`ReviewerID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


INSERT INTO `Developers` (Name, Location, FoundingDate) VALUES
('Epic Games', 'Cary, North Carolina, USA', '1991-01-01'),
('Nintendo', 'Kyoto, Japan', '1889-09-23'),
('Valve Corporation', 'Bellevue, Washington, USA', '1996-08-24'),
('CD Projekt Red', 'Warsaw, Poland', '2002-06-06'),
('Square Enix', 'Tokyo, Japan', '1986-09-22'),
('Capcom', 'Osaka, Japan', '1979-06-11'),
('Sega', 'Irvine, California', '1960-06-03'),
('PlatinumGames', 'Osaka, Japan', '2006-02-01'),
('Sucker Punch Productions', 'Bellevue, Washington, USA', '1997-09-01'),
('Project Sora', 'Tokyo, Japan', '2009-01-22');

INSERT INTO `Publishers` (Name, Headquarters, FoundingDate) VALUES
('Electronic Arts', 'Redwood City, California, USA', '1982-05-28'),
('Nintendo', 'Kyoto, Japan', '1889-09-23'),
('Activision Blizzard', 'Santa Monica, California, USA', '2008-07-10'),
('Ubisoft', 'Montreuil, France', '1986-03-28'),
('Sony Interactive Entertainment', 'San Mateo, California, USA', '1994-11-16'),
('Square Enix', 'Tokyo, Japan', '1986-09-22'),
('Capcom', 'Osaka, Japan', '1979-06-11'),
('Sega', 'Irvine, California', '1960-06-03');

INSERT INTO `Games` (Title, ReleaseDate, Genre, Platform, DeveloperID, PublisherID) VALUES
('Fortnite', '2017-07-25', 'Battle Royale', 'Multi-platform', 1, 1),
('Sonic The Hedgehog', '1991-06-23', 'Platformer', 'Multi-platform', 7, 8),
('Mario Kart 8 Deluxe', '2017-04-10', 'Racing', 'Switch', 2,2),
('Final Fantasy VII INTERGRADE', '2020-04-10', 'Action Adventure RPG', 'Multi-platform', 5,5),
('The Legend of Zelda: Breath of the Wild', '2017-03-03', 'Action-adventure', 'Switch', 2, 2),
('Half-Life 2', '2004-11-16', 'First-person shooter', 'PC', 3, 3),
('The Witcher 3: Wild Hunt', '2015-05-18', 'Action RPG', 'Multi-platform', 4, 4),
('Final Fantasy XV', '2016-11-29', 'Action RPG', 'Multi-platform', 5, 5),
('Monster Hunter: World', '2018-01-26', 'Action RPG', 'Multi-platform', 6, 6),
('Dragon Quest XI', '2017-07-29', 'JRPG', 'Multi-platform', 5, 5),
('Resident Evil 2', '2019-01-25', 'Survival Horror', 'Multi-platform', 6, 6),
('Bayonetta 3', '2022-10-28', 'Action', 'Switch', 8, 2),
('Majora\'s Mask 3D', '2015-02-13', 'Action-adventure', '3DS', 2, 2),
('Ghost of Tsushima', '2020-07-17', 'Action-adventure', 'PlayStation 4', 9, 5),
('Street Fighter 6', '2023-06-02', 'Fighting', 'Multi-platform', 6, 6),
('Kid Icarus: Uprising', '2012-03-22', 'Action', '3DS', 10, 2);


INSERT INTO `Reviewers` (Name, Affiliation, ExperienceYears) VALUES
('Jane Doe', 'IGN', 5),
('John Appleseed', 'GameSpot', 7),
('Alex Johnson', 'Kotaku', 3),
('Sam Lee', 'Polygon', 4),
('Mia Wong', 'Eurogamer', 6),
('Carlos Hernandez', 'VG247', 5),
('Ella Thompson', 'The Verge', 4),
('Marco Polo', 'TechRadar', 8),
('Aisha Khan', 'PC Gamer', 6),
('Omar Farooq', 'Destructoid', 5),
('Isabella Garcia', 'Game Informer', 7),
('Raj Patel', 'The Guardian', 9),
('Emily Gains', 'GamesRadar+', 3),
('Liam Wong', 'VGChartz', 2),
('Zoe Kim', 'IGN', 4);

INSERT INTO `GameReviews` (GameID, ReviewerID, Score, ReviewText, ReviewDate) VALUES
(1, 1, 85, 'An innovative take on the battle royale genre, Fortnite blends building mechanics with intense action. A constantly evolving experience.', '2017-08-10'),
(2, 2, 90, 'Sonic The Hedgehog remains a timeless classic that redefined platformers. Its speed and design continue to captivate.', '1991-07-10'),
(3, 3, 92, 'Mario Kart 8 Deluxe is the pinnacle of fun, offering unparalleled multiplayer racing that shines on the Switch.', '2017-05-10'),
(4, 4, 95, 'Final Fantasy VII INTERGRADE is a masterpiece that expands on the original with stunning visuals and engaging gameplay.', '2020-05-10'),
(5, 5, 98, 'The Legend of Zelda: Breath of the Wild is an open-world marvel, setting a new standard for adventure games.', '2017-04-03'),
(6, 6, 96, 'Half-Life 2’s narrative depth, combined with groundbreaking physics-based gameplay, creates an immersive world.', '2004-12-16'),
(7, 7, 93, 'The Witcher 3: Wild Hunt is an epic journey through a rich, detailed world filled with moral ambiguity.', '2015-06-18'),
(8, 8, 89, 'Final Fantasy XV combines road trip vibes with deep RPG elements, offering a unique experience despite its flaws.', '2016-12-29'),
(9, 9, 94, 'Monster Hunter: World’s engaging hunts and expansive world make it a thrilling entry in the franchise.', '2018-02-26'),
(10, 10, 91, 'Dragon Quest XI offers a comforting yet adventurous JRPG experience, with a story that’s both grand and heartwarming.', '2017-08-29'),
(11, 11, 97, 'Resident Evil 2 sets a new standard for remakes, masterfully blending horror, action, and nostalgia.', '2019-02-25'),
(12, 1, 88, 'Bayonetta 3 delivers stylish action and over-the-top combat, proving to be a worthy sequel in the series.', '2022-11-28'),
(13, 2, 95, 'Majora’s Mask 3D is a beautifully haunting adventure that stands out as one of the most unique Zelda experiences.', '2015-03-13'),
(14, 3, 90, 'Ghost of Tsushima offers a breathtaking open world, combining samurai action with a touching story.', '2020-08-17'),
(15, 4, 87, 'Street Fighter 6 refines the series’ fighting mechanics, offering both depth for veterans and accessibility for newcomers.', '2023-07-02'),
(16, 5, 92, 'Kid Icarus: Uprising is an underappreciated gem, blending action, strategy, and humor in a way few games can.', '2012-04-22');


