IF DB_ID('PracticeVaultDB') IS NOT NULL
BEGIN
    ALTER DATABASE PracticeVaultDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PracticeVaultDB;
END
GO

CREATE DATABASE PracticeVaultDB;
GO

USE PracticeVaultDB;
GO


CREATE TABLE Users
(
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL
);
GO


CREATE TABLE Songs
(
    SongID INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(150) NOT NULL,
    Artist VARCHAR(150) NOT NULL,
    Album VARCHAR(150),
    DurationSeconds INT,

    SpotifyTrackID VARCHAR(100) NULL,
    SpotifyURL VARCHAR(500) NULL
);
GO


CREATE TABLE UserSongs
(
    UserSongID INT IDENTITY(1,1) PRIMARY KEY,

    UserID INT NOT NULL,
    SongID INT NOT NULL,

    Tuning VARCHAR(30),
    Difficulty VARCHAR(30),
    BPM INT,
    TabURL VARCHAR(500),
    Notes VARCHAR(1000),

    Progress INT NOT NULL DEFAULT 0,
    DateAdded DATETIME NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (SongID) REFERENCES Songs(SongID)
);
GO


INSERT INTO Users (Username, Email, PasswordHash)
VALUES
('Andrew', 'andrew@example.com', 'mock_password_hash'),
('TestUser', 'test@example.com', 'mock_password_hash');
GO


INSERT INTO Songs
    (Title, Artist, Album, DurationSeconds)
VALUES
    ('Hail to the King', 'Avenged Sevenfold', 'Hail to the King', 304),
    ('Bat Country', 'Avenged Sevenfold', 'City of Evil', 312),
    ('November Rain', 'Guns N'' Roses', 'Use Your Illusion I', 537),
    ('Sweet Child O'' Mine', 'Guns N'' Roses', 'Appetite for Destruction', 356);
GO


INSERT INTO UserSongs
    (UserID, SongID, Tuning, Difficulty, BPM, TabURL, Notes, Progress)
VALUES
    (1, 1, 'Drop D', 'Hard', 118, 'https://example.com/hail', 'Working on the solo', 60),
    (1, 3, 'Eb Standard', 'Medium', 80, 'https://example.com/november', 'Practice the solos', 40),
    (1, 4, 'Eb Standard', 'Medium', 125, 'https://example.com/sweet-child', 'Working on intro', 75),
    (2, 2, 'Drop D', 'Hard', 125, 'https://example.com/bat-country', 'Working on lead sections', 25);
GO