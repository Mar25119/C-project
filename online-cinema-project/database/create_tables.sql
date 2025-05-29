CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    PasswordHash TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Content (
    Id INTEGER PRIMARY KEY,
    Title TEXT NOT NULL,
    Year INTEGER,
    Description TEXT
);

CREATE TABLE IF NOT EXISTS Movies (
    ContentId INTEGER PRIMARY KEY,
    Duration INTEGER,
    Director TEXT,
    FOREIGN KEY(ContentId) REFERENCES Content(Id)
);

CREATE TABLE IF NOT EXISTS Series (
    ContentId INTEGER PRIMARY KEY,
    Seasons INTEGER,
    Episodes INTEGER,
    FOREIGN KEY(ContentId) REFERENCES Content(Id)
);

CREATE TABLE IF NOT EXISTS Reviews (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER,
    ContentId INTEGER,
    Text TEXT,
    Rating INTEGER CHECK(Rating BETWEEN 1 AND 10),
    FOREIGN KEY(UserId) REFERENCES Users(Id),
    FOREIGN KEY(ContentId) REFERENCES Content(Id)
);

CREATE TABLE IF NOT EXISTS Subscriptions (
    UserId INTEGER PRIMARY KEY,
    Type TEXT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    FOREIGN KEY(UserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS Genres (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT UNIQUE NOT NULL
);

CREATE TABLE IF NOT EXISTS MovieGenres (
    MovieId INTEGER,
    GenreId INTEGER,
    PRIMARY KEY(MovieId, GenreId),
    FOREIGN KEY(MovieId) REFERENCES Movies(ContentId),
    FOREIGN KEY(GenreId) REFERENCES Genres(Id)
);