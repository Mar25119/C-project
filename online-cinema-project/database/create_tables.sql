-- database/create_tables.sql

CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    PasswordHash TEXT NOT NULL
);

CREATE TABLE Subscriptions (
    UserId INTEGER PRIMARY KEY,
    Type TEXT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Content (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Year INTEGER,
    Description TEXT
);

CREATE TABLE Movies (
    ContentId INTEGER PRIMARY KEY,
    Duration INTEGER NOT NULL,
    Director TEXT,
    FOREIGN KEY (ContentId) REFERENCES Content(Id)
);

CREATE TABLE TvSeries (
    ContentId INTEGER PRIMARY KEY,
    Seasons INTEGER NOT NULL,
    Episodes INTEGER NOT NULL,
    FOREIGN KEY (ContentId) REFERENCES Content(Id)
);

CREATE TABLE Reviews (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    ContentId INTEGER NOT NULL,
    Text TEXT,
    Rating INTEGER CHECK(Rating BETWEEN 1 AND 10),
    CreatedAt DATE DEFAULT CURRENT_DATE,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (ContentId) REFERENCES Content(Id)
);

CREATE TABLE Genres (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT UNIQUE NOT NULL
);

CREATE TABLE MovieGenres (
    MovieId INTEGER NOT NULL,
    GenreId INTEGER NOT NULL,
    PRIMARY KEY (MovieId, GenreId),
    FOREIGN KEY (MovieId) REFERENCES Movies(ContentId),
    FOREIGN KEY (GenreId) REFERENCES Genres(Id)
);

CREATE TABLE WatchHistory (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    ContentId INTEGER NOT NULL,
    WatchedAt DATE DEFAULT CURRENT_DATE,
    Progress INTEGER DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (ContentId) REFERENCES Content(Id)
);