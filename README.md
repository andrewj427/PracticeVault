# PracticeVault

With this project, users will be able to record their progress in learning certain songs on guitar. Users will be able to upload the song name, artist, duration, tuning, difficulty, BPM, a link to tabs (sheet music), as well as other notes to a Microsoft SQL database. 

This project will use the Spotify Web API to help the user search for songs and artists

#Database Structure

User table
UserID (PK)
Username
Password

Song table
SongId (PK)
Title
Artist
Album
SpotifyId
Duration

UserSongs table
UserSongId (PK)
SongId (FK)
Tuning
Difficulty
Progress
Status
BPM
TabLink
Notes
